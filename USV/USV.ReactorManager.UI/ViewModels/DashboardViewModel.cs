using E3.ReactorManager.BusinessProcessingUnit.Model.Interfaces;
using E3.ReactorManager.DesignExperiment.Model;
using E3.ReactorManager.DesignExperiment.Model.Data;
using E3.ReactorManager.Interfaces.DataAbstractionLayer;
using E3.ReactorManager.Interfaces.HardwareAbstractionLayer;
using E3.ReactorManager.Interfaces.HardwareAbstractionLayer.Data;
using E3.UserManager.Model.Data;
using EventAggregator.Core;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;
using Unity;

namespace USV.ReactorManager.UI.ViewModels
{
    public class DashboardViewModel : BindableBase, INavigationAware, IRegionMemberLifetime
    {
        IList<PropertyInfo> existingProperties;
        TaskScheduler taskScheduler;
        Timer _timer = new Timer();
        IRegionManager regionManager;
        IDatabaseReader databaseReader;
        IBusinessProcessingUnit bpu;
        IFieldDevicesCommunicator fieldDevicesCommunicator;
        IExperimentInfoProvider experimentInfoProvider;
        IEventAggregator eventAggregator;

        public DashboardViewModel(IUnityContainer containerProvider, IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            this.regionManager = regionManager;
            this.eventAggregator = eventAggregator;
            eventAggregator.GetEvent<GetLoggedInUserName>().Subscribe(PublishUsername);
            eventAggregator.GetEvent<GetLoggedInUserDetails>().Subscribe(PublishUserDetails);

            InitialisationIsDone = false;
            taskScheduler = TaskScheduler.FromCurrentSynchronizationContext();

            databaseReader = containerProvider.Resolve<IDatabaseReader>();
            bpu = containerProvider.Resolve<IBusinessProcessingUnit>();
            experimentInfoProvider = containerProvider.Resolve<IExperimentInfoProvider>();
            fieldDevicesCommunicator = containerProvider.Resolve<IFieldDevicesCommunicator>();
            fieldDevicesCommunicator.FieldPointDataReceived += OnFieldPointDataReceived;

            /* Update existing properties list */
            existingProperties = new List<PropertyInfo>(GetType().GetProperties());

            DateTimeString = DateTime.Now.ToString();
        }

        private void PublishUserDetails()
        {
            eventAggregator.GetEvent<UpdateLoggedInUserDetails>().Publish(UserDetails);
        }

        private void PublishUsername()
        {
            eventAggregator.GetEvent<UpdateLoggedInUsername>().Publish(UserDetails.Name);
        }

        #region Navigation Aware Handlers
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            UserDetails = navigationContext.Parameters["UserDetails"] as User;
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            
        }
        #endregion

        #region Page Loaded Functions
        public void PageLoaded()
        {
            Task.Factory.StartNew(new Action(StartTimer));
            Task<IList<Batch>>.Factory.StartNew(new Func<IList<Batch>>(experimentInfoProvider.GetAllRunningBatchesInThePlant))
                .ContinueWith(new Action<Task<IList<Batch>>>(UpdateRunningExperiments), taskScheduler);
        }
        #endregion

        #region Running experiments Writers to Plc
        private void UpdateRunningExperiments(Task<IList<Batch>> task)
        {
            RunningExperiments = new ObservableCollection<Batch>(task.Result);

            InitialisationIsDone = true;

            /* Update the Running Experiments to the Field Devices Communicator */
            Task.Factory.StartNew(new Action(OnUpdateRunningExperimentsToFieldDevicesCommunicator));
        }
        /// <summary>
        /// Updates the Running Batch Status of a Reactor to the PLC
        /// after creating a new batch in Design Experiment
        /// </summary>
        private void OnUpdateRunningExperimentsToFieldDevicesCommunicator()
        {
            foreach (var runningExperiment in RunningExperiments.ToList())
            {
                if (bpu.ConfirmSendDirectCommandsToPlc())
                {
                    fieldDevicesCommunicator
                        .SendCommandToDevice(runningExperiment.FieldDeviceIdentifier,
                                                    "RunningBatchStatus",
                                                    "bool",
                                                    bool.TrueString);
                }
            }
        }

        #endregion

        #region Navigation Functions
        public void NavigateToOtherScreen(string screenIdentifier)
        {
            NavigationParameters parameters = new NavigationParameters();
            parameters.Add("UserDetails", UserDetails);
            regionManager.RequestNavigate("SelectedViewPane", screenIdentifier, parameters);
        }

        public void NavigateToReactorControl(string deviceId)
        {
            NavigationParameters parameters = new NavigationParameters();
            parameters.Add("UserDetails", UserDetails);
            parameters.Add("DeviceId", deviceId);
            regionManager.RequestNavigate("SelectedViewPane", "ReactorControl", parameters);
        }

        #endregion

        #region LiveDateTime in UI

        private void StartTimer()
        {
            _timer = new Timer(TimeSpan.FromSeconds(1).TotalMilliseconds);
            _timer.Elapsed += OnTimerTick;
            _timer.Start();
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            Task.Factory.StartNew(new Action<object>(UpdateLiveDateTime), taskScheduler, TaskCreationOptions.None);
        }

        private void UpdateLiveDateTime(object obj)
        {
            DateTimeString = DateTime.Now.ToString();
        }
        #endregion

        #region LiveData Handlers

        private void UpdatePropertyValue(Task<LiveDataEventArgs> task)
        {
            var liveDataEventArgs = task.Result;

            if (liveDataEventArgs.PropertyInfo != null)
            {
                liveDataEventArgs.PropertyInfo.SetValue(this, liveDataEventArgs.LiveData, null);
            }
        }

        private void OnFieldPointDataReceived(object sender, FieldPointDataReceivedArgs fieldPointDataChangedArgs)
        {
            if (fieldPointDataChangedArgs.FieldPointIdentifier.Contains("UsedNow"))
            {
                /* Validate Live Data and Update UI 
                 * only if it contains information about Field devices Usage Status */
                LiveData = fieldPointDataChangedArgs;

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
            var liveDataEventArgs = (LiveDataEventArgs)liveData;

            liveDataEventArgs.PropertyInfo = existingProperties.FirstOrDefault(property => property.Name == liveDataEventArgs.PropertyInfoIdentifier);

            return liveDataEventArgs;
        }

        #endregion

        #region Commands
        private ICommand _loadedCommand;
        public ICommand LoadedCommand
        {
            get => _loadedCommand ?? (_loadedCommand = new DelegateCommand(PageLoaded));
            set { _loadedCommand = value; }
        }
        private ICommand _navigateToReactorControlCommand;
        public ICommand NavigateToReactorControlCommand
        {
            get => _navigateToReactorControlCommand ?? (_navigateToReactorControlCommand = new DelegateCommand<string>(NavigateToReactorControl));
        }

        private ICommand _navigateToOtherScreenCommand;
        public ICommand NavigateToOtherScreenCommand
        {
            get => _navigateToOtherScreenCommand ?? (_navigateToOtherScreenCommand = new DelegateCommand<string>(NavigateToOtherScreen));
            set { _navigateToOtherScreenCommand = value; }
        }
        #endregion

        #region Properties
        public bool KeepAlive
        {
            get => false;
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

        private bool _initialisationIsDone;
        public bool InitialisationIsDone
        {
            get { return _initialisationIsDone; }
            set
            {
                _initialisationIsDone = value;
                RaisePropertyChanged();
            }
        }
        /// <summary>
        /// gives current date time to GUI
        /// </summary>
        private string _dateTimeString;
        public string DateTimeString
        {
            get { return _dateTimeString; }
            set
            {
                _dateTimeString = value;
                RaisePropertyChanged();
            }
        }
        /// <summary>
        /// Details of user
        /// </summary>
        private User _userDetails;
        public User UserDetails
        {
            get { return _userDetails; }
            set
            {
                _userDetails = value;
                RaisePropertyChanged();
            }
        }
        /// <summary>
        /// Fetches all the running experiments from database
        /// </summary>
        private ObservableCollection<Batch> _runningExperiments;
        public ObservableCollection<Batch> RunningExperiments
        {
            get => _runningExperiments ?? (_runningExperiments = new ObservableCollection<Batch>());
            set
            {
                _runningExperiments = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Indicates the Usage status of Field device
        /// When live Data is received for this variable it will be updated
        /// so that all the reactors in Dashboard UI will refresh their
        /// usage status with Field devices communicator
        /// </summary>
        private string _usedNow;
        public string UsedNow
        {
            get { return _usedNow; }
            set
            {
                _usedNow = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Capture the LiveData Temporarily For updating the Usage Status in Dashboard View
        /// </summary>
        private FieldPointDataReceivedArgs _liveData;
        public FieldPointDataReceivedArgs LiveData
        {
            get => _liveData;
            set
            {
                _liveData = value;
                RaisePropertyChanged();
            }
        }
        #endregion

    }
}
