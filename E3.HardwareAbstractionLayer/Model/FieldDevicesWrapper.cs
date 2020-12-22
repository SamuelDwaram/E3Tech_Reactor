using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using E3.HardwareAbstractionLayer.Helpers;
using E3.ReactorManager.ControllerProvider.Model;
using E3.ReactorManager.ControllerProvider.Model.Interfaces;
using E3.ReactorManager.Interfaces.DataAbstractionLayer;
using E3.ReactorManager.Interfaces.DataAbstractionLayer.Data;
using E3.ReactorManager.Interfaces.Framework.Logging;
using E3.ReactorManager.Interfaces.HardwareAbstractionLayer;
using E3.ReactorManager.Interfaces.HardwareAbstractionLayer.Data;
using Unity;

namespace E3.HardwareAbstractionLayer.Model
{
    public class FieldDevicesWrapper
    {
        #region Fields
        IDatabaseReader databaseReader;
        ILogger logger;
        TaskScheduler taskScheduler;
        ControllerHandlerFactory controllerHandlerFactory;
        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public FieldDevicesWrapper(IUnityContainer containerProvider)
        {
            fieldDevices = new List<FieldDevice>();
            databaseReader = containerProvider.Resolve<IDatabaseReader>();
            logger = containerProvider.Resolve<ILogger>();
            controllerHandlerFactory = containerProvider.Resolve<ControllerHandlerFactory>();
            taskScheduler = TaskScheduler.FromCurrentSynchronizationContext();
        }

        #region Properties
        /// <summary>
        /// Field Devices
        /// </summary>
        private IList<FieldDevice> fieldDevices;
        public IList<FieldDevice> FieldDevices
        {
            get => fieldDevices ?? (fieldDevices = new List<FieldDevice>());
            set => fieldDevices = value;
        }

        private IList<IController> _controllers;
        public IList<IController> Controllers
        {
            get => _controllers ?? (_controllers = new List<IController>());
            set => _controllers = value;
        }
        #endregion

        #region Events
        public EventHandler<FieldPointDataReceivedArgs> FieldPointDataReceived;
        #endregion

        #region Initialize Field Devices
        internal Task<IList<FieldDevice>> Initialize(TaskScheduler taskScheduler)
        {
            var tcs = new TaskCompletionSource<IList<FieldDevice>>();
            CancellationToken token = new CancellationToken();
            var task1 = InitializeFieldDeviceData(token);
            var task2 = task1.ContinueWith(t => GetConnectFieldDevicesTask(t.Result, token));
            var task3 = task2.ContinueWith(t => GetCreateNotificationHandlesTask(t.Result.Result, token, taskScheduler));
            task3.ContinueWith(t => tcs.SetResult(t.Result.Result));
            return tcs.Task;
        }

        /// <summary>
        /// Pass the OnFieldPointDataChangedNotification Receiver method as a callback
        /// to avoid multiple subscription for the same field point value change notification
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public Task<IList<FieldDevice>> InitializeFieldDeviceData(CancellationToken token)
        {
            return Task<IList<FieldDevice>>.Factory.StartNew(new Func<IList<FieldDevice>>(PrepareFieldDevices), token);
        }

        private IList<FieldDevice> PrepareFieldDevices()
        {
            IList<FieldDevice> fieldDevicesList = new List<FieldDevice>();

            foreach (DataRow row in databaseReader.ExecuteReadCommand("select * from dbo.FieldDevices", CommandType.Text).AsEnumerable())
            {
                var fieldDevice = new FieldDevice
                {
                    Identifier = row["Identifier"].ToString(),
                    Label = row["Label"].ToString(),
                    Type = row["Type"].ToString(),
                    SensorsData = PrepareSensorsDataSetForFieldDevice(row["Identifier"].ToString()),
                    CommandPoints = PrepareCommandPointsForFieldDevice(row["Identifier"].ToString()),
                    ConnectedControllers = PrepareControllersForFieldDevice(row["Identifier"].ToString())
                };

                foreach (IController connectedController in fieldDevice.ConnectedControllers)
                {
                    if (Controllers.Any(controller => controller.Identifier == connectedController.Identifier))
                    {
                        //Continue
                    }
                    else
                    {
                        //Add the new controller to the Existing List = Controllers
                        Controllers.Add(connectedController);
                    }
                }

                fieldDevicesList.Add(fieldDevice);
            }

            return fieldDevicesList;
        }

        private IList<FieldPoint> PrepareCommandPointsForFieldDevice(string deviceId)
        {
            return (from DataRow row in databaseReader.ExecuteReadCommand($"select * from dbo.FieldPoints" + 
                    $" where SensorDataSetIdentifier = any(select SensorDataSetIdentifier from[dbo].CommandPoints where FieldDeviceIdentifier = '{deviceId}')" +
                    $" and Label = any(select Label from[dbo].CommandPoints where FieldDeviceIdentifier = '{deviceId}')", CommandType.Text).AsEnumerable()
                    select new FieldPoint
                    {
                        Label = row["Label"].ToString(),
                        Description = row["Description"].ToString(),
                        TypeOfAddress = row["TypeOfAddress"].ToString(),
                        MemoryAddress = row["MemoryAddress"].ToString(),
                        FieldPointDataType = row["FieldPointDataType"].ToString(),
                        SensorDataSetIdentifier = row["SensorDataSetIdentifier"].ToString(),
                        RequireNotificationService = (row["RequireNotificationService"].ToString().Length > 0) ? bool.Parse(row["RequireNotificationService"].ToString()) : false,
                        ToBeLogged = (row["ToBeLogged"].ToString().Length > 0) ? bool.Parse(row["ToBeLogged"].ToString()) : false,
                        MaxValue = row["MaxValue"].ToString(),
                        Offset = row["Offset"].ToString(),
                        Multiplier = row["Multiplier"].ToString(),
                        SourceControllerIdentifier = GetControllerInstance(row["SourceControllerIdentifier"].ToString()).Identifier
                    }).ToList();
        }

        private IList<IController> PrepareControllersForFieldDevice(string fieldDeviceIdentifier)
        {
            IList<IController> controllers = new List<IController>();
            foreach (DataRow row in databaseReader.ExecuteReadCommand($"select * from dbo.FieldDevicesAndConnectedControllers where FieldDeviceIdentifier = '{fieldDeviceIdentifier}'", CommandType.Text).AsEnumerable())
            {
                controllers.Add(GetControllerInstance(row["ControllerIdentifier"].ToString()));
            }

            return controllers;
        }

        private IController GetControllerInstance(string controllerIdentifier)
        {
            IController controller = null;

            foreach (DataRow row in databaseReader.ExecuteReadCommand($"select * from dbo.Controllers where Identifier = '{controllerIdentifier}'", CommandType.Text).AsEnumerable())
            {
                controller = controllerHandlerFactory.CreateControllerObject(row["ProviderName"].ToString());
                controller.Identifier = row["Identifier"].ToString();
                controller.Address = row["Address"].ToString();
                controller.PortNumber = row["PortNumber"].ToString();
                controller.Label = row["Label"].ToString();
                controller.ResponseTime = Convert.ToInt32(row["ResponseTime"] ?? 1000);
                controller.UsedMemoryAddresses = GetUsedMemoryAddressesOfController(row["Identifier"].ToString());
            }

            return controller;
        }

        private IList<string> GetUsedMemoryAddressesOfController(string identifier)
        {
            return (from DataRow row in databaseReader.ExecuteReadCommand($"select * from dbo.UsedMemoryAddressesInControllers where ControllerIdentifier='{identifier}'", CommandType.Text).AsEnumerable()
                    select row["UsedMemoryAddress"].ToString()).ToList();
        }

        private IList<SensorsDataSet> PrepareSensorsDataSetForFieldDevice(string fieldDeviceIdentifier)
        {
            IList<SensorsDataSet> sensorsDataSet = new List<SensorsDataSet>();

            foreach (DataRow row in databaseReader.ExecuteReadCommand($"select * from dbo.SensorsDataSet where FieldDeviceIdentifier = '{fieldDeviceIdentifier}'", CommandType.Text).AsEnumerable())
            {
                var eachSensorsdataSet = new SensorsDataSet
                {
                    Identifier = row["Identifier"].ToString(),
                    Label = row["Label"].ToString(),
                    DataUnit = row["DataUnit"].ToString(),
                    SensorsFieldPoints = PrepareFieldPointsForSensorsDataSet(row["Identifier"].ToString())
                };

                sensorsDataSet.Add(eachSensorsdataSet);
            }

            return sensorsDataSet;
        }

        private IList<FieldPoint> PrepareFieldPointsForSensorsDataSet(string sensorsDataSetIdentifier)
        {
            IList<FieldPoint> fieldPoints = new List<FieldPoint>();

            foreach (DataRow row in databaseReader.ExecuteReadCommand($"select * from dbo.FieldPoints where SensorDataSetIdentifier = '{sensorsDataSetIdentifier}'", CommandType.Text).AsEnumerable())
            {
                var fieldPoint = new FieldPoint
                {
                    Label = row["Label"].ToString(),
                    Description = row["Description"].ToString(),
                    TypeOfAddress = row["TypeOfAddress"].ToString(),
                    MemoryAddress = row["MemoryAddress"].ToString(),
                    FieldPointDataType = row["FieldPointDataType"].ToString(),
                    SensorDataSetIdentifier = row["SensorDataSetIdentifier"].ToString(),
                    RequireNotificationService = (row["RequireNotificationService"].ToString().Length > 0) ? bool.Parse(row["RequireNotificationService"].ToString()) : false,
                    ToBeLogged = (row["ToBeLogged"].ToString().Length > 0) ? bool.Parse(row["ToBeLogged"].ToString()) : false,
                    MaxValue = row["MaxValue"].ToString(),
                    Offset = row["Offset"].ToString(),
                    Multiplier = row["Multiplier"].ToString(),
                    SourceControllerIdentifier = GetControllerInstance(row["SourceControllerIdentifier"].ToString()).Identifier
                };

                fieldPoints.Add(fieldPoint);
            }

            return fieldPoints;
        }

        /// <summary>
        /// Connect to PLC using TwinCAT client
        /// </summary>
        public Task<IList<FieldDevice>> GetConnectFieldDevicesTask(IList<FieldDevice> fieldDevices, CancellationToken token)
        {
            return Task<IList<FieldDevice>>.Factory.StartNew(new Func<object, IList<FieldDevice>>(ConnectToFieldDevices), fieldDevices, token);
        }

        private IList<FieldDevice> ConnectToFieldDevices(object fieldDevices)
        {
            //TODO : Check the connections of the field devices with connected controllers

            return (IList<FieldDevice>)fieldDevices;
        }

        private Task<IList<FieldDevice>> GetCreateNotificationHandlesTask(IList<FieldDevice> fieldDevices, CancellationToken token, TaskScheduler taskScheduler)
        {
            return Task<IList<FieldDevice>>.Factory.StartNew(new Func<object, IList<FieldDevice>>(CreateNotificationHandlesSync), fieldDevices, token, TaskCreationOptions.LongRunning, taskScheduler);
        }

        private IList<FieldDevice> CreateNotificationHandlesSync(object fieldDevices)
        {
            FieldDevices = (IList<FieldDevice>)fieldDevices;
            
            //TODO : Create Notification Handles for the Field Devices
            
            return FieldDevices;
        }
        #endregion

        #region Notification Field point data readers from the plc
        /// <summary>
        /// update only changed fieldpoint data
        /// </summary>
        /// <param name="fieldPointEventArgs"></param>
        public void CheckAndReadChangedSensorsData(FieldPointDataReceivedArgs fieldPointEventArgs)
        {
            //update only changed fieldpoint data
            FieldDevices
                .Where(fieldDevice => fieldDevice.Identifier == fieldPointEventArgs.FieldDeviceIdentifier).First()
                .SensorsData.Where(sensorsDataSet => sensorsDataSet.Identifier == fieldPointEventArgs.SensorDataSetIdentifier).First()
                .SensorsFieldPoints.Where(fieldPoint => fieldPoint.Label == fieldPointEventArgs.FieldPointIdentifier).ToList()
                .ForEach(item => item.Value = fieldPointEventArgs.NewFieldPointData);
        }
        #endregion

        #region Field Device and Field point data providers to other modules
        /// <summary>
        /// Read all field Devices
        /// </summary>
        /// <returns></returns>
        public IList<FieldDevice> ReadAllFieldDevicesData()
        {
            foreach (var fieldDevice in FieldDevices)
            {
                /*
                 * Read all the field points in the field device
                 * only if the connection between the plc of the field device exists
                 */
                foreach (var sensorsDataSet in fieldDevice.SensorsData)
                {
                    foreach (var fieldPoint in sensorsDataSet.SensorsFieldPoints)
                    {
                        // Read data of the field point only
                        // if it does not subscribes to NotificationService in plc
                        if (!fieldPoint.RequireNotificationService)
                        {
                            string fieldPointValue = GetFieldPointValueFromController(fieldPoint, fieldDevice.ConnectedControllers);
                            /*
                             * Raise FieldPointDataReceived event if the
                             * latest read field point value is different from old field point value
                             */
                            if (fieldPointValue != fieldPoint.Value
                                    && !string.IsNullOrWhiteSpace(fieldPointValue))
                            {
                                fieldPoint.Value = fieldPointValue;

                                /* update the value changed boolean bit */
                                fieldPoint.ValueChanged = true;
                            }
                        }
                    }
                }
            }

            return FieldDevices;
        }

        private string GetFieldPointValueFromController(FieldPoint fieldPoint, IList<IController> connectedControllers)
        {
            IController controller = connectedControllers.Where(eachController => eachController.Address == fieldPoint.SourceControllerIdentifier).First();

            switch (fieldPoint.FieldPointDataType)
            {
                //case "int":
                //    return controller.Read<int>(fieldPoint.MemoryAddress).ToString();
                //case "float":
                //    return controller.Read<float>(fieldPoint.MemoryAddress).ToString();
                //case "bool":
                //    return controller.Read<bool>(fieldPoint.MemoryAddress).ToString();
                //case "double":
                //    return controller.Read<double>(fieldPoint.MemoryAddress).ToString();
                default:
                    return string.Empty;
            }
        }

        /// <summary>
        /// Read particular field Device data
        /// </summary>
        /// <returns></returns>
        public async Task<FieldDevice> ReadRequestedFieldDeviceData(string requestedFieldDeviceIdentifier)
        {
            FieldDevice toReturnFieldDevice = new FieldDevice();

            foreach (var fieldDevice in FieldDevices)
            {
                if (requestedFieldDeviceIdentifier.Equals(fieldDevice.Identifier))
                {
                    toReturnFieldDevice = fieldDevice;
                    break;
                }
            }

            await Task.Yield();

            return toReturnFieldDevice;
        }

        /// <summary>
        /// Returns all FieldDevicesRunning Status
        /// </summary>
        /// <returns></returns>
        public IList<FieldDevice> GetAllFieldDevicesData()
        {
            return FieldDevices;
        }

        public dynamic ReadFieldPointValue(string fieldDeviceIdentifier, string fieldPointLabel)
        {
            foreach (var fieldDevice in FieldDevices)
            {
                if (fieldDevice.Identifier.Equals(fieldDeviceIdentifier))
                {
                    foreach (var sensorsDataSet in fieldDevice.SensorsData)
                    {
                        foreach (var fieldPoint in sensorsDataSet.SensorsFieldPoints)
                        {
                            if (fieldPoint.Label.Equals(fieldPointLabel))
                            {
                                Type fieldPointDataType = GetDataType(fieldPoint.FieldPointDataType);
                                return TryParse(fieldPointDataType, fieldPoint.Value);
                            }
                        }
                    }
                }
            }

            return null;
        }

        public T ReadFieldPointValue<T>(string fieldDeviceIdentifier, string fieldPointIdentifier)
        {
            var fieldDevice = new List<FieldDevice>(FieldDevices).Find(device => device.Identifier == fieldDeviceIdentifier);

            foreach (var sensorsDataSet in fieldDevice.SensorsData)
            {
                var fieldPoint = new List<FieldPoint>(sensorsDataSet.SensorsFieldPoints).Find(obj => obj.Label == fieldPointIdentifier);
                if (fieldPoint != null)
                {
                    return TConverter.ChangeType<T>(fieldPoint.Value);
                }
            }

            return default;
        }
        #endregion

        #region Data Readers from Controllers
        public DataReadFromController ReadDataFromOneController(string controllerIdentifier)
        {
            IController controller = Controllers.First(item => item.Identifier == controllerIdentifier);
            Dictionary<string, ushort> dataReadFromOneControllerDictionary = new Dictionary<string, ushort>();
            Dictionary<string, ushort> dataReadFromOneUsedMemoryAddressInControllerDictionary = new Dictionary<string, ushort>();
            foreach (string usedMemoryAddress in controller.UsedMemoryAddresses)
            {
                dataReadFromOneUsedMemoryAddressInControllerDictionary = controller.Read(usedMemoryAddress);
                /* Add the Data Read from one controller to the Main List 
                   Except the one with Key ReadOperationStatus as it only indicates the read operation status from the controller */
                dataReadFromOneUsedMemoryAddressInControllerDictionary
                    .Where(keyValuePair => keyValuePair.Key != "ReadOperationStatus").ToList()
                    .ForEach(keyValuePair => dataReadFromOneControllerDictionary.Add(keyValuePair.Key, keyValuePair.Value));
            }

            //Add the data read from one controller to the Main List
            //only if the controller.UsedMemoryAddress > 0 (then only read operation status is obtained)
            DataReadFromController dataReadFromControllerObject = new DataReadFromController
            {
                Identifier = controller.Identifier,
                Label = controller.Label,
                Address = controller.Address,
                ProviderName = controller.ProviderName,
                Data = dataReadFromOneControllerDictionary,
                ReadOperationStatus = controller.UsedMemoryAddresses.Count > 0 ? Convert.ToBoolean(dataReadFromOneUsedMemoryAddressInControllerDictionary["ReadOperationStatus"]) : false,
            };

            return dataReadFromControllerObject;
        }
        #endregion

        public bool SendCommandToController(string controllerId, string memoryAddress, object writeValue)
        {
            return GetControllerInstance(controllerId).Write(memoryAddress, writeValue);
        }

        #region Data Parsing Functions
        public dynamic TryParse(Type fieldPointDataType, string fieldPointValue)
        {
            switch (Type.GetTypeCode(fieldPointDataType))
            {
                case TypeCode.Int32:
                    return Convert.ToInt32(fieldPointValue);
                case TypeCode.String:
                    return fieldPointValue;
                case TypeCode.Boolean:
                    return bool.Parse(fieldPointValue);
                case TypeCode.Double:
                    return double.Parse(fieldPointValue);
                case TypeCode.Single:
                    return float.Parse(fieldPointValue);
            }
            return null;
        }

        /// <summary>
        /// converts the command.writevalue 
        ///     into required format
        /// </summary>
        /// <param name="commandDataType"></param>
        /// <param name="writeValue"></param>
        /// <returns></returns>
        private object ParseCommandData(string commandDataType, string writeValue)
        {
            switch (commandDataType)
            {
                case "bool":
                    return bool.Parse(writeValue);
                case "int":
                    return int.Parse(writeValue);
                case "float":
                    return float.Parse(writeValue);
                case "string":
                    return writeValue;
            }
            return string.Empty;
        }

        /// <summary>
        /// Returns the fieldPointDataType for
        ///     reading its value
        /// </summary>
        /// <param name="dataType"></param>
        /// <returns></returns>
        public Type GetDataType(string dataType)
        {
            switch (dataType)
            {
                case "bool":
                    return typeof(bool);
                case "int":
                    return typeof(int);
                case "string":
                    return typeof(string);
                case "float":
                    return typeof(float);
            }
            return typeof(Nullable);
        }
        #endregion

        #region Close All Field Device Connections to Plc
        /// <summary>
        /// Close all fieldDevice connections on closing the application
        /// </summary>
        public async void CloseAllFieldDeviceConnections()
        {
            foreach (var fieldDevice in FieldDevices)
            {
                fieldDevice.RelatedPlc.Disconnect();
            }

            await Task.Yield();
        }
        #endregion
    }
}
