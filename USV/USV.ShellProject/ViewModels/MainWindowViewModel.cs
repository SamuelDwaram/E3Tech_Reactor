using E3.AuditTrailManager.Model;
using E3.AuditTrailManager.Model.Enums;
using E3.ReactorManager.BusinessProcessingUnit.Model.Interfaces;
using E3.ReactorManager.Interfaces.Framework.Logging;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Unity;

namespace USV.ShellProject.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        IUnityContainer unityContainer;
        IBusinessProcessingUnit bpu;
        TaskScheduler taskScheduler;
        IRegionManager regionManager;
        ILogger logger;
        IAuditTrailManager auditTrailManager;

        public MainWindowViewModel(IUnityContainer containerProvider, IRegionManager regionManager)
        {
            this.unityContainer = containerProvider;
            taskScheduler = TaskScheduler.FromCurrentSynchronizationContext();
            this.regionManager = regionManager;
        }

        private void Loaded()
        {
            auditTrailManager = unityContainer.Resolve<IAuditTrailManager>();
            logger = unityContainer.Resolve<ILogger>();
            bpu = unityContainer.Resolve<IBusinessProcessingUnit>();
            bpu.InitializeFieldDevices(new Action<Task>(NavigateToLoginAfterFieldDevicesInitialized), taskScheduler);
        }

        private void NavigateToLoginAfterFieldDevicesInitialized(Task task)
        {
            if (task.IsCompleted)
            {
                InitialisationIsDone = true;
                CommunicationStatus = true;
                regionManager.RequestNavigate("SelectedViewPane", "Login");
            }
        }

        #region CloseApplication
        private bool CanCloseApplication()
        {
            return true;
        }
        public async void CloseApplication()
        {
            logger.Log(LogType.Information, "Shutting down the application");

            await auditTrailManager.RecordEventSync("System is shutting down", string.Empty, EventTypeEnum.UserManagement);

            /*
             * Kill the application after closing all the connections and logging audit
             */
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        #endregion

        #region Navigation Functions
        public void NavigateToOtherScreen(string screenIdentifier)
        {
            regionManager.RequestNavigate("SelectedViewPane", screenIdentifier);
        }
        #endregion

        public async void LogOut()
        {
            await auditTrailManager.RecordEventSync("User Logged Out", string.Empty, EventTypeEnum.UserManagement);
        }

        #region Properties

        private bool _communicationStatus;
        public bool CommunicationStatus
        {
            get { return _communicationStatus; }
            set
            {
                _communicationStatus = value;
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
        #endregion

        #region Commands
        private ICommand _loadedCommand;
        public ICommand LoadedCommand
        {
            get => _loadedCommand ?? (_loadedCommand = new DelegateCommand(Loaded));
        }

        private ICommand _navigateToOtherScreenCommand;
        public ICommand NavigateToOtherScreenCommand
        {
            get => _navigateToOtherScreenCommand ?? (_navigateToOtherScreenCommand = new DelegateCommand<string>(NavigateToOtherScreen));
            set { _navigateToOtherScreenCommand = value; }
        }

        private ICommand _logOutCommand;
        public ICommand LogOutCommand
        {
            get => _logOutCommand ?? (_logOutCommand = new DelegateCommand(LogOut));
            set => SetProperty(ref _logOutCommand, value);
        }

        /// <summary>
        /// Command executes when Application closes
        /// </summary>
        private ICommand _closedCommand;
        public ICommand ClosedCommand
        {
            get => _closedCommand ?? (_closedCommand = new DelegateCommand(CloseApplication, CanCloseApplication));
            set { _closedCommand = value; }
        }
        #endregion
    }
}
