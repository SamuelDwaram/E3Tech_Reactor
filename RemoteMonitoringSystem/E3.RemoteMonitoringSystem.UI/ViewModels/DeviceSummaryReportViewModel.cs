using E3.ReactorManager.Interfaces.DataAbstractionLayer;
using E3.RemoteMonitoringSystem.UI.Models;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Unity;

namespace E3.RemoteMonitoringSystem.UI.ViewModels
{
    public class DeviceSummaryReportViewModel : BindableBase
    {
        private readonly IDatabaseReader databaseReader;
        private readonly DevicesReportHandler devicesReportHandler;

        public DeviceSummaryReportViewModel(IUnityContainer containerProvider, IDatabaseReader databaseReader, DevicesReportHandler devicesReportHandler)
        {
            this.databaseReader = databaseReader;
            this.devicesReportHandler = devicesReportHandler;
        }

        public void PrintReport()
        {
            Task.Factory.StartNew(() => devicesReportHandler.PrintDeviceSummaryReport(SelectedFieldDevice, AvailableFieldDevices[SelectedFieldDevice], SelectedStartDate, SelectedEndDate));
        }

        private void GetAvailableFieldDevices()
        {
            Task.Factory.StartNew(new Func<Dictionary<string, string>>(databaseReader.GetAvailableFieldDevices))
                .ContinueWith(new Action<Task<Dictionary<string, string>>>(UpdateAvailableFieldDevices));
        }

        private void UpdateAvailableFieldDevices(Task<Dictionary<string, string>> task)
        {
            if (task.IsCompleted)
            {
                AvailableFieldDevices = task.Result;
            }
        }

        #region Commands
        private ICommand _getAvailableFieldDevicesCommand;
        public ICommand GetAvailableFieldDevicesCommand
        {
            get => _getAvailableFieldDevicesCommand ?? (_getAvailableFieldDevicesCommand = new DelegateCommand(GetAvailableFieldDevices));
            set => _getAvailableFieldDevicesCommand = value;
        }

        private ICommand _printReportCommand;
        public ICommand PrintReportCommand
        {
            get => _printReportCommand ?? (_printReportCommand = new DelegateCommand(PrintReport, CanPrintReport)
                        .ObservesProperty(() => SelectedFieldDevice));
            set => SetProperty(ref _printReportCommand, value);
        }

        private bool CanPrintReport()
        {
            return !string.IsNullOrWhiteSpace(SelectedFieldDevice);
        }
        #endregion

        #region Properties
        private string _selectedFieldDevice;
        public string SelectedFieldDevice
        {
            get => _selectedFieldDevice;
            set => SetProperty(ref _selectedFieldDevice, value);
        }

        private DateTime _selectedStartDate;
        public DateTime SelectedStartDate
        {
            get => _selectedStartDate == default ? DateTime.Now : _selectedStartDate;
            set => SetProperty(ref _selectedStartDate, value);
        }

        private DateTime _selectedEndDate;
        public DateTime SelectedEndDate
        {
            get => _selectedEndDate == default ? DateTime.Now : _selectedEndDate;
            set => SetProperty(ref _selectedEndDate, value);
        }

        /// <summary>
        /// Dictionary< FieldDeviceIdentifier, FieldDeviceLabel>
        /// </summary>
        private Dictionary<string, string> _availableFieldDevices;

        public Dictionary<string, string> AvailableFieldDevices
        {
            get => _availableFieldDevices ?? (_availableFieldDevices = new Dictionary<string, string>());
            set
            {
                _availableFieldDevices = value;
                RaisePropertyChanged();
            }
        }

        #endregion
    }
}
