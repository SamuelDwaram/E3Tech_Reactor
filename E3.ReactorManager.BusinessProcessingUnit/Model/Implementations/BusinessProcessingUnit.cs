using System;
using E3.ReactorManager.BusinessProcessingUnit.Model.Interfaces;
using Unity;
using E3.ReactorManager.MqttHandler.Interfaces;
using E3.ReactorManager.MqttHandler.Data;
using E3.ReactorManager.MessageQueueClient.Data;
using Microsoft.Win32;
using E3.ReactorManager.Interfaces.HardwareAbstractionLayer;
using E3.ReactorManager.BusinessProcessingUnit.Model.Helpers;
using E3.ReactorManager.Interfaces.HardwareAbstractionLayer.Data;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace E3.ReactorManager.BusinessProcessingUnit.Model.Implementations
{
    public sealed class BusinessProcessingUnit : IBusinessProcessingUnit
    {
        TaskScheduler taskScheduler;
        IMqttClientHandler mqttClientHandler;
        IFieldDevicesCommunicator fieldDevicesCommunicator;
        QueueMessageParser queueMessageParser;
        private DeviceType deviceType;
        CancellationTokenSource waitForFieldDevicesDataCancellationTokenSource;
        Task waitForFieldDevicesDataForSomeTimeTask;

        public BusinessProcessingUnit(IUnityContainer containerProvider)
        {
            taskScheduler = TaskScheduler.FromCurrentSynchronizationContext();
            fieldDevicesCommunicator = containerProvider.Resolve<IFieldDevicesCommunicator>();
            fieldDevicesCommunicator.FieldPointDataReceived += OnLiveDataReceived;

            if (containerProvider.IsRegistered<IMqttClientHandler>())
            {
                queueMessageParser = containerProvider.Resolve<QueueMessageParser>();
                mqttClientHandler = containerProvider.Resolve<IMqttClientHandler>();
                mqttClientHandler.NewMessageReceivedFromMqttServer += HandleMessageReceivedFromMqttServer;
                StartMqtt();
            }

            deviceType = GetDeviceType();
            StartDataLogging();
            waitForFieldDevicesDataCancellationTokenSource = new CancellationTokenSource();
        }

        private void StartDataLogging()
        {
            /* Start Data Logging only if Device Type is HMI */
            if (DeviceType == DeviceType.HMI)
            {
                Task.Factory.StartNew(new Action(fieldDevicesCommunicator.StartDataLogging));
            }
        }

        private void StartMqtt()
        {
            var task = Task.Factory.StartNew(new Action(StartCommunicationWithMqttServer));
            task.ContinueWith(new Action<Task>(UpdateTheConnectionStatusToUI), TaskContinuationOptions.OnlyOnRanToCompletion);
        }

        private void UpdateTheConnectionStatusToUI(Task task)
        {
            if (mqttClientHandler.GetConnectionStatus())
            {
                // display successful message to user
            }
            else
            {
                MqttServerConnectionFailed?.Invoke(this, null);
            }
        }

        public void StartCommunicationWithMqttServer()
        {
            mqttClientHandler.ConnectToHostServer();

            if (mqttClientHandler.GetConnectionStatus())
            {
                SubscribeToQueueTopics(DeviceType);
            }
        }

        private void SubscribeToQueueTopics(DeviceType deviceType)
        {
            if (DeviceType == DeviceType.HMI)
            {
                mqttClientHandler.SubscribeToTopic(MessageTopic.InitializeFieldDevices);
                mqttClientHandler.SubscribeToTopic(MessageTopic.CommandToDevice);
            }
            else
            {
                mqttClientHandler.SubscribeToTopic(MessageTopic.InitializeFieldDevices);
                mqttClientHandler.SubscribeToTopic(MessageTopic.LiveData);
                mqttClientHandler.SubscribeToTopic(MessageTopic.SystemShutDown);
            }
        }

        private void OnLiveDataReceived(object sender, FieldPointDataReceivedArgs liveData)
        {
            if (mqttClientHandler != null && mqttClientHandler.GetConnectionStatus())
            {
                if (deviceType == DeviceType.HMI)
                {
                    /* Share live data to the Mqtt Server only if the device type is HMI */
                    mqttClientHandler.PublishMessage(MessageTopic.LiveData, liveData);
                }
            }
        }

        /// <summary>
        /// Based on Topic Divert Message respectively
        /// If live data received, Notify Graphical UI to update
        /// If Command from HMI, Notify to Hardware Interface Layer and Wait for Acknowledgement
        ///     Also Notify Tablets for Command Update
        ///     Also do not allow Command of same type to happen again
        /// Same is Case with Command from Tablet
        /// If Alarm from Hardware Layer
        ///     Notify to HMI UI
        ///     Send Message to Queue Topic 
        ///         Where notification is shown in Tablet
        /// </summary>
        private void HandleMessageReceivedFromMqttServer(object sender, QueueMessageEventArgs queueMessageEventArgs)
        {
            switch (queueMessageEventArgs.Topic)
            {
                case MessageTopic.InitializeFieldDevices:
                    HandleInitializeFieldDevicesMessage(queueMessageEventArgs);
                    break;
                case MessageTopic.AlarmRaised:
                    break;
                case MessageTopic.NewDataLoggedIntoDatabase:
                    break;
                case MessageTopic.CommandToDevice:
                    HandleCommandToDeviceMessage(queueMessageEventArgs);
                    break;
                case MessageTopic.LiveData:
                    HandleLiveDataMessage(queueMessageEventArgs);
                    break;
                case MessageTopic.SystemShutDown:
                    HandleSystemShutDownMessage(queueMessageEventArgs);
                    break;
                case MessageTopic.NotRecognized:
                    break;
                default:
                    break;
            }
        }

        #region Queue Message Handlers

        private void HandleSystemShutDownMessage(QueueMessageEventArgs queueMessageEventArgs)
        {
            /* Send Notification to UI to ShutDown application
               as HMI has Sent SystemShutDown message to Mqtt Server */
            var shutDownApplicationEventArgs = new ShutDownApplicationEventArgs
            {
                Context = "HMI is ShutDown",
                ShutDownDueTo = "HMI was Shut Down so there is no communication with Controller"
            };
            SendNotificationToShutDownApplicationEvent?.Invoke(this, shutDownApplicationEventArgs);
        }

        private void HandleCommandToDeviceMessage(QueueMessageEventArgs queueMessageEventArgs)
        {
            if (DeviceType == DeviceType.HMI)
            {
                /* Take action for this message topic only if the device type is HMI */
                var msgData = queueMessageParser.ParseQueueMessage<CommandToDeviceArgs>(queueMessageEventArgs);
                SendCommandToDevice(msgData.FieldDeviceId, msgData.FieldPointLabel, msgData.DataTypeOfCommand, msgData.WriteValue);
            }
        }

        private void HandleLiveDataMessage(QueueMessageEventArgs queueMessageEventArgs)
        {
            if (DeviceType != DeviceType.HMI)
            {
                /* Take action for this message topic only if the device type is not HMI */
                var liveData = queueMessageParser.ParseQueueMessage<FieldPointDataReceivedArgs>(queueMessageEventArgs);
                fieldDevicesCommunicator.ShareLiveDataToAllModules(liveData);
            }
        }

        private void HandleInitializeFieldDevicesMessage(QueueMessageEventArgs queueMessageEventArgs)
        {
            /* check if the message contains the request for field devices data(which is a string)
             * or it contains the field devices data */
            if (CheckIfMessageIsString(queueMessageEventArgs))
            {
                /* If the message is string */
                if (DeviceType == DeviceType.HMI)
                {
                    /* Take action for this message only if the device type is HMI */
                    Task.Factory.StartNew(new Action(ShareFieldDevicesDataWithNewlyConnectedMqttClient));
                }
            }
            else
            {
                if (DeviceType != DeviceType.HMI)
                {
                    /* Update the Field Devices data only if the device type is not HMI
                     * and this is the first time to update the Field devices data for this Device */
                    if (!waitForFieldDevicesDataForSomeTimeTask.IsCompleted)
                    {
                        /* Check if the waitForFieldDevicesDataForSomeTimeTask is not completed
                            then only cancel the task */
                        waitForFieldDevicesDataCancellationTokenSource.Cancel();
                    }

                    Task.Factory.StartNew(new Func<object, IList<FieldDevice>>(PopulateFieldDevicesData), queueMessageEventArgs)
                        .ContinueWith(new Action<Task<IList<FieldDevice>>>(fieldDevicesCommunicator.UpdateFieldDevicesDataForMobileDevicesInitialization))
                        .ContinueWith(new Action<Task>(UpdateDataReceivedEventToUI), taskScheduler);
                }
            }
            
        }
        
        private void UpdateDataReceivedEventToUI(Task task)
        {
            if (task.IsCompleted)
            {
                FieldDevicesDataReceivedFromMqttServer?.Invoke(this, null);
            }
            else
            {
                if (task.IsFaulted)
                {
                    // display error to user
                }
            }
        }

        private IList<FieldDevice> PopulateFieldDevicesData(object arg)
        {
            try
            {
                return queueMessageParser.ParseQueueMessage<IList<FieldDevice>>(arg as QueueMessageEventArgs);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool CheckIfMessageIsString(QueueMessageEventArgs queueMessageEventArgs)
        {
            try
            {
                queueMessageParser.ParseQueueMessage<string>(queueMessageEventArgs);
            }
            catch(Exception)
            {
                return false;
            }

            return true;
        }

        #endregion

        private void ShareFieldDevicesDataWithNewlyConnectedMqttClient()
        {
            if (mqttClientHandler.GetConnectionStatus())
            {
                mqttClientHandler.PublishMessage<IList<FieldDevice>>(MessageTopic.InitializeFieldDevices, fieldDevicesCommunicator.GetAllFieldDevicesData());
            }
        }
        
        /// <summary>
        /// When UI is Clicked
        ///     Send Command to Harware Abstraction Layer for further processing
        /// </summary>
        /// <param name="fieldDeviceIdentifier"></param>
        /// <param name="fieldPointIdentifier"></param>
        /// <param name="dataTypeOfCommand"></param>
        /// <param name="writeValue"></param>
        public void SendCommandToDevice(string fieldDeviceIdentifier, string fieldPointIdentifier, string dataTypeOfCommand, string writeValue)
        {
            if (DeviceType == DeviceType.HMI)
            {
                /* Send command to fieldDevicesCommunicator in Hardware Abstraction Layer */
                fieldDevicesCommunicator.SendCommandToDevice(fieldDeviceIdentifier, fieldPointIdentifier, dataTypeOfCommand, writeValue);
            }
            else
            {
                /* Send Command Data into Queue with Message Topic as CommandToDevice */
                var commandToDeviceArgs = new CommandToDeviceArgs
                {
                    FieldDeviceId = fieldDeviceIdentifier,
                    FieldPointLabel = fieldPointIdentifier,
                    DataTypeOfCommand = dataTypeOfCommand,
                    WriteValue = writeValue
                };
                Task.Factory.StartNew(new Func<object, object>(SendCommandDataToMqttServer), commandToDeviceArgs);
            }
        }

        private object SendCommandDataToMqttServer(object arg)
        {
            if (mqttClientHandler.GetConnectionStatus())
            {
                mqttClientHandler.PublishMessage<CommandToDeviceArgs>(MessageTopic.CommandToDevice, arg as CommandToDeviceArgs);
            }

            return null;
        }

        public DeviceType GetDeviceType()
        {
            const string userRoot = "HKEY_LOCAL_MACHINE";
            const string subkey = "SOFTWARE\\DeviceInfo";
            const string keyName = userRoot + "\\" + subkey;

            var registryKeyValue = Registry.GetValue(keyName, "Type", DeviceType.HMI);

            if (registryKeyValue != null)
            {
                if (Enum.TryParse(registryKeyValue.ToString(), out DeviceType enumParsingResult))
                {
                    return enumParsingResult;
                }
            }

            return DeviceType.HMI;
        }

        public void InitializeFieldDevices(Action<Task> callBack, TaskScheduler taskScheduler)
        {
            if (DeviceType == DeviceType.HMI)
            {
                /* Start cyclic polling of field devices only if the Device type is HMI */
                fieldDevicesCommunicator.StartCyclicPollingOfFieldDevices(callBack, taskScheduler);
            }
            else
            {
                /* Send Msg to Mqtt Server requesting initialization of field devices data */
                SendRequestForFieldDevicesDataToMqttServer();
            }
        }

        private void SendRequestForFieldDevicesDataToMqttServer()
        {
            if (mqttClientHandler.GetConnectionStatus())
            {
                mqttClientHandler.PublishMessage<string>(MessageTopic.InitializeFieldDevices, "Please Share Field Devices Data for Initialization");

                /* 
                 * create a task containing a delay of 5 seconds
                 * after completing the delay check whether the field devices data has been received or not
                 * if not received then raise an event HMI application is not responding
                 * receive this event in the MainViewModel and show a popup to shut down the tab application 
                 * and start after the HMI application has been started
                 */
                waitForFieldDevicesDataForSomeTimeTask 
                    = Task.Factory.StartNew(new Action(WaitForFieldDevicesDataForSomeTime), waitForFieldDevicesDataCancellationTokenSource.Token)
                    .ContinueWith(new Action<Task>(SendNotificationToShutDownApplication), taskScheduler);
            }
        }

        private void SendNotificationToShutDownApplication(Task task)
        {
            if (task.IsCompleted)
            {
                /* Send Notification to UI to shut down application
                   as Field devices data has not been received 
                   since long back request has been sent to Mqtt Server */
                var shutDownApplicationEventArgs = new ShutDownApplicationEventArgs
                {
                    Context = "HMI failed to Respond",
                    ShutDownDueTo = "No Data Received from HMI, Please Shut Down"
                };
                SendNotificationToShutDownApplicationEvent?.Invoke(this, shutDownApplicationEventArgs);
            }
        }

        private void WaitForFieldDevicesDataForSomeTime()
        {
            Thread.Sleep(5000);
        }

        public bool ConfirmUpdateUIRequest()
        {
            /* If Device Type is not HMI then the UI has to be navigated back to login page 
             * after the Field Devices Data is received from Mqtt server */
            if (DeviceType != DeviceType.HMI)
            {
                return true;
            }

            return false;
        }

        public bool ConfirmSendDirectCommandsToPlc()
        {
            /* Send DirectCommandsToPlc only if Devicetype is HMI */
            if (DeviceType == DeviceType.HMI)
            {
                return true;
            }

            return false;
        }

        public event EventHandler MqttServerConnectionFailed;
        public event EventHandler FieldDevicesDataReceivedFromMqttServer;
        public event ShutDownApplicationEventHandler SendNotificationToShutDownApplicationEvent;

        public DeviceType DeviceType => deviceType;
    }
}
