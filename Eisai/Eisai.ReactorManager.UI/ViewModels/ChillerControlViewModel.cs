using E3.AuditTrailManager.Model;
using E3.AuditTrailManager.Model.Enums;
using E3.Bpu.Services;
using E3.ReactorManager.Interfaces.HardwareAbstractionLayer;
using E3.ReactorManager.Interfaces.HardwareAbstractionLayer.Data;
using E3.UserManager.Model.Data;
using E3.UserManager.Model.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Unity;

namespace Eisai.ReactorManager.UI.ViewModels
{
    public class ChillerControlViewModel : BindableBase, INavigationAware, IRegionMemberLifetime
    {
        private readonly IRegionManager regionManager;
        private readonly List<PropertyInfo> existingProperties;
        private readonly TaskScheduler taskScheduler;
        private readonly IFieldDevicesCommunicator fieldDevicesCommunicator;
        private readonly IBusinessProcessingUnit businessProcessingUnit;
        private readonly IRoleManager roleManager;
        private readonly IAuditTrailManager auditTrailManager;

        public ChillerControlViewModel(IUnityContainer containerProvider, IRegionManager regionManager, IAuditTrailManager auditTrailManager)
        {
            this.regionManager = regionManager;
            taskScheduler = TaskScheduler.FromCurrentSynchronizationContext();
            existingProperties = new List<PropertyInfo>(GetType().GetProperties());

            #region Resolve All Dependencies
            fieldDevicesCommunicator = containerProvider.Resolve<IFieldDevicesCommunicator>();
            businessProcessingUnit = containerProvider.Resolve<IBusinessProcessingUnit>();
            roleManager = containerProvider.Resolve<IRoleManager>();
            this.auditTrailManager = auditTrailManager;
            #endregion

            fieldDevicesCommunicator.FieldPointDataReceived += OnLiveDataReceived;
            UserDetails = (User)Application.Current.Resources["LoggedInUser"];
        }

        #region Navigation Aware Handlers
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            NavigationParameters = navigationContext.Parameters;
            if (NavigationParameters.ContainsKey("DeviceId"))
            {
                CurrentFieldDeviceName = (string)NavigationParameters["DeviceId"];
            }
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }
        #endregion

        public void PageLoaded()
        {
            Task.Factory.StartNew(new Func<object, string>(GetFieldDeviceLabel), CurrentFieldDeviceName)
                .ContinueWith(new Action<Task<string>>(UpdateFieldDeviceLabel), taskScheduler);

            Task.Factory.StartNew(new Func<object, FieldDevice>(GetFieldDeviceData), CurrentFieldDeviceName)
                .ContinueWith(new Action<Task<FieldDevice>>(UpdateFieldDeviceData), taskScheduler);
        }

        #region Live Data Handlers
        private void UpdatePropertyValue(Task<LiveDataEventArgs> task)
        {
            var liveDataEventArgs = task.Result;

            if (liveDataEventArgs != null && liveDataEventArgs.PropertyInfo != null && liveDataEventArgs.LiveData != null)
            {
                liveDataEventArgs.PropertyInfo.SetValue(this, liveDataEventArgs.LiveData, null);
            }
        }

        private void OnLiveDataReceived(object sender, FieldPointDataReceivedArgs fieldPointDataChangedArgs)
        {
            if (fieldPointDataChangedArgs.FieldDeviceIdentifier == CurrentFieldDeviceName)
            {
                var liveDataEventArgs = new LiveDataEventArgs
                {
                    PropertyInfoIdentifier = fieldPointDataChangedArgs.FieldPointDescription,
                    LiveData = fieldPointDataChangedArgs.NewFieldPointData,
                };

                Task.Factory.StartNew(new Func<object, LiveDataEventArgs>(ValidateLiveDataReceived), liveDataEventArgs)
                    .ContinueWith(new Action<Task<LiveDataEventArgs>>(UpdatePropertyValue), taskScheduler);
            }
        }

        private LiveDataEventArgs ValidateLiveDataReceived(object liveData)
        {
            if (liveData != null && existingProperties != null)
            {
                var liveDataEventArgs = (LiveDataEventArgs)liveData;

                liveDataEventArgs.PropertyInfo = existingProperties.FirstOrDefault(property => property.Name == liveDataEventArgs.PropertyInfoIdentifier);

                return liveDataEventArgs;
            }

            return null;
        }
        #endregion

        #region Update Field Device label
        private void UpdateFieldDeviceLabel(Task<string> obj)
        {
            CurrentFieldDeviceGUILabel = obj.Result;
        }

        private string GetFieldDeviceLabel(object deviceId)
        {
            return fieldDevicesCommunicator.GetFieldDeviceLabel((string)deviceId);
        }
        #endregion

        #region Update Field device data initially
        private FieldDevice GetFieldDeviceData(object deviceId)
        {
            return fieldDevicesCommunicator.GetFieldDeviceData((string)deviceId);
        }

        private void UpdateFieldDeviceData(Task<FieldDevice> obj)
        {
            var fieldDeviceData = obj.Result;

            if (fieldDeviceData != null)
            {
                foreach (var sensorDataSet in fieldDeviceData.SensorsData)
                {
                    foreach (var fieldPoint in sensorDataSet.SensorsFieldPoints)
                    {
                        var liveDataEventArgs = new LiveDataEventArgs
                        {
                            PropertyInfoIdentifier = fieldPoint.Description,
                            LiveData = fieldPoint.Value,
                        };

                        Task.Factory.StartNew(new Func<object, LiveDataEventArgs>(ValidateLiveDataReceived), liveDataEventArgs)
                            .ContinueWith(new Action<Task<LiveDataEventArgs>>(UpdatePropertyValue), taskScheduler);
                    }
                }
            }
        }
        #endregion

        #region Hpp Functions
        private bool CanHppOnOff()
        {
            return UserDetails != null
                && roleManager.CanAccessModule(UserDetails.Roles, "ChillerControl");
        }
        public void HppOnOff()
        {
            bool oldHppStatus = bool.Parse(HppStatusFeedback);
            businessProcessingUnit.SendCommandToDevice(CurrentFieldDeviceName, "HppStatus", "bool", (!bool.Parse(HppStatusFeedback)).ToString());
            auditTrailManager.RecordEventAsync("Switched " + CurrentFieldDeviceGUILabel + " Huber Pump " + (oldHppStatus ? " off" : " on"), UserDetails.Name, oldHppStatus ? EventTypeEnum.SwitchedOff : EventTypeEnum.SwitchedOn);
        }
        #endregion

        #region Cpp Functions
        private bool CanCppOnOff()
        {
            return UserDetails != null
                && roleManager.CanAccessModule(UserDetails.Roles, "ChillerControl");
        }
        public void CppOnOff()
        {
            bool oldCppStatus = bool.Parse(CppStatus);
            businessProcessingUnit.SendCommandToDevice(CurrentFieldDeviceName, "CppStatus", "bool", (!bool.Parse(CppStatus)).ToString());
            auditTrailManager.RecordEventAsync("Switched " + CurrentFieldDeviceGUILabel + " Condenser Pump " + (oldCppStatus ? " off" : " on"), UserDetails.Name, oldCppStatus ? EventTypeEnum.SwitchedOff : EventTypeEnum.SwitchedOn);
        }
        #endregion

        #region CoolingSov Functions
        private bool CanCoolingSovOnOff()
        {
            return UserDetails != null
                && roleManager.CanAccessModule(UserDetails.Roles, "ChillerControl");
        }
        public void CoolingSovOnOff()
        {
            bool oldCoolingSovStatus = bool.Parse(CoolingSovStatus);
            businessProcessingUnit.SendCommandToDevice(CurrentFieldDeviceName, "CoolingSovStatus", "bool", (!bool.Parse(CoolingSovStatus)).ToString());
            auditTrailManager.RecordEventAsync("Switched " + CurrentFieldDeviceGUILabel + " Cooling Solenoid " + (oldCoolingSovStatus ? " off" : " on"), UserDetails.Name, oldCoolingSovStatus ? EventTypeEnum.SwitchedOff : EventTypeEnum.SwitchedOn);
        }
        #endregion

        #region Trane chiller Status
        public bool CanChangetraneChillerStatus()
        {
            return UserDetails != null
                && roleManager.CanAccessModule(UserDetails.Roles, "ChillerControl");
        }
        public void ChangetraneChillerStatus()
        {
            bool oldTraneChillerStatus = bool.Parse(TraneChillerStatusFeedback);
            businessProcessingUnit.SendCommandToDevice(CurrentFieldDeviceName, "TraneChillerStatus", "bool", (!bool.Parse(TraneChillerStatusFeedback)).ToString());
            auditTrailManager.RecordEventAsync("Switched TraneChiller " + (oldTraneChillerStatus ? " off" : " on"), UserDetails.Name, oldTraneChillerStatus ? EventTypeEnum.SwitchedOff : EventTypeEnum.SwitchedOn);
        }
        #endregion

        public void NavigateToOtherScreen(string screenIdentifier)
        {
            regionManager.RequestNavigate("SelectedViewPane", screenIdentifier, new NavigationParameters { { "UserDetails", UserDetails } });
        }

        #region Commands
        private ICommand _loadedCommand;
        public ICommand LoadedCommand
        {
            get => _loadedCommand ?? (_loadedCommand = new DelegateCommand(PageLoaded));
            set { _loadedCommand = value; }
        }
        private ICommand _navigateToOtherScreenCommand;
        public ICommand NavigateToOtherScreenCommand
        {
            get => _navigateToOtherScreenCommand ?? (_navigateToOtherScreenCommand = new DelegateCommand<string>(NavigateToOtherScreen));
            set { _navigateToOtherScreenCommand = value; }
        }
        private ICommand _hppOnOffCommand;
        public ICommand HppOnOffCommand
        {
            get => _hppOnOffCommand ?? (_hppOnOffCommand = new DelegateCommand(HppOnOff, CanHppOnOff)
                .ObservesProperty(() => UserDetails));
            set { _hppOnOffCommand = value; }
        }

        private ICommand _cppOnOffCommand;
        public ICommand CppOnOffCommand
        {
            get => _cppOnOffCommand ?? (_cppOnOffCommand = new DelegateCommand(CppOnOff, CanCppOnOff)
                .ObservesProperty(() => UserDetails));
            set { _cppOnOffCommand = value; }
        }

        private ICommand _coolingSovOnOffCommand;
        public ICommand CoolingSovOnOffCommand
        {
            get => _coolingSovOnOffCommand ?? (_coolingSovOnOffCommand = new DelegateCommand(CoolingSovOnOff, CanCoolingSovOnOff)
                .ObservesProperty(() => UserDetails));
            set { _coolingSovOnOffCommand = value; }
        }
        private ICommand _changeTraneChillerStatusCommand;
        public ICommand ChangeTraneChillerStatusCommand
        {
            get => _changeTraneChillerStatusCommand ?? (_changeTraneChillerStatusCommand = new DelegateCommand(ChangetraneChillerStatus, CanChangetraneChillerStatus)
                .ObservesProperty(() => UserDetails));
            set { _changeTraneChillerStatusCommand = value; }
        }
        #endregion

        #region Properties
        private NavigationParameters _navigationParameters;
        public NavigationParameters NavigationParameters
        {
            get => _navigationParameters ?? (_navigationParameters = new NavigationParameters());
            set => SetProperty(ref _navigationParameters, value);
        }

        public bool KeepAlive
        {
            get => false;
        }

        /// <summary>
        /// Details of user
        /// </summary>
        private User _userDetails;
        public User UserDetails
        {
            get => _userDetails ?? (_userDetails = new User());
            set => SetProperty(ref _userDetails, value);
        }

        /// <summary>
        /// Contains the field device(reactor) name 
        /// in which the user was in currently
        /// </summary>
        private string _currentFieldDeviceName;
        public string CurrentFieldDeviceName
        {
            get => _currentFieldDeviceName;
            set => SetProperty(ref _currentFieldDeviceName, value);
        }

        /// <summary>
        /// Contains current batch running in this field device(reactor)
        /// </summary>
        private string _currentFieldDeviceGUILabel;
        public string CurrentFieldDeviceGUILabel
        {
            get => _currentFieldDeviceGUILabel;
            set => SetProperty(ref _currentFieldDeviceGUILabel, value);
        }

        #region Trane Chiller
        private string _traneChillerStatus;
        public string TraneChillerStatus
        {
            get => _traneChillerStatus;
            set => SetProperty(ref _traneChillerStatus, value);
        }
        private string _traneChillerStatusFeedback;
        public string TraneChillerStatusFeedback
        {
            get => _traneChillerStatusFeedback;
            set => SetProperty(ref _traneChillerStatusFeedback, value);
        }
        private string _traneChillerCurrentTemperature;
        public string TraneChillerCurrentTemperature
        {
            get => _traneChillerCurrentTemperature;
            set => SetProperty(ref _traneChillerCurrentTemperature, value);
        }
        #endregion

        #region Switches
        /// <summary>
        /// Describes the status of Pressure switch
        /// </summary>
        private string _pressureSwitchStatus;
        public string PressureSwitchStatus
        {
            get => _pressureSwitchStatus;
            set => SetProperty(ref _pressureSwitchStatus, value);
        }
        /// <summary>
        /// Describes the status of Flow switch
        /// </summary>
        private string _flowSwitchStatus;
        public string FlowSwitchStatus
        {
            get => _flowSwitchStatus;
            set => SetProperty(ref _flowSwitchStatus, value);
        }
        /// <summary>
        /// Describes the status of HighLevel
        /// </summary>
        private string _highLevelSwitchStatus;
        public string HighLevelSwitchStatus
        {
            get => _highLevelSwitchStatus;
            set => SetProperty(ref _highLevelSwitchStatus, value);
        }
        /// <summary>
        /// Describes the status of LowLevel
        /// </summary>
        private string _lowLevelSwitchStatus;
        public string LowLevelSwitchStatus
        {
            get => _lowLevelSwitchStatus;
            set => SetProperty(ref _lowLevelSwitchStatus, value);
        }
        #endregion

        #region HC related properties
        /// <summary>
        /// Describes the status of HPP
        /// </summary>
        private string _hppStatus;
        public string HppStatus
        {
            get => _hppStatus;
            set => SetProperty(ref _hppStatus, value);
        }
        /// <summary>
        /// Describes the status of HppFeedback
        /// </summary>
        private string _hppStatusFeedback;
        public string HppStatusFeedback
        {
            get => _hppStatusFeedback;
            set => SetProperty(ref _hppStatusFeedback, value);
        }
        /// <summary>
        /// Describes the status of HppTrip
        /// </summary>
        private string _hppTripStatus;
        public string HppTripStatus
        {
            get => _hppTripStatus;
            set => SetProperty(ref _hppTripStatus, value);
        }
        /// <summary>
        /// Describes the HeatCoolChilledTemperature
        /// </summary>
        private string _heatCoolChilledTemperature;
        public string HeatCoolChilledTemperature
        {
            get => _heatCoolChilledTemperature;
            set => SetProperty(ref _heatCoolChilledTemperature, value);
        }
        #endregion

        #region Condenser
        /// <summary>
        /// Describes the CondenserChilledTemperature
        /// </summary>
        private string _condenserChilledTemperature;
        public string CondenserChilledTemperature
        {
            get => _condenserChilledTemperature;
            set => SetProperty(ref _condenserChilledTemperature, value);
        }
        /// <summary>
        /// Describes the status of Cpp
        /// </summary>
        private string _cppStatus;
        public string CppStatus
        {
            get => _cppStatus;
            set => SetProperty(ref _cppStatus, value);
        }
        #endregion

        /// <summary>
        /// Describes the status of Cooling Sov
        /// </summary>
        private string _coolingSovStatus;
        public string CoolingSovStatus
        {
            get => _coolingSovStatus;
            set => SetProperty(ref _coolingSovStatus, value);
        }
        #endregion

    }
}
