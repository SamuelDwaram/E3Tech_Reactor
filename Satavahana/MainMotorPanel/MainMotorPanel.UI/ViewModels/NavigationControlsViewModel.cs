using E3Tech.Navigation;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using Unity;

namespace MainMotorPanel.UI.ViewModels
{
    public class NavigationControlsViewModel : BindableBase
    {
        IRegionManager regionManager;
        IViewManager viewManager;

        public NavigationControlsViewModel(IRegionManager regionManager, IUnityContainer containerProvider)
        {
            this.regionManager = regionManager;
            viewManager = containerProvider.Resolve<IViewManager>();
            RegisteredViews = viewManager.GetRegisteredViews();
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
            SetActiveView("MainMotorPanel");
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
        #endregion

        #region Properties
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
