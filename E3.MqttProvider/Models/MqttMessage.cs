namespace E3.MqttProvider.Models
{
    public class MqttMessage
    {
        public string Topic { get; set; }

        public string MessageType { get; set; }

        public byte[] Message { get; set; }

        public object ExtractedMessage { get; set; }
    }
}
