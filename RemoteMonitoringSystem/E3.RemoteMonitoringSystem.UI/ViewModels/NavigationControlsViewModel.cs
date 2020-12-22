using E3Tech.Navigation;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Unity;

namespace E3.RemoteMonitoringSystem.UI.ViewModels
{
    public class NavigationControlsViewModel : BindableBase
    {
        IRegionManager regionManager;
        IViewManager viewManager;
        Timer timer = new Timer(1000);

        public NavigationControlsViewModel(IRegionManager regionManager, IUnityContainer containerProvider)
        {
            this.regionManager = regionManager;
            viewManager = containerProvider.Resolve<IViewManager>();
            RegisteredViews = viewManager.GetRegisteredViews();
            timer.Elapsed += Timer_Elapsed;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            RunningTime = DateTime.Now.ToString();
        }

        private void NavigateToOtherPage(string pageName)
        {
            regionManager.RequestNavigate("SelectedViewPane", pageName);
            SetActiveView(pageName);
        }

        public void SetActiveView(string activeViewName)
        {
            ActiveView = activeViewName;
        }

        private void PageLoaded()
        {
            timer.Start();
            SetActiveView("Dashboard");
        }

        private void ShutDownApp()
        {
            Application.Current.Shutdown();
        }

        public void OpenKeyboard()
        {
            Process.Start(@"C:\Program Files\Common Files\microsoft shared\ink\TabTip.exe");
        }

        #region Commands
        private ICommand _navigateCommand;
        public ICommand NavigateCommand
        {
            get => _navigateCommand ?? (_navigateCommand = new DelegateCommand<string>(NavigateToOtherPage));
            set => _navigateCommand = value;
        }

        private ICommand _loadedCommand;
        public ICommand LoadedCommand
        {
            get => _loadedCommand ?? (_loadedCommand = new DelegateCommand(PageLoaded));
            set => _loadedCommand = value;
        }

        private ICommand _shutDownAppCommand;
        public ICommand ShutDownAppCommand
        {
            get => _shutDownAppCommand ?? (_shutDownAppCommand = new DelegateCommand(ShutDownApp));
            set => _shutDownAppCommand = value;
        }

        private ICommand _openKeyboardCommand;
        public ICommand OpenKeyboardCommand
        {
            get => _openKeyboardCommand ?? (_openKeyboardCommand = new DelegateCommand(OpenKeyboard));
            set => _openKeyboardCommand = value;
        }

        public ICommand MinimizeCommand
        {
            get => new DelegateCommand<UserControl>(uc => {
                Window currentWindow = Window.GetWindow(uc);
                if (currentWindow == null)
                {
                    // Skip.
                }
                else
                {
                    currentWindow.WindowState = WindowState.Minimized; 
                }
            });
        }
        #endregion

        #region Properties
        private string _runningTime;
        public string RunningTime
        {
            get => _runningTime;
            set => SetProperty(ref _runningTime, value);
        }

        private ObservableCollection<string> _registeredViews;
        public ObservableCollection<string> RegisteredViews
        {
            get => _registeredViews ?? (_registeredViews = new ObservableCollection<string>());
            set
            {
                _registeredViews = value;
                RaisePropertyChanged();
            }
        }

        private string _activeView;
        public string ActiveView
        {
            get => _activeView;
            set
            {
                _activeView = value;
                RaisePropertyChanged();
            }
        }
        #endregion
    }
}
