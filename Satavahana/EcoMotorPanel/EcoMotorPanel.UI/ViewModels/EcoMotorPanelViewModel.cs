using E3.ReactorManager.Interfaces.HardwareAbstractionLayer;
using E3.ReactorManager.Interfaces.HardwareAbstractionLayer.Data;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Input;
using Unity;

namespace EcoMotorPanel.UI.ViewModels
{
    public class EcoMotorPanelViewModel : BindableBase
    {
        IFieldDevicesCommunicator fieldDevicesCommunicator;
        TaskScheduler taskScheduler;
        IList<PropertyInfo> existingProperties;

        public EcoMotorPanelViewModel(IUnityContainer containerProvider)
        {
            taskScheduler = TaskScheduler.FromCurrentSynchronizationContext();
            existingProperties = new List<PropertyInfo>(GetType().GetProperties());
            fieldDevicesCommunicator = containerProvider.Resolve<IFieldDevicesCommunicator>();
            fieldDevicesCommunicator.FieldPointDataReceived += OnLiveDataReceived;

            Task.Factory.StartNew(new Action(ExecuteCyclicWriteCommands));
        }

        private void ExecuteCyclicWriteCommands()
        {
            try
            {
                SendArmatureCurrentCommand();
                SendExcitationCurrentCommand();
                SendMotorStatus();
                SendRPM();
                SendVoltmeterCommand();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Task.Factory.StartNew(new Action(ExecuteCyclicWriteCommands));
            }
        }

        public void PageLoaded()
        {
            Task.Factory.StartNew(new Func<object, FieldDevice>(GetFieldDeviceData), DeviceId)
                .ContinueWith(new Action<Task<FieldDevice>>(UpdateFieldDeviceData), taskScheduler);
        }

        public double GetScaledValue(double inValue, double minValue, double maxValue, double scaleMinValue, double scaleMaxValue)
        {
            return Math.Round((scaleMaxValue - scaleMinValue) / (maxValue - minValue) * (inValue - minValue) + scaleMinValue, 0);
        }

        #region Voltmeter
        public void SendVoltmeterCommand()
        {
            fieldDevicesCommunicator.SendCommandToDevice(DeviceId, "Voltage", "float", GetVoltage().ToString());
        }
        public double GetVoltage()
        {
            switch (Convert.ToInt32(VoltmeterSelector))
            {
                case 1:
                case 2:
                case 7:
                case 8:
                    return 0;
                case 3:
                case 4:
                    return 195;
                case 5:
                case 6:
                    return GetScaledValue(Convert.ToDouble(RPMOriginal), 0, 63, 215, 210);
                default:
                    return 0;
            }
        }
        #endregion

        #region ExcitationAmmeter
        public void SendExcitationCurrentCommand()
        {
            fieldDevicesCommunicator.SendCommandToDevice(DeviceId, "ExcitationCurrent", "float", GetExcitationCurrent().ToString());
        }
        public double GetExcitationCurrent()
        {
            while (Convert.ToInt32(EmergencySwitch) == 1)
            {
                switch (Convert.ToInt32(PropulsionWheelInput))
                {
                    case 1:
                        return 0;
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                    case 7:
                        return GetScaledValue(Convert.ToDouble(GovernorWheelInput), 994, 64, 58, 199);
                    case 8:
                        return GetScaledValue(Convert.ToDouble(GovernorWheelInput), 994, 64, 105, 199);
                    default:
                        return 0;
                }
            }
            return 0;
        }
        #endregion

        #region ArmatureAmmeter
        public void SendArmatureCurrentCommand()
        {
            fieldDevicesCommunicator.SendCommandToDevice(DeviceId, "ArmatureCurrent", "float", GetArmatureCurrent().ToString());
        }
        public double GetArmatureCurrent()
        {
            while (Convert.ToInt32(EmergencySwitch) == 1)
            {
                switch (Convert.ToInt32(PropulsionWheelInput))
                {
                    case 1:
                    case 2:
                    case 6:
                        return 128;
                    case 3:
                    case 5:
                        return 139;
                    case 4:
                        return GetScaledValue(Convert.ToDouble(RPMOriginal), 25, 48, 135, 158);
                    case 7:
                        return 161;
                    case 8:
                        return GetScaledValue(Convert.ToDouble(RPMOriginal), 25, 64, 130, 212);
                    default:
                        return 128;
                }
            }

            return 128;
        }
        #endregion

        #region Tachometer
        public void SendRPM()
        {
            fieldDevicesCommunicator.SendCommandToDevice(DeviceId, "RPMOriginal", "float", GetRpm().ToString());
            fieldDevicesCommunicator.SendCommandToDevice(DeviceId, "RPM", "float", GetRpmUpdated(GetRpm()).ToString());
        }
        public double GetRpmUpdated(double currentRpm)
        {
            return currentRpm == 0 ? currentRpm : GetScaledValue(currentRpm, 20, 63, 100, 180);
        }
        public double GetRpm()
        {
            while (Convert.ToInt32(EmergencySwitch) == 1)
            {
                switch (Convert.ToInt32(PropulsionWheelInput))
                {
                    case 1:
                    case 2:
                    case 6:
                        return 0;
                    case 3:
                    case 5:
                        return GetScaledValue(Convert.ToInt32(GovernorWheelInput), 994, 64, 25, 20);
                    case 4:
                        return GetScaledValue(Convert.ToInt32(GovernorWheelInput), 994, 64, 48, 25);
                    case 7:
                        // return GetScaledValue(Convert.ToInt32(GovernorWheelInput), 64, 994, 25, 20);
                        return 45;
                    case 8:
                        return GetScaledValue(Convert.ToInt32(GovernorWheelInput), 994, 64, 64, 45);
                    default:
                        return 0;
                }
            }
            return 0;
        }
        #endregion

        #region LEDControl
        public void SendMotorStatus()
        {
            fieldDevicesCommunicator.SendCommandToDevice(DeviceId, "MotorStatusIndicator", "float", GetMotorStatus().ToString());
        }
        public int GetMotorStatus()
        {
            if (Convert.ToInt32(EmergencySwitch) == 1)
            {
                switch (Convert.ToInt32(PropulsionWheelInput))
                {
                    case 1:
                        return 2;
                    case 6:
                    case 2:
                    case 4:
                    case 8:
                        return 3;
                    case 3:
                    case 5:
                    case 7:
                        return 11;
                    default:
                        return 2;
                }
            }
            else if (Convert.ToInt32(EmergencySwitch) == 0)
            {
                return 6;
            }

            return 2;
        }
        #endregion

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
                        return;
                    }
                }
            }
        }
        #endregion

        private ICommand _loadedCommand;
        public ICommand LoadedCommand
        {
            get => _loadedCommand ?? (_loadedCommand = new DelegateCommand(PageLoaded));
            set => SetProperty(ref _loadedCommand, value);
        }

        #region Properties
        public string DeviceId
        {
            get => "Motor_2";
        }

        public string DeviceLabel
        {
            get => "Eco Motor Panel";
        }

        private string _excitationCurrent;
        public string ExcitationCurrent
        {
            get => _excitationCurrent;
            set => SetProperty(ref _excitationCurrent, value);
        }

        private string _armatureCurrent;
        public string ArmatureCurrent
        {
            get => _armatureCurrent;
            set => SetProperty(ref _armatureCurrent, value);
        }

        private string _rpmOriginal;
        public string RPMOriginal
        {
            get => _rpmOriginal;
            set => SetProperty(ref _rpmOriginal, value);
        }

        private string _voltage;
        public string Voltage
        {
            get => _voltage;
            set => SetProperty(ref _voltage, value);
        }

        private string _motorStatusIndicator;
        public string MotorStatusIndicator
        {
            get => _motorStatusIndicator;
            set => SetProperty(ref _motorStatusIndicator, value);
        }

        private string _propulsionWheelInput;
        public string PropulsionWheelInput
        {
            get => _propulsionWheelInput;
            set => SetProperty(ref _propulsionWheelInput, value);
        }

        private string _voltmeterSelector;
        public string VoltmeterSelector
        {
            get => _voltmeterSelector;
            set => SetProperty(ref _voltmeterSelector, value);
        }

        private string _emergencySwitch;
        public string EmergencySwitch
        {
            get => _emergencySwitch;
            set => SetProperty(ref _emergencySwitch, value);
        }

        private string _governorWheelInput;
        public string GovernorWheelInput
        {
            get => _governorWheelInput;
            set => SetProperty(ref _governorWheelInput, value);
        }
        #endregion

    }
}
