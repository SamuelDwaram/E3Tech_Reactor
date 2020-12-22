using E3.MqttProvider.Models;
using System;

namespace E3.Bpu.Services
{
    public interface IBusinessProcessingUnit
    {
        event EventHandler MqttServerConnectionFailed;
        event ShutDownClientEventHandler ShutDownClient;

        void SendCommandToDevice(string fieldDeviceId, string fieldPointId, string dataTypeOfCommand, string writeValue);
        void SendCommandToDevice(CommandToDeviceArgs commandToDeviceArgs);
    }

    public delegate void ShutDownClientEventHandler(object sender, ShutDownClientEventArgs args);

    public class ShutDownClientEventArgs : EventArgs
    {
        /// <summary>
        /// Main Context why the shutdown event was raised
        /// </summary>
        public string Context { get; set; }

        /// <summary>
        /// Detailed Message why the shut down event was raised
        /// </summary>
        public string Info { get; set; }
    }
}
