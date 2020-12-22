using BaseWpf.Base;
using E3.ReactorManager.Interfaces.HardwareAbstractionLayer;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Windows.Input;
using Unity;

namespace E3.RemoteMonitoringSystem.UI.ViewModels
{
    public class DashboardViewModel : BindableBase
    {
        IFieldDevicesCommunicator fieldDevicesCommunicator;
        IRegionManager regionManager;

        public DashboardViewModel(IUnityContainer containerProvider, IRegionManager regionManager, IFieldDevicesCommunicator fieldDevicesCommunicator)
        {
            this.fieldDevicesCommunicator = fieldDevicesCommunicator;
            this.regionManager = regionManager;
        }

        public void PageLoaded()
        {
            
        }

        #region Commands

        private ICommand loadedCommand;
        public ICommand LoadedCommand
        {
            get => loadedCommand ?? (loadedCommand = new RelayCommand(new Action(PageLoaded)));
            set => loadedCommand = value;
        }

        private ICommand _navigateCommand;
        public ICommand NavigateCommand
        {
            get => _navigateCommand ?? (_navigateCommand = new DelegateCommand<string>(NavigateToOtherPage));
            set => _navigateCommand = value;
        }

        private void NavigateToOtherPage(string pageName)
        {
            regionManager.RequestNavigate("SelectedViewPane", pageName);
        }
        #endregion
    }
}
