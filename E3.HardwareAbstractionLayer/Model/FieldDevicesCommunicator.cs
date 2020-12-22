using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using E3.HardwareAbstractionLayer.Helpers;
using E3.ReactorManager.ControllerProvider.Model.Enums;
using E3.ReactorManager.ControllerProvider.Model.Interfaces;
using E3.ReactorManager.Interfaces.DataAbstractionLayer;
using E3.ReactorManager.Interfaces.DataAbstractionLayer.Data;
using E3.ReactorManager.Interfaces.Framework.Logging;
using E3.ReactorManager.Interfaces.HardwareAbstractionLayer;
using E3.ReactorManager.Interfaces.HardwareAbstractionLayer.Data;
using Unity;
using Timer = System.Timers.Timer;

namespace E3.HardwareAbstractionLayer.Model
{
    public class FieldDevicesCommunicator : IFieldDevicesCommunicator
    {
        public FieldDevicesWrapper fieldDeviceWrapper;
        private readonly ILogger logger;
        private readonly Timer dataLogTimer;
        private readonly Timer cyclicReadWriteDevicesTimer;
        private readonly IDatabaseWriter databaseWriter;
        bool readWriteOpToggler;
        private Dictionary<string, bool> devicesCommunicationStatus = new Dictionary<string, bool>();

        public FieldDevicesCommunicator(IUnityContainer containerProvider)
        {
            fieldDeviceWrapper = containerProvider.Resolve<FieldDevicesWrapper>();
            logger = containerProvider.Resolve<ILogger>();
            databaseWriter = containerProvider.Resolve<IDatabaseWriter>();

            fieldDeviceWrapper.FieldPointDataReceived += OnFieldPointDataReceived;
            CommandHistory = new List<Command>();

            cyclicReadWriteDevicesTimer = new Timer(10);
            cyclicReadWriteDevicesTimer.Elapsed += CyclicReadWriteDevicesTimer_Tick;
            dataLogTimer = new Timer(GetLoggingIntervalDetails().TotalMilliseconds);
            dataLogTimer.Elapsed += DataLogTimerTick;
        }

        #region Cyclic Read & Write field devices
        private void CyclicReadWriteDevicesTimer_Tick(object sender, EventArgs e)
        {
            //Stop the timer before starting the Cyclic poll task
            cyclicReadWriteDevicesTimer.Stop();

            Task.Factory.StartNew(new Action(() => {
                if (readWriteOpToggler)
                {
                    WriteAllCommandValuesToControllers();
                }
                else
                {
                    ReadAllControllersDataAndUpdateFieldDevicesData();
                }
                //Toggle the readWriteOpToggler bit
                readWriteOpToggler = !readWriteOpToggler;
            })).ContinueWith(new Action<Task>(RestartCyclicReadWriteDevicesTimer));
        }

        private void WriteAllCommandValuesToControllers()
        {
            foreach (FieldDevice fieldDevice in fieldDeviceWrapper.FieldDevices)
            {
                //Order FieldPoints according to Memory address
                fieldDevice.CommandPoints.OrderBy(fieldPoint => fieldPoint.MemoryAddress);

                //Group FieldPoints based on SourceControllerIdentifier
                IEnumerable<IGrouping<string, FieldPoint>> groupedBySourceControllerResult
                    = from fieldPoint in fieldDevice.CommandPoints
                      group fieldPoint by fieldPoint.SourceControllerIdentifier;

                foreach (IGrouping<string, FieldPoint> groupedByController in groupedBySourceControllerResult)
                {
                    //Group FieldPoints based on SourceControllerIdentifier
                    IEnumerable<IGrouping<string, FieldPoint>> groupedBySensorsDataSetResult
                        = from fieldPoint in groupedByController
                          group fieldPoint by fieldPoint.SensorDataSetIdentifier;

                    foreach (IGrouping<string, FieldPoint> groupedBySensorsDataSet in groupedBySensorsDataSetResult)
                    {
                        ulong minMemoryAddress
                                = groupedBySensorsDataSet.Min(fieldPoint => Convert.ToUInt64(fieldPoint.MemoryAddress.Split('|')[0]));
                        ulong maxMemoryAddress
                            = groupedBySensorsDataSet.Max(fieldPoint => Convert.ToUInt64(fieldPoint.MemoryAddress.Split('|')[0]));

                        IList<object> writeData = new List<object>();
                        for (ulong memoryCounter = minMemoryAddress; memoryCounter <= maxMemoryAddress; memoryCounter++)
                        {
                            FieldPoint fieldPoint
                                = groupedBySensorsDataSet.Where(item => item.MemoryAddress.Split('|')[0] == memoryCounter.ToString()).FirstOrDefault();
                            if (fieldPoint == null)
                            {
                                writeData.Add(0);
                            }
                            else
                            {
                                writeData.Add(fieldPoint.CommandValue);
                            }
                        }

                        bool commandExecutionStatus
                            = fieldDeviceWrapper.SendCommandToController(groupedByController.Key,
                                                                            minMemoryAddress.ToString() + "|" + (maxMemoryAddress - minMemoryAddress + 1), writeData);
                    }
                }
            }
        }

        private void ReadAllControllersDataAndUpdateFieldDevicesData()
        {
            foreach (IController controller in fieldDeviceWrapper.Controllers)
            {
                //First Read data completely from one controller and then start Updating asynchronously
                //otherwise UnAuthorizedAccessExceptions will come in case of Serial Port Communication
                DataReadFromController dataReadFromController = fieldDeviceWrapper.ReadDataFromOneController(controller.Identifier);
                Task.Factory.StartNew(new Action<object>(UpdateFieldDevicesDataWithDataReadFromController), dataReadFromController, TaskCreationOptions.None);
            }
        }

        private void UpdateFieldDevicesDataWithDataReadFromController(object obj)
        {
            DataReadFromController dataReadFromController = obj as DataReadFromController;
            foreach (FieldDevice fieldDevice in fieldDeviceWrapper.FieldDevices)
            {
                if (fieldDevice.ConnectedControllers.Any(controller => controller.Identifier == dataReadFromController.Identifier))
                {
                    foreach (SensorsDataSet sensorsDataSet in fieldDevice.SensorsData)
                    {
                        foreach (FieldPoint fieldPoint in sensorsDataSet.SensorsFieldPoints)
                        {
                            if (fieldPoint.SourceControllerIdentifier == dataReadFromController.Identifier)
                            {
                                //update communication status dictionary
                                if (devicesCommunicationStatus.ContainsKey(fieldDevice.Identifier))
                                {
                                    devicesCommunicationStatus[fieldDevice.Identifier] = dataReadFromController.ReadOperationStatus;
                                }
                                else
                                {
                                    devicesCommunicationStatus.Add(fieldDevice.Identifier, dataReadFromController.ReadOperationStatus);
                                }

                                string[] memoryAddressInfo = fieldPoint.MemoryAddress.Split('|');
                                string startAddress = memoryAddressInfo[0];
                                int registersCountToBeRead = int.Parse(memoryAddressInfo[1]);

                                if (dataReadFromController.Data.ContainsKey(startAddress))
                                {
                                    string fieldPointValueReadFromController = string.Empty;
                                    float multiplier = !string.IsNullOrWhiteSpace(fieldPoint.Multiplier) ? Convert.ToSingle(fieldPoint.Multiplier) : 1;
                                    if (dataReadFromController.ReadOperationStatus == false)
                                    {
                                        //Failed to read the corresponding Memory address value from controller
                                        if (fieldPoint.AreMaximumFailedReadOperationsCompleted())
                                        {
                                            fieldPointValueReadFromController = "NC";
                                        }
                                        else
                                        {
                                            fieldPoint.IncreaseFailedReadOperationStatusCounter();
                                        }
                                    }
                                    else
                                    {
                                        //Reset the FailedReadOperationStatuscounter in the field point class
                                        //if Read Operation is successful atleast once
                                        fieldPoint.ResetFailedReadOperationStatusCounter();

                                        ControllerTypeEnum controllerType = (ControllerTypeEnum)Enum.Parse(typeof(ControllerTypeEnum), dataReadFromController.ProviderName);
                                        switch (controllerType)
                                        {
                                            case ControllerTypeEnum.ModbusRtu:

                                                if (registersCountToBeRead == 1)
                                                {
                                                    fieldPointValueReadFromController 
                                                        = ApplyMultiplierToGivenValue(multiplier, 
                                                                string.IsNullOrWhiteSpace(fieldPoint.MaxValue) ? Convert.ToSingle(dataReadFromController.Data[startAddress])
                                                                : GetScaledValue(dataReadFromController.Data[startAddress], Convert.ToSingle(fieldPoint.MaxValue)));
                                                }
                                                else if (registersCountToBeRead == 2)
                                                {
                                                    byte[] valueBytes = new byte[4];
                                                    byte[] lowOrderBits = BitConverter.GetBytes(dataReadFromController.Data[startAddress]);
                                                    byte[] highOrderBits = BitConverter.GetBytes(dataReadFromController.Data[(ushort.Parse(startAddress) + 1).ToString()]);

                                                    valueBytes[0] = highOrderBits[0];
                                                    valueBytes[1] = highOrderBits[1];

                                                    valueBytes[2] = lowOrderBits[0];
                                                    valueBytes[3] = lowOrderBits[1];

                                                    fieldPointValueReadFromController = ApplyMultiplierToGivenValue(multiplier, Convert.ToSingle(Math.Round(BitConverter.ToSingle(valueBytes, 0), 1)));
                                                }
                                                break;
                                            default:
                                                break;
                                        }

                                        //convert field point value according to its data type
                                        ConvertFieldPointValueToItsDataType(ref fieldPointValueReadFromController, fieldPoint.FieldPointDataType);
                                    }

                                    //Update Field Point value to other Modules
                                    if (fieldPointValueReadFromController != fieldPoint.Value)
                                    {
                                        if (fieldPointValueReadFromController == "NC")
                                        {
                                            //Update the Old Field Point value with the latest One
                                            fieldPoint.Value = "NC";
                                            PrepareLiveDataObjecttoUpdateUI(fieldDevice.Identifier, fieldPoint);
                                        }
                                        else if(fieldPointValueReadFromController != string.Empty)
                                        {
                                            //Update the Old Field Point value with the latest One
                                            fieldPoint.Value = fieldPointValueReadFromController;
                                            PrepareLiveDataObjecttoUpdateUI(fieldDevice.Identifier, fieldPoint);
                                        }
                                        else
                                        {
                                            /* 
                                             * Don't update Ui if fieldPointValueReadFromController value is string.Empty
                                             * because we are using a failure counter to decide the success or failure of Read Operation
                                             */
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public string ApplyMultiplierToGivenValue(float multiplier, float value)
        {
            return Math.Round(multiplier * value, 1).ToString();
        }

        private void ConvertFieldPointValueToItsDataType(ref string fieldPointValue, string dataType)
        {
            switch (dataType)
            {
                case "bool":
                    fieldPointValue = Convert.ToSingle(fieldPointValue) >= 0.5 ? bool.TrueString : bool.FalseString;
                    break;
                case "float":
                    break;
                case "int":
                    fieldPointValue = Convert.ToInt32(fieldPointValue).ToString();
                    break;
                default:
                    break;
            }
        }

        public void PrepareLiveDataObjecttoUpdateUI(string deviceId, FieldPoint fieldPoint)
        {
            //After Updating FieldDevicesData Update UI
            FieldPointDataReceivedArgs fieldPointDataReceivedArgs = new FieldPointDataReceivedArgs
            {
                FieldDeviceIdentifier = deviceId,
                FieldPointIdentifier = fieldPoint.Label,
                SensorDataSetIdentifier = fieldPoint.SensorDataSetIdentifier,
                FieldPointDataType = fieldPoint.FieldPointDataType,
                FieldPointType = fieldPoint.TypeOfAddress,
                FieldPointDescription = fieldPoint.Description,
                NewFieldPointData = fieldPoint.Value,
            };
            Task.Factory.StartNew(new Action<object>(UpdateUIWithLatestDataReadFromController), fieldPointDataReceivedArgs, TaskCreationOptions.None);
        }

        public void UpdateUIWithLatestDataReadFromController(object changedFieldPointInfo)
        {
            FieldPointDataReceivedArgs fieldPointDataReceivedArgs = changedFieldPointInfo as FieldPointDataReceivedArgs;
            //Raise FieldPointDataReceived event to notify latest data to other modules
            if (FieldPointDataReceived != null)
            {
                var receivers = FieldPointDataReceived.GetInvocationList();
                foreach (var receiver in receivers)
                {
                    ((EventHandler<FieldPointDataReceivedArgs>)receiver).BeginInvoke(this, fieldPointDataReceivedArgs, null, null);
                }
            }
        }

        private float GetScaledValue(ushort givenValue, float maxValueAllowed)
        {
            double scaledValue = Math.Round((Math.Abs(givenValue) * maxValueAllowed) / 65535, 1);
            return (float)Convert.ChangeType(scaledValue, typeof(float));
        }

        private void RestartCyclicReadWriteDevicesTimer(Task task)
        {
            if (task.IsCompleted)
            {
                cyclicReadWriteDevicesTimer.Start();
            }
            else
            {
                if (task.IsFaulted)
                {
                    // display error to user
                }
            }
        }
        #endregion

        #region Data Logging
        public void StartDataLogging()
        {
            dataLogTimer.Start();
        }

        private void DataLogTimerTick(object sender, EventArgs e)
        {
            Task.Factory.StartNew(new Action(LogLiveData))
                .ContinueWith(new Action<Task>(NotifyDataLoggedEventToAllModules));
        }

        private void LogLiveData()
        {
            foreach (var fieldDevice in fieldDeviceWrapper.FieldDevices)
            {
                try
                {
                    IList<DbParameterInfo> dbParameters = new List<DbParameterInfo>();
                    fieldDevice.SensorsData.ToList()
                        .ForEach(sd => sd.SensorsFieldPoints.ToList().ForEach(fp => {
                            if (fp.ToBeLogged)
                            {
                                dbParameters.Add(new DbParameterInfo(fp.Description, string.IsNullOrWhiteSpace(fp.Value) || fp.Value.Contains("NC") ? "0" : fp.Value, DbType.String));
                            }
                        }));
                    if (dbParameters.Count > 0)
                    {
                        dbParameters.Add(new DbParameterInfo("FieldDeviceIdentifier", fieldDevice.Identifier, DbType.String));
                        databaseWriter.ExecuteWriteCommand("LogLiveData", CommandType.StoredProcedure, dbParameters);
                        logger.Log(LogType.Information, $"Data Logged for {fieldDevice.Label}");
                    }
                    else
                    {
                        logger.Log(LogType.Information, $"No parameters available to Log Data for {fieldDevice.Label}");
                    }
                }
                catch (Exception ex)
                {
                    logger.Log(LogType.Error, $"Data Logging failed for {fieldDevice.Label}", ex);
                }
            }
        }

        private void NotifyDataLoggedEventToAllModules(Task task)
        {
            NewDataLoggedIntoDatabase?.Invoke(this, EventArgs.Empty);
        }

        public TimeSpan GetLoggingIntervalDetails()
        {
            var intervalType = ConfigurationManager.AppSettings["LoggingIntervalType"];
            var timeInterval = ConfigurationManager.AppSettings["LoggingTimeInterval"];
            switch (intervalType)
            {
                case "Hours":
                    return TimeSpan.FromHours(int.Parse(timeInterval));
                case "Minutes":
                    return TimeSpan.FromMinutes(int.Parse(timeInterval));
                case "Seconds":
                    return TimeSpan.FromSeconds(int.Parse(timeInterval));
                case "MilliSeconds":
                    return TimeSpan.FromMilliseconds(int.Parse(timeInterval));
                case "Ticks":
                    return TimeSpan.FromTicks(int.Parse(timeInterval));
                default:
                    break;
            }
            return TimeSpan.FromMinutes(5);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Contains the History of Commands executed
        /// </summary>
        public IList<Command> CommandHistory
        {
            get; set;
        }
        #endregion

        #region Events
        public event EventHandler<FieldPointDataReceivedArgs> FieldPointDataReceived;
        public event EventHandler<CommandAckEventArgs> CommandAcknowledgementReceived;
        public event EventHandler NewDataLoggedIntoDatabase;
        #endregion

        #region Functions
        public string GetFieldDeviceIdentifier(string fieldDeviceLabel)
        {
            foreach (var fieldDevice in fieldDeviceWrapper.FieldDevices)
            {
                if (fieldDevice.Label == fieldDeviceLabel)
                {
                    return fieldDevice.Identifier;
                }
            }

            return null;
        }

        public Dictionary<string, string> GetConnectedController(string deviceId, string fieldPointLabel)
        {
            FieldDevice fieldDevice = fieldDeviceWrapper.FieldDevices.First(d => d.Identifier == deviceId);
            string controllerIdentifier
                = fieldDevice.SensorsData.ToList().First(s => s.SensorsFieldPoints.Any(f => f.Label == fieldPointLabel))
                        .SensorsFieldPoints.First(f => f.Label == fieldPointLabel).SourceControllerIdentifier;
            IController controller = fieldDeviceWrapper.Controllers.First(c => c.Identifier == controllerIdentifier);
            Dictionary<string, string> controllerInfo = new Dictionary<string, string>
            {
                {"Id", controller.Identifier },
                {"Label" , controller.Label},
                {"Address", controller.Address },
                {"CommunicationPort", controller.PortNumber }
            };
            return controllerInfo;
        }

        public string GetFieldDeviceType(string deviceId)
        {
            return fieldDeviceWrapper.FieldDevices.First(fd => fd.Identifier == deviceId).Type;
        }

        public string GetFieldDeviceLabel(string fieldDeviceIdentifier)
        {
            foreach (var fieldDevice in fieldDeviceWrapper.FieldDevices)
            {
                if (fieldDevice.Identifier == fieldDeviceIdentifier)
                {
                    return fieldDevice.Label;
                }
            }

            return null;
        }
        /// <summary>
        /// Read all the fieldPoint values 
        ///     initially at the start of program from the PLC
        /// </summary>
        public IList<FieldDevice> ReadAllSensorsLiveData()
        {
            IList<FieldDevice> fieldDevicesData = new List<FieldDevice>();

            fieldDevicesData =
                         fieldDeviceWrapper.ReadAllFieldDevicesData();

            return fieldDevicesData;
        }

        /// <summary>
        /// Gets the requested field device data from field device data
        /// </summary>
        /// <param name="fieldDeviceIdentifier"></param>
        public async Task<FieldDevice> ReadRequestedFieldDeviceData(string fieldDeviceIdentifier)
        {

            FieldDevice requestedFieldDeviceData = new FieldDevice();

            requestedFieldDeviceData
                = await fieldDeviceWrapper.ReadRequestedFieldDeviceData(fieldDeviceIdentifier);

            return requestedFieldDeviceData;
        }

        /// <summary>
        /// Check and send command to Device
        /// </summary>
        /// <param name="fieldDeviceIdentifier"></param>
        /// <param name="fieldPoint"></param>
        public void SendCommandToDevice(string fieldDeviceIdentifier, string fieldPointLabel, string dataTypeOfCommand, string writeValue)
        {
            foreach (var fieldDevice in fieldDeviceWrapper.FieldDevices)
            {
                if (fieldDevice.Identifier.Equals(fieldDeviceIdentifier))
                {
                    foreach (var fieldPoint in fieldDevice.CommandPoints)
                    {
                        if (fieldPoint.Label.Equals(fieldPointLabel))
                        {
                            fieldPoint.CommandValue = writeValue.ToString();
                            return;
                        }
                    }
                }
            }
        }

        public dynamic ReadFieldPointValue(string fieldDeviceIdentifier, string fieldPointIdentifier)
        {
            return fieldDeviceWrapper.ReadFieldPointValue(fieldDeviceIdentifier, fieldPointIdentifier);
        }
        public IList<FieldDevice> GetAllFieldDevicesData()
        {
            return fieldDeviceWrapper.GetAllFieldDevicesData();
        }

        public IList<string> GetAllFieldDeviceIdentifiers()
        {
            var identifierList = new List<string>();
            foreach (var fieldDevice in fieldDeviceWrapper.FieldDevices)
            {
                if (fieldDevice.Type.Equals("Reactor"))
                {
                    identifierList.Add(fieldDevice.Identifier);
                }
            }
            return identifierList;
        }


        public FieldDevice GetFieldDeviceData(string fieldDeviceIdentifier)
        {
            return fieldDeviceWrapper.FieldDevices.FirstOrDefault(s => s.Identifier == fieldDeviceIdentifier);
        }
        #endregion

        #region EventReceivers
        /// <summary>
        /// Receives the Field PointDataReceived event here
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="fieldPointEventArgs"></param>
        private void OnFieldPointDataReceived(object sender, FieldPointDataReceivedArgs fieldPointEventArgs)
        {
            //Raise FieldPointDataReceived event to notify live data to other modules
            if (FieldPointDataReceived != null)
            {
                var receivers = FieldPointDataReceived.GetInvocationList();
                foreach (var receiver in receivers)
                {
                    ((EventHandler<FieldPointDataReceivedArgs>)receiver).BeginInvoke(this, fieldPointEventArgs, null, null);
                }
            }
        }

        public void StartCyclicPollingOfFieldDevices(Action<Task> callback, TaskScheduler taskScheduler)
        {
            logger.Log(LogType.Information, "Starting Initialization of field devices");

            var task1 = fieldDeviceWrapper.Initialize(taskScheduler);
            task1.ContinueWith((t) =>
            {
                cyclicReadWriteDevicesTimer.Start();
            }).ContinueWith(callback, taskScheduler)
            .ContinueWith((t) => dataLogTimer.Start());
        }

        public T ReadFieldPointValue<T>(string fieldDeviceIdentifier, string fieldPointIdentifier)
        {
            return fieldDeviceWrapper.ReadFieldPointValue<T>(fieldDeviceIdentifier, fieldPointIdentifier);
        }

        public void ShareLiveDataToAllModules(FieldPointDataReceivedArgs fieldPointDataReceivedArgs)
        {
            //update the FieldDevices variable in FieldDevicesWrapper
            fieldDeviceWrapper.CheckAndReadChangedSensorsData(fieldPointDataReceivedArgs);

            //Raise FieldPointDataReceived event to notify live data to other modules
            if (FieldPointDataReceived != null)
            {
                var receivers = FieldPointDataReceived.GetInvocationList();
                foreach (var receiver in receivers)
                {
                    ((EventHandler<FieldPointDataReceivedArgs>)receiver).BeginInvoke(this, fieldPointDataReceivedArgs, null, null);
                }
            }
        }

        public void UpdateFieldDevicesDataForMobileDevicesInitialization(Task<IList<FieldDevice>> task)
        {
            if (task.IsCompleted)
            {
                fieldDeviceWrapper.FieldDevices = task.Result;
            }
            else
            {
                if (task.IsFaulted)
                {
                    // display error to user
                }
            }
        }

        public Dictionary<string, T> ReadFieldPointsInDataUnit<T>(string deviceId, string dataUnit)
        {
            return fieldDeviceWrapper.FieldDevices.First(device => device.Identifier == deviceId)
                    .SensorsData.First(sensorsDataSet => sensorsDataSet.DataUnit == dataUnit)
                    .SensorsFieldPoints.ToDictionary(fieldPoint => fieldPoint.Label, 
                        fieldPoint => TConverter.ChangeType<T>(fieldPoint.Value == "NC" || string.IsNullOrWhiteSpace(fieldPoint.Value) ? GetDefaultValue(fieldPoint.FieldPointDataType) : fieldPoint.Value));
        }

        public string GetDefaultValue(string dataType)
        {
            switch (dataType)
            {
                case "bool":
                    return bool.FalseString;
                case "int":
                case "float":
                case "double":
                    return "0";
                default:
                    return string.Empty;
            }
        }
        #endregion

        public Device GetBasicDeviceInfo(string fieldDeviceIdentifier)
        {
            FieldDevice fieldDevice = fieldDeviceWrapper.FieldDevices.First(d => d.Identifier == fieldDeviceIdentifier);
            return new Device
            {
                Id = fieldDevice.Identifier,
                Label = fieldDevice.Label,
                Type = fieldDevice.Type,
            };
        }
    }
}
