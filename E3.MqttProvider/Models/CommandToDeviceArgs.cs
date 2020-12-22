using System;

namespace E3.MqttProvider.Models
{
    public class CommandToDeviceArgs
    {
        public string FieldDeviceId { get; set; }

        public string FieldPointLabel { get; set; }

        public string DataTypeOfCommand { get; set; }

        public string WriteValue { get; set; }
    }
}
