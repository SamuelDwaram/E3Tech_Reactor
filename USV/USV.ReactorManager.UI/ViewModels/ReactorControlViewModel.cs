using E3.ReactorManager.Interfaces.HardwareAbstractionLayer;
using E3.UserManager.Model.Data;
using EventAggregator.Core;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Unity;

namespace USV.ReactorManager.UI.ViewModels
{
    public class ReactorControlViewModel : BindableBase, INavigationAware, IRegionMemberLifetime
    {
        TaskScheduler taskScheduler;
        IFieldDevicesCommunicator fieldDevicesCommunicator;
        IRegionManager regionManager;
        IEventAggregator eventAggregator;

        public ReactorControlViewModel(IUnityContainer containerProvider, IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            this.regionManager = regionManager;
            this.eventAggregator = eventAggregator;
            eventAggregator.GetEvent<GetDeviceIdForUpdatingExperimentInfo>().Subscribe(ReturnCurrentDeviceIdForUpdatingExperimentInfo);
            eventAggregator.GetEvent<GetDeviceIdForUpdatingActionComments>().Subscribe(ReturnCurrentDeviceIdForUpdatingActionComments);
            taskScheduler = TaskScheduler.FromCurrentSynchronizationContext();
            fieldDevicesCommunicator = containerProvider.Resolve<IFieldDevicesCommunicator>();
        }

        #region Event Aggregator Event Handlers
        private void ReturnCurrentDeviceIdForUpdatingActionComments()
        {
            eventAggregator.GetEvent<UpdateActionCommentsForGivenDeviceId>().Publish(CurrentFieldDeviceName);
        }

        private void ReturnCurrentDeviceIdForUpdatingExperimentInfo()
        {
            eventAggregator.GetEvent<UpdateExperimentInfoOfDeviceEvent>().Publish(CurrentFieldDeviceName);
        }
        #endregion

        #region Navigation Aware Handlers
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            NavigationParameters = navigationContext.Parameters;
            UserDetails = navigationContext.Parameters["UserDetails"] as User;
            CurrentFieldDeviceName = navigationContext.Parameters["DeviceId"].ToString();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            
        }
        #endregion

        public void PageLoaded()
        {
            Task.Factory.StartNew(new Func<object, string>(GetFieldDeviceLabel), CurrentFieldDeviceName)
                .ContinueWith(new Action<Task<string>>(UpdateFieldDeviceLabel), taskScheduler);
        }

        #region Update Field Device label
        private void UpdateFieldDeviceLabel(Task<string> obj)
        {
            CurrentFieldDeviceGuiLabel = obj.Result;
        }

        private string GetFieldDeviceLabel(object deviceId)
        {
            return fieldDevicesCommunicator.GetFieldDeviceLabel((string)deviceId);
        }
        #endregion

        public void NavigateToOtherScreen(string screenIdentifier)
        {
            NavigationParameters parameters = new NavigationParameters();
            parameters.Add("UserDetails", UserDetails);
            regionManager.RequestNavigate("SelectedViewPane", screenIdentifier, parameters);   
        }

        private ICommand _loadedCommand;
        public ICommand LoadedCommand
        {
            get => _loadedCommand ?? (_loadedCommand = new DelegateCommand(PageLoaded));
            set { _loadedCommand = value; }
        }
        
        private ICommand _navigateToOtherScreenCommand;
        public ICommand NavigateToOtherScreenCommand
        {
            get => _navigateToOtherScreenCommand ?? (_navigateToOtherScreenCommand = new DelegateCommand<string>(NavigateToOtherScreen));
            set { _navigateToOtherScreenCommand = value; }
        }

        #region Properties
        private NavigationParameters _navigationParameters;
        public NavigationParameters NavigationParameters
        {
            get => _navigationParameters ?? (_navigationParameters = new NavigationParameters());
            set
            {
                _navigationParameters = value;
                RaisePropertyChanged();
            }
        }

        public bool KeepAlive
        {
            get => false;
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
        /// Contains the field device identifier of the Reactor Control Page
        /// </summary>
        private string _currentFieldDeviceName;
        public string CurrentFieldDeviceName
        {
            get { return _currentFieldDeviceName; }
            set
            {
                _currentFieldDeviceName = value;
                RaisePropertyChanged();
            }
        }

        private string _currentFieldDeviceGuiLabel;
        public string CurrentFieldDeviceGuiLabel
        {
            get => _currentFieldDeviceGuiLabel;
            set
            {
                _currentFieldDeviceGuiLabel = value;
                RaisePropertyChanged();
            }
        }
        #endregion

    }
}
