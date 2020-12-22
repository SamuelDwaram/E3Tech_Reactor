using Prism.Mvvm;
using System.Windows.Input;

namespace E3.FieldDevicesInfoPopulator.Model.Data
{
    public class FieldDeviceStatus : BindableBase
    {
        /// <summary>
        /// Device Identifier
        /// </summary>
        private string _identifier;
        public string Identifier
        {
            get => _identifier;
            set => SetProperty(ref _identifier, value);
        }

        /// <summary>
        /// Device Label
        /// </summary>
        private string _label;
        public string Label
        {
            get => _label;
            set => SetProperty(ref _label, value);
        }

        private string _deviceType;
        public string DeviceType
        {
            get => _deviceType;
            set => SetProperty(ref _deviceType, value);
        }

        /// <summary>
        /// Device Status => true or false
        /// </summary>
        private string _status;
        public string Status
        {
            get => _status;
            set => SetProperty(ref _status, value);
        }

        private bool _alarmStatus;
        public bool AlarmStatus
        {
            get => _alarmStatus;
            set => SetProperty(ref _alarmStatus, value);
        }

        private ICommand _navigateToParametersViewCommand;
        public ICommand NavigateToParametersViewCommand
        {
            get => _navigateToParametersViewCommand;
            set => SetProperty(ref _navigateToParametersViewCommand, value);
        }
    }
}
