using E3.AuditTrailManager.Model;
using E3.AuditTrailManager.Model.Enums;
using E3.ReactorManager.BusinessProcessingUnit.Model.Interfaces;
using E3.ReactorManager.Interfaces.HardwareAbstractionLayer;
using E3.ReactorManager.Interfaces.HardwareAbstractionLayer.Data;
using E3.UserManager.Model.Data;
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

namespace USV.ReactorManager.UI.ViewModels
{
    public class FieldDeviceParametersViewModel : BindableBase
    {
        IRegionManager regionManager;
        IFieldDevicesCommunicator fieldDevicesCommunicator;
        IBusinessProcessingUnit bpu;
        IList<PropertyInfo> existingProperties;
        TaskScheduler taskScheduler;
        IAuditTrailManager auditTrailManager;

        public FieldDeviceParametersViewModel(IUnityContainer containerProvider, IRegionManager regionManager)
        {
            taskScheduler = TaskScheduler.FromCurrentSynchronizationContext();
            this.regionManager = regionManager;
            this.bpu = containerProvider.Resolve<IBusinessProcessingUnit>();
            this.fieldDevicesCommunicator = containerProvider.Resolve<IFieldDevicesCommunicator>();
            fieldDevicesCommunicator.FieldPointDataReceived += OnLiveDataReceived;
            auditTrailManager = containerProvider.Resolve<IAuditTrailManager>();
        }

        #region Live Data Handlers

        private void UpdateExistingPropertiesList()
        {
            existingProperties = new List<PropertyInfo>(GetType().GetProperties());
        }

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
            if (fieldPointDataChangedArgs.FieldDeviceIdentifier == FieldDeviceIdentifier)
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

        public void UpdateFieldDeviceParameters()
        {
            Task updateExistingPropertiesTask = Task.Factory.StartNew(new Action(UpdateExistingPropertiesList));
            Task.WaitAny(updateExistingPropertiesTask);

            NavigationParameters parameters = (NavigationParameters)regionManager.Regions["FieldDeviceParameters"].Context;
            FieldDeviceIdentifier = parameters["DeviceId"].ToString();
            UserDetails = (User)parameters["UserDetails"];

            Task.Factory.StartNew(new Func<object, FieldDevice>(GetFieldDeviceData), FieldDeviceIdentifier)
                .ContinueWith(new Action<Task<FieldDevice>>(UpdateFieldDeviceData), taskScheduler);
        }

        public void ChangeOperatingMode()
        {
            bool remoteModeSelection = bool.Parse(RemoteModeSelection);
            bpu.SendCommandToDevice(FieldDeviceIdentifier, "RemoteModeSelection", "bool", (!remoteModeSelection).ToString());
            auditTrailManager.RecordEventAsync("Switched RemoteMode " + (remoteModeSelection ? " off" : " on"), UserDetails.Name, remoteModeSelection ? EventTypeEnum.SwitchedOff : EventTypeEnum.SwitchedOn);
        }

        public void ChangeEmergencyStatus()
        {
            bool emergencyStatus = bool.Parse(EmergencyStatus);
            bpu.SendCommandToDevice(FieldDeviceIdentifier, "EmergencyStatus", "bool", (!emergencyStatus).ToString());
            auditTrailManager.RecordEventAsync("Switched Emergency " + (emergencyStatus ? " off" : " on"), UserDetails.Name, emergencyStatus ? EventTypeEnum.SwitchedOff : EventTypeEnum.SwitchedOn);
        }

        #region Stirrer Functions
        public void ChangeStirrerStatus()
        {
            bool stirrerCurrentStatus = bool.Parse(StirrerStatusFeedback);
            bpu.SendCommandToDevice(FieldDeviceIdentifier, "StirrerStatus", "bool", (!stirrerCurrentStatus).ToString());
            auditTrailManager.RecordEventAsync("Switched Stirrer " + (stirrerCurrentStatus ? " off" : " on"), UserDetails.Name, stirrerCurrentStatus ? EventTypeEnum.SwitchedOff : EventTypeEnum.SwitchedOn);
        }

        public void ChangeStirrerSpeedSetPoint()
        {
            if (!string.IsNullOrEmpty(StirrerSpeedSetPoint))
            {
                //first convert the user entered stirrer Speed SetPoint to integer
                int toBeSetStirrerSpeedSetPoint = Convert.ToInt16(StirrerSpeedSetPoint);
                if (toBeSetStirrerSpeedSetPoint <= 500 && toBeSetStirrerSpeedSetPoint >= 20)
                {
                    bpu.SendCommandToDevice(FieldDeviceIdentifier, "StirrerSpeedSetPoint", "int", toBeSetStirrerSpeedSetPoint.ToString());
                    auditTrailManager.RecordEventAsync("Changed StirrerSpeedSetPoint from " + OldStirrerSpeedSetPoint + " to " + toBeSetStirrerSpeedSetPoint, UserDetails.Name, EventTypeEnum.ChangedSetPoint);
                }
                else if (toBeSetStirrerSpeedSetPoint > 500)
                {
                    MessageBox.Show("Maximum Stirrer Speed is less than 500 rpm");
                    StirrerSpeedSetPoint = null;
                }
                else if (toBeSetStirrerSpeedSetPoint < 20)
                {
                    MessageBox.Show("Minimum Stirrer Speed is 20 rpm");
                    StirrerSpeedSetPoint = null;
                }
            }
            else
            {
                MessageBox.Show("Please Enter some data as a set point",
                                "Stirrer SetPoint Null Error",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
        }

        public void UpdateStirrerOldSpeedSetPoint(string newSetPoint)
        {
            if (!string.IsNullOrWhiteSpace(newSetPoint))
            {
                //update OLd SetPoint
                OldStirrerSpeedSetPoint = newSetPoint;
            }
        }

        #endregion

        #region Dosing Pump Functions
        public void ChangeDosingPumpStatus(string dosingPumpIdentifier)
        {
            int dosingPumpIndex = Convert.ToInt32(dosingPumpIdentifier.Substring(dosingPumpIdentifier.IndexOf("_") + 1));
            bool dosingPumpCurrentStatus = bool.Parse(GetType().GetProperty("DosingPumpStatusFeedback_" + dosingPumpIndex).GetValue(this, null).ToString());
            bpu.SendCommandToDevice(FieldDeviceIdentifier, "DosingPumpStatus_" + dosingPumpIndex, "bool", (!dosingPumpCurrentStatus).ToString());
            auditTrailManager.RecordEventAsync("Switched DosingPump " + dosingPumpIndex + (dosingPumpCurrentStatus ? " off" : " on"), UserDetails.Name, dosingPumpCurrentStatus ? EventTypeEnum.SwitchedOff : EventTypeEnum.SwitchedOn);
        }

        public void ChangeDosingPumpFlowSetPoint(string dosingPumpIdentifier)
        {
            if (!string.IsNullOrEmpty(GetType().GetProperty(dosingPumpIdentifier).GetValue(this, null).ToString()))
            {
                float toBeSetDosingFlow = float.Parse(GetType().GetProperty(dosingPumpIdentifier).GetValue(this, null).ToString());
                if (toBeSetDosingFlow <= 6000 && toBeSetDosingFlow >= 0)
                {
                    bpu.SendCommandToDevice(FieldDeviceIdentifier, dosingPumpIdentifier, "float", toBeSetDosingFlow.ToString());
                    auditTrailManager.RecordEventAsync("Changed "+ dosingPumpIdentifier +" from " + GetType().GetProperty("Old" + dosingPumpIdentifier).GetValue(this, null).ToString() + " to " + toBeSetDosingFlow, UserDetails.Name, EventTypeEnum.ChangedSetPoint);
                }
                else if (toBeSetDosingFlow > 6000)
                {
                    MessageBox.Show("Maximum Dosing Flow is 6000 ml");
                }
                else if (toBeSetDosingFlow < 0)
                {
                    MessageBox.Show("Minimum Dosing Flow is 0 lpm");
                }
            }
            else
            {
                MessageBox.Show("Please enter some data as a set point",
                                "Dosing Pump SetPoint Null Error",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
        }

        public void UpdateDosingPumpFlowOldSetPoint(string dosingPumpIdentifier)
        {
            switch (dosingPumpIdentifier)
            {
                case "DosingPumpFlowSetPoint_1":
                    OldDosingPumpFlowSetPoint_1 = GetType().GetProperty(dosingPumpIdentifier).GetValue(this, null).ToString();
                    break;
                case "DosingPumpFlowSetPoint_3":
                    OldDosingPumpFlowSetPoint_3 = GetType().GetProperty(dosingPumpIdentifier).GetValue(this, null).ToString();
                    break;
                default:
                    break;
            }
        }
        #endregion

        public void ChangeSovValveStatus(string SovValveIdentifier)
        {
            bool sovStatus = bool.Parse(GetType().GetProperty(SovValveIdentifier).GetValue(this, null).ToString());
            bpu.SendCommandToDevice(FieldDeviceIdentifier, SovValveIdentifier, "bool", (!sovStatus).ToString());
            auditTrailManager.RecordEventAsync("Switched " + SovValveIdentifier + (sovStatus ? " off" : " on"), UserDetails.Name, sovStatus ? EventTypeEnum.SwitchedOff : EventTypeEnum.SwitchedOn);
        }

        #region Stirrer Commands
        private ICommand _changeStirrerStatusCommand;
        public ICommand ChangeStirrerStatusCommand
        {
            get => _changeStirrerStatusCommand ?? (_changeStirrerStatusCommand = new DelegateCommand(ChangeStirrerStatus));
            set { _changeStirrerStatusCommand = value; }
        }

        private ICommand _changeStirrerSpeedSetPointCommand;
        public ICommand ChangeStirrerSpeedSetPointCommand
        {
            get => _changeStirrerSpeedSetPointCommand ?? (_changeStirrerSpeedSetPointCommand = new DelegateCommand(ChangeStirrerSpeedSetPoint));
            set { _changeStirrerSpeedSetPointCommand = value; }
        }

        private ICommand _updateStirrerOldSpeedSetPointCommand;
        public ICommand UpdateStirrerOldSpeedSetPointCommand
        {
            get => _updateStirrerOldSpeedSetPointCommand ?? (_updateStirrerOldSpeedSetPointCommand = new DelegateCommand<string>(UpdateStirrerOldSpeedSetPoint));
            set => _updateStirrerOldSpeedSetPointCommand = value;
        }
        #endregion

        #region Dosing Pump Commands
        private ICommand _updateDosingPumpFlowOldSetPointCommand;
        public ICommand UpdateDosingPumpFlowOldSetPointCommand
        {
            get => _updateDosingPumpFlowOldSetPointCommand 
                ?? (_updateDosingPumpFlowOldSetPointCommand = new DelegateCommand<string>(UpdateDosingPumpFlowOldSetPoint)
                .ObservesProperty(() => DosingPumpFlowSetPoint_1)
                .ObservesProperty(() => DosingPumpFlowSetPoint_3));
            set => _updateDosingPumpFlowOldSetPointCommand = value;
        }

        private ICommand _changeDosingPumpStatusCommand;
        public ICommand ChangeDosingPumpStatusCommand
        {
            get => _changeDosingPumpStatusCommand ?? (_changeDosingPumpStatusCommand = new DelegateCommand<string>(ChangeDosingPumpStatus));
            set { _changeDosingPumpStatusCommand = value; }
        }

        private ICommand _changeDosingPumpFlowSetPointCommand;
        public ICommand ChangeDosingPumpFlowSetPointCommand
        {
            get => _changeDosingPumpFlowSetPointCommand ?? (_changeDosingPumpFlowSetPointCommand = new DelegateCommand<string>(ChangeDosingPumpFlowSetPoint));
            set => _changeDosingPumpFlowSetPointCommand = value;
        }
        #endregion

        #region Commands
        private ICommand _updateFieldDeviceParametersCommand;
        public ICommand UpdateFieldDeviceParametersCommand
        {
            get => _updateFieldDeviceParametersCommand ?? (_updateFieldDeviceParametersCommand = new DelegateCommand(UpdateFieldDeviceParameters));
            set => _updateFieldDeviceParametersCommand = value;
        }
        
        private ICommand _emergencyCommand;
        public ICommand EmergencyCommand
        {
            get => _emergencyCommand ?? (_emergencyCommand = new DelegateCommand(ChangeEmergencyStatus));
            set { _emergencyCommand = value; }
        }
        
        private ICommand _changeSovValveStatusCommand;
        public ICommand ChangeSovValveStatusCommand
        {
            get => _changeSovValveStatusCommand ?? (_changeSovValveStatusCommand = new DelegateCommand<string>(ChangeSovValveStatus));
            set { _changeSovValveStatusCommand = value; }
        }
        
        private ICommand _changeOperatingModeCommand;
        public ICommand ChangeOperatingModeCommand
        {
            get => _changeOperatingModeCommand ?? (_changeOperatingModeCommand = new DelegateCommand(ChangeOperatingMode));
            set { _changeOperatingModeCommand = value; }
        }
        #endregion

        #region Properties

        private string _fieldDeviceIdentifier;
        public string FieldDeviceIdentifier
        {
            get => _fieldDeviceIdentifier;
            set
            {
                _fieldDeviceIdentifier = value;
                RaisePropertyChanged();
            }
        }

        private User _userDetails;
        public User UserDetails
        {
            get => _userDetails ?? (_userDetails = new User());
            set
            {
                _userDetails = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region FieldDevice Properties

        private string _remoteModeSelection;
        public string RemoteModeSelection
        {
            get { return _remoteModeSelection; }
            set
            {
                _remoteModeSelection = value;
                RaisePropertyChanged();
            }
        }

        private string _emergencyStatus;
        public string EmergencyStatus
        {
            get { return _emergencyStatus; }
            set
            {
                _emergencyStatus = value;
                RaisePropertyChanged();
            }
        }

        private string _reactorMassTemperature;
        public string ReactorMassTemperature
        {
            get { return _reactorMassTemperature; }
            set
            {
                _reactorMassTemperature = value;
                RaisePropertyChanged();
            }
        }

        #region Stirrer
        private string _stirrerStatus;
        public string StirrerStatus
        {
            get { return _stirrerStatus; }
            set
            {
                _stirrerStatus = value;
                RaisePropertyChanged();
            }
        }
        private string _stirrerStatusFeedback;
        public string StirrerStatusFeedback
        {
            get { return _stirrerStatusFeedback; }
            set
            {
                _stirrerStatusFeedback = value;
                RaisePropertyChanged();
            }
        }
        private string _stirrerSpeedSetPoint;
        public string StirrerSpeedSetPoint
        {
            get { return _stirrerSpeedSetPoint; }
            set
            {
                _stirrerSpeedSetPoint = value;
                RaisePropertyChanged();
            }
        }
        private string _stirrerCurrentSpeed;
        public string StirrerCurrentSpeed
        {
            get { return _stirrerCurrentSpeed; }
            set
            {
                _stirrerCurrentSpeed = value;
                RaisePropertyChanged();
            }
        }

        private string _oldStirrerSpeedSetPoint;
        public string OldStirrerSpeedSetPoint
        {
            get => _oldStirrerSpeedSetPoint;
            set
            {
                _oldStirrerSpeedSetPoint = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region Dosing Pump 1
        private string _dosingPumpStatus_1;
        public string DosingPumpStatus_1
        {
            get { return _dosingPumpStatus_1; }
            set
            {
                _dosingPumpStatus_1 = value;
                RaisePropertyChanged();
            }
        }
        private string _dosingPumpStatusFeedback_1;
        public string DosingPumpStatusFeedback_1
        {
            get { return _dosingPumpStatusFeedback_1; }
            set
            {
                _dosingPumpStatusFeedback_1 = value;
                RaisePropertyChanged();
            }
        }
        private string _dosingPumpFlowSetPoint_1;
        public string DosingPumpFlowSetPoint_1
        {
            get { return _dosingPumpFlowSetPoint_1; }
            set
            {
                _dosingPumpFlowSetPoint_1 = value;
                RaisePropertyChanged();
            }
        }
        private string _dosingPumpCurrentDosingFlow_1;
        public string DosingPumpCurrentDosingFlow
        {
            get { return _dosingPumpCurrentDosingFlow_1; }
            set
            {
                _dosingPumpCurrentDosingFlow_1 = value;
                RaisePropertyChanged();
            }
        }

        private string _oldDosingPumpFlowSetPoint_1;
        public string OldDosingPumpFlowSetPoint_1
        {
            get => _oldDosingPumpFlowSetPoint_1;
            set
            {
                _oldDosingPumpFlowSetPoint_1 = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Dosing Pump 2
        private string _dosingPumpStatus_2;
        public string DosingPumpStatus_2
        {
            get { return _dosingPumpStatus_2; }
            set
            {
                _dosingPumpStatus_2 = value;
                RaisePropertyChanged();
            }
        }
        private string _dosingPumpStatusFeedback_2;
        public string DosingPumpStatusFeedback_2
        {
            get { return _dosingPumpStatusFeedback_2; }
            set
            {
                _dosingPumpStatusFeedback_2 = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region Dosing Pump 3
        private string _dosingPumpStatus_3;
        public string DosingPumpStatus_3
        {
            get { return _dosingPumpStatus_3; }
            set
            {
                _dosingPumpStatus_3 = value;
                RaisePropertyChanged();
            }
        }
        private string _dosingPumpStatusFeedback_3;
        public string DosingPumpStatusFeedback_3
        {
            get { return _dosingPumpStatusFeedback_3; }
            set
            {
                _dosingPumpStatusFeedback_3 = value;
                RaisePropertyChanged();
            }
        }
        private string _dosingPumpFlowSetPoint_3;
        public string DosingPumpFlowSetPoint_3
        {
            get { return _dosingPumpFlowSetPoint_3; }
            set
            {
                _dosingPumpFlowSetPoint_3 = value;
                RaisePropertyChanged();
            }
        }
        private string _dosingPumpCurrentDosingFlow_3;
        public string DosingPumpCurrentDosingFlow_3
        {
            get { return _dosingPumpCurrentDosingFlow_3; }
            set
            {
                _dosingPumpCurrentDosingFlow_3 = value;
                RaisePropertyChanged();
            }
        }

        private string _oldDosingPumpFlowSetPoint_3;
        public string OldDosingPumpFlowSetPoint_3
        {
            get => _oldDosingPumpFlowSetPoint_3;
            set
            {
                _oldDosingPumpFlowSetPoint_3 = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Dosing Pump 4
        private string _dosingPumpStatus_4;
        public string DosingPumpStatus_4
        {
            get { return _dosingPumpStatus_4; }
            set
            {
                _dosingPumpStatus_4 = value;
                RaisePropertyChanged();
            }
        }
        private string _dosingPumpStatusFeedback_4;
        public string DosingPumpStatusFeedback_4
        {
            get { return _dosingPumpStatusFeedback_4; }
            set
            {
                _dosingPumpStatusFeedback_4 = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region Dosing Totalizer
        private string _dosingPumpTotalizerValue_1;
        public string DosingPumpTotalizerValue_1
        {
            get { return _dosingPumpTotalizerValue_1; }
            set
            {
                _dosingPumpTotalizerValue_1 = value;
                RaisePropertyChanged();
            }
        }

        private string _dosingPumpTotalizerValue_3;
        public string DosingPumpTotalizerValue_3
        {
            get { return _dosingPumpTotalizerValue_3; }
            set
            {
                _dosingPumpTotalizerValue_3 = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region Sov Valves
        private string _sovStatus_1;
        public string SovStatus_1
        {
            get { return _sovStatus_1; }
            set
            {
                _sovStatus_1 = value;
                RaisePropertyChanged();
            }
        }
        private string _sovStatus_2;
        public string SovStatus_2
        {
            get { return _sovStatus_2; }
            set
            {
                _sovStatus_2 = value;
                RaisePropertyChanged();
            }
        }
        private string _sovStatus_3;
        public string SovStatus_3
        {
            get { return _sovStatus_3; }
            set
            {
                _sovStatus_3 = value;
                RaisePropertyChanged();
            }
        }
        private string _sovStatus_4;
        public string SovStatus_4
        {
            get { return _sovStatus_4; }
            set
            {
                _sovStatus_4 = value;
                RaisePropertyChanged();
            }
        }
        private string _sovStatus_5;
        public string SovStatus_5
        {
            get { return _sovStatus_5; }
            set
            {
                _sovStatus_5 = value;
                RaisePropertyChanged();
            }
        }
        private string _sovStatus_6;
        public string SovStatus_6
        {
            get { return _sovStatus_6; }
            set
            {
                _sovStatus_6 = value;
                RaisePropertyChanged();
            }
        }
        private string _sovStatus_7;
        public string SovStatus_7
        {
            get { return _sovStatus_7; }
            set
            {
                _sovStatus_7 = value;
                RaisePropertyChanged();
            }
        }
        private string _sovStatus_8;
        public string SovStatus_8
        {
            get { return _sovStatus_8; }
            set
            {
                _sovStatus_8 = value;
                RaisePropertyChanged();
            }
        }
        private string _sovStatus_9;
        public string SovStatus_9
        {
            get { return _sovStatus_9; }
            set
            {
                _sovStatus_9 = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #endregion
    }
}
