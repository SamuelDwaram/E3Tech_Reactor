using E3.ReactorManager.Interfaces.HardwareAbstractionLayer;
using E3.ReactorManager.Interfaces.HardwareAbstractionLayer.Data;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Windows.Input;

namespace E3.RemoteClient.Ui.ViewModels
{
    public class DashboardViewModel : BindableBase, IRegionMemberLifetime
    {
        private readonly IRegionManager regionManager;
        private readonly IFieldDevicesCommunicator fieldDevicesCommunicator;

        public DashboardViewModel(IRegionManager regionManager, IFieldDevicesCommunicator fieldDevicesCommunicator)
        {
            this.regionManager = regionManager;
            this.fieldDevicesCommunicator = fieldDevicesCommunicator;
            this.fieldDevicesCommunicator.FieldPointDataReceived += FieldDevicesCommunicator_FieldPointDataReceived;
        }

        private void FieldDevicesCommunicator_FieldPointDataReceived(object sender, FieldPointDataReceivedArgs liveData)
        {
            if (liveData.FieldDeviceIdentifier == "OtherEquipment" && liveData.FieldPointIdentifier.Contains("Status"))
            {
                UsedNow = new string[] { liveData.FieldDeviceIdentifier, liveData.FieldPointIdentifier, liveData.NewFieldPointData };
            }
            if (liveData.FieldPointIdentifier == "UsedNow")
            {
                UsedNow = new string[] { liveData.FieldDeviceIdentifier, liveData.FieldPointIdentifier, liveData.NewFieldPointData };
            }
        }

        public void Navigate(string deviceId)
        {
            Device device = fieldDevicesCommunicator.GetBasicDeviceInfo(deviceId);
            regionManager.RequestNavigate("SelectedViewPane", device.Type, new NavigationParameters {
                { "Device", device }
            });
        }

        #region Commands
        private ICommand _navigateCommand;
        public ICommand NavigateCommand
        {
            get => _navigateCommand ?? (_navigateCommand = new DelegateCommand<string>(Navigate));
            set => SetProperty(ref _navigateCommand, value);
        }
        #endregion

        #region Properties
        public bool KeepAlive { get; set; } = false;

        private string[] _usedNow;
        public string[] UsedNow
        {
            get { return _usedNow; }
            set { SetProperty(ref _usedNow, value); }
        }
        #endregion
    }
}
