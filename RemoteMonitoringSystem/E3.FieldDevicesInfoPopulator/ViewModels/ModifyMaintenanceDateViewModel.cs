using E3.ReactorManager.Interfaces.DataAbstractionLayer;
using E3.ReactorManager.Interfaces.DataAbstractionLayer.Data;
using E3.ReactorManager.Interfaces.Framework.Logging;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Unity;

namespace E3.FieldDevicesInfoPopulator.ViewModels
{
    public class ModifyMaintenaceDateViewModel : BindableBase, IRegionMemberLifetime
    {
        ILogger logger;
        IDatabaseWriter databaseWriter;

        public ModifyMaintenaceDateViewModel(IUnityContainer containerProvider, ILogger logger, IDatabaseWriter databaseWriter)
        {
            this.logger = logger;
            this.databaseWriter = databaseWriter;
        }

        #region Modify Maintenance Date
        private bool CanUpdateMaintenanceDate()
        {
            return !string.IsNullOrWhiteSpace(ReasonForMaintenance) && !string.IsNullOrWhiteSpace(UpdatedMaintenanceDate);
        }

        public void UpdateMaintenanceDate()
        {
            Task.Factory.StartNew(() => {
                logger.Log(LogType.Information, "Updating Maintenance Date : " + UpdatedMaintenanceDate + " for reason : " + ReasonForMaintenance);
                IList<DbParameterInfo> parameters = new List<DbParameterInfo>
                {
                    new DbParameterInfo("FieldDeviceIdentifier", DeviceId, System.Data.DbType.String),
                    new DbParameterInfo("ReasonForMaintenance", ReasonForMaintenance, System.Data.DbType.String),
                    new DbParameterInfo("UpdatedMaintenanceDate", UpdatedMaintenanceDate, System.Data.DbType.DateTime)
                };
                databaseWriter.ExecuteWriteCommand("UpdateMaintenanceDate", System.Data.CommandType.StoredProcedure, parameters);
                UpdateLastMaintenanceDate?.BeginInvoke(this, null, null, null);
            });
        }
        #endregion

        public void UpdateParameters(string deviceId)
        {
            DeviceId = deviceId;
        }

        public void PageLoaded(UserControl userControl)
        {
            CurrentWindow = Window.GetWindow(userControl);
        }

        public void CloseWindow(Window window)
        {
            if (window != null)
            {
                window.Close();
            }
        }

        #region Commands
        private ICommand _closeWindowCommand;
        public ICommand CloseWindowCommand
        {
            get => _closeWindowCommand ?? (_closeWindowCommand = new DelegateCommand<Window>(CloseWindow));
            set => _closeWindowCommand = value;
        }

        private ICommand _loadedCommand;
        public ICommand LoadedCommand
        {
            get => _loadedCommand ?? (_loadedCommand = new DelegateCommand<UserControl>(PageLoaded));
            set => _loadedCommand = value;
        }

        private ICommand _updateMaintenanceDateCommand;
        public ICommand UpdateMaintenanceDateCommand
        {
            get => _updateMaintenanceDateCommand
                ?? (_updateMaintenanceDateCommand = new DelegateCommand(UpdateMaintenanceDate, CanUpdateMaintenanceDate)
                .ObservesProperty(() => ReasonForMaintenance)
                .ObservesProperty(() => UpdatedMaintenanceDate));
            set => _updateMaintenanceDateCommand = value;
        }
        #endregion

        #region Properties
        private Window _currentWindow;
        public Window CurrentWindow
        {
            get => _currentWindow;
            set
            {
                _currentWindow = value;
                RaisePropertyChanged();
            }
        }

        public bool KeepAlive
        {
            get => false;
        }

        private string _deviceId;
        public string DeviceId
        {
            get => _deviceId;
            set => SetProperty(ref _deviceId, value);
        }

        private string _modifiedMaintenanceDate;
        public string UpdatedMaintenanceDate
        {
            get => _modifiedMaintenanceDate;
            set => SetProperty(ref _modifiedMaintenanceDate, value);
        }

        private string _reasonForMaintenance;
        public string ReasonForMaintenance
        {
            get => _reasonForMaintenance;
            set => SetProperty(ref _reasonForMaintenance, value);
        }

        private DateTime? _displayStartDate;
        public DateTime? DisplayStartDate
        {
            get => _displayStartDate ?? (_displayStartDate = DateTime.Today.Subtract(TimeSpan.FromDays(20)));
            set => SetProperty(ref _displayStartDate, value);
        }

        private DateTime? _displayEndDate;
        public DateTime? DisplayEndDate
        {
            get => _displayEndDate ?? (_displayEndDate = DateTime.Today);
            set => SetProperty(ref _displayEndDate, value);
        }
        #endregion

        public event EventHandler UpdateLastMaintenanceDate;
    }

}
