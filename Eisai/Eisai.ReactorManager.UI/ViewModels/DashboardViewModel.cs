using E3.ReactorManager.DesignExperiment.Model;
using E3.ReactorManager.DesignExperiment.Model.Data;
using E3.ReactorManager.Interfaces.HardwareAbstractionLayer;
using E3.ReactorManager.Interfaces.HardwareAbstractionLayer.Data;
using E3.UserManager.Model.Data;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Unity;
using Timer = System.Timers.Timer;

namespace Eisai.ReactorManager.UI.ViewModels
{
    public class DashboardViewModel : BindableBase, IRegionMemberLifetime
    {
        private readonly List<PropertyInfo> existingProperties;
        private readonly TaskScheduler taskScheduler;
        private Timer _timer = new Timer();
        private readonly IRegionManager regionManager;
        private readonly IExperimentInfoProvider experimentInfoProvider;
        private readonly IFieldDevicesCommunicator fieldDevicesCommunicator;

        public DashboardViewModel(IUnityContainer containerProvider, IRegionManager regionManager)
        {
            InitialisationIsDone = false;
            taskScheduler = TaskScheduler.FromCurrentSynchronizationContext();

            #region Resolve All Dependencies
            this.regionManager = regionManager;
            experimentInfoProvider = containerProvider.Resolve<IExperimentInfoProvider>();
            fieldDevicesCommunicator = containerProvider.Resolve<IFieldDevicesCommunicator>();
            fieldDevicesCommunicator.FieldPointDataReceived += OnFieldPointDataReceived;
            #endregion

            /* Update existing properties list */
            existingProperties = new List<PropertyInfo>(GetType().GetProperties());
            DateTimeString = DateTime.Now.ToString();

            //Update User Details
            UserDetails = (User)Application.Current.Resources["LoggedInUser"];
        }

        public void PageLoaded()
        {
            Task.Factory.StartNew(new Action(StartTimer));
            Task.Factory.StartNew(GetRunningExperiments);
        }

        private void GetRunningExperiments()
        {
            Task.Factory.StartNew(new Func<IList<Batch>>(experimentInfoProvider.GetAllRunningBatchesInThePlant))
                .ContinueWith((task) => {
                    RunningExperiments = new ObservableCollection<Batch>(task.Result);
                    RaisePropertyChanged(nameof(RunningExperiments));
                    if (!InitialisationIsDone)
                    {
                        InitialisationIsDone = true;
                    }
                }).ContinueWith(t => {
                    Thread.Sleep(5000);
                    Task.Factory.StartNew(GetRunningExperiments);
                });
        }

        #region Navigation Functions
        public void NavigateToOtherScreen(string screenIdentifier)
        {
            regionManager.RequestNavigate("SelectedViewPane", screenIdentifier, NavigationParameters);
        }

        public void GoToReactorControl(string deviceId)
        {
            NavigationParameters.Add("DeviceId", deviceId);
            regionManager.RequestNavigate("SelectedViewPane", "ReactorControl", NavigationParameters);
        }

        public void GoToChillerControl(string deviceId)
        {
            NavigationParameters.Add("DeviceId", deviceId);
            regionManager.RequestNavigate("SelectedViewPane", "ChillerControl", NavigationParameters);
        }

        public void GoToOtherEquipment(string deviceId)
        {
            NavigationParameters.Add("DeviceId", deviceId);
            regionManager.RequestNavigate("SelectedViewPane", "OtherEquipment", NavigationParameters);
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
        private ICommand _goToReactorControlCommand;
        public ICommand GoToReactorControlCommand
        {
            get => _goToReactorControlCommand ?? (_goToReactorControlCommand = new DelegateCommand<string>(GoToReactorControl));
            set => SetProperty(ref _goToReactorControlCommand, value);
        }

        private ICommand _goToChillerControlCommand;
        public ICommand GoToChillerControlCommand
        {
            get => _goToChillerControlCommand ?? (_goToChillerControlCommand = new DelegateCommand<string>(GoToChillerControl));
            set => SetProperty(ref _goToChillerControlCommand, value);
        }

        private ICommand _goToOtherEquipmentCommand;
        public ICommand GoToOtherEquipmentCommand
        {
            get => _goToOtherEquipmentCommand ?? (_goToOtherEquipmentCommand = new DelegateCommand<string>(GoToOtherEquipment));
            set => SetProperty(ref _goToOtherEquipmentCommand, value);
        }

        private ICommand _navigateToOtherScreenCommand;
        public ICommand NavigateToOtherScreenCommand
        {
            get => _navigateToOtherScreenCommand ?? (_navigateToOtherScreenCommand = new DelegateCommand<string>(NavigateToOtherScreen));
            set { _navigateToOtherScreenCommand = value; }
        }
        #endregion

        #region Properties
        public bool KeepAlive => false;

        private bool _initialisationIsDone;
        public bool InitialisationIsDone
        {
            get => _initialisationIsDone;
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
            get => _userDetails ?? (_userDetails = new User());
            set
            {
                _userDetails = value;
                RaisePropertyChanged();
            }
        }

        private NavigationParameters _navigationParameters;
        public NavigationParameters NavigationParameters
        {
            get => _navigationParameters ?? (_navigationParameters = new NavigationParameters());
            set => SetProperty(ref _navigationParameters, value);
        }
        /// <summary>
        /// Fetches all the running experiments from database
        /// </summary>
        public ObservableCollection<Batch> RunningExperiments { get; set; } = new ObservableCollection<Batch>();
        
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
