using E3.DialogServices.Model;
using E3.FieldDevicesInfoPopulator.Model.Data;
using E3.FieldDevicesInfoPopulator.Views;
using E3.ReactorManager.Interfaces.DataAbstractionLayer;
using E3.ReactorManager.Interfaces.DataAbstractionLayer.Data;
using E3.ReactorManager.Interfaces.HardwareAbstractionLayer;
using E3.ReactorManager.Interfaces.HardwareAbstractionLayer.Data;
using E3.SystemAlarmManager.Services;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Unity;

namespace E3.FieldDevicesInfoPopulator.ViewModels
{
    public class FieldDeviceParameterInfoViewModel : BindableBase
    {
        private readonly IDatabaseReader databaseReader;
        private readonly IDatabaseWriter databaseWriter;
        private readonly IDialogServiceProvider dialogServiceProvider;
        private readonly ISystemAlarmsManager systemAlarmsManager;
        private readonly IUnityContainer unityContainer;
        private readonly IFieldDevicesCommunicator fieldDevicesCommunicator;

        public FieldDeviceParameterInfoViewModel(IFieldDevicesCommunicator fieldDevicesCommunicator, IDialogServiceProvider dialogServiceProvider, ISystemAlarmsManager systemAlarmsManager, IUnityContainer containerProvider, IDatabaseReader databaseReader, IDatabaseWriter databaseWriter)
        {
            this.fieldDevicesCommunicator = fieldDevicesCommunicator;
            this.systemAlarmsManager = systemAlarmsManager;
            this.systemAlarmsManager.RefreshSystemAlarms += SystemAlarmsManager_RefreshSystemAlarms;
            this.unityContainer = containerProvider;
            this.dialogServiceProvider = dialogServiceProvider;
            this.databaseReader = databaseReader;
            this.databaseWriter = databaseWriter;
        }

        private void SystemAlarmsManager_RefreshSystemAlarms(IEnumerable<SystemAlarmManager.Models.SystemAlarm> systemAlarms)
        {
            if (systemAlarms.Where(alarm => alarm.DeviceId == DeviceId).All(alarm => alarm.State == SystemAlarmManager.Models.AlarmState.Resolved))
            {
                AlarmsRaised = false;
                //Update Parameter Alarms
                foreach (KeyValuePair<string, string> keyValuePair in ParametersData)
                {
                    ParametersAlarms[keyValuePair.Key] = false;
                }
                RaisePropertyChanged(nameof(ParametersAlarms));
            }
            else
            {
                AlarmsRaised = true;

                IEnumerable<SystemAlarmManager.Models.SystemAlarm> filteredAlarms
                    = systemAlarms.Where(alarm => alarm.DeviceId == DeviceId && alarm.State != SystemAlarmManager.Models.AlarmState.Resolved);
                //Update Parameter Alarms
                foreach (KeyValuePair<string, string> keyValuePair in ParametersData)
                {
                    if (filteredAlarms.Any(alarm => alarm.FieldPointLabel.Contains(keyValuePair.Key)))
                    {
                        ParametersAlarms[keyValuePair.Key] = true;
                    }
                    else
                    {
                        ParametersAlarms[keyValuePair.Key] = false;
                    }
                }
                RaisePropertyChanged(nameof(ParametersAlarms));
            }
        }

        public void SubscribeToFieldDeviceCommunicator() => fieldDevicesCommunicator.FieldPointDataReceived += OnFieldPointDataReceived;

        private void OnFieldPointDataReceived(object sender, FieldPointDataReceivedArgs liveData)
        {
            if (DeviceId == liveData.FieldDeviceIdentifier)
            {
                if (ParametersData.ContainsKey(liveData.FieldPointIdentifier))
                {
                    UpdateParameterInfo(liveData.FieldPointIdentifier, liveData.NewFieldPointData);
                }
            }
        }

        #region Parameter Data handlers
        public void PrepareParametersDataDictionary(IList<ParameterInfo> fieldDeviceParameters)
        {
            foreach (ParameterInfo parameterInfo in fieldDeviceParameters)
            {
                if (ContainsParameter(parameterInfo.Name))
                {
                    //Don't add the parameter to the Dictionary
                }
                else
                {
                    AddParameterInfo(parameterInfo.Name, parameterInfo.Value);
                }
            }
        }

        public void UpdateParameterInfo(string paramName, string paramValue)
        {
            if (ParametersData.ContainsKey(paramName))
            {
                ParametersData[paramName] = paramValue;
                RaisePropertyChanged(nameof(ParametersData));
            }
        }

        public bool ContainsParameter(string paramName)
        {
            if (ParametersData.ContainsKey(paramName))
            {
                return true;
            }

            return false;
        }

        public void AddParameterInfo(string paramName, string paramValue)
        {
            if (!ParametersData.ContainsKey(paramName))
            {
                //Add to the ParametersData dictionary only if it does not contains given param
                ParametersData.Add(paramName, paramValue);
                ParametersAlarms.Add(paramName, false);
            }
        }
        #endregion

        #region Maintenance Date handlers
        public void UpdateMaintenanceDate()
        {
            ModifyMaintenaceDateViewModel modifyMaintenaceDateViewModel = unityContainer.Resolve<ModifyMaintenaceDateViewModel>();
            modifyMaintenaceDateViewModel.UpdateParameters(DeviceId);
            modifyMaintenaceDateViewModel.UpdateLastMaintenanceDate += OnUpdateLastMaintenanceDate;
            dialogServiceProvider.ShowDynamicDialogWindow("Modify Maintenance Info", default, new ModifyMaintenanceDateView(modifyMaintenaceDateViewModel));
        }

        private void OnUpdateLastMaintenanceDate(object sender, EventArgs e)
        {
            GetlastMaintenanceDate();
        }

        public void GetlastMaintenanceDate()
        {
            Task.Factory.StartNew(new Func<object, DateTime>(GetLastMaintenanceDateFromDB), DeviceId)
                .ContinueWith(new Action<Task<DateTime>>(UpdateUI));
        }

        private void UpdateUI(Task<DateTime> task)
        {
            if (task.Result == default)
            {
                LastMaintenanceDate = "Not Updated";
            }
            else
            {
                LastMaintenanceDate = task.Result.ToString("dd-MM-yyyy");
            }
        }

        private DateTime GetLastMaintenanceDateFromDB(object deviceId)
        {
            if (deviceId != null)
            {
                IList<DbParameterInfo> parameters = new List<DbParameterInfo>
                {
                    new DbParameterInfo("@FieldDeviceIdentifier", deviceId, DbType.String)
                };
                foreach (DataRow row in databaseReader.ExecuteReadCommand("GetLastMaintenaceDate", CommandType.StoredProcedure, parameters).AsEnumerable())
                {
                    return Convert.ToDateTime(row["MaintenanceDate"]);
                }
            }

            return default;
        }
        #endregion

        #region Commands
        private ICommand _getMaintenanceDateCommand;
        public ICommand GetMaintenanceDateCommand
        {
            get => _getMaintenanceDateCommand ?? (_getMaintenanceDateCommand = new DelegateCommand(GetlastMaintenanceDate));
            set => _getMaintenanceDateCommand = value;
        }

        private ICommand _updateMaintenanceDateCommand;
        public ICommand UpdateMaintenanceDateCommand
        {
            get => _updateMaintenanceDateCommand ?? (_updateMaintenanceDateCommand = new DelegateCommand(UpdateMaintenanceDate));
            set => _updateMaintenanceDateCommand = value;
        }
        #endregion

        #region Properties
        private string _deviceLabel;
        public string DeviceLabel
        {
            get => _deviceLabel;
            set
            {
                _deviceLabel = value;
                RaisePropertyChanged();
            }
        }

        private string _deviceId;
        public string DeviceId
        {
            get => _deviceId;
            set
            {
                _deviceId = value;
                RaisePropertyChanged();
            }
        }

        private bool _alarmsRaised;
        public bool AlarmsRaised
        {
            get => _alarmsRaised;
            set
            {
                _alarmsRaised = value;
                RaisePropertyChanged();
            }
        }

        private Dictionary<string, string> _parameterData;
        public Dictionary<string, string> ParametersData
        {
            get => _parameterData ?? (_parameterData = new Dictionary<string, string>());
            set
            {
                _parameterData = value;
                RaisePropertyChanged();
            }
        }

        private Dictionary<string, bool> _parametersAlarms;
        public Dictionary<string, bool> ParametersAlarms
        {
            get => _parametersAlarms ?? (_parametersAlarms = new Dictionary<string, bool>());
            set => SetProperty(ref _parametersAlarms, value);
        }
        #endregion

        #region Maintenance Date Properties
        private string _lastMaintenanceDate;
        public string LastMaintenanceDate
        {
            get => _lastMaintenanceDate;
            set
            {
                _lastMaintenanceDate = value;
                RaisePropertyChanged();
            }
        }

        private string _selectedMaintenanceDate;
        public string SelectedMaintenanceDate
        {
            get => _selectedMaintenanceDate;
            set => SetProperty(ref _selectedMaintenanceDate, value);
        }

        private string _reasonForMaintenance;
        public string ReasonForMaintenance
        {
            get => _reasonForMaintenance;
            set => SetProperty(ref _reasonForMaintenance, value);
        }
        #endregion
    }
}
