using E3.Bpu.Models;
using E3.Bpu.Services;
using E3.ReactorManager.Interfaces.HardwareAbstractionLayer;
using E3.ReactorManager.Interfaces.HardwareAbstractionLayer.Data;
using E3.UserManager.Model.Data;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace E3.RemoteClient.Ui.ViewModels
{
    public class OtherEquipmentViewModel : BindableBase, IRegionMemberLifetime, INavigationAware
    {
        private readonly IRegionManager regionManager;
        private readonly IFieldDevicesCommunicator fieldDevicesCommunicator;
        private readonly IDevicesHandler devicesHandler;
        private readonly IBusinessProcessingUnit bpu;
        private readonly TaskScheduler taskScheduler;

        public OtherEquipmentViewModel(IRegionManager regionManager, IFieldDevicesCommunicator fieldDevicesCommunicator, IDevicesHandler devicesHandler, IBusinessProcessingUnit bpu)
        {
            taskScheduler = TaskScheduler.FromCurrentSynchronizationContext();
            this.regionManager = regionManager;
            this.fieldDevicesCommunicator = fieldDevicesCommunicator;
            this.devicesHandler = devicesHandler;
            this.bpu = bpu;
            this.fieldDevicesCommunicator.FieldPointDataReceived += FieldDevicesCommunicator_FieldPointDataReceived;
        }

        private void FieldDevicesCommunicator_FieldPointDataReceived(object sender, FieldPointDataReceivedArgs liveData)
        {
            if (liveData.FieldDeviceIdentifier == CurrentDevice.Id)
            {
                if (DeviceParametersContainer.ContainsKey(liveData.FieldPointIdentifier))
                {
                    DeviceParametersContainer[liveData.FieldPointIdentifier] = liveData.NewFieldPointData;
                    RaisePropertyChanged(nameof(DeviceParametersContainer));
                }
            }
        }

        #region Navigation Handlers
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            CurrentDevice = navigationContext.Parameters.GetValue<Device>("Device");
            CurrentFieldDeviceName = CurrentDevice.Id;
            LoadDeviceParameters();
        }

        private void LoadDeviceParameters()
        {
            if (Application.Current.Resources.Contains("LoggedInUser"))
            {
                UserDetails = (User)Application.Current.Resources["LoggedInUser"];  //Update LoggedInUser Details
                RaisePropertyChanged(nameof(UserDetails));
            }

            Task.Factory.StartNew(new Func<object, FieldDevice>((t) => fieldDevicesCommunicator.GetFieldDeviceData((string)t)), CurrentDevice.Id)
                .ContinueWith(new Func<Task<FieldDevice>, Dictionary<string, string>>((t) => {
                    return (from SensorsDataSet sensorsDataSet in t.Result.SensorsData
                            from FieldPoint fieldPoint in sensorsDataSet.SensorsFieldPoints.Where(fp => fp.RequireNotificationService == true)
                            select new KeyValuePair<string, string>(fieldPoint.Label, fieldPoint.Value))
                            .ToDictionary(item => item.Key, item => item.Value);
                })).ContinueWith((t) => DeviceParametersContainer = t.Result, taskScheduler);
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return false;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }
        #endregion

        #region Commands
        public ICommand NavigateCommand
        {
            get => new DelegateCommand<string>(pageName => regionManager.RequestNavigate("SelectedViewPane", pageName));
        }

        private bool CanSendCommandToDevice()
        {
            return devicesHandler.CurrentDevice.ModulesAccessible.Contains("OtherEquipment")
                    && UserDetails.Roles.Any(role => role.ModulesAccessable.Contains("OtherEquipment"));
        }

        public ICommand SendCommandToDevice
        {
            get => new DelegateCommand<object[]>(commandArray => {
                bpu.SendCommandToDevice(CurrentFieldDeviceName, commandArray[0].ToString(), commandArray[1].ToString(), commandArray[2].ToString());
            }, commandArray => CanSendCommandToDevice()).ObservesProperty(() => UserDetails);
        }
        #endregion

        #region Properties
        public bool KeepAlive { get => false; }

        private Device _currentDevice;
        public Device CurrentDevice
        {
            get => _currentDevice ?? (_currentDevice = new Device());
            set => SetProperty(ref _currentDevice, value);
        }

        private string _currentFieldDeviceName;
        public string CurrentFieldDeviceName
        {
            get => _currentFieldDeviceName;
            set => SetProperty(ref _currentFieldDeviceName, value);
        }

        private Dictionary<string, string> _deviceParameters;
        public Dictionary<string, string> DeviceParametersContainer
        {
            get => _deviceParameters ?? (_deviceParameters = new Dictionary<string, string>());
            set => SetProperty(ref _deviceParameters, value);
        }

        public User UserDetails { get; set; } = new User();
        public RegisteredDevice RegisteredDevice => devicesHandler.CurrentDevice;
        #endregion
    }
}
