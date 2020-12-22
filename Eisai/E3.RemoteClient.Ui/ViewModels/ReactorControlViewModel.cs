using E3.Bpu.Models;
using E3.Bpu.Services;
using E3.Mediator.Models;
using E3.Mediator.Services;
using E3.ReactorManager.DesignExperiment.Model;
using E3.ReactorManager.DesignExperiment.Model.Data;
using E3.ReactorManager.Interfaces.DataAbstractionLayer;
using E3.ReactorManager.Interfaces.DataAbstractionLayer.Data;
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
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace E3.RemoteClient.Ui.ViewModels
{
    public class ReactorControlViewModel : BindableBase, IRegionMemberLifetime, INavigationAware
    {
        private readonly IRegionManager regionManager;
        private readonly IDatabaseReader databaseReader;
        private readonly IExperimentInfoProvider experimentInfoProvider;
        private readonly MediatorService mediatorService;
        private readonly IFieldDevicesCommunicator fieldDevicesCommunicator;
        private readonly IDevicesHandler devicesHandler;
        private readonly IBusinessProcessingUnit bpu;
        private readonly TaskScheduler taskScheduler;

        public ReactorControlViewModel(IRegionManager regionManager, IDatabaseReader databaseReader, IExperimentInfoProvider experimentInfoProvider, MediatorService mediatorService, IFieldDevicesCommunicator fieldDevicesCommunicator, IDevicesHandler devicesHandler, IBusinessProcessingUnit bpu)
        {
            taskScheduler = TaskScheduler.FromCurrentSynchronizationContext();
            this.regionManager = regionManager;
            this.databaseReader = databaseReader;
            this.experimentInfoProvider = experimentInfoProvider;
            this.fieldDevicesCommunicator = fieldDevicesCommunicator;
            this.devicesHandler = devicesHandler;
            this.bpu = bpu;
            this.fieldDevicesCommunicator.FieldPointDataReceived += FieldDevicesCommunicator_FieldPointDataReceived;
            this.mediatorService = mediatorService;
        }

        private void FieldDevicesCommunicator_FieldPointDataReceived(object sender, FieldPointDataReceivedArgs liveData)
        {
            if (liveData.FieldDeviceIdentifier == CurrentDevice.Id)
            {
                if (DeviceParametersContainer.ContainsKey(liveData.FieldPointIdentifier))
                {
                    DeviceParametersContainer[liveData.FieldPointIdentifier] = liveData.NewFieldPointData;
                    RaisePropertyChanged(nameof(DeviceParametersContainer));
                }
            }
        }

        #region Navigation Handlers
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            CurrentDevice = navigationContext.Parameters.GetValue<Device>("Device");
            CurrentFieldDeviceName = CurrentDevice.Id;
            LoadDeviceParameters();
            Task.Factory.StartNew(new Action(UpdateDeviceRelatedInfo));
        }

        private void LoadDeviceParameters()
        {
            if (Application.Current.Resources.Contains("LoggedInUser"))
            {
                UserDetails = (User)Application.Current.Resources["LoggedInUser"];  //Update LoggedInUser Details
                RaisePropertyChanged(nameof(UserDetails));
            }
           
            Task.Factory.StartNew(new Func<object, FieldDevice>((t) => fieldDevicesCommunicator.GetFieldDeviceData((string)t)), CurrentDevice.Id)
                .ContinueWith(new Func<Task<FieldDevice>, Dictionary<string, string>>((t) => {
                    return (from SensorsDataSet sensorsDataSet in t.Result.SensorsData
                            from FieldPoint fieldPoint in sensorsDataSet.SensorsFieldPoints.Where(fp => fp.RequireNotificationService == true)
                            select new KeyValuePair<string, string>(fieldPoint.Label, fieldPoint.Value))
                            .ToDictionary(item => item.Key, item => item.Value);
                })).ContinueWith((t) => DeviceParametersContainer = t.Result, taskScheduler);

            //notify other modules to load their information of the device
            mediatorService.NotifyColleagues(InMemoryMediatorMessageContainer.UpdateSelectedDeviceId, CurrentDevice);
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return false;
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
            .ContinueWith(task => {
                // Check if ConnectedHc or ConnectedStirrer is empty 
                // and if empty then read the connected equipments data from Database
                if (string.IsNullOrWhiteSpace(ConnectedHc) || string.IsNullOrWhiteSpace(ConnectedStirrer))
                {
                    //Fetch the connected equipments task from database
                    Task.Factory.StartNew(new Func<object, DataTable>(GetConnectedEquipments), CurrentFieldDeviceName)
                        .ContinueWith(new Action<Task<DataTable>>(t => {
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

        #region Commands
        public ICommand NavigateCommand 
        {
            get => new DelegateCommand<string>(pageName => regionManager.RequestNavigate("SelectedViewPane", pageName));
        }

        private bool CanSendCommandToDevice()
        {
            return devicesHandler.CurrentDevice.ModulesAccessible.Contains("ReactorControl")
                    && UserDetails.Roles.Any(role => role.ModulesAccessable.Contains("ReactorControl"));
        }

        public ICommand SendCommandToDevice
        {
            get => new DelegateCommand<object[]>(commandArray => {
                bpu.SendCommandToDevice(CurrentFieldDeviceName, commandArray[0].ToString(), commandArray[1].ToString(), commandArray[2].ToString());
            }, commandArray => CanSendCommandToDevice()).ObservesProperty(() => UserDetails);
        }
        #endregion

        #region Properties
        public bool KeepAlive { get => false; }
        private bool _dosingPumpUsage;
        public bool DosingPumpUsage
        {
            get { return _dosingPumpUsage; }
            set { SetProperty(ref _dosingPumpUsage, value); }
        }

        private string _connectedHc;
        public string ConnectedHc
        {
            get { return _connectedHc; }
            set { SetProperty(ref _connectedHc, value); }
        }

        private string _connectedStirrer;
        public string ConnectedStirrer
        {
            get { return _connectedStirrer; }
            set { SetProperty(ref _connectedStirrer, value); }
        }

        private Device _currentDevice;
        public Device CurrentDevice
        {
            get => _currentDevice ?? (_currentDevice = new Device());
            set => SetProperty(ref _currentDevice, value);
        }

        private string _currentFieldDeviceName;
        public string CurrentFieldDeviceName
        {
            get => _currentFieldDeviceName;
            set => SetProperty(ref _currentFieldDeviceName, value);
        }

        private Dictionary<string, string> _deviceParameters;
        public Dictionary<string, string> DeviceParametersContainer
        {
            get => _deviceParameters ?? (_deviceParameters = new Dictionary<string, string>());
            set => SetProperty(ref _deviceParameters, value);
        }

        public User UserDetails { get; set; } = new User();
        public RegisteredDevice RegisteredDevice => devicesHandler.CurrentDevice;
        #endregion
    }
}
