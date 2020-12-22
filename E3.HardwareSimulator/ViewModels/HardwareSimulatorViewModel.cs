using E3.HardwareSimulator.Models;
using E3.HardwareSimulator.Services;
using E3.ReactorManager.Interfaces.DataAbstractionLayer;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace E3.HardwareSimulator.ViewModels
{
    public class HardwareSimulatorViewModel : BindableBase
    {
        private readonly IHardwareSimulator hardwareSimulator;
        private readonly IDatabaseReader databaseReader;

        public HardwareSimulatorViewModel(IHardwareSimulator hardwareSimulator, IDatabaseReader databaseReader)
        {
            this.hardwareSimulator = hardwareSimulator;
            this.databaseReader = databaseReader;
            Task.Factory.StartNew(new Func<IList<Device>>(LoadFieldDevices))
                .ContinueWith((t) => FieldDevices = t.Result);
        }

        public IList<Device> LoadFieldDevices()
        {
            return (from DataRow row in databaseReader.ExecuteReadCommand("GetAvailableFieldDevices", CommandType.StoredProcedure).AsEnumerable()
                    select new Device
                    {
                        Id = row["Identifier"].ToString(),
                        Label = row["Label"].ToString()
                    }).ToList();
        }

        public void SaveChangesInDeviceParameters()
        {
            foreach (DeviceParameter parameter in DeviceParameters)
            {
                hardwareSimulator.UpdateFieldPointData(SelectedDevice.Id, parameter.Name, parameter.Value);
            }
        }

        public void UpdateDeviceParameters(Device device)
        {
            DeviceParameters = hardwareSimulator.GetDeviceParameters(device.Id);
        }

        #region Commands
        private ICommand _updateDeviceParametersCommand;
        public ICommand UpdateDeviceParametersCommand
        {
            get => _updateDeviceParametersCommand ?? (_updateDeviceParametersCommand = new DelegateCommand<Device>(UpdateDeviceParameters));
            set => SetProperty(ref _updateDeviceParametersCommand, value);
        }

        private ICommand _saveChangesInDeviceParametersCommand;
        public ICommand SaveChangesInDeviceParametersCommand
        {
            get => _saveChangesInDeviceParametersCommand ?? (_saveChangesInDeviceParametersCommand = new DelegateCommand(SaveChangesInDeviceParameters));
            set => SetProperty(ref _saveChangesInDeviceParametersCommand, value);
        }
        #endregion

        #region Properties
        private IList<Device> _fieldDevices;
        public IList<Device> FieldDevices
        {
            get => _fieldDevices ?? (_fieldDevices = new List<Device>());
            set => SetProperty(ref _fieldDevices, value);
        }

        private Device _selectedDevice;
        public Device SelectedDevice
        {
            get => _selectedDevice ?? (_selectedDevice = new Device());
            set => SetProperty(ref _selectedDevice, value);
        }

        private IList<DeviceParameter> _deviceParameters;
        public IList<DeviceParameter> DeviceParameters
        {
            get => _deviceParameters ?? (_deviceParameters = new List<DeviceParameter>());
            set => SetProperty(ref _deviceParameters, value);
        }
        #endregion
    }
}
