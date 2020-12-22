namespace E3.EquipmentUsageTracker.Model.Data
{
    public class EquipmentAndConnectedFieldDeviceArgs
    {
        public string EquipmentIdentifier { get; set; }

        /// <summary>
        /// Device to which Equipment is connected
        /// </summary>
        public string FieldDeviceConnectedTo { get; set; }

        /// <summary>
        /// Label of the Device to which Equipment is connected
        /// </summary>
        public string FieldDeviceLabel { get; set; }
    }
}
