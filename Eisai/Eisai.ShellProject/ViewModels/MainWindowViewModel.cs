using E3.ActivityMonitor.Services;
using E3.AuditTrailManager.Model;
using E3.AuditTrailManager.Model.Enums;
using E3.Bpu.Services;
using E3.ReactorManager.Interfaces.Framework.Logging;
using E3.TrendsManager.Models;
using E3.TrendsManager.Services;
using E3.UserManager.Model.Data;
using E3.UserManager.Model.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Unity;
using Unity.Resolution;

namespace Eisai.ShellProject.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IUnityContainer unityContainer;
        private readonly TaskScheduler taskScheduler;
        private readonly IRegionManager regionManager;
        private ILogger logger;
        private IAuditTrailManager auditTrailManager;

        public MainWindowViewModel(IUnityContainer unityContainer, IRegionManager regionManager)
        {
            this.unityContainer = unityContainer;
            taskScheduler = TaskScheduler.FromCurrentSynchronizationContext();
            this.regionManager = regionManager;
        }

        private void Loaded()
        {
            Task.Factory.StartNew(new Action(UpdateActiveView));

            auditTrailManager = unityContainer.Resolve<IAuditTrailManager>();
            logger = unityContainer.Resolve<ILogger>();
            unityContainer.Resolve<IBusinessProcessingUnit>(new ResolverOverride[] {
                new ParameterOverride("uiCallBack", (Action<Task>)NavigateToLoginAfterFieldDevicesInitialized),
                new ParameterOverride("uiTaskScheduler", taskScheduler),
            });
            unityContainer.Resolve<IActivityMonitor>().ApplicationIsIdle += MainWindowViewModel_ApplicationIsIdle;
        }

        private void MainWindowViewModel_ApplicationIsIdle(object sender, EventArgs e)
        {
            Task.Factory.StartNew(new Action(() => NavigateToOtherScreen("Login")), CancellationToken.None, TaskCreationOptions.None, taskScheduler);
        }

        private void UpdateActiveView()
        {
            IRegionNavigationJournalEntry currentEntry = regionManager.Regions["SelectedViewPane"].NavigationService.Journal.CurrentEntry;
            if (currentEntry == null)
            {
                ActiveView = "";
            }
            else
            {
                ActiveView = currentEntry.Uri.ToString();
            }

            Thread.Sleep(1000);
            Task.Factory.StartNew(UpdateActiveView);
        }

        private void NavigateToLoginAfterFieldDevicesInitialized(Task task)
        {
            if (task.IsCompleted)
            {
                InitialisationIsDone = true;
                CommunicationStatus = true;
                regionManager.RequestNavigate("SelectedViewPane", "Login");
                InitializeTrendsModule();
            }
        }

        private void InitializeTrendsModule()
        {
            unityContainer.Resolve<ITrendsManager>(new ParameterOverride[] {
                new ParameterOverride("trendDevices", new List<TrendDevice>() {
                    GetTrendParameters("Reactor_1", "RD/GSA-01 20L"),
                    GetTrendParameters("Reactor_2", "RD/GSA-02 50L"),
                    GetTrendParameters("Reactor_3", "RD/GSA-03 100L"),
                    GetTrendParameters("Reactor_4", "RD/GSA-04 50L"),
                    GetTrendParameters("Reactor_5", "RD/GSA-05 10L"),
                    GetTrendParameters("Reactor_6", "RD/GSA-06 30L"),
                })
            });
        }

        private TrendDevice GetTrendParameters(string deviceId, string deviceLabel)
        {
            return new TrendDevice
            {
                DeviceId = deviceId,
                DeviceLabel = deviceLabel,
                Parameters = new List<TrendParameter>
                {
                    new TrendParameter
                    {
                        Label = "MassTemp",
                        Limits = "-90|200",
                        FieldPointId = "ReactorMassTemperature",
                        SensorDataSetId = "sensorDataSet_1",
                        Units = "°C",
                        IsLiveTrendParameter = true
                    },
                    new TrendParameter
                    {
                        Label = "SetPoint",
                        Limits = "-90|200",
                        FieldPointId = "HeatCoolSetPoint",
                        SensorDataSetId = "sensorDataSet_1",
                        Units = "°C",
                        IsLiveTrendParameter = true
                    },
                    new TrendParameter
                    {
                        Label = "Jacket",
                        Limits = "-90|200",
                        FieldPointId = "JacketOutletTemperature",
                        SensorDataSetId = "sensorDataSet_1",
                        Units = "°C",
                        IsLiveTrendParameter = true
                    },
                    new TrendParameter
                    {
                        Label = "RPM",
                        Limits = "200",
                        FieldPointId = "RPM",
                        SensorDataSetId = "sensorDataSet_1",
                        Units = ""
                    }
                }
            };
        }

        public async void CloseApplication()
        {
            logger.Log(LogType.Information, "Shutting down the application");
            await auditTrailManager.RecordEventSync("SHUT DOWN", GetLoggedInUserName(), EventTypeEnum.UserManagement);
            /*
             * Kill the application after closing all the connections and logging audit
             */
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        public string GetLoggedInUserName()
        {
            if (Application.Current.Resources.Contains("LoggedInUser"))
            {
                return Application.Current.Resources["LoggedInUser"].GetType().GetProperty("Name").GetValue(Application.Current.Resources["LoggedInUser"]).ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        #region Navigation Functions
        public void NavigateToOtherScreen(string screenIdentifier)
        {
            regionManager.RequestNavigate("SelectedViewPane", screenIdentifier);
        }
        #endregion

        public async void LogOut()
        {
            await auditTrailManager.RecordEventSync("User Logged Out", GetLoggedInUserName(), EventTypeEnum.UserManagement);
            NavigateToOtherScreen("Login");
        }

        #region Properties
        public IUnityContainer UnityContainer => unityContainer;
        
        private string _activeView;
        public string ActiveView
        {
            get { return _activeView; }
            set { SetProperty(ref _activeView, value); }
        }

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

        public ICommand ClosedCommand
        {
            get => new DelegateCommand(() => Task.Factory.StartNew(() => {
                User loggedInUser = (User)Application.Current.Resources["LoggedInUser"];
                if (loggedInUser != null)
                {
                    unityContainer.Resolve<IUserManager>().UpdateUserLoginStatus(loggedInUser.UserID, false);
                }
            }).ContinueWith(t => CloseApplication()));
        }
        #endregion
    }
}
