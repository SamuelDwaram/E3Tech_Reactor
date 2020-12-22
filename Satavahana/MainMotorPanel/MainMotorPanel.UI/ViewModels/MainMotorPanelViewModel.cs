using E3.ReactorManager.Interfaces.HardwareAbstractionLayer;
using E3.ReactorManager.Interfaces.HardwareAbstractionLayer.Data;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Unity;

namespace MainMotorPanel.UI.ViewModels
{
    public class MainMotorPanelViewModel : BindableBase
    {
        private readonly IFieldDevicesCommunicator fieldDevicesCommunicator;
        private readonly TaskScheduler taskScheduler;
        private readonly IList<PropertyInfo> existingProperties;

        public MainMotorPanelViewModel(IUnityContainer containerProvider)
        {
            taskScheduler = TaskScheduler.FromCurrentSynchronizationContext();
            existingProperties = new List<PropertyInfo>(GetType().GetProperties());
            fieldDevicesCommunicator = containerProvider.Resolve<IFieldDevicesCommunicator>();
            fieldDevicesCommunicator.FieldPointDataReceived += OnLiveDataReceived;

            Task.Factory.StartNew(new Action(ExecuteCyclicWriteCommands));
            Task.Factory.StartNew(new Action(() => Thread.Sleep(100))).ContinueWith((t) => Operation());
        }

        #region Cyclic Commands
        private void ExecuteCyclicWriteCommands()
        {
            try
            {
                SendArmatureWheelLockCommand();
                SendReverserWheelLockCommand();
                SendPropulsionWheelLockCommand();
                SendMotorStatusIndicatorCommand();
                SendVoltmeterCommand();
                SendRPMCommand();
                SendArmatureCurrentCommand();
                SendExcitationCurrentCommand();
                SendBatteryBanksCommand();
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

        private void SendBatteryBanksCommand()
        {
            //DisChargeBatteryBank_1
            fieldDevicesCommunicator.SendCommandToDevice(DeviceId, "DisChargeBatteryBank_1",
                        "float", GetDischargeBatteryCurrent1().ToString());

            //DisChargeBatteryBank_2
            fieldDevicesCommunicator.SendCommandToDevice(DeviceId, "DisChargeBatteryBank_2",
                                        "float", GetDischargeBatteryCurrent2().ToString());
        }

        private void SendExcitationCurrentCommand()
        {
            fieldDevicesCommunicator.SendCommandToDevice(DeviceId, "BowExcitationCurrent",
                            "float", GetBowExcitationCurrent().ToString());
            fieldDevicesCommunicator.SendCommandToDevice(DeviceId, "SternExcitationCurrent",
                                        "float", GetSternExcitationCurrent().ToString());
        }

        private void SendArmatureCurrentCommand()
        {
            fieldDevicesCommunicator.SendCommandToDevice(DeviceId, "BowArmatureCurrent",
                            "float", GetBowArmatureCurrent().ToString());
            fieldDevicesCommunicator.SendCommandToDevice(DeviceId, "SternArmatureCurrent",
                                        "float", GetSternArmatureCurrent().ToString());
        }

        private void SendMotorStatusIndicatorCommand()
        {
            fieldDevicesCommunicator.SendCommandToDevice(DeviceId, "MotorStatusIndicator",
                "float", GetMotorStatus().ToString());
        }

        private void SendPropulsionWheelLockCommand()
        {
            fieldDevicesCommunicator.SendCommandToDevice(DeviceId, "PropulsionWheelInputLock",
                "float", PropulsionWheelInputLock ?? "0");
        }

        private void SendReverserWheelLockCommand()
        {
            fieldDevicesCommunicator.SendCommandToDevice(DeviceId, "ReverserWheelLock",
                "float", ReverserWheelLock ?? "0");
        }

        private void SendArmatureWheelLockCommand()
        {
            fieldDevicesCommunicator.SendCommandToDevice(DeviceId, "ArmatureSelectorWheelLock",
                "float", ArmatureSelectorWheelLock ?? "0");
        }
        #endregion

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
                    {
                        if (Convert.ToInt32(GrouperControlSwitchSelectionInput) == 1)
                        {
                            //if (Convert.ToInt32(ReverserWheelInput) == 1)
                            //{
                            //    if (Convert.ToInt32(PropulsionWheelInput) == 5 || Convert.ToInt32(PropulsionWheelInput) == 1)
                            //    {
                            //        return GetScaledValue(Convert.ToDouble(RPM), 150, 232, 140, 130);
                            //    }

                            //    else return 140;
                            //}
                            //else if (Convert.ToInt32(ReverserWheelInput) == 3)
                            //{
                            //    if (Convert.ToInt32(PropulsionWheelInput) == 5 || Convert.ToInt32(PropulsionWheelInput) == 1)
                            //    {
                            //        return GetScaledValue(Convert.ToDouble(RPM), 107, 22, 140, 130);
                            //    }

                            //    else return 140;
                            //}
                            //else
                            //{
                            //    return 140;
                            //}
                            return 150;
                        }
                        else if (Convert.ToInt32(GrouperControlSwitchSelectionInput) == 2)
                        {
                            //if (Convert.ToInt32(ReverserWheelInput) == 1)
                            //{
                            //    if (Convert.ToInt32(PropulsionWheelInput) == 5 || Convert.ToInt32(PropulsionWheelInput) == 1)
                            //    {
                            //        return GetScaledValue(Convert.ToDouble(RPM), 150, 210, 106, 98);
                            //    }

                            //    else return 106;
                            //}
                            //else if (Convert.ToInt32(ReverserWheelInput) == 3)
                            //{
                            //    if (Convert.ToInt32(PropulsionWheelInput) == 5 || Convert.ToInt32(PropulsionWheelInput) == 1)
                            //    {
                            //        return GetScaledValue(Convert.ToDouble(RPM), 106, 46, 106, 90);
                            //    }

                            //    else return 106;
                            //}
                            //else
                            //{
                            //    return 106;
                            //}
                            return 106;
                        }

                        else
                        {
                            return 0;
                        }
                    }
                case 7:
                case 6:
                    return 70;
                case 2:
                case 3:
                    switch (Convert.ToInt32(GrouperControlSwitchSelectionInput))
                    {
                        case 1:
                            return 53;
                        case 2:
                            return 106;
                        default:
                            return 0;
                    }
                case 4:
                case 5:
                    return 106;
                default:
                    return 0;
            }
        }
        #endregion

        #region Excitation Current
        public int GetBowExcitationCurrent()
        {
            if ((Convert.ToInt32(ArmatureSelectorWheelInput) == 2 || Convert.ToInt32(ArmatureSelectorWheelInput) == 3) &&
                Convert.ToInt32(EmergencySwitch) == 0 && Convert.ToInt32(ArmatureBreakerInput) == 1)
            {
                switch (Convert.ToInt32(ReverserWheelInput))
                {
                    case 1://Forward
                        return Convert.ToInt32(PropulsionWheelInput) == 3 ? Convert.ToInt32(GetScaledValue(Convert.ToDouble(GovernorWheelInput), 35, 995, 237, 187)) :
                               Convert.ToInt32(GetScaledValue(Convert.ToDouble(GovernorWheelInput), 35, 995, 222, 187));
                    case 2:
                        return 162;
                    case 3://REverse
                        return Convert.ToInt32(PropulsionWheelInput) == 3 ? Convert.ToInt32(GetScaledValue(Convert.ToDouble(GovernorWheelInput), 995, 35, 126, 55)) :
                               Convert.ToInt32(GetScaledValue(Convert.ToDouble(GovernorWheelInput), 995, 35, 126, 76));
                    default:
                        return 162;
                }
            }

            return 162;
        }

        public int GetSternExcitationCurrent()
        {
            if ((Convert.ToInt32(ArmatureSelectorWheelInput) == 1 || Convert.ToInt32(ArmatureSelectorWheelInput) == 3)
                && Convert.ToInt32(ArmatureBreakerInput) == 1 && Convert.ToInt32(EmergencySwitch) == 0)
            {
                switch (Convert.ToInt32(ReverserWheelInput))
                {
                    case 1:
                        return Convert.ToInt32(PropulsionWheelInput) == 3 ? Convert.ToInt32(GetScaledValue(Convert.ToDouble(GovernorWheelInput), 35, 995, 223, 185)) :
                               Convert.ToInt32(GetScaledValue(Convert.ToDouble(GovernorWheelInput), 35, 995, 212, 185));
                    case 2:
                        return 160;
                    case 3:
                        return Convert.ToInt32(PropulsionWheelInput) == 3 ? Convert.ToInt32(GetScaledValue(Convert.ToDouble(GovernorWheelInput), 995, 35, 120, 54)) :
                            Convert.ToInt32(GetScaledValue(Convert.ToDouble(GovernorWheelInput), 995, 35, 120, 74));
                    default:
                        return 160;
                }
            }
            return 160;
        }
        #endregion

        #region Armature Current
        public int GetBowArmatureCurrent()
        {
            if (Convert.ToInt32(ArmatureSelectorWheelInput) == 2 || Convert.ToInt32(ArmatureSelectorWheelInput) == 3
                && Convert.ToInt32(EmergencySwitch) == 0)
            {
                switch (Convert.ToInt32(ReverserWheelInput))
                {
                    case 1:
                        return Convert.ToInt32(PropulsionWheelInput) == 3 ? 160 : (Convert.ToInt32(PropulsionWheelInput) == 2 || Convert.ToInt32(PropulsionWheelInput) == 4 ?
                            205 : (Convert.ToInt32(GrouperControlSwitchSelectionInput) == 2 ? Convert.ToInt32(GetScaledValue(Convert.ToDouble(RPMOriginal), 155, 207, 170, 198)) :
                            Convert.ToInt32(GetScaledValue(Convert.ToDouble(RPMOriginal), 155, 232, 170, 232))));
                    case 2:
                        return Convert.ToInt32(BowArmatureCurrent);
                    case 3:
                        return Convert.ToInt32(PropulsionWheelInput) == 3 ? 160 : (Convert.ToInt32(PropulsionWheelInput) == 2 || Convert.ToInt32(PropulsionWheelInput) == 4 ?
                            88 : (Convert.ToInt32(GrouperControlSwitchSelectionInput) == 2 ? Convert.ToInt32(GetScaledValue(Convert.ToDouble(RPMOriginal), 102, 49, 144, 100)) :
                            Convert.ToInt32(GetScaledValue(Convert.ToDouble(RPMOriginal), 102, 22, 144, 45))));

                    default:
                        return 160;
                }
            }
            return 160;
        }

        public int GetSternArmatureCurrent()
        {
            if (Convert.ToInt32(ArmatureSelectorWheelInput) == 1 || Convert.ToInt32(ArmatureSelectorWheelInput) == 3
                && Convert.ToInt32(EmergencySwitch) == 0)
            {
                switch (Convert.ToInt32(ReverserWheelInput))
                {
                    case 1:
                        return Convert.ToInt32(PropulsionWheelInput) == 3 ? 156 : (Convert.ToInt32(PropulsionWheelInput) == 2 || Convert.ToInt32(PropulsionWheelInput) == 4 ?
                            215 : (Convert.ToInt32(GrouperControlSwitchSelectionInput) == 2 ? Convert.ToInt32(GetScaledValue(Convert.ToDouble(RPMOriginal), 155, 207, 169, 205)) :
                            Convert.ToInt32(GetScaledValue(Convert.ToDouble(RPMOriginal), 155, 232, 169, 242))));
                    case 2:
                        return Convert.ToInt32(SternArmatureCurrent);
                    case 3:
                        return Convert.ToInt32(PropulsionWheelInput) == 3 ? 156 : (Convert.ToInt32(PropulsionWheelInput) == 2 || Convert.ToInt32(PropulsionWheelInput) == 4 ?
                           79 : (Convert.ToInt32(GrouperControlSwitchSelectionInput) == 2 ? Convert.ToInt32(GetScaledValue(Convert.ToDouble(RPMOriginal), 102, 49, 140, 90)) :
                           Convert.ToInt32(GetScaledValue(Convert.ToDouble(RPMOriginal), 102, 22, 140, 38))));

                    default:
                        return 154;
                }
            }
            return 156;
        }
        #endregion

        #region Charge And Discharge Battery Banks
        public int GetDischargeBatteryCurrent1()
        {
            if (Convert.ToInt32(ReverserWheelInput) == 1)
            {
                return Convert.ToInt32(PropulsionWheelInput) != 3 ?
                    (Convert.ToInt32(ArmatureSelectorWheelInput) == 3 || Convert.ToInt32(ArmatureSelectorWheelInput) == 2 ?
                    Convert.ToInt32(GetScaledValue(Convert.ToInt32(BowArmatureCurrent), 170, 232, 47, 215)) :
                    Convert.ToInt32(GetScaledValue(Convert.ToInt32(SternArmatureCurrent), 169, 242, 47, 215))) : 26;
            }
            else if (Convert.ToInt32(ReverserWheelInput) == 3)
            {
                return Convert.ToInt32(PropulsionWheelInput) != 3 ?
                    (Convert.ToInt32(ArmatureSelectorWheelInput) == 3 || Convert.ToInt32(ArmatureSelectorWheelInput) == 2 ?
                   (Convert.ToInt32(GetScaledValue(Convert.ToInt32(BowArmatureCurrent), 144, 45, 47, 215))) :
                   (Convert.ToInt32(GetScaledValue(Convert.ToInt32(SternArmatureCurrent), 140, 38, 47, 215)))) : 26;
            }
            return 26;
        }

        public int GetDischargeBatteryCurrent2()
        {
            if (Convert.ToInt32(ReverserWheelInput) == 1)
            {
                return Convert.ToInt32(PropulsionWheelInput) != 3 ?
                    (Convert.ToInt32(ArmatureSelectorWheelInput) == 3 || Convert.ToInt32(ArmatureSelectorWheelInput) == 2 ?
                    Convert.ToInt32(GetScaledValue(Convert.ToInt32(BowArmatureCurrent), 170, 232, 44, 198)) :
                    Convert.ToInt32(GetScaledValue(Convert.ToInt32(SternArmatureCurrent), 169, 242, 44, 198))) : 26;
            }
            else if (Convert.ToInt32(ReverserWheelInput) == 3)
            {
                return Convert.ToInt32(PropulsionWheelInput) != 3 ?
                    (Convert.ToInt32(ArmatureSelectorWheelInput) == 3 || Convert.ToInt32(ArmatureSelectorWheelInput) == 2 ?
                   (Convert.ToInt32(GetScaledValue(Convert.ToInt32(BowArmatureCurrent), 144, 45, 44, 198))) :
                   (Convert.ToInt32(GetScaledValue(Convert.ToInt32(SternArmatureCurrent), 140, 38, 44, 198)))) : 26;
            }
            return 26;
        }
        #endregion

        #region Tachometer
        public void SendRPMCommand()
        {
            fieldDevicesCommunicator.SendCommandToDevice(DeviceId, "RPMOriginal", "float", GetRpm().ToString());
            fieldDevicesCommunicator.SendCommandToDevice(DeviceId, "RPM", "float", GetRpmUpdated(GetRpm()).ToString());
        }
        public double GetRpmUpdated(double currentRpm)
        {
            return (currentRpm == 128 || currentRpm == 0) ? 0 : (currentRpm < 128 ? GetScaledValue(currentRpm, 107, 22, 100, 480) : GetScaledValue(currentRpm, 150, 232, 100, 480));
        }
        public double GetRpm()
        {

            if (Convert.ToInt32(GrouperControlSwitchSelectionInput) == 2)//Parallel
            {
                if (Convert.ToInt32(ReverserWheelInput) == 1 && Convert.ToInt32(EmergencySwitch) == 0) //Forward
                {
                    switch (Convert.ToInt32(PropulsionWheelInput))
                    {
                        case 3:
                            return 128;
                        case 4:
                            return 146;
                        case 5:
                            return GetScaledValue(Convert.ToInt32(GovernorWheelInput), 35, 995, 155, 181);
                        case 2:
                            return 168;
                        case 1:
                            return GetScaledValue(Convert.ToInt32(GovernorWheelInput), 35, 995, 168, 207);
                        default:
                            return 128;
                    }
                }
                else if (Convert.ToInt32(ReverserWheelInput) == 3 && Convert.ToInt32(EmergencySwitch) == 0)//Reverse
                {
                    switch (Convert.ToInt32(PropulsionWheelInput))
                    {
                        case 3:
                            return 128;
                        case 4:
                            return 111;
                        case 5:
                            return GetScaledValue(Convert.ToInt32(GovernorWheelInput), 32, 995, 102, 75);
                        case 2:
                            return 89;
                        case 1:
                            return GetScaledValue(Convert.ToInt32(GovernorWheelInput), 32, 995, 89, 49);
                        default:
                            return 128;
                    }
                }
                else if (Convert.ToInt32(EmergencySwitch) == 1)
                {
                    return 128;
                }
                else
                {
                    return 128;
                }
            }
            else if (Convert.ToInt32(GrouperControlSwitchSelectionInput) == 1)//Series
            {
                if (Convert.ToInt32(ReverserWheelInput) == 1 && Convert.ToInt32(EmergencySwitch) == 0) //Forward
                {
                    switch (Convert.ToInt32(PropulsionWheelInput))
                    {
                        case 3:
                            return 128;
                        case 4:
                            return 146;
                        case 5:
                            return GetScaledValue(Convert.ToInt32(GovernorWheelInput), 35, 995, 155, 181);
                        case 2:
                            return 168;
                        case 1:
                            return GetScaledValue(Convert.ToInt32(GovernorWheelInput), 35, 995, 168, 232);
                        default:
                            return 128;
                    }
                }
                else if (Convert.ToInt32(ReverserWheelInput) == 3 && Convert.ToInt32(EmergencySwitch) == 0)//Reverse
                {
                    switch (Convert.ToInt32(PropulsionWheelInput))
                    {
                        case 3:
                            return 128;
                        case 4:
                            return 111;
                        case 5:
                            return GetScaledValue(Convert.ToInt32(GovernorWheelInput), 32, 995, 102, 75);
                        case 2:
                            return 89;
                        case 1:
                            return GetScaledValue(Convert.ToInt32(GovernorWheelInput), 32, 995, 89, 22);
                        default:
                            return 128;
                    }
                }
                else if (Convert.ToInt32(EmergencySwitch) == 1)
                {
                    return 128;
                }
                else
                {
                    return 128;
                }
            }

            else
            {
                return 128;
            }

        }
        #endregion

        #region LedControl
        public int GetMotorStatus()
        {
            if (Convert.ToInt32(EmergencySwitch) == 0)
            {
                if (Convert.ToInt32(ArmatureSelectorWheelInput) == 1 && (Convert.ToInt32(PropulsionWheelInput) == 2 ||
                    Convert.ToInt32(PropulsionWheelInput) == 4) && Convert.ToInt32(GrouperControlSwitchModeInput) != 0 &&
                    Convert.ToInt32(GrouperControlSwitchSelectionInput) == 1)
                    return 465;//Series,Stern,Start-1,2
                else if (Convert.ToInt32(ArmatureSelectorWheelInput) == 2 && (Convert.ToInt32(PropulsionWheelInput) == 2 ||
                    Convert.ToInt32(PropulsionWheelInput) == 4)
                    && Convert.ToInt32(GrouperControlSwitchModeInput) != 0 &&
                    Convert.ToInt32(GrouperControlSwitchSelectionInput) == 1)
                    return 451;//Series,Bow,Start-1,2
                else if (Convert.ToInt32(ArmatureSelectorWheelInput) == 3 && (Convert.ToInt32(PropulsionWheelInput) == 2 ||
                   Convert.ToInt32(PropulsionWheelInput) == 4) && Convert.ToInt32(GrouperControlSwitchModeInput) != 0 &&
                   Convert.ToInt32(GrouperControlSwitchSelectionInput) == 1)
                    return 467;//Series,Both,Start-1,2
                else if (Convert.ToInt32(ArmatureSelectorWheelInput) == 2 && (Convert.ToInt32(PropulsionWheelInput) != 2 ||
                    Convert.ToInt32(PropulsionWheelInput) != 4) && Convert.ToInt32(ArmatureBreakerInput) == 1
                    && Convert.ToInt32(GrouperControlSwitchModeInput) != 0 && Convert.ToInt32(GrouperControlSwitchSelectionInput) == 1)
                    return 450;//Series,Bow,Run-1,2
                else if (Convert.ToInt32(ArmatureSelectorWheelInput) == 1 && (Convert.ToInt32(PropulsionWheelInput) != 2 ||
                    Convert.ToInt32(PropulsionWheelInput) != 4) && Convert.ToInt32(GrouperControlSwitchModeInput) != 0 &&
                    Convert.ToInt32(ArmatureBreakerInput) == 1 && Convert.ToInt32(GrouperControlSwitchSelectionInput) == 1)
                    return 464;//Series,Stern,Run-1,2
                else if (Convert.ToInt32(ArmatureSelectorWheelInput) == 3 && (Convert.ToInt32(PropulsionWheelInput) != 2 ||
                    Convert.ToInt32(PropulsionWheelInput) != 4) && Convert.ToInt32(GrouperControlSwitchModeInput) != 0 &&
                    Convert.ToInt32(ArmatureBreakerInput) == 1 && Convert.ToInt32(GrouperControlSwitchSelectionInput) == 1)
                    return 466;//Series,Both,Run-1,2

                else if (Convert.ToInt32(ArmatureSelectorWheelInput) == 2 && (Convert.ToInt32(PropulsionWheelInput) == 2 ||
                    Convert.ToInt32(PropulsionWheelInput) == 4) && Convert.ToInt32(GrouperControlSwitchModeInput) != 0 &&
                    Convert.ToInt32(GrouperControlSwitchSelectionInput) == 2)
                    return 1219;//Parallel,Bow,Start-1,2        
                else if (Convert.ToInt32(ArmatureSelectorWheelInput) == 1 && (Convert.ToInt32(PropulsionWheelInput) == 2 ||
                    Convert.ToInt32(PropulsionWheelInput) == 4) && Convert.ToInt32(GrouperControlSwitchModeInput) != 0 &&
                    Convert.ToInt32(GrouperControlSwitchSelectionInput) == 2)
                    return 1233;//Parallel,Stern,Start-1,2
                else if (Convert.ToInt32(ArmatureSelectorWheelInput) == 3 && (Convert.ToInt32(PropulsionWheelInput) == 2 ||
                     Convert.ToInt32(PropulsionWheelInput) == 4) && Convert.ToInt32(GrouperControlSwitchModeInput) != 0 &&
                     Convert.ToInt32(GrouperControlSwitchSelectionInput) == 2)
                    return 1235;//paralell,Both-,Start-1,2
                else if (Convert.ToInt32(ArmatureSelectorWheelInput) == 2 && (Convert.ToInt32(PropulsionWheelInput) != 2 ||
                     Convert.ToInt32(PropulsionWheelInput) != 4) && Convert.ToInt32(GrouperControlSwitchModeInput) != 0 &&
                     Convert.ToInt32(GrouperControlSwitchSelectionInput) == 2)
                    return 1218;//Parallel,Bow-,Run-1,2
                else if (Convert.ToInt32(ArmatureSelectorWheelInput) == 1 && (Convert.ToInt32(PropulsionWheelInput) != 2 ||
                    Convert.ToInt32(PropulsionWheelInput) != 4) && Convert.ToInt32(GrouperControlSwitchModeInput) != 0
                    && Convert.ToInt32(ArmatureBreakerInput) == 1 && Convert.ToInt32(GrouperControlSwitchSelectionInput) == 2)
                    return 1232;//Parallel,Stern,Run-1,2
                else if (Convert.ToInt32(ArmatureSelectorWheelInput) == 3 && (Convert.ToInt32(PropulsionWheelInput) != 2 ||
                    Convert.ToInt32(PropulsionWheelInput) != 4) && Convert.ToInt32(GrouperControlSwitchModeInput) != 0
                    && Convert.ToInt32(ArmatureBreakerInput) == 1 && Convert.ToInt32(GrouperControlSwitchSelectionInput) == 2)
                    return 1234;//Parallel,Both,Run-1,2


                else if (Convert.ToInt32(GrouperControlSwitchModeInput) != 0 && Convert.ToInt32(GrouperControlSwitchSelectionInput) == 1)
                    return 448;//Series
                else if (Convert.ToInt32(GrouperControlSwitchModeInput) != 0 && Convert.ToInt32(GrouperControlSwitchSelectionInput) == 2)
                    return 1216;//Parallel
                else if (Convert.ToInt32(GrouperControlSwitchModeInput) != 0 && Convert.ToInt32(GrouperControlSwitchSelectionInput) == 0)
                    return 704;// off
                else
                    return 0;
            }
            else
            {
                return 32;
            }

        }


        #endregion

        #region Interlocking Operation
        private void Operation()
        {
            switch (Scenario())
            {
                case 1:
                    //Console.WriteLine("In Case 1");
                    PropulsionWheelInputLock = "255";
                    ReverserWheelLock = "0";
                    ArmatureSelectorWheelLock = "0";
                    break;
                case 2:
                    //Console.WriteLine("In Case 2");
                    PropulsionWheelInputLock = "255";
                    ReverserWheelLock = "255";
                    ArmatureSelectorWheelLock = "0";
                    break;
                case 3://Contacts Check
                    //Console.WriteLine("In Case 3");
                    PropulsionWheelInputLock = "0";
                    ReverserWheelLock = "0";
                    ArmatureSelectorWheelLock = "255";
                    break;
                case 4://Armature Selected And Breaker ON
                    //Console.WriteLine("In Case 4");
                    PropulsionWheelInputLock = "255";
                    ReverserWheelLock = "0";
                    ArmatureSelectorWheelLock = "255";
                    break;
                case 5://Motor Running Sequence Start
                    //Console.WriteLine("In Case 5");
                    PropulsionWheelInputLock = "0";
                    ReverserWheelLock = "0";
                    ArmatureSelectorWheelLock = "255";
                    break;
                case 6://Motor Running
                case 7:
                    //Console.WriteLine("In Case 6");
                    PropulsionWheelInputLock = "0";
                    ReverserWheelLock = "255";
                    ArmatureSelectorWheelLock = "255";
                    break;
                default:
                    //Console.WriteLine("In Default");
                    break;
            }
            Task.Factory.StartNew(new Action(() => Thread.Sleep(100))).ContinueWith((t) => Operation());
        }
        public int Scenario()
        {
            if (Convert.ToInt32(ArmatureBreakerInput) == 0 && Convert.ToInt32(ReverserWheelInput) == 2 &&
                Convert.ToInt32(ArmatureSelectorWheelInput) != 4 && Convert.ToInt32(PropulsionWheelInput) == 3)//Full OFF Condition
                return 1;
            else if (Convert.ToInt32(ArmatureBreakerInput) == 0 && Convert.ToInt32(ReverserWheelInput) == 2 &&
                Convert.ToInt32(ArmatureSelectorWheelInput) != 4 && Convert.ToInt32(PropulsionWheelInput) == 3)//Armature Selected
                return 2;
            else if (Convert.ToInt32(ArmatureBreakerInput) == 0 && Convert.ToInt32(ReverserWheelInput) != 2 &&
                Convert.ToInt32(ArmatureSelectorWheelInput) != 4 && Convert.ToInt32(PropulsionWheelInput) == 3)//Contacts Check
                return 3;
            else if (Convert.ToInt32(ArmatureBreakerInput) == 1 && Convert.ToInt32(ReverserWheelInput) == 2 &&
                Convert.ToInt32(ArmatureSelectorWheelInput) != 4 && Convert.ToInt32(PropulsionWheelInput) == 3)//Armature Selected And Breaker ON
                return 4;
            else if (Convert.ToInt32(ArmatureBreakerInput) == 1 && Convert.ToInt32(ReverserWheelInput) != 2 &&
                Convert.ToInt32(ArmatureSelectorWheelInput) != 4
                && (Convert.ToInt32(PropulsionWheelInput) == 3 || Convert.ToInt32(PropulsionWheelInput) == 4))//Motor Running Sequence Start
                return 5;
            else if (Convert.ToInt32(ArmatureBreakerInput) == 1 && Convert.ToInt32(ReverserWheelInput) != 2 &&
                Convert.ToInt32(ArmatureSelectorWheelInput) != 4 && (Convert.ToInt32(PropulsionWheelInput) != 3 &&
                Convert.ToInt32(PropulsionWheelInput) != 4))//Motor Running 
                return 6;
            else if (Convert.ToInt32(ArmatureBreakerInput) == 0 && Convert.ToInt32(ReverserWheelInput) != 2 &&
                Convert.ToInt32(ArmatureSelectorWheelInput) != 4 && (Convert.ToInt32(PropulsionWheelInput) != 3 &&
                Convert.ToInt32(PropulsionWheelInput) != 4))//Motor Running 
                return 7;
            else
                return 0;
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
            get => "Motor_1";
        }

        public string DeviceLabel
        {
            get => "Main Motor Panel";
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
            get => _reverserWheelInput ?? "1";
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

        private string _rpmOriginal;
        public string RPMOriginal
        {
            get => _rpmOriginal;
            set => SetProperty(ref _rpmOriginal, value);
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
