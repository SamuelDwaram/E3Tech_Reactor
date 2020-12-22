namespace E3.ReactorManager.MessageQueueClient.Data
{
    public class CommandToDeviceArgs
    {
        public string FieldDeviceId { get; set; }

        public string FieldPointLabel { get; set; }

        public string DataTypeOfCommand { get; set; }

        public string WriteValue { get; set; }
    }
}
