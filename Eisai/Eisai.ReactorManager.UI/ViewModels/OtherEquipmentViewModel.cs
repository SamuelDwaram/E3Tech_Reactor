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
    public class OtherEquipmentViewModel : BindableBase, IRegionMemberLifetime, INavigationAware
    {
        private readonly IRegionManager regionManager;
        private readonly List<PropertyInfo> existingProperties;
        private readonly TaskScheduler taskScheduler;
        private readonly IFieldDevicesCommunicator fieldDevicesCommunicator;
        private readonly IBusinessProcessingUnit businessProcessingUnit;
        private readonly IRoleManager roleManager;
        private readonly IAuditTrailManager auditTrailManager;

        public OtherEquipmentViewModel(IUnityContainer containerProvider, IRegionManager regionManager, IAuditTrailManager auditTrailManager)
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

        #region Centrifuge

        #region On/Off Status Change
        private bool CanCentrifugeOnOff()
        {
            return UserDetails != null
                && roleManager.CanAccessModule(UserDetails.Roles, "OtherEquipment");
        }
        public void CentrifugeOnOff()
        {
            bool oldCentrifugeStatus = bool.Parse(CentrifugeStatusFeedback);
            businessProcessingUnit.SendCommandToDevice(CurrentFieldDeviceName, "CentrifugeStatus", "bool", (!bool.Parse(CentrifugeStatusFeedback)).ToString());
            auditTrailManager.RecordEventAsync("Switched Centrifuge " + (oldCentrifugeStatus ? " off" : " on"), UserDetails.Name, oldCentrifugeStatus ? EventTypeEnum.SwitchedOff : EventTypeEnum.SwitchedOn);
        }
        #endregion

        #region Set Point Change
        private bool CanChangeCentrifugeSpeedSetPoint()
        {
            return UserDetails != null
                && roleManager.CanAccessModule(UserDetails.Roles, "OtherEquipment");
        }
        public void ChangeCentrifugeSpeedSetPoint()
        {
            int tobeSetCentrifugeSpeedSetPoint = int.Parse(Math.Floor(Convert.ToDouble(CentrifugeSpeedSetPoint)).ToString());
            if (tobeSetCentrifugeSpeedSetPoint <= 2000 && tobeSetCentrifugeSpeedSetPoint >= 200)
            {
                businessProcessingUnit.SendCommandToDevice(CurrentFieldDeviceName, "CentrifugeSpeedSetPoint", "int", tobeSetCentrifugeSpeedSetPoint.ToString());
                auditTrailManager.RecordEventAsync("Changed Centrifuge Speed SetPoint from " + OldSetPointCentrifuge + " to " + tobeSetCentrifugeSpeedSetPoint, UserDetails.Name, EventTypeEnum.ChangedSetPoint);
            }
            else
            {
                MessageBox.Show("Please Enter Speed Set Point Between 200 and 2000");
                CentrifugeSpeedSetPoint = string.Empty;
            }
        }
        #endregion

        #region SetPoint GotFocus
        public void GotFocusCentrifuge(string oldSetPoint)
        {
            if (!string.IsNullOrEmpty(oldSetPoint))
            {
                //update old setpoint
                OldSetPointCentrifuge = oldSetPoint;
            }
        }
        #endregion

        #endregion

        #region Scrubber
        public bool CanChangeScrubberStatus()
        {
            return UserDetails != null
                && roleManager.CanAccessModule(UserDetails.Roles, "OtherEquipment");
        }
        public void ChangeScrubber_1_Status()
        {
            bool oldScrubberStatus = bool.Parse(Scrubber_1_StatusFeedback);
            businessProcessingUnit.SendCommandToDevice(CurrentFieldDeviceName, "Scrubber_1_Status", "bool", (!bool.Parse(Scrubber_1_StatusFeedback)).ToString());
            auditTrailManager.RecordEventAsync("Switched Scrubber 1 " + (oldScrubberStatus ? " off" : " on"), UserDetails.Name, oldScrubberStatus ? EventTypeEnum.SwitchedOff : EventTypeEnum.SwitchedOn);
        }
        public void ChangeScrubber_2_Status()
        {
            bool oldScrubberStatus = bool.Parse(Scrubber_2_StatusFeedback);
            businessProcessingUnit.SendCommandToDevice(CurrentFieldDeviceName, "Scrubber_2_Status", "bool", (!bool.Parse(Scrubber_2_StatusFeedback)).ToString());
            auditTrailManager.RecordEventAsync("Switched Scrubber 2 " + (oldScrubberStatus ? " off" : " on"), UserDetails.Name, oldScrubberStatus ? EventTypeEnum.SwitchedOff : EventTypeEnum.SwitchedOn);
        }
        public void ChangeScrubber_3_Status()
        {
            bool oldScrubberStatus = bool.Parse(Scrubber_3_StatusFeedback);
            businessProcessingUnit.SendCommandToDevice(CurrentFieldDeviceName, "Scrubber_3_Status", "bool", (!bool.Parse(Scrubber_3_StatusFeedback)).ToString());
            auditTrailManager.RecordEventAsync("Switched Scrubber 3 " + (oldScrubberStatus ? " off" : " on"), UserDetails.Name, oldScrubberStatus ? EventTypeEnum.SwitchedOff : EventTypeEnum.SwitchedOn);
        }
        #endregion

        #region Ahu 
        public bool CanChangeAhuStatus()
        {
            return UserDetails != null
                && roleManager.CanAccessModule(UserDetails.Roles, "OtherEquipment");
        }
        public void ChangeAhuStatus()
        {
            bool oldAhuStatus = bool.Parse(AhuStatusFeedback);
            businessProcessingUnit
                .SendCommandToDevice(CurrentFieldDeviceName, "AhuStatus", "bool", (!bool.Parse(AhuStatusFeedback)).ToString());
            auditTrailManager.RecordEventAsync("Switched Ahu " + (oldAhuStatus ? " off" : " on"), UserDetails.Name, oldAhuStatus ? EventTypeEnum.SwitchedOff : EventTypeEnum.SwitchedOn);
        }
        #endregion

        public void NavigateToOtherScreen(string screenIdentifier)
        {
            regionManager.RequestNavigate("SelectedViewPane", screenIdentifier, new NavigationParameters { { "UserDetails", UserDetails } });
        }

        public void ChangeLampStatus()
        {
            bool oldLampStatus = bool.Parse(LampStatus);
            businessProcessingUnit
                .SendCommandToDevice(CurrentFieldDeviceName, "LampStatus", "bool", (!oldLampStatus).ToString());
            auditTrailManager.RecordEventAsync("Switched Lamp " + (oldLampStatus ? " off" : " on"), UserDetails.Name, oldLampStatus ? EventTypeEnum.SwitchedOff : EventTypeEnum.SwitchedOn);
        }

        public void ChangeCameraStatus()
        {
            bool oldCameraStatus = bool.Parse(CameraStatus);
            businessProcessingUnit
                .SendCommandToDevice(CurrentFieldDeviceName, "CameraStatus", "bool", (!oldCameraStatus).ToString());
            auditTrailManager.RecordEventAsync("Switched Camera " + (oldCameraStatus ? " off" : " on"), UserDetails.Name, oldCameraStatus ? EventTypeEnum.SwitchedOff : EventTypeEnum.SwitchedOn);
        }

        #region Commands
        private ICommand _loadedCommand;
        public ICommand LoadedCommand
        {
            get => _loadedCommand ?? (_loadedCommand = new DelegateCommand(PageLoaded));
            set { _loadedCommand = value; }
        }
        private ICommand _gotFocusCommandCentrifuge;
        public ICommand GotFocusCommandCentrifuge
        {
            get => _gotFocusCommandCentrifuge ?? (_gotFocusCommandCentrifuge = new DelegateCommand<string>(GotFocusCentrifuge));
            set { _gotFocusCommandCentrifuge = value; }
        }
        private ICommand _centrifugeOnOffCommand;
        public ICommand CentrifugeOnOffCommand
        {
            get => _centrifugeOnOffCommand 
                ?? (_centrifugeOnOffCommand = new DelegateCommand(CentrifugeOnOff, CanCentrifugeOnOff)
                .ObservesProperty(() => UserDetails));
            set { _centrifugeOnOffCommand = value; }
        }
        private ICommand _changeCentrifugeSpeedSetPointCommand;
        public ICommand ChangeCentrifugeSpeedSetPointCommand
        {
            get => _changeCentrifugeSpeedSetPointCommand 
                ?? (_changeCentrifugeSpeedSetPointCommand = new DelegateCommand(ChangeCentrifugeSpeedSetPoint, CanChangeCentrifugeSpeedSetPoint)
                .ObservesProperty(() => UserDetails));
            set { _changeCentrifugeSpeedSetPointCommand = value; }
        }
        private ICommand _navigateToOtherScreenCommand;
        public ICommand NavigateToOtherScreenCommand
        {
            get => _navigateToOtherScreenCommand 
                ?? (_navigateToOtherScreenCommand = new DelegateCommand<string>(NavigateToOtherScreen)
                .ObservesProperty(() => UserDetails));
            set { _navigateToOtherScreenCommand = value; }
        }
        private ICommand _changeScrubber_1_StatusCommand;
        public ICommand ChangeScrubber_1_StatusCommand
        {
            get => _changeScrubber_1_StatusCommand 
                ?? (_changeScrubber_1_StatusCommand = new DelegateCommand(ChangeScrubber_1_Status, CanChangeScrubberStatus)
                .ObservesProperty(() => UserDetails));
            set { _changeScrubber_1_StatusCommand = value; }
        }
        private ICommand _changeScrubber_2_StatusCommand;
        public ICommand ChangeScrubber_2_StatusCommand
        {
            get => _changeScrubber_2_StatusCommand 
                ?? (_changeScrubber_2_StatusCommand = new DelegateCommand(ChangeScrubber_2_Status, CanChangeScrubberStatus)
                .ObservesProperty(() => UserDetails));
            set { _changeScrubber_2_StatusCommand = value; }
        }
        private ICommand _changeScrubber_3_StatusCommand;
        public ICommand ChangeScrubber_3_StatusCommand
        {
            get => _changeScrubber_3_StatusCommand 
                ?? (_changeScrubber_3_StatusCommand = new DelegateCommand(ChangeScrubber_3_Status, CanChangeScrubberStatus)
                .ObservesProperty(() => UserDetails));
            set { _changeScrubber_3_StatusCommand = value; }
        }
        private ICommand _changeAhuStatusCommand;
        public ICommand ChangeAhuStatusCommand
        {
            get => _changeAhuStatusCommand 
                ?? (_changeAhuStatusCommand = new DelegateCommand(ChangeAhuStatus, CanChangeAhuStatus)
                .ObservesProperty(() => UserDetails));
            set { _changeAhuStatusCommand = value; }
        }

        private ICommand _changeCameraStatusCommand;
        public ICommand ChangeCameraStatusCommand
        {
            get => _changeCameraStatusCommand ?? (_changeCameraStatusCommand = new DelegateCommand(ChangeCameraStatus));
            set => SetProperty(ref _changeCameraStatusCommand, value);
        }

        private ICommand _changeLampStatusCommand;
        public ICommand ChangeLampStatusCommand
        {
            get => _changeLampStatusCommand ?? (_changeLampStatusCommand = new DelegateCommand(ChangeLampStatus));
            set => SetProperty(ref _changeLampStatusCommand, value);
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

        #region Ahu

        private string _ahuStatus;
        public string AhuStatus
        {
            get { return _ahuStatus; }
            set
            {
                _ahuStatus = value;
                RaisePropertyChanged();
            }
        }
        private string _ahuStatusFeedback;
        public string AhuStatusFeedback
        {
            get { return _ahuStatusFeedback; }
            set
            {
                _ahuStatusFeedback = value;
                RaisePropertyChanged();
            }
        }
        private string _ahuTripStatusFeedback;
        public string AhuTripStatusFeedback
        {
            get { return _ahuTripStatusFeedback; }
            set
            {
                _ahuTripStatusFeedback = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region Scrubber 1

        private string _scrubber_1_Status;
        public string Scrubber_1_Status
        {
            get { return _scrubber_1_Status; }
            set
            {
                _scrubber_1_Status = value;
                RaisePropertyChanged();
            }
        }
        private string _scrubber_1_StatusFeedback;
        public string Scrubber_1_StatusFeedback
        {
            get { return _scrubber_1_StatusFeedback; }
            set
            {
                _scrubber_1_StatusFeedback = value;
                RaisePropertyChanged();
            }
        }
        private string _scrubber_1_TripStatusFeedback;
        public string Scrubber_1_TripStatusFeedback
        {
            get { return _scrubber_1_TripStatusFeedback; }
            set
            {
                _scrubber_1_TripStatusFeedback = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region Scrubber 2

        private string _scrubber_2_Status;
        public string Scrubber_2_Status
        {
            get { return _scrubber_2_Status; }
            set
            {
                _scrubber_2_Status = value;
                RaisePropertyChanged();
            }
        }
        private string _scrubber_2_StatusFeedback;
        public string Scrubber_2_StatusFeedback
        {
            get { return _scrubber_2_StatusFeedback; }
            set
            {
                _scrubber_2_StatusFeedback = value;
                RaisePropertyChanged();
            }
        }
        private string _scrubber_2_TripStatusFeedback;
        public string Scrubber_2_TripStatusFeedback
        {
            get { return _scrubber_2_TripStatusFeedback; }
            set
            {
                _scrubber_2_TripStatusFeedback = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region Scrubber 3

        private string _scrubber_3_Status;
        public string Scrubber_3_Status
        {
            get { return _scrubber_3_Status; }
            set
            {
                _scrubber_3_Status = value;
                RaisePropertyChanged();
            }
        }
        private string _scrubber_3_StatusFeedback;
        public string Scrubber_3_StatusFeedback
        {
            get { return _scrubber_3_StatusFeedback; }
            set
            {
                _scrubber_3_StatusFeedback = value;
                RaisePropertyChanged();
            }
        }
        private string _scrubber_3_TripStatusFeedback;
        public string Scrubber_3_TripStatusFeedback
        {
            get { return _scrubber_3_TripStatusFeedback; }
            set
            {
                _scrubber_3_TripStatusFeedback = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region Anfd

        private string _anfdStatus;
        public string AnfdStatus
        {
            get { return _anfdStatus; }
            set
            {
                _anfdStatus = value;
                RaisePropertyChanged();
            }
        }

        private string _anfdCurrentTemperature;
        public string AnfdCurrentTemperature
        {
            get { return _anfdCurrentTemperature; }
            set
            {
                _anfdCurrentTemperature = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region Rcvd

        private string _rcvdStatus;
        public string RcvdStatus
        {
            get { return _rcvdStatus; }
            set
            {
                _rcvdStatus = value;
                RaisePropertyChanged();
            }
        }

        private string _rcvdCurrentSpeed;
        public string RcvdCurrentSpeed
        {
            get { return _rcvdCurrentSpeed; }
            set
            {
                _rcvdCurrentSpeed = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        private string _cameraStatus;
        public string CameraStatus
        {
            get => _cameraStatus ?? (_cameraStatus = bool.FalseString);
            set => SetProperty(ref _cameraStatus, value);
        }

        private string _lampStatus;
        public string LampStatus
        {
            get => _lampStatus ?? (_lampStatus = bool.FalseString);
            set => SetProperty(ref _lampStatus, value);
        }

        private string _rotavapourStatus;
        public string RotavapourStatus
        {
            get { return _rotavapourStatus; }
            set
            {
                _rotavapourStatus = value;
                RaisePropertyChanged();
            }
        }

        private string _pinmillStatus;
        public string PinmillStatus
        {
            get { return _pinmillStatus; }
            set
            {
                _pinmillStatus = value;
                RaisePropertyChanged();
            }
        }
        private string _jetmillStatus;
        public string JetmillStatus
        {
            get { return _jetmillStatus; }
            set
            {
                _jetmillStatus = value;
                RaisePropertyChanged();
            }
        }

        #region Centrifuge

        private string _centrifugeStatus;
        public string CentrifugeStatus
        {
            get { return _centrifugeStatus; }
            set
            {
                _centrifugeStatus = value;
            }
        }
        private string _centrifugeSpeedSetPoint;
        public string CentrifugeSpeedSetPoint
        {
            get { return _centrifugeSpeedSetPoint; }
            set
            {
                _centrifugeSpeedSetPoint = value;
                RaisePropertyChanged();
            }
        }
        private string _oldSetPointCentrifuge;
        public string OldSetPointCentrifuge
        {
            get { return _oldSetPointCentrifuge; }
            set
            {
                _oldSetPointCentrifuge = value;
                RaisePropertyChanged();
            }
        }

        private string _centrifugeStatusFeedback;
        public string CentrifugeStatusFeedback
        {
            get { return _centrifugeStatusFeedback; }
            set
            {
                _centrifugeStatusFeedback = value;
                RaisePropertyChanged();
            }
        }
        private string _centrifugeCurrentSpeed;
        public string CentrifugeCurrentSpeed
        {
            get { return _centrifugeCurrentSpeed; }
            set
            {
                _centrifugeCurrentSpeed = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #endregion

    }
}
