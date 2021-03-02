using E3.AuditTrailManager.Model;
using E3.AuditTrailManager.Model.Enums;
using E3.Mediator.Models;
using E3.Mediator.Services;
using E3.ReactorManager.Interfaces.DataAbstractionLayer;
using E3.ReactorManager.Interfaces.HardwareAbstractionLayer;
using E3.ReactorManager.Interfaces.HardwareAbstractionLayer.Data;
using E3.UserManager.Model.Data;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Basf.Ui.ViewModels
{
    public class ReactorControlViewModel : BindableBase, IRegionMemberLifetime
    {
        private readonly IFieldDevicesCommunicator fieldDevicesCommunicator;
        private readonly IAuditTrailManager auditTrailManager;
        private readonly IRegionManager regionmanager;
        private readonly TaskScheduler taskScheduler;

        public ReactorControlViewModel(IFieldDevicesCommunicator fieldDevicesCommunicator, MediatorService mediatorService, IAuditTrailManager auditTrailManager, IDatabaseReader databaseReader, IRegionManager regionmanager)
        {
            taskScheduler = TaskScheduler.FromCurrentSynchronizationContext();
            this.fieldDevicesCommunicator = fieldDevicesCommunicator;
            this.auditTrailManager = auditTrailManager;
            this.regionmanager = regionmanager;
            this.fieldDevicesCommunicator.FieldPointDataReceived += OnLiveDataReceived;
            Task.Factory.StartNew(new Action(LoadDeviceParametersFromHardwareLayer))
                .ContinueWith(t => UpdateUi());
            Task.Factory.StartNew(new Func<DataTable>(() => {
                return databaseReader.ExecuteReadCommand($"select * from dbo.Equipments where FieldDeviceConnectedTo='{DeviceId}'", CommandType.Text);
            })).ContinueWith(t => {
                if (t.IsCompleted && t.Exception == null)
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
            }).ContinueWith(t => {
                RaisePropertyChanged(nameof(ConnectedHc));
                RaisePropertyChanged(nameof(ConnectedStirrer));
            }, taskScheduler);
            mediatorService.NotifyColleagues(InMemoryMediatorMessageContainer.UpdateSelectedDeviceId, new Device
            {
                Id = DeviceId,
                Label = DeviceLabel,
                Type = "Reactor"
            });
            UserDetails = (User)Application.Current.Resources["LoggedInUser"];
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
                        UpdateSetPoints(fieldPoint.Label, fieldPoint.Value);
                    }
                }
            }
        }

        private void OnLiveDataReceived(object sender, FieldPointDataReceivedArgs liveData)
        {
            if (ParameterDictionary.ContainsKey(liveData.FieldPointIdentifier))
            {
                ParameterDictionary[liveData.FieldPointIdentifier] = liveData.NewFieldPointData;
                UpdateUi();
            }
            UpdateSetPoints(liveData.FieldPointIdentifier, liveData.NewFieldPointData);
        }

        private void UpdateSetPoints(string fpId, string data)
        {
            if (fpId.Contains(nameof(HeatCoolSetPoint)))
            {
                HeatCoolSetPoint = data;
            }
            else if (fpId.Contains(nameof(StirrerSpeedSetPoint)))
            {
                StirrerSpeedSetPoint = data;
            }
        }

        private void UpdateUi()
        {
            Task.Factory.StartNew(new Action(() => RaisePropertyChanged(nameof(ParameterDictionary))), CancellationToken.None, TaskCreationOptions.None, taskScheduler);
        }

        private void ChangeStirrerSetPoint()
        {
            //first convert the user entered stirrer Speed SetPoint to integer
            var toBeSetStirrerSpeedSetPoint = Convert.ToInt16(StirrerSpeedSetPoint);

            if (toBeSetStirrerSpeedSetPoint <= 200 && toBeSetStirrerSpeedSetPoint >= 0)
            {
                fieldDevicesCommunicator
                        .SendCommandToDevice(DeviceId, "StirrerSpeedSetPoint", "int", toBeSetStirrerSpeedSetPoint.ToString());
                auditTrailManager.RecordEventAsync($"Changed {DeviceId} Stirrer Speed SetPoint from {OldStirrerSetPoint} to {toBeSetStirrerSpeedSetPoint}", UserDetails.Name, EventTypeEnum.ChangedSetPoint);
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

        private void ChangeHcSetPoint()
        {
            var newHCSetPoint = Convert.ToSingle(HeatCoolSetPoint);
            if (newHCSetPoint >= -90 && newHCSetPoint <= 200)
            {
                fieldDevicesCommunicator
                        .SendCommandToDevice(DeviceId, "HeatCoolSetPoint", "float", newHCSetPoint.ToString());
                auditTrailManager.RecordEventAsync($"Changed {DeviceLabel} HC SetPoint from {OldHcSetPoint} to {newHCSetPoint}", UserDetails.Name, EventTypeEnum.ChangedSetPoint);
            }
            else
            {
                MessageBox.Show("Please Enter HC SetPoint between -90 and 200",
                                "HC SetPoint Exceeded Limit Error",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
        }

        #region Commands
        public ICommand ChangeStirrerSetPointCommand
        {
            get => new DelegateCommand(ChangeStirrerSetPoint);
        }
        public ICommand ChangeHcSetPointCommand
        {
            get => new DelegateCommand(ChangeHcSetPoint);
        }
        public ICommand SaveOldHcSetPointCommand
        {
            get => new DelegateCommand<object>(sp => OldHcSetPoint = (string)sp);
        }
        public ICommand SaveOldStirrerSpeedSetPointCommand
        {
            get => new DelegateCommand<object>(sp => OldStirrerSetPoint = (string)sp);
        }
        public ICommand SendCommandToDevice
        {
            get => new DelegateCommand<string>(str => {
                string[] strArray = str.Split('|');
                fieldDevicesCommunicator.SendCommandToDevice(DeviceId, strArray[0], strArray[1], strArray[2]);
            });
        }
        public ICommand NavigateCommand
        {
            get => new DelegateCommand<object>(page => regionmanager.RequestNavigate("SelectedViewPane", (string)page));
        }
        #endregion

        #region Properties
        public bool KeepAlive { get => false; }
        public string OldStirrerSetPoint { get; set; } = "0";
        private string stirrerSpeedSetPoint = "0";
        public string StirrerSpeedSetPoint
        {
            get { return stirrerSpeedSetPoint; }
            set { SetProperty(ref stirrerSpeedSetPoint, value); }
        }
        public string OldHcSetPoint { get; set; } = "0";
        private string heatCoolSetPoint = "0";
        public string HeatCoolSetPoint
        {
            get { return heatCoolSetPoint; }
            set { SetProperty(ref heatCoolSetPoint, value); }
        }
        public User UserDetails { get; set; }
        public string ConnectedHc { get; set; } = string.Empty;
        public string ConnectedStirrer { get; set; } = string.Empty;
        public string DeviceId => "Reactor_1";
        public string DeviceLabel => "ATR-30L";
        public Dictionary<string, string> ParameterDictionary { get; set; } = new Dictionary<string, string>();
        #endregion
    }
}
