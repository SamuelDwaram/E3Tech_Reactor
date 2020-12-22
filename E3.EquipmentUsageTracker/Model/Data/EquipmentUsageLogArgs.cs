namespace E3.EquipmentUsageTracker.Model.Data
{
    public class EquipmentUsageLogArgs
    {
        public int SerialNumber { get; set; }

        public string DeviceId { get; set; }

        public string ProjectName { get; set; }

        public string BatchNumber { get; set; }

        public string FromDate { get; set; }

        public string ToDate { get; set; }

        public string CleaningSolvent { get; set; }

        public string CleanedBy { get; set; }
    }
}
