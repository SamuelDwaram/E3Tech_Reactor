using System;
using System.Threading.Tasks;

namespace E3.ReactorManager.BusinessProcessingUnit.Model.Interfaces
{
    /// <summary>
    /// Contact for Business Processing Unit
    /// </summary>
    public interface IBusinessProcessingUnit
    {
        event EventHandler MqttServerConnectionFailed;

        event EventHandler FieldDevicesDataReceivedFromMqttServer;

        event ShutDownApplicationEventHandler SendNotificationToShutDownApplicationEvent;

        void InitializeFieldDevices(Action<Task> callBack, TaskScheduler taskScheduler);

        bool ConfirmUpdateUIRequest();

        bool ConfirmSendDirectCommandsToPlc();

        DeviceType DeviceType { get; }

        /// <summary>
        /// When UI is Clicked
        ///     Send Command to Harware Abstraction Layer for further processing
        /// </summary>
        /// <param name="fieldDeviceIdentifier"></param>
        /// <param name="fieldPointIdentifier"></param>
        /// <param name="dataTypeOfCommand"></param>
        /// <param name="writeValue"></param>
        void SendCommandToDevice(string fieldDeviceIdentifier, string fieldPointIdentifier,string dataTypeOfCommand, string writeValue);
    }

    public delegate void ShutDownApplicationEventHandler(object sender, ShutDownApplicationEventArgs args);

    public class ShutDownApplicationEventArgs : EventArgs
    {
        /// <summary>
        /// Main Context why the shutdown event was raised
        /// </summary>
        public string Context { get; set; }

        /// <summary>
        /// Detailed Message why the shut down event was raised
        /// </summary>
        public string ShutDownDueTo { get; set; }
    }
}
