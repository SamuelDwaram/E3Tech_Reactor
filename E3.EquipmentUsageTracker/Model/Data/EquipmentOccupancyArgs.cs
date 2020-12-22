using LiveCharts;

namespace E3.EquipmentUsageTracker.Model.Data
{
    public class EquipmentOccupancyArgs
    {
        public string EquipmentIdentifier { get; set; }

        /// <summary>
        /// Device to which Equipment is connected
        /// </summary>
        public string ConnectedDeviceId { get; set; }

        private ChartValues<double> _usedValue;
        public ChartValues<double> UsedValue
        {
            get => _usedValue ?? (_usedValue = new ChartValues<double>());
            set => _usedValue = value;
        }

        private ChartValues<double> _totalUsableValue;
        public ChartValues<double> TotalUsableValue
        {
            get => _totalUsableValue ?? (_totalUsableValue = new ChartValues<double>());
            set => _totalUsableValue = value;
        }
    }
}
