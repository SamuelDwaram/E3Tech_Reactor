using E3.AuditTrailManager.Model;
using E3.AuditTrailManager.Model.Enums;
using E3.EquipmentUsageTracker.Model;
using E3.EquipmentUsageTracker.Model.Data;
using E3.Mediator.Services;
using E3.Mediator.Models;
using E3.Bpu.Services;
using E3.ReactorManager.DesignExperiment.Model;
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
using System.Data;
using E3.ReactorManager.Interfaces.DataAbstractionLayer.Data;
using E3.ReactorManager.Interfaces.DataAbstractionLayer;
using E3.ReactorManager.DesignExperiment.Model.Data;

namespace Eisai.ReactorManager.UI.ViewModels
{
    public class ReactorControlViewModel : BindableBase, INavigationAware, IRegionMemberLifetime
    {
        private readonly MediatorService mediatorService;
        private readonly IRegionManager regionManager;
        private readonly IDatabaseReader databaseReader;
        private readonly List<PropertyInfo> existingProperties;
        private readonly TaskScheduler taskScheduler;
        private readonly IEquipmentUsageTracker equipmentUsageTracker;
        private readonly IFieldDevicesCommunicator fieldDevicesCommunicator;
        private readonly IBusinessProcessingUnit businessProcessingUnit;
        private readonly IExperimentInfoProvider experimentInfoProvider;
        private readonly IRoleManager roleManager;
        private readonly IAuditTrailManager auditTrailManager;

        public ReactorControlViewModel(IUnityContainer containerProvider, MediatorService mediatorService, IRegionManager regionManager, IDatabaseReader databaseReader, IAuditTrailManager auditTrailManager)
        {
            this.databaseReader = databaseReader;
            this.mediatorService = mediatorService;
            this.regionManager = regionManager;
            taskScheduler = TaskScheduler.FromCurrentSynchronizationContext();
            existingProperties = new List<PropertyInfo>(GetType().GetProperties());

            #region Resolve All Dependencies
            equipmentUsageTracker = containerProvider.Resolve<IEquipmentUsageTracker>();
            fieldDevicesCommunicator = containerProvider.Resolve<IFieldDevicesCommunicator>();
            businessProcessingUnit = containerProvider.Resolve<IBusinessProcessingUnit>();
            experimentInfoProvider = containerProvider.Resolve<IExperimentInfoProvider>();
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

            //update device related info
            Task.Factory.StartNew(new Action(UpdateDeviceRelatedInfo));
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }
        #endregion

        public void UpdateDeviceRelatedInfo()
        {
            //Update dosing pump usage && connected equipments using batch info
            Task.Factory.StartNew(new Func<string>(() =>
            {
                Batch batchInfo = experimentInfoProvider.GetBatchRunningInDevice(CurrentFieldDeviceName);
                if (batchInfo == null)
                {
                    return bool.TrueString;
                }
                else
                {
                    // Update the connected equipments if the batch is running
                    // according to batch info
                    ConnectedHc = batchInfo.HCIdentifier;
                    ConnectedStirrer = batchInfo.StirrerIdentifier;

                    return batchInfo.DosingPumpUsage;
                }
            })).ContinueWith(t => DosingPumpUsage = Convert.ToBoolean(t.Result), taskScheduler)
            .ContinueWith(t => {
                // Check if ConnectedHc or ConnectedStirrer is empty 
                // and if empty then read the connected equipments data from Database
                if (string.IsNullOrWhiteSpace(ConnectedHc) || string.IsNullOrWhiteSpace(ConnectedStirrer))
                {
                    //Fetch the connected equipments task from database
                    Task.Factory.StartNew(new Func<object, DataTable>(GetConnectedEquipments), CurrentFieldDeviceName)
                        .ContinueWith(new Action<Task<DataTable>>((t) => {
                            if (t.IsCompleted)
                            {
                                foreach (DataRow row in t.Result.AsEnumerable())
                                {
                                    if (row["EquipmentType"].ToString() == "Stirrer")
                                    {
                                        ConnectedStirrer = row["EquipmentModel"].ToString();
                                    }

                                    if (row["EquipmentType"].ToString() == "HC")
                                    {
                                        ConnectedHc = row["EquipmentName"].ToString();
                                    }
                                }
                            }
                        }));
                }
            });
        }

        private DataTable GetConnectedEquipments(object deviceId)
        {
            IList<DbParameterInfo> parameters = new List<DbParameterInfo>
            {
                new DbParameterInfo("@FieldDeviceIdentifier", (string)deviceId, DbType.String)
            };
            return databaseReader.ExecuteReadCommand("GetAllConnectedEquipments", CommandType.StoredProcedure, parameters);
        }

        public void PageLoaded()
        {
            Task.Factory.StartNew(new Func<object, string>(GetFieldDeviceLabel), CurrentFieldDeviceName)
                .ContinueWith(new Action<Task<string>>(UpdateFieldDeviceLabel), taskScheduler);

            Task.Factory.StartNew(new Func<object, FieldDevice>(GetFieldDeviceData), CurrentFieldDeviceName)
                .ContinueWith(new Action<Task<FieldDevice>>(UpdateFieldDeviceData), taskScheduler);

            Task.Factory.StartNew(new Func<object, IList<EquipmentUsageLogArgs>>(GetEquipmentUsageLogArgsData), CurrentFieldDeviceName)
                .ContinueWith(new Action<Task<IList<EquipmentUsageLogArgs>>>(UpdateEquipmentUsageLogArgsData));
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
            mediatorService.NotifyColleagues(InMemoryMediatorMessageContainer.UpdateSelectedDeviceId, new Device { 
                Id = CurrentFieldDeviceName,
                Label = CurrentFieldDeviceGUILabel
            });
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

        #region Equipment Usage Log Args Updaters
        private void UpdateEquipmentUsageLogArgsData(Task<IList<EquipmentUsageLogArgs>> task)
        {
            if (task.IsCompleted)
            {
                EquipmentUsageLogArgs = task.Result;
            }
            else
            {
                if (task.IsFaulted)
                {
                    // display error to user
                }
            }
        }

        private IList<EquipmentUsageLogArgs> GetEquipmentUsageLogArgsData(object deviceId)
        {
            return equipmentUsageTracker.GetEquipmentUsageLog(deviceId as string);
        }
        #endregion

        #region Heating Cooling Functions

        #region On/Off Status Change
        private bool CanChangeHeatCoolStatus()
        {
            return UserDetails != null
                && roleManager.CanAccessModule(UserDetails.Roles, "ReactorControl");
        }
        public void ChangeHeatCoolStatus()
        {
            bool oldHcStatus = bool.Parse(HeatCoolStatusFeedback);
            businessProcessingUnit
                    .SendCommandToDevice(CurrentFieldDeviceName, "HeatCoolStatus", "bool", (!bool.Parse(HeatCoolStatusFeedback)).ToString());
            auditTrailManager.RecordEventAsync("Switched "+ CurrentFieldDeviceGUILabel +" HC " + (oldHcStatus ? " off" : " on"), UserDetails.Name, oldHcStatus ? EventTypeEnum.SwitchedOff : EventTypeEnum.SwitchedOn);
        }
        #endregion

        #region Set Point Change
        private bool CanSetHCSetPoint()
        {
            return UserDetails != null
                && roleManager.CanAccessModule(UserDetails.Roles, "ReactorControl");
        }
        public void SetHCSetPoint()
        {
            var newHCSetPoint = int.Parse(Math.Floor(Convert.ToDouble(HeatCoolSetPoint)).ToString());
            if (newHCSetPoint >= -90 && newHCSetPoint <= 200)
            {
                businessProcessingUnit
                        .SendCommandToDevice(CurrentFieldDeviceName, "HeatCoolSetPoint", "int", newHCSetPoint.ToString());
                auditTrailManager.RecordEventAsync("Changed "+ CurrentFieldDeviceGUILabel +" HC SetPoint from " + OldHcSetPoint + " to " + newHCSetPoint, UserDetails.Name, EventTypeEnum.ChangedSetPoint);
            }
            else
            {
                MessageBox.Show("Please Enter HC SetPoint between -90 and 200",
                                "HC SetPoint Exceeded Limit Error",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
        }
        #endregion

        #region SetPoint Got Focus
        public void GotFocusHc(string oldSetPoint)
        {
            if (!string.IsNullOrEmpty(oldSetPoint))
            {
                //update OLd SetPoint
                OldHcSetPoint = oldSetPoint;
            }
        }
        #endregion

        #endregion

        #region Stirrer Functions

        #region On/Off Status Change
        private bool CanStirrerOnOff()
        {
            return UserDetails != null
                && roleManager.CanAccessModule(UserDetails.Roles, "ReactorControl");
        }
        public void StirrerOnOff()
        {
            bool oldStirrerStatus = bool.Parse(StirrerStatusFeedback);
            businessProcessingUnit
                    .SendCommandToDevice(CurrentFieldDeviceName, "StirrerStatus", "bool", (!bool.Parse(StirrerStatusFeedback)).ToString());
            auditTrailManager.RecordEventAsync("Switched " + CurrentFieldDeviceGUILabel + " Stirrer " + (oldStirrerStatus ? " off" : " on"), UserDetails.Name, oldStirrerStatus ? EventTypeEnum.SwitchedOff : EventTypeEnum.SwitchedOn);
        }
        #endregion

        #region Set Point Change
        public bool CanSetStirrerSpeedSetPoint()
        {
            return UserDetails != null
                && roleManager.CanAccessModule(UserDetails.Roles, "ReactorControl");
        }

        /// <summary>
        /// Executes when user enters stirrerspeed set point
        ///  and presses Enter
        ///  to set Stirrer speed set point
        /// </summary>
        public void SetStirrerSpeedSetPoint()
        {
            //first convert the user entered stirrer Speed SetPoint to integer
            var toBeSetStirrerSpeedSetPoint = Convert.ToInt16(StirrerSpeedSetPoint);

            if (toBeSetStirrerSpeedSetPoint <= 200 && toBeSetStirrerSpeedSetPoint >= 0)
            {
                businessProcessingUnit
                        .SendCommandToDevice(CurrentFieldDeviceName, "StirrerSpeedSetPoint", "int", toBeSetStirrerSpeedSetPoint.ToString());
                auditTrailManager.RecordEventAsync("Changed " + CurrentFieldDeviceGUILabel + " Stirrer Speed SetPoint from " + OldStirrerSetPoint + " to " + toBeSetStirrerSpeedSetPoint, UserDetails.Name, EventTypeEnum.ChangedSetPoint);
            }
            else if (toBeSetStirrerSpeedSetPoint > 200)
            {
                MessageBox.Show("Maximum Stirrer Speed is less than 200 rpm");
                StirrerSpeedSetPoint = null;
            }
            else if (toBeSetStirrerSpeedSetPoint < 0)
            {
                MessageBox.Show("Minimum Stirrer Speed is 0 rpm");
                StirrerSpeedSetPoint = null;
            }
        }
        #endregion

        #region SetPoint Got Focus
        public void GotFocusStirrer(string oldSetPoint)
        {
            if (!string.IsNullOrEmpty(oldSetPoint))
            {
                //update OLd SetPoint
                OldStirrerSetPoint = oldSetPoint;
            }
        }
        #endregion

        #endregion

        #region Dosing Pump Functions

        #region On/Off Status Change
        private bool CanChangeDosingPumpStatus()
        {
            return UserDetails != null
                && roleManager.CanAccessModule(UserDetails.Roles, "ReactorControl");
        }
        public void ChangeDosingPumpStatus()
        {
            bool oldDosingPumpStatus = bool.Parse(DosingPumpStatusFeedback);
            businessProcessingUnit
                    .SendCommandToDevice(CurrentFieldDeviceName, "DosingPumpStatus", "bool", (!bool.Parse(DosingPumpStatusFeedback)).ToString());
            auditTrailManager.RecordEventAsync("Switched " + CurrentFieldDeviceGUILabel + " Dosing Pump " + (oldDosingPumpStatus ? " off" : " on"), UserDetails.Name, oldDosingPumpStatus ? EventTypeEnum.SwitchedOff : EventTypeEnum.SwitchedOn);
        }
        #endregion

        #region Set Point Change
        private bool CanSetDosingPumpFlow()
        {
            return UserDetails != null
                && roleManager.CanAccessModule(UserDetails.Roles, "ReactorControl");
        }
        public void ChangeDosingPumpSetPoint()
        {
            var toBeSetDosingFlow = float.Parse(DosingPumpFlowSetPoint);
            if (toBeSetDosingFlow <= 2.7 && toBeSetDosingFlow >= 0)
            {
                businessProcessingUnit
                        .SendCommandToDevice(CurrentFieldDeviceName, "DosingPumpFlowSetPoint", "float", toBeSetDosingFlow.ToString());
                auditTrailManager.RecordEventAsync("Changed " + CurrentFieldDeviceGUILabel + " Dosing Pump Flow SetPoint from " + OldDosingPumpFlowSetPoint + " to " + toBeSetDosingFlow, UserDetails.Name, EventTypeEnum.ChangedSetPoint);
            }
            else if (toBeSetDosingFlow > 2.7)
            {
                MessageBox.Show("Maximum Dosing Flow is 2.7 lpm");
                DosingPumpFlowSetPoint = null;
            }
            else if (toBeSetDosingFlow < 0)
            {
                MessageBox.Show("Minimum Dosing Flow is 0 lpm");
                DosingPumpFlowSetPoint = null;
            }
        }
        #endregion

        #region SetPoint Got Focus
        public void GotFocusDosingPump(string oldSetPoint)
        {
            if (!string.IsNullOrEmpty(oldSetPoint))
            {
                //update OLd SetPoint
                OldDosingPumpFlowSetPoint = oldSetPoint;
            }
        }
        #endregion

        #endregion

        #region Emergency Functions

        #region On/Off Status Change
        private bool CanChangeEmergencyStatus()
        {
            return UserDetails != null
                && roleManager.CanAccessModule(UserDetails.Roles, "ReactorControl");
        }
        public void ChangeEmergencyStatus()
        {
            bool oldEmergencyStatus = bool.Parse(EmergencyStatus);
            businessProcessingUnit
                .SendCommandToDevice(CurrentFieldDeviceName, "EmergencyStatus", "bool", (!bool.Parse(EmergencyStatus)).ToString());
            auditTrailManager.RecordEventAsync("Switched " + CurrentFieldDeviceGUILabel + " Emergency " + (oldEmergencyStatus ? " off" : " on"), UserDetails.Name, oldEmergencyStatus ? EventTypeEnum.SwitchedOff : EventTypeEnum.SwitchedOn);
        }
        #endregion

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
        /// <summary>
        /// Command (Binded to HeatCoolSystem) in GUI
        /// </summary>
        private ICommand _hCOnOffCommand;
        public ICommand HCOnOffCommand
        {
            get => _hCOnOffCommand 
                ?? (_hCOnOffCommand = new DelegateCommand(ChangeHeatCoolStatus, CanChangeHeatCoolStatus)
                .ObservesProperty(() => UserDetails));
            set { _hCOnOffCommand = value; }
        }

        /// <summary>
        /// command binded to Reactor Sv textbox in GUI
        /// </summary>
        private ICommand _changeHeatCoolSetPointCommand;
        public ICommand ChangeHeatCoolSetPointCommand
        {
            get => _changeHeatCoolSetPointCommand 
                ?? (_changeHeatCoolSetPointCommand = new DelegateCommand(SetHCSetPoint, CanSetHCSetPoint)
                .ObservesProperty(() => UserDetails));
            set { _changeHeatCoolSetPointCommand = value; }
        }

        /// <summary>
        /// Command(Binded to Stirrer Button) in GUI
        /// </summary>
        private ICommand _stirrerOnOffCommand;
        public ICommand StirrerOnOffCommand
        {
            get => _stirrerOnOffCommand 
                ?? (_stirrerOnOffCommand = new DelegateCommand(StirrerOnOff, CanStirrerOnOff)
                .ObservesProperty(() => UserDetails));
            set { _stirrerOnOffCommand = value; }
        }

        /// <summary>
        /// Command (Binded to The SetStirrerSpeedTextBox) in GUI
        /// </summary>
        private ICommand _changeStirrerSetPointCommand;
        public ICommand ChangeStirrerSetPointCommand
        {
            get => _changeStirrerSetPointCommand 
                ?? (_changeStirrerSetPointCommand = new DelegateCommand(SetStirrerSpeedSetPoint, CanSetStirrerSpeedSetPoint)
                .ObservesProperty(() => UserDetails));
            set { _changeStirrerSetPointCommand = value; }
        }
        /// <summary>
        /// Command (Binded to Emergency Button) in GUI
        /// </summary>
        private ICommand _emergencyCommand;
        public ICommand EmergencyCommand
        {
            get => _emergencyCommand
                ?? (_emergencyCommand = new DelegateCommand(ChangeEmergencyStatus, CanChangeEmergencyStatus)
                .ObservesProperty(() => UserDetails));
            set { _emergencyCommand = value; }
        }

        /// <summary>
        /// command binded to dosing pump button in gui
        /// </summary>
        private ICommand _dosingPumpOnOffCommand;
        public ICommand DosingPumpOnOffCommand
        {
            get => _dosingPumpOnOffCommand
                ?? (_dosingPumpOnOffCommand = new DelegateCommand(ChangeDosingPumpStatus, CanChangeDosingPumpStatus)
                .ObservesProperty(() => UserDetails));
            set { _dosingPumpOnOffCommand = value; }
        }

        /// <summary>
        /// command binded to dosing flow textbox in GUI
        /// </summary>
        private ICommand _changeDosingPumpFlowSetPointCommand;
        public ICommand ChangeDosingPumpFlowSetPointCommand
        {
            get => _changeDosingPumpFlowSetPointCommand 
                ?? (_changeDosingPumpFlowSetPointCommand = new DelegateCommand(ChangeDosingPumpSetPoint, CanSetDosingPumpFlow)
                .ObservesProperty(() => UserDetails));
            set { _changeDosingPumpFlowSetPointCommand = value; }
        }
        
        /// <summary>
        /// Command binded to back button in gui
        /// </summary>
        private ICommand _navigateToOtherScreenCommand;
        public ICommand NavigateToOtherScreenCommand
        {
            get => _navigateToOtherScreenCommand ?? (_navigateToOtherScreenCommand = new DelegateCommand<string>(NavigateToOtherScreen));
            set { _navigateToOtherScreenCommand = value; }
        }

        private ICommand _gotFocusCommandHC;
        public ICommand GotFocusCommandHc
        {
            get => _gotFocusCommandHC ?? (_gotFocusCommandHC = new DelegateCommand<string>(GotFocusHc));
            set { _gotFocusCommandHC = value; }
        }

        private ICommand _gotFocusCommandStirrer;
        public ICommand GotFocusCommandStirrer
        {
            get => _gotFocusCommandStirrer ?? (_gotFocusCommandStirrer = new DelegateCommand<string>(GotFocusStirrer));
            set { _gotFocusCommandStirrer = value; }
        }

        private ICommand _gotFocusCommandDosingPump;
        public ICommand GotFocusCommandDosingPump
        {
            get => _gotFocusCommandDosingPump ?? (_gotFocusCommandDosingPump = new DelegateCommand<string>(GotFocusDosingPump));
            set { _gotFocusCommandDosingPump = value; }
        }
        #endregion

        #region Properties
        private string _connectedStirrer = string.Empty;
        public string ConnectedStirrer
        {
            get { return _connectedStirrer; }
            set { SetProperty(ref _connectedStirrer, value); }
        }

        private string _connectedHc = string.Empty;    
        public string ConnectedHc
        {
            get { return _connectedHc; }
            set { SetProperty(ref _connectedHc, value); }
        }

        private bool _dosingPumpUsage;
        public bool DosingPumpUsage
        {
            get { return _dosingPumpUsage; }
            set { SetProperty(ref _dosingPumpUsage, value); }
        }
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
        /// <summary>
        /// Value of Conductivity Meter
        /// </summary>
        private string _conductivityMeterValue;
        public string ConductivityMeterValue
        {
            get => _conductivityMeterValue;
            set => SetProperty(ref _conductivityMeterValue, value);
        }
        /// <summary>
        /// Value of Turbidity Meter
        /// </summary>
        private string _turbidityMeterValue;
        public string TurbidityMeterValue
        {
            get => _turbidityMeterValue;
            set => SetProperty(ref _turbidityMeterValue, value);
        }

        private IList<EquipmentUsageLogArgs> _equipmentUsageLogArgs;
        public IList<EquipmentUsageLogArgs> EquipmentUsageLogArgs
        {
            get => _equipmentUsageLogArgs ?? (_equipmentUsageLogArgs = new List<EquipmentUsageLogArgs>());
            set => SetProperty(ref _equipmentUsageLogArgs, value);
        }

        #region Terminate Batch Properties

        private string _username;
        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        private string _password;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        private string _cleaningSolvent;
        public string CleaningSolvent
        {
            get => _cleaningSolvent;
            set => SetProperty(ref _cleaningSolvent, value);
        }

        #endregion

        #region Heat Cool Properties
        private string _heatCoolStatus;
        public string HeatCoolStatus
        {
            get => _heatCoolStatus;
            set => SetProperty(ref _heatCoolStatus, value);
        }
        private string _heatCoolSetPoint;
        public string HeatCoolSetPoint
        {
            get => _heatCoolSetPoint;
            set => SetProperty(ref _heatCoolSetPoint, value);
        }
        private string _heatCoolStatusFeedback;
        public string HeatCoolStatusFeedback
        {
            get => _heatCoolStatusFeedback;
            set => SetProperty(ref _heatCoolStatusFeedback, value);
        }
        private string _heatCoolTemperatureControl;
        public string HeatCoolTemperatureControl
        {
            get => _heatCoolTemperatureControl;
            set => SetProperty(ref _heatCoolTemperatureControl, value);
        }
        private string _heatCoolFailure;
        public string HeatCoolFailure
        {
            get => _heatCoolFailure;
            set => SetProperty(ref _heatCoolFailure, value);
        }
        /// <summary>
        /// Set Point previously given to HC 
        /// </summary>
        private string _oldHcSetPoint;
        public string OldHcSetPoint
        {
            get => _oldHcSetPoint;
            set => SetProperty(ref _oldHcSetPoint, value);
        }
        #endregion

        #region Stirrer Properties
        private string _stirrerStatus;
        public string StirrerStatus
        {
            get => _stirrerStatus;
            set => SetProperty(ref _stirrerStatus, value);
        }
        private string _stirrerStatusFeedback;
        public string StirrerStatusFeedback
        {
            get => _stirrerStatusFeedback;
            set => SetProperty(ref _stirrerStatusFeedback, value);
        }
        private string _stirrerSpeedSetPoint;
        public string StirrerSpeedSetPoint
        {
            get => _stirrerSpeedSetPoint;
            set => SetProperty(ref _stirrerSpeedSetPoint, value);
        }
        private string _stirrerCurrentSpeed;
        public string StirrerCurrentSpeed
        {
            get => _stirrerCurrentSpeed;
            set => SetProperty(ref _stirrerCurrentSpeed, value);
        }
        private string _stirrerFailure;
        public string StirreFailure
        {
            get => _stirrerFailure;
            set => SetProperty(ref _stirrerFailure, value);
        }
        /// <summary>
        /// Contains the previous set point of stirrer
        /// </summary>
        private string _oldStirrerSetPoint;
        public string OldStirrerSetPoint
        {
            get => _oldStirrerSetPoint;
            set => SetProperty(ref _oldStirrerSetPoint, value);
        }
        #endregion

        #region Reactor Parameters
        private string _reactorMassTemperature;
        public string ReactorMassTemperature
        {
            get => _reactorMassTemperature;
            set => SetProperty(ref _reactorMassTemperature, value);
        }
        private string _pressure;
        public string Pressure
        {
            get => _pressure;
            set => SetProperty(ref _pressure, value);
        }
        private string _phValue;
        public string PHvalue
        {
            get => _phValue;
            set => SetProperty(ref _phValue, value);
        }
        private string _jacketOutletTemperature;
        public string JacketOutletTemperature
        {
            get => _jacketOutletTemperature;
            set => SetProperty(ref _jacketOutletTemperature, value);
        }

        #endregion

        #region Emergency Properties
        private string _emergencyStatus;
        public string EmergencyStatus
        {
            get => _emergencyStatus;
            set => SetProperty(ref _emergencyStatus, value);
        }
        #endregion

        #region Dosing Pump 
        private string _dosingPumpStatus;
        public string DosingPumpStatus
        {
            get => _dosingPumpStatus;
            set => SetProperty(ref _dosingPumpStatus, value);
        }
        private string _dosingPumpStatusFeedback;
        public string DosingPumpStatusFeedback
        {
            get => _dosingPumpStatusFeedback;
            set => SetProperty(ref _dosingPumpStatusFeedback, value);
        }
        private string _dosingPumpFlowSetPoint;
        public string DosingPumpFlowSetPoint
        {
            get => _dosingPumpFlowSetPoint;
            set => SetProperty(ref _dosingPumpFlowSetPoint, value);
        }
        private string _dosingPumpCurrentFlow;
        public string DosingPumpCurrentFlow
        {
            get => _dosingPumpCurrentFlow;
            set => SetProperty(ref _dosingPumpCurrentFlow, value);
        }
        private string _oldDosingPumpFlowSetPoint;
        public string OldDosingPumpFlowSetPoint
        {
            get => _oldDosingPumpFlowSetPoint;
            set => SetProperty(ref _oldDosingPumpFlowSetPoint, value);
        }
        #endregion

        #endregion

    }
}
