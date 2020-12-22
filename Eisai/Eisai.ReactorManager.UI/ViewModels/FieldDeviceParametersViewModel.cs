using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Unity;

namespace Eisai.ReactorManager.UI.ViewModels
{
    public class FieldDeviceParametersViewModel : BindableBase, INavigationAware
    {
        IRegionManager regionManager;

        public FieldDeviceParametersViewModel(IUnityContainer containerProvider, IRegionManager regionManager)
        {
            this.regionManager = regionManager;
        }

        public void PageLoaded(NavigationParameters parameters)
        {
            NavigationParameters = parameters;
        }

        #region Navigation Aware Handlers
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            
        }
        #endregion

        #region Commands
        private ICommand _loadedCommand;
        public ICommand LoadedCommand
        {
            get => _loadedCommand ?? (_loadedCommand = new DelegateCommand<NavigationParameters>(PageLoaded));
            set => SetProperty(ref _loadedCommand, value);
        }
        #endregion

        #region Properties
        private NavigationParameters _navigationParameters;
        public NavigationParameters NavigationParameters
        {
            get => _navigationParameters ?? (_navigationParameters = new NavigationParameters());
            set => SetProperty(ref _navigationParameters, value);
        }
        #endregion
    }
}
