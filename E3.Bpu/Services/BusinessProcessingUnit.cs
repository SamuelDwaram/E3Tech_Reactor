using E3.Bpu.Models;
using E3.MqttProvider.Models;
using E3.MqttProvider.Services;
using E3.ReactorManager.Interfaces.HardwareAbstractionLayer;
using E3.ReactorManager.Interfaces.HardwareAbstractionLayer.Data;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Unity;

namespace E3.Bpu.Services
{
    public class BusinessProcessingUnit : IBusinessProcessingUnit
    {
        private readonly IFieldDevicesCommunicator fieldDevicesCommunicator;
        private readonly IMqttManager mqttManager;
        private readonly DeviceType deviceType;
        private readonly Action<Task> uiCallBack;
        private readonly TaskScheduler uiTaskScheduler;
        private bool fieldDevicesDataReceivedFromMqttServer;
        private readonly IUnityContainer unityContainer;

        public BusinessProcessingUnit(IFieldDevicesCommunicator fieldDevicesCommunicator, IDevicesHandler devicesHandler,
            IUnityContainer unityContainer, Action<Task> uiCallBack, TaskScheduler uiTaskScheduler)
        {
            deviceType = devicesHandler.CurrentDevice.DeviceType;
            this.uiCallBack = uiCallBack;
            this.uiTaskScheduler = uiTaskScheduler;
            this.fieldDevicesCommunicator = fieldDevicesCommunicator;
            this.fieldDevicesCommunicator.FieldPointDataReceived += OnLiveDataReceived;

            this.unityContainer = unityContainer;
            if (unityContainer.IsRegistered<IMqttManager>())
            {
                mqttManager = unityContainer.Resolve<IMqttManager>();
                mqttManager.MqttMessageReceived += MqttManager_MqttMessageReceived;
            }

            Task.Factory.StartNew(InitializeMqtt)
                .ContinueWith((t) => InitializeFieldDevices(uiCallBack, uiTaskScheduler));
        }

        private void InitializeFieldDevices(Action<Task> callBack, TaskScheduler uiTaskScheduler)
        {
            if (deviceType == DeviceType.HMI)
            {
                /* Start cyclic polling of field devices only if the Device type is HMI */
                fieldDevicesCommunicator.StartCyclicPollingOfFieldDevices(callBack, uiTaskScheduler);
            }
            else
            {
                /* Send Msg to Mqtt Server requesting initialization of field devices data */
                SendRequestForFieldDevicesDataToMqttServer();
            }
        }

        private void SendRequestForFieldDevicesDataToMqttServer()
        {
            if (mqttManager.IsConnected())
            {
                mqttManager.Publish("InitializeFieldDevices", "Please Share Field Devices Data for Initialization");
            }
            Task.Factory.StartNew(() => Thread.Sleep(30000))
                .ContinueWith((t) => {
                    if (fieldDevicesDataReceivedFromMqttServer)
                    {
                        //Skip. Data Received From Mqtt Server
                    }
                    else
                    {
                        ShutDownClientApp("No Data For Initialization", "FieldDevices Data is not received from Hmi");
                    }
                });
        }

        private void InitializeMqtt()
        {
            mqttManager.Connect();

            if (mqttManager.IsConnected())
            {
                SubscribeToQueueTopics(deviceType);
            }
            else
            {
                MqttServerConnectionFailed?.BeginInvoke(this, new EventArgs(), null, null);
            }
        }

        private void SubscribeToQueueTopics(DeviceType deviceType)
        {
            if (deviceType == DeviceType.HMI)
            {
                mqttManager.SubscribeToTopic("InitializeFieldDevices");
                mqttManager.SubscribeToTopic("CommandToDevice");
            }
            else
            {
                mqttManager.SubscribeToTopic("InitializeFieldDevicesAck");
                mqttManager.SubscribeToTopic("LiveData");
                mqttManager.SubscribeToTopic("SystemShutDown");
            }
        }

        private void MqttManager_MqttMessageReceived(string clientId, MqttMessage mqttMessage)
        {
            switch (mqttMessage.Topic)
            {
                case "InitializeFieldDevices":
                    HandleInitializeFieldDevicesMessage(mqttMessage);
                    break;
                case "InitializeFieldDevicesAck":
                    HandleInitializeFieldDevicesAckMessage(mqttMessage);
                    break;
                case "CommandToDevice":
                    HandleCommandToDeviceMessage(mqttMessage);
                    break;
                case "LiveData":
                    HandleLiveDataMessage(mqttMessage);
                    break;
                case "SystemShutDown":
                    HandleSystemShutDownMessage(mqttMessage);
                    break;
                default:
                    break;
            }
        }

        #region Queue Message Handlers
        private void HandleSystemShutDownMessage(MqttMessage mqttMessage)
        {
            ShutDownClientApp("HMI ShutDown", mqttMessage.ExtractedMessage as string);
        }

        public void ShutDownClientApp(string context, string info)
        {
            /* Send Notification to UI to ShutDown application
               as HMI has Sent SystemShutDown message to Mqtt Server */
            var shutDownApplicationEventArgs = new ShutDownClientEventArgs
            {
                Context = context,
                Info = info
            };
            ShutDownClient?.Invoke(this, null);
        }

        private void HandleCommandToDeviceMessage(MqttMessage mqttMessage)
        {
            /* Take action for this message topic only if the device type is HMI */
            SendCommandToDevice(mqttMessage.ExtractedMessage as CommandToDeviceArgs);
        }

        private void HandleLiveDataMessage(MqttMessage mqttMessage)
        {
            if (deviceType != DeviceType.HMI)
            {
                /* Take action for this message topic only if the device type is not HMI */
                fieldDevicesCommunicator
                    .ShareLiveDataToAllModules(mqttMessage.ExtractedMessage as FieldPointDataReceivedArgs);
            }
        }

        private void HandleInitializeFieldDevicesMessage(MqttMessage mqttMessage)
        {
            Task.Factory.StartNew(() => {
                if (mqttManager.IsConnected())
                {
                    mqttManager.Publish("InitializeFieldDevicesAck", fieldDevicesCommunicator.GetAllFieldDevicesData());
                }
            });
        }

        private void HandleInitializeFieldDevicesAckMessage(MqttMessage mqttMessage)
        {
            Task.Factory.StartNew(new Func<object, IList<FieldDevice>>((data) => { 
                fieldDevicesDataReceivedFromMqttServer = true;
                return (IList<FieldDevice>)(data as MqttMessage).ExtractedMessage; 
            }), mqttMessage).ContinueWith(new Action<Task<IList<FieldDevice>>>(fieldDevicesCommunicator.UpdateFieldDevicesDataForMobileDevicesInitialization))
                .ContinueWith(new Action<Task>(uiCallBack), uiTaskScheduler);
        }
        #endregion

        private void OnLiveDataReceived(object sender, FieldPointDataReceivedArgs liveData)
        {
            if (mqttManager.IsConnected())
            {
                if (deviceType == DeviceType.HMI)
                {
                    /* Share live data to the Mqtt Server only if the device type is HMI */
                    mqttManager.Publish("LiveData", liveData);
                }
            }
        }

        public void SendCommandToDevice(CommandToDeviceArgs commandToDeviceArgs)
        {
            if (deviceType == DeviceType.HMI)
            {
                /* Send command to fieldDevicesCommunicator in Hardware Abstraction Layer */
                fieldDevicesCommunicator.SendCommandToDevice(commandToDeviceArgs.FieldDeviceId,
                    commandToDeviceArgs.FieldPointLabel,
                    commandToDeviceArgs.DataTypeOfCommand,
                    commandToDeviceArgs.WriteValue);
            }
            else
            {
                Task.Factory.StartNew(() => {
                    if (mqttManager.IsConnected())
                    {
                        mqttManager.Publish("CommandToDevice", commandToDeviceArgs);
                    }
                });
            }
        }

        public void SendCommandToDevice(string fieldDeviceId, string fieldPointId, string dataTypeOfCommand, string writeValue)
        {
            SendCommandToDevice(new CommandToDeviceArgs
            {
                FieldDeviceId = fieldDeviceId,
                FieldPointLabel = fieldPointId,
                DataTypeOfCommand = dataTypeOfCommand,
                WriteValue = writeValue
            });
        }

        public event EventHandler MqttServerConnectionFailed;
        public event ShutDownClientEventHandler ShutDownClient;
    }
}
