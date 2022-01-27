using E3.AuditTrailManager.Model;
using E3.AuditTrailManager.Model.Enums;
using E3.ReactorManager.Interfaces.HardwareAbstractionLayer;
using E3.ReactorManager.Interfaces.HardwareAbstractionLayer.Data;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Anathem.Ui.ViewModels
{
    public class ReactorControlViewModel : BindableBase, IRegionMemberLifetime
    {
        private readonly IFieldDevicesCommunicator fieldDevicesCommunicator;
        private readonly IRegionManager regionManager;
        private readonly TaskScheduler taskScheduler;

        public ReactorControlViewModel(IFieldDevicesCommunicator fieldDevicesCommunicator, IRegionManager regionManager)
        {
            taskScheduler = TaskScheduler.FromCurrentSynchronizationContext();
            this.fieldDevicesCommunicator = fieldDevicesCommunicator;
            this.regionManager = regionManager;
            this.fieldDevicesCommunicator.FieldPointDataReceived += FieldDevicesCommunicator_FieldPointDataReceived;
            Task.Factory.StartNew(new Action(LoadDeviceParametersFromHardwareLayer))
                .ContinueWith(t => UpdateUi());
        }

        private void LoadDeviceParametersFromHardwareLayer()
        {
            foreach (SensorsDataSet sensorsDataSet in fieldDevicesCommunicator.GetFieldDeviceData(DeviceId).SensorsData)
            {
                foreach (FieldPoint fieldPoint in sensorsDataSet.SensorsFieldPoints)
                {
                    if (fieldPoint.TypeOfAddress == "FieldPoint")
                    {
                        if (ParameterDictionary.ContainsKey(fieldPoint.Label))
                        {
                            ParameterDictionary[fieldPoint.Label] = fieldPoint.Value;
                        }
                        else
                        {
                            ParameterDictionary.Add(fieldPoint.Label, fieldPoint.Value);
                        }
                    }
                }
            }
        }

        private void FieldDevicesCommunicator_FieldPointDataReceived(object sender, FieldPointDataReceivedArgs liveData)
        {
            if (ParameterDictionary.ContainsKey(liveData.FieldPointIdentifier))
            {
                ParameterDictionary[liveData.FieldPointIdentifier] = liveData.NewFieldPointData;
                UpdateUi();
            }
            StirrerCurrentSpeed_1 = Convert.ToString(Convert.ToDouble(ParameterDictionary["StirrerFeedback_1"]));

            StirrerCurrentSpeed_2 = Convert.ToString(Convert.ToDouble(ParameterDictionary["StirrerFeedback_2"]));

            StirrerCurrentSpeed_3 = Convert.ToString(Convert.ToDouble(ParameterDictionary["StirrerFeedback_3"]));



        }
        private void ChangeChillerSetPoint()
        {
            var newChillerSetPoint = Convert.ToSingle(ChillerSetPoint);
            if (newChillerSetPoint >= -60 && newChillerSetPoint <= 200)
            {
                fieldDevicesCommunicator
                        .SendCommandToDevice(DeviceId, "ChillerSetpoint", "double", newChillerSetPoint.ToString());
                //fieldDevicesCommunicator
                //        .SendCommandToDevice(DeviceId, "HcCommandInProgress", "bool", bool.TrueString);
                ////auditTrailManager.RecordEventAsync($"Changed {DeviceLabel} HC SetPoint from {OldHcSetPoint} to {newHCSetPoint}", UserDetails.Name, EventTypeEnum.ChangedSetPoint);
            }
            else
            {
                MessageBox.Show("Please Enter Chiller SetPoint between -60 and 200",
                                "Chiller SetPoint Exceeded Limit Error",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
        }
        private void ChangeStirrer1SetPoint()
        {
            var newStirrer1Setpoint = Convert.ToSingle(StirrerSetpoint_1);

            //StirrerSetpoint_1 = newStirrer1Setpoint.ToString();
            if (newStirrer1Setpoint >= 0 && newStirrer1Setpoint <= 200)
            {
                fieldDevicesCommunicator
                        .SendCommandToDevice(DeviceId, "StirrerSetpoint_1", "double", newStirrer1Setpoint.ToString());
                //fieldDevicesCommunicator
                //        .SendCommandToDevice(DeviceId, "HcCommandInProgress", "bool", bool.TrueString);
                //AuditTrailManager.RecordEventAsync($"Changed {DeviceLabel} HC SetPoint  {newStirrer1Setpoint}", UserDetails.Name, EventTypeEnum.ChangedSetPoint);
            }
            else
            {
                MessageBox.Show("Please Enter Stirrer SetPoint between 0 and 200",
                                "Stirrer SetPoint Exceeded Limit Error",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
            //StirrerSetpoint_1 = Convert.ToString(Convert.ToSingle(ParameterDictionary["StirrerSetpoint_1"]));

        }
        private void ChangeStirrer2SetPoint()
        {
            var newStirrer2Setpoint = Convert.ToSingle(StirrerSetpoint_2);
            if (newStirrer2Setpoint >= 0 && newStirrer2Setpoint <= 200)
            {
                fieldDevicesCommunicator
                        .SendCommandToDevice(DeviceId, "StirrerSetpoint_2", "double", newStirrer2Setpoint.ToString());
                //fieldDevicesCommunicator
                //        .SendCommandToDevice(DeviceId, "HcCommandInProgress", "bool", bool.TrueString);
                ////auditTrailManager.RecordEventAsync($"Changed {DeviceLabel} HC SetPoint from {OldHcSetPoint} to {newHCSetPoint}", UserDetails.Name, EventTypeEnum.ChangedSetPoint);
            }
            else
            {
                MessageBox.Show("Please Enter Stirrer SetPoint between 0 and 200",
                                "Stirrer SetPoint Exceeded Limit Error",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
        }
        private void ChangeStirrer3SetPoint()
        {
            var newStirrer3Setpoint = Convert.ToSingle(StirrerSetpoint_3);
            if (newStirrer3Setpoint >= 0 && newStirrer3Setpoint <= 200)
            {
                fieldDevicesCommunicator
                        .SendCommandToDevice(DeviceId, "StirrerSetpoint_3", "double", newStirrer3Setpoint.ToString());
                //fieldDevicesCommunicator
                //        .SendCommandToDevice(DeviceId, "HcCommandInProgress", "bool", bool.TrueString);
                ////auditTrailManager.RecordEventAsync($"Changed {DeviceLabel} HC SetPoint from {OldHcSetPoint} to {newHCSetPoint}", UserDetails.Name, EventTypeEnum.ChangedSetPoint);
            }
            else
            {
                MessageBox.Show("Please Enter Stirrer SetPoint between 0 and 200",
                                "Stirrer SetPoint Exceeded Limit Error",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
        }

        private void UpdateUi()
        {
            Task.Factory.StartNew(new Action(() => RaisePropertyChanged(nameof(ParameterDictionary))), CancellationToken.None, TaskCreationOptions.None, taskScheduler);
        }


        public ICommand SendCommandToDevice
        {
            get => new DelegateCommand<string>(str => {
                string[] strArray = str.Split('|');
                fieldDevicesCommunicator.SendCommandToDevice(DeviceId, strArray[0], strArray[1], strArray[2]);
            });
        }
        public ICommand ChangeChillerSetPointCommand
        {
            get => new DelegateCommand(ChangeChillerSetPoint);
        }
        public ICommand ChangeStirrer1SetPointCommand
        {
            get => new DelegateCommand(ChangeStirrer1SetPoint);
        }
        public ICommand ChangeStirrer2SetPointCommand
        {
            get => new DelegateCommand(ChangeStirrer2SetPoint);
        }
        public ICommand ChangeStirrer3SetPointCommand
        {
            get => new DelegateCommand(ChangeStirrer3SetPoint);
        }
        public ICommand NavigateCommand
        {
            get => new DelegateCommand<string>(str => regionManager.RequestNavigate("SelectedViewPane", str));
        }

        public bool KeepAlive { get; set; } = false;
        public string DeviceId => "Reactor_1";
        public Dictionary<string, string> ParameterDictionary { get; set; } = new Dictionary<string, string>();

        private string _stirrerCurrentSpeed_1;
        public string StirrerCurrentSpeed_1
        {
            get { return _stirrerCurrentSpeed_1; }
            set
            {
                _stirrerCurrentSpeed_1 = value;
                RaisePropertyChanged();
            }
        }
        private string _stirrerCurrentSpeed_2;
        public string StirrerCurrentSpeed_2
        {
            get { return _stirrerCurrentSpeed_2; }
            set
            {
                _stirrerCurrentSpeed_2 = value;
                RaisePropertyChanged();
            }
        }
        private string _stirrerCurrentSpeed_3;
        public string StirrerCurrentSpeed_3
        {
            get { return _stirrerCurrentSpeed_3; }
            set
            {
                _stirrerCurrentSpeed_3 = value;
                RaisePropertyChanged();
            }
        }
        private string heatCoolSetPoint = "0";
        public string HeatCoolSetPoint
        {
            get { return heatCoolSetPoint; }
            set { SetProperty(ref heatCoolSetPoint, value); }
        }
        private string chillerSetPoint = "25";
        public string ChillerSetPoint
        {
            get { return chillerSetPoint; }
            set { SetProperty(ref chillerSetPoint, value); }
        }
        private string stirrerSetpoint_1 ="0";
        public string StirrerSetpoint_1
        {
            get { return stirrerSetpoint_1; }
            set { SetProperty(ref stirrerSetpoint_1, value); }
        }
        private string stirrerSetpoint_2 = "0";
        public string StirrerSetpoint_2
        {
            get { return stirrerSetpoint_2; }
            set { SetProperty(ref stirrerSetpoint_2, value); }
        }
        private string stirrerSetpoint_3 = "0";
        public string StirrerSetpoint_3
        {
            get { return stirrerSetpoint_3; }
            set { SetProperty(ref stirrerSetpoint_3, value); }
        }
    }
}
