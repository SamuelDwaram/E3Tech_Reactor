using E3.EquipmentUsageTracker.Model;
using E3.EquipmentUsageTracker.Model.Data;
using E3.EquipmentUsageTracker.Model.Enums;
using LiveCharts;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Unity;

namespace E3.EquipmentUsageTracker.ViewModels
{
    public class EquipmentOccupancyViewModel : BindableBase
    {
        IEquipmentUsageTracker equipmentUsageTracker;

        public EquipmentOccupancyViewModel(IUnityContainer containerProvider)
        {
            equipmentUsageTracker = containerProvider.Resolve<IEquipmentUsageTracker>();
            equipmentUsageTracker.UpdateEquipmentOccupancyEvent += OnUpdateEquipmentOccupancyEvent;
        }

        private void OnUpdateEquipmentOccupancyEvent(string selectedMonth, OccupancyReportTypeEnum selectedOccupancyReportType)
        {
            Task.Factory.StartNew(new Func<EquipmentOccupancyArgs>(() => {
                //update the equipment occupancy
                return equipmentUsageTracker.GetEquipmentOccupancy(DeviceId, EquipmentIdentifier, selectedMonth, selectedOccupancyReportType);
            })).ContinueWith(new Action<Task<EquipmentOccupancyArgs>>((task) => {
                UsedValue = task.Result.UsedValue;
                TotalUsableValue = task.Result.TotalUsableValue;
            }));
        }

        public void SetParameters(string equipmentId, string deviceId, string fieldDeviceLabel)
        {
            EquipmentIdentifier = equipmentId;
            DeviceId = deviceId;
            FieldDeviceLabel = fieldDeviceLabel;
        }

        public void LoadEquipmentOccupancyIntially()
        {
            Task.Factory.StartNew(new Func<EquipmentOccupancyArgs>(() => {
                //update the equipment occupancy
                return equipmentUsageTracker.GetEquipmentOccupancy(DeviceId, EquipmentIdentifier, DateTime.Now.Month.ToString(), OccupancyReportTypeEnum.Days);
            })).ContinueWith(new Action<Task<EquipmentOccupancyArgs>>((task) => {
                UsedValue = task.Result.UsedValue;
                TotalUsableValue = task.Result.TotalUsableValue;
            }));
        }

        private ICommand _loadedCommand;
        public ICommand LoadedCommand
        {
            get => _loadedCommand ?? (_loadedCommand = new DelegateCommand(LoadEquipmentOccupancyIntially));
            set => SetProperty(ref _loadedCommand, value);
        }

        #region Properties
        private string _equipmentIdentifier;
        public string EquipmentIdentifier
        {
            get => _equipmentIdentifier ?? (_equipmentIdentifier = string.Empty);
            set => SetProperty(ref _equipmentIdentifier, value);
        }

        private string _deviceId;
        public string DeviceId
        {
            get => _deviceId ?? (_deviceId = string.Empty);
            set => SetProperty(ref _deviceId, value);
        }

        public string _fieldDeviceLabel;
        public string FieldDeviceLabel
        {
            get => _fieldDeviceLabel ?? (_fieldDeviceLabel = string.Empty);
            set => SetProperty(ref _fieldDeviceLabel, value);
        }
        #endregion

        #region Pie Chart Properties
        /// <summary>
        /// Label format for the Pie Chart
        /// </summary>
        public Func<ChartPoint, string> PointLabel { get; set; } = chartPoint => string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);

        /// <summary>
        /// Usable Value of Equipment
        /// </summary>
        private ChartValues<double> _totalUsableValue;
        public ChartValues<double> TotalUsableValue
        {
            get => _totalUsableValue ?? (_totalUsableValue = new ChartValues<double> { 720 });
            set => SetProperty(ref _totalUsableValue, value);
        }

        /// <summary>
        /// Used Value of Equipment
        /// </summary>
        private ChartValues<double> _usedValue;
        public ChartValues<double> UsedValue
        {
            get => _usedValue ?? (_usedValue = new ChartValues<double> { 0 });
            set => SetProperty(ref _usedValue, value);
        }
        #endregion

    }
}
