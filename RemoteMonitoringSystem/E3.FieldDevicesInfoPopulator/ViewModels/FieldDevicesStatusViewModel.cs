using E3.FieldDevicesInfoPopulator.Model;
using E3.FieldDevicesInfoPopulator.Model.Data;
using E3.ReactorManager.Interfaces.HardwareAbstractionLayer;
using E3.ReactorManager.Interfaces.HardwareAbstractionLayer.Data;
using E3.SystemAlarmManager.Models;
using E3.SystemAlarmManager.Services;
using Prism.Commands;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Unity;

namespace E3.FieldDevicesInfoPopulator.ViewModels
{
    public class FieldDevicesStatusViewModel : BindableBase
    {
        IFieldDevicesCommunicator fieldDevicesCommunicator;
        DeviceStatusToParametersViewNavigator parametersViewNavigator;
        private readonly ISystemAlarmsManager systemAlarmsManager;

        public FieldDevicesStatusViewModel(IUnityContainer containerProvider, ISystemAlarmsManager systemAlarmsManager)
        {
            fieldDevicesCommunicator = containerProvider.Resolve<IFieldDevicesCommunicator>();
            fieldDevicesCommunicator.FieldPointDataReceived += OnFieldPointDataReceived;
            parametersViewNavigator = containerProvider.Resolve<DeviceStatusToParametersViewNavigator>();
            this.systemAlarmsManager = systemAlarmsManager;
            this.systemAlarmsManager.RefreshSystemAlarms += SystemAlarmsManager_RefreshSystemAlarms;
        }

        private void SystemAlarmsManager_RefreshSystemAlarms(IEnumerable<SystemAlarm> systemAlarms)
        {
            IEnumerable<IGrouping<string, SystemAlarm>> groupCollection = systemAlarms.Where(alarm => alarm.State != AlarmState.Resolved).GroupBy(alarm => alarm.DeviceId);

            foreach (FieldDeviceStatus fieldDeviceStatus in FieldDevicesStatusCollection)
            {
                if (groupCollection.Any(g => g.Key == fieldDeviceStatus.Identifier))
                {
                    fieldDeviceStatus.AlarmStatus = true;
                }
                else
                {
                    fieldDeviceStatus.AlarmStatus = false;
                }
            }
            RaisePropertyChanged(nameof(FieldDevicesStatusCollection));
        }

        private void OnFieldPointDataReceived(object sender, FieldPointDataReceivedArgs e)
        {
            if (e.FieldPointIdentifier == "Status")
            {
                FieldDevicesStatusCollection.Where(item => item.Identifier == e.FieldDeviceIdentifier).ToList()
                    .ForEach(item => item.Status = e.NewFieldPointData);
            }
            RaisePropertyChanged(nameof(FieldDevicesStatusCollection));
        }

        private void UpdateFieldDevicesStatusOnPageLoad()
        {
            foreach (FieldDevice fieldDevice in fieldDevicesCommunicator.GetAllFieldDevicesData())
            {
                SensorsDataSet statusSensorsDataSet
                    = fieldDevice.SensorsData.Where(sensorsDataSet => sensorsDataSet.Label.Contains("Status")).ToList().FirstOrDefault();
                if (statusSensorsDataSet != null)
                {
                    FieldPoint statusFieldPoint = statusSensorsDataSet.SensorsFieldPoints.Where(fieldPoint => fieldPoint.Label == "Status").FirstOrDefault();
                    if (statusFieldPoint != null)
                    {
                        if (FieldDevicesStatusCollection.Any(item => item.Identifier == fieldDevice.Identifier))
                        {
                            FieldDevicesStatusCollection.Where(item => item.Identifier == fieldDevice.Identifier).ToList()
                                .ForEach(item => item.Status = statusFieldPoint.Value);
                        }
                        else
                        {
                            FieldDevicesStatusCollection.Add(new FieldDeviceStatus
                            {
                                Identifier = fieldDevice.Identifier,
                                Label = fieldDevice.Label,
                                Status = statusFieldPoint.Value,
                                DeviceType = fieldDevice.Type,
                                NavigateToParametersViewCommand = this.NavigateToParametersViewCommand,
                            });
                        }
                    }
                    else
                    {
                        FieldDevicesStatusCollection.Add(new FieldDeviceStatus
                        {
                            Identifier = fieldDevice.Identifier,
                            Label = fieldDevice.Label,
                            Status = bool.FalseString,
                            DeviceType = fieldDevice.Type,
                            NavigateToParametersViewCommand = this.NavigateToParametersViewCommand,
                        });
                    }
                    RaisePropertyChanged("FieldDevicesStatusCollection");
                }
            }
        }

        public void NavigateToParametersView(string deviceType)
        {
            parametersViewNavigator.SwitchToParametersView(deviceType);
        }

        #region Commands
        private ICommand _updateFieldDevicesStatusCommand;
        public ICommand UpdateFieldDevicesStatusCommand
        {
            get => _updateFieldDevicesStatusCommand ?? (_updateFieldDevicesStatusCommand = new DelegateCommand(UpdateFieldDevicesStatusOnPageLoad));
            set => _updateFieldDevicesStatusCommand = value;
        }

        private ICommand _navigateToParametersViewCommand;
        public ICommand NavigateToParametersViewCommand
        {
            get => _navigateToParametersViewCommand ?? (_navigateToParametersViewCommand = new DelegateCommand<string>(NavigateToParametersView));
            set => SetProperty(ref _navigateToParametersViewCommand, value);
        }
        #endregion

        private ObservableCollection<FieldDeviceStatus> _fieldDevicesStatusCollection;
        public ObservableCollection<FieldDeviceStatus> FieldDevicesStatusCollection
        {
            get => _fieldDevicesStatusCollection ?? (_fieldDevicesStatusCollection = new ObservableCollection<FieldDeviceStatus>());
            set
            {
                _fieldDevicesStatusCollection = value;
                RaisePropertyChanged();
            }
        }

    }

}
