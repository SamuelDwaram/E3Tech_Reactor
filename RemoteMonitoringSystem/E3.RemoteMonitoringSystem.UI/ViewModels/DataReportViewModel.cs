using E3.ReactorManager.Interfaces.DataAbstractionLayer;
using E3.RemoteMonitoringSystem.UI.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace E3.RemoteMonitoringSystem.UI.ViewModels
{
    public class DataReportViewModel : BindableBase, IRegionMemberLifetime
    {
        private readonly IDatabaseReader databaseReader;
        private readonly DevicesReportHandler devicesReportHandler;

        public DataReportViewModel(IDatabaseReader databaseReader, DevicesReportHandler devicesReportHandler)
        {
            this.databaseReader = databaseReader;
            this.devicesReportHandler = devicesReportHandler;
        }

        private void PrintReport()
        {
            SelectedStartDate = SelectedStartDate.Date + new TimeSpan(StartHour, StartMinute, 0);
            SelectedEndDate = SelectedEndDate.Date + new TimeSpan(EndHour, EndMinute, 0);
            Task.Factory.StartNew(new Action(() => devicesReportHandler.PrintDataReport(SelectedFieldDevice, AvailableFieldDevices[SelectedFieldDevice], SelectedParameters, SelectedStartDate, SelectedEndDate)));
        }

        private void GetFieldDeviceParameters(string deviceId)
        {
            Task.Factory.StartNew(new Func<IEnumerable<DataRow>>(() => databaseReader.ExecuteReadCommand($"select DataUnit from dbo.SensorsDataSet where FieldDeviceIdentifier = '{deviceId}' and DataUnit is not null", CommandType.Text).AsEnumerable()))
                .ContinueWith(new Action<Task<IEnumerable<DataRow>>>(t => AvailableFieldDeviceParameters = (from DataRow row in t.Result
                                                                                                            select Convert.ToString(row["DataUnit"])).ToList()));
        }

        private void GetAvailableFieldDevices()
        {
            Task.Factory.StartNew(new Func<Dictionary<string, string>>(() => {
                return (from DataRow row in databaseReader.ExecuteReadCommand("GetDataRecordedDevices", CommandType.StoredProcedure).AsEnumerable()
                        select new KeyValuePair<string, string>(Convert.ToString(row["DeviceId"]), Convert.ToString(row["DeviceLabel"])
                        )).ToDictionary(keyValuePair => keyValuePair.Key, keyValuePair => keyValuePair.Value);
            })).ContinueWith(new Action<Task<Dictionary<string, string>>>(t => AvailableFieldDevices = t.Result));
        }

        public void AddToSelectedParameters(string parameter)
        {
            if (!SelectedParameters.Contains(parameter))
            {
                SelectedParameters.Add(parameter);
            }
        }

        public void RemoveFromSelectedParameters(string parameter)
        {
            if (SelectedParameters.Contains(parameter))
            {
                SelectedParameters.Remove(parameter);
            }
        }

        #region Commands
        private ICommand _addToSelectedParametersCommand;
        public ICommand AddToSelectedParametersCommand
        {
            get => _addToSelectedParametersCommand ?? (_addToSelectedParametersCommand = new DelegateCommand<string>(AddToSelectedParameters));
            set => _addToSelectedParametersCommand = value;
        }

        private ICommand _removeFromSelectedParametersCommand;
        public ICommand RemoveFromSelectedParametersCommand
        {
            get => _removeFromSelectedParametersCommand ?? (_removeFromSelectedParametersCommand = new DelegateCommand<string>(RemoveFromSelectedParameters));
            set => _removeFromSelectedParametersCommand = value;
        }

        private ICommand _getAvailableFieldDevicesCommand;
        public ICommand GetAvailableFieldDevicesCommand
        {
            get => _getAvailableFieldDevicesCommand ?? (_getAvailableFieldDevicesCommand = new DelegateCommand(GetAvailableFieldDevices));
            set => _getAvailableFieldDevicesCommand = value;
        }

        private ICommand _getFieldDeviceParametersCommand;
        public ICommand GetFieldDeviceParametersCommand
        {
            get => _getFieldDeviceParametersCommand ?? (_getFieldDeviceParametersCommand = new DelegateCommand<string>(GetFieldDeviceParameters));
            set => _getFieldDeviceParametersCommand = value;
        }

        private ICommand _printReportCommand;
        public ICommand PrintReportCommand
        {
            get => _printReportCommand ?? (_printReportCommand = new DelegateCommand(PrintReport));
            set => _printReportCommand = value;
        }
        #endregion

        #region Properties
        public bool KeepAlive
        {
            get => false;
        }

        private IList<string> _selectedParameters;
        public IList<string> SelectedParameters
        {
            get => _selectedParameters ?? (_selectedParameters = new List<string>());
            set
            {
                _selectedParameters = value;
                RaisePropertyChanged();
            }
        }

        private IList<string> _availableFieldDeviceParameters;
        public IList<string> AvailableFieldDeviceParameters
        {
            get => _availableFieldDeviceParameters ?? (_availableFieldDeviceParameters = new List<string>());
            set
            {
                _availableFieldDeviceParameters = value;
                RaisePropertyChanged();
            }
        }

        private DateTime _selectedStartDate;
        public DateTime SelectedStartDate
        {
            get => _selectedStartDate != default ? _selectedStartDate : (_selectedStartDate = DateTime.Now);
            set
            {
                _selectedStartDate = value;
                RaisePropertyChanged();
            }
        }

        private DateTime _selectedEndDate;
        public DateTime SelectedEndDate
        {
            get => _selectedEndDate != default ? _selectedEndDate : (_selectedEndDate = DateTime.Now);
            set
            {
                _selectedEndDate = value;
                RaisePropertyChanged();
            }
        }

        private int _startHour;
        public int StartHour
        {
            get => _startHour;
            set => SetProperty(ref _startHour, value);
        }

        private int _startMinute = 0;
        public int StartMinute
        {
            get => _startMinute;
            set => SetProperty(ref _startMinute, value);
        }

        private int _endHour;
        public int EndHour
        {
            get => _endHour;
            set => SetProperty(ref _endHour, value);
        }

        private int _endMinute = 0;
        public int EndMinute
        {
            get => _endMinute;
            set => SetProperty(ref _endMinute, value);
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

        private string _selectedFieldDevice;
        public string SelectedFieldDevice
        {
            get => _selectedFieldDevice;
            set
            {
                _selectedFieldDevice = value;
                RaisePropertyChanged();
            }
        }
        #endregion
    }
}

