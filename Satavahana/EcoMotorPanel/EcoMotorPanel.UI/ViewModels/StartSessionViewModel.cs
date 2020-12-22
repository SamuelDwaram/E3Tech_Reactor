using E3.ReactorManager.Interfaces.DataAbstractionLayer;
using E3.ReactorManager.Interfaces.HardwareAbstractionLayer;
using E3.ReactorManager.Interfaces.HardwareAbstractionLayer.Data;
using E3.SessionManager.Models;
using E3.SessionManager.Services;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Unity;
using Unity.Resolution;

namespace EcoMotorPanel.UI.ViewModels
{
    public class StartSessionViewModel : BindableBase
    {

        private readonly IDatabaseWriter databaseWriter;
        private ISessionManager sessionManager;
        private readonly IFieldDevicesCommunicator fieldDevicesCommunicator;
        private readonly IList<PropertyInfo> existingProperties;
        private readonly TaskScheduler taskScheduler;
        private readonly IUnityContainer unityContainer;

        public StartSessionViewModel(IDatabaseWriter databaseWriter, IUnityContainer unityContainer, IFieldDevicesCommunicator fieldDevicesCommunicator)
        {
            this.unityContainer = unityContainer;
            existingProperties = GetType().GetProperties();
            taskScheduler = TaskScheduler.FromCurrentSynchronizationContext();

            this.databaseWriter = databaseWriter;
            this.sessionManager = unityContainer.Resolve<ISessionManager>(new ResolverOverride[] {
                new ParameterOverride("deviceId", DeviceId)
            });

            this.fieldDevicesCommunicator = fieldDevicesCommunicator;
            this.fieldDevicesCommunicator.FieldPointDataReceived += FieldDevicesCommunicator_FieldPointDataReceived;

        }

        public void StartSession()
        {
            CurrentSessionId = sessionManager.StartSession(TrainerName, TraineeName, TraineeRegion, DeviceId, Remarks);
        }

        public string GetValue(string value)
        {
            return string.IsNullOrWhiteSpace(value) ? "0" : value;
        }

        public void EndSession()
        {
            sessionManager.EndSession(CurrentSessionId);

            //Reset the session parameters
            CurrentSessionId = 0;
            TrainerName = string.Empty;
            TraineeName = string.Empty;
            TraineeRegion = string.Empty;
            Remarks = string.Empty;

            MessageBox.Show("Ended Session Successfully. Go to Existing Sessions Page for the Session Reports", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public bool CanStartSession()
        {
            return CurrentSessionId == 0
                    && !string.IsNullOrWhiteSpace(TraineeName)
                    && !string.IsNullOrWhiteSpace(TrainerName)
                    && !string.IsNullOrWhiteSpace(TraineeRegion)
                    && !string.IsNullOrWhiteSpace(DeviceId)
                    && !string.IsNullOrWhiteSpace(Remarks);
        }

        public bool CanEndSession()
        {
            return CurrentSessionId > 0;
        }
        private void FieldDevicesCommunicator_FieldPointDataReceived(object sender, FieldPointDataReceivedArgs fieldPointDataChangedArgs)
        {
            if (fieldPointDataChangedArgs.FieldDeviceIdentifier == DeviceId)
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

        public void Loaded()
        {
            Task.Factory.StartNew(new Func<object, FieldDevice>(GetFieldDeviceData), DeviceId)
                .ContinueWith(new Action<Task<FieldDevice>>(UpdateFieldDeviceData))
                .ContinueWith((task) =>
                {
                    //Update the Current Running Session Properties
                    Task.Factory.StartNew(new Func<Session>(sessionManager.GetCurrentRunningSessionIfAny))
                        .ContinueWith(new Action<Task<Session>>((t) =>
                        {
                            if (t.Result == null)
                            {
                                // Skip. No session is running
                                return;
                            }
                            Session currentSession = t.Result;
                            CurrentSessionId = currentSession.Id;
                            TrainerName = currentSession.TrainerName;
                            TraineeName = currentSession.TraineeName;
                            TraineeRegion = currentSession.TraineeRegion;
                            Remarks = currentSession.Remarks;
                            SessionStartTime = currentSession.StartTimeStamp.ToString();
                            SessionEndTime = currentSession.EndTimeStamp.ToString();
                        }));
                });
        }

        #region Live Data Handlers
        private void UpdatePropertyValue(Task<LiveDataEventArgs> task)
        {
            var liveDataEventArgs = task.Result;

            if (liveDataEventArgs != null && liveDataEventArgs.PropertyInfo != null && liveDataEventArgs.LiveData != null)
            {
                liveDataEventArgs.PropertyInfo
                                    .SetValue(this, liveDataEventArgs.LiveData == "NC" ? null : liveDataEventArgs.LiveData, null);
            }
        }

        private void OnLiveDataReceived(object sender, FieldPointDataReceivedArgs fieldPointDataChangedArgs)
        {
            if (fieldPointDataChangedArgs.FieldDeviceIdentifier == DeviceId)
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
                            PropertyInfoIdentifier = fieldPoint.Label,
                            LiveData = fieldPoint.Value,
                        };

                        Task.Factory.StartNew(new Func<object, LiveDataEventArgs>(ValidateLiveDataReceived), liveDataEventArgs)
                            .ContinueWith(new Action<Task<LiveDataEventArgs>>(UpdatePropertyValue), taskScheduler);
                    }
                }
            }
        }
#endregion

        #region Commands
        private ICommand _startSessionCommand;
        public ICommand StartSessionCommand
        {
            get => _startSessionCommand ?? (_startSessionCommand = new DelegateCommand(StartSession, CanStartSession)
                       .ObservesProperty(() => CurrentSessionId).ObservesProperty(() => TrainerName)
                       .ObservesProperty(() => TraineeName).ObservesProperty(() => TraineeRegion)
                       .ObservesProperty(() => DeviceId).ObservesProperty(() => Remarks));
            set => SetProperty(ref _startSessionCommand, value);
        }

        private ICommand _endSessionCommand;
        public ICommand EndSessionCommand
        {
            get => _endSessionCommand ?? (_endSessionCommand = new DelegateCommand(EndSession, CanEndSession)
                .ObservesProperty(() => CurrentSessionId));
            set => SetProperty(ref _endSessionCommand, value);
        }

        private ICommand _loadedCommand;
        public ICommand LoadedCommand
        {
            get => _loadedCommand ?? (_loadedCommand = new DelegateCommand(Loaded));
            set => SetProperty(ref _loadedCommand, value);
        }
        #endregion

        #region Properties
        private string _sessionNo;
        public string SessionNo
        {
            get => _sessionNo;
            set => SetProperty(ref _sessionNo, value);
        }

        private string _trainerName;
        public string TrainerName
        {
            get => _trainerName;
            set => SetProperty(ref _trainerName, value);
        }
        private string _traineeName;
        public string TraineeName
        {
            get => _traineeName;
            set => SetProperty(ref _traineeName, value);
        }

        private string _traineeRegion;
        public string TraineeRegion
        {
            get => _traineeRegion;
            set => SetProperty(ref _traineeRegion, value);
        }

        private string _motorPanelSelection;
        public string MotorPanelSelection
        {
            get => _motorPanelSelection;
            set => SetProperty(ref _motorPanelSelection, value);
        }

        private string _remarks;
        public string Remarks
        {
            get => _remarks;
            set => SetProperty(ref _remarks, value);
        }

        private string _sessionStartTime;
        public string SessionStartTime
        {
            get => _sessionStartTime;
            set => SetProperty(ref _sessionStartTime, value);
        }

        private string _sessionEndTime;
        public string SessionEndTime
        {
            get => _sessionEndTime;
            set => SetProperty(ref _sessionEndTime, value);
        }

        private string _deviceId;
        public string DeviceId
        {
            get => _deviceId ?? "Motor_2";
            set => SetProperty(ref _deviceId, value);
        }

        private int _currentSessionId;
        public int CurrentSessionId
        {
            get => _currentSessionId;
            set => SetProperty(ref _currentSessionId, value);
        }

        #endregion

        #region Motor Panel Properties

        private string _rpmOriginal;
        public string RPMOriginal
        {
            get => _rpmOriginal;
            set => SetProperty(ref _rpmOriginal, value);
        }
        private string _motorStatusIndicator;
        public string MotorStatusIndicator
        {
            get => _motorStatusIndicator;
            set => SetProperty(ref _motorStatusIndicator, value);
        }

        #region Propulsion Wheel
        private string _propulsionWheelInput;
        public string PropulsionWheelInput
        {
            get => _propulsionWheelInput ?? "1";
            set => SetProperty(ref _propulsionWheelInput, value);
        }

        private string _propulsionWheelInputLock;
        public string PropulsionWheelInputLock
        {
            get => _propulsionWheelInputLock;
            set => SetProperty(ref _propulsionWheelInputLock, value);
        }
        #endregion

        #region Armature Wheel
        private string _armatureBreakerInput;
        public string ArmatureBreakerInput
        {
            get => _armatureBreakerInput ?? "0";
            set => SetProperty(ref _armatureBreakerInput, value);
        }

        private string _armatureSelectorWheelInput;
        public string ArmatureSelectorWheelInput
        {
            get => _armatureSelectorWheelInput ?? "1";
            set => SetProperty(ref _armatureSelectorWheelInput, value);
        }

        private string _armatureSelectorWheelLock;
        public string ArmatureSelectorWheelLock
        {
            get => _armatureSelectorWheelLock;
            set => SetProperty(ref _armatureSelectorWheelLock, value);
        }
        #endregion

        #region Reverser Wheel
        private string _reverserWheelLock;
        public string ReverserWheelLock
        {
            get => _reverserWheelLock;
            set => SetProperty(ref _reverserWheelLock, value);
        }

        private string _reverserWheelInput;
        public string ReverserWheelInput
        {
            get => _reverserWheelInput ?? "2";
            set => SetProperty(ref _reverserWheelInput, value);
        }
        #endregion

        #region Grouper Control Switch
        private string _grouperControlSwitchModeInput;
        public string GrouperControlSwitchModeInput
        {
            get => _grouperControlSwitchModeInput ?? "0";
            set => SetProperty(ref _grouperControlSwitchModeInput, value);
        }

        private string _grouperControlSwitchSelectionInput;
        public string GrouperControlSwitchSelectionInput
        {
            get => _grouperControlSwitchSelectionInput ?? "0";
            set => SetProperty(ref _grouperControlSwitchSelectionInput, value);
        }
        #endregion

        #region Bow & Stern
        private string _bowExcitationCurrent;
        public string BowExcitationCurrent
        {
            get => _bowExcitationCurrent;
            set => SetProperty(ref _bowExcitationCurrent, value);
        }

        private string _sternExcitationCurrent;
        public string SternExcitationCurrent
        {
            get => _sternExcitationCurrent;
            set => SetProperty(ref _sternExcitationCurrent, value);
        }

        private string _bowArmatureCurrent;
        public string BowArmatureCurrent
        {
            get => _bowArmatureCurrent;
            set => SetProperty(ref _bowArmatureCurrent, value);
        }

        private string _sternArmatureCurrent;
        public string SternArmatureCurrent
        {
            get => _sternArmatureCurrent;
            set => SetProperty(ref _sternArmatureCurrent, value);
        }
        #endregion

        private string _governorWheelInput;
        public string GovernorWheelInput
        {
            get => _governorWheelInput;
            set => SetProperty(ref _governorWheelInput, value);
        }

        private string _rpm;
        public string RPM
        {
            get => _rpm ?? "0";
            set => SetProperty(ref _rpm, value);
        }

        private string _emergencySwitch;
        public string EmergencySwitch
        {
            get => _emergencySwitch;
            set => SetProperty(ref _emergencySwitch, value);
        }

        #region Voltmeter
        private string _voltmeterSelector;
        public string VoltmeterSelector
        {
            get => _voltmeterSelector;
            set => SetProperty(ref _voltmeterSelector, value);
        }

        private string _voltage;
        public string Voltage
        {
            get => _voltage;
            set => SetProperty(ref _voltage, value);
        }
        #endregion

        #region Charge & Discharge Battery
        private string _chargeBatteryBank_1;
        public string ChargeBatteryBank_1
        {
            get => _chargeBatteryBank_1;
            set => SetProperty(ref _chargeBatteryBank_1, value);
        }

        private string _chargeBatteryBank_2;
        public string ChargeBatteryBank_2
        {
            get => _chargeBatteryBank_2;
            set => SetProperty(ref _chargeBatteryBank_2, value);
        }

        private string _disChargeBatteryBank_1;
        public string DisChargeBatteryBank_1
        {
            get => _disChargeBatteryBank_1;
            set => SetProperty(ref _disChargeBatteryBank_1, value);
        }

        private string _disChargeBatteryBank_2;
        public string DisChargeBatteryBank_2
        {
            get => _disChargeBatteryBank_2;
            set => SetProperty(ref _disChargeBatteryBank_2, value);
        }
        #endregion

        #endregion


    }
}
