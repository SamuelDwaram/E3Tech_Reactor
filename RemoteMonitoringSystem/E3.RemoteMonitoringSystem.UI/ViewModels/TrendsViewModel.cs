using Prism.Commands;
using Prism.Regions;
using System.Windows.Input;

namespace E3.RemoteMonitoringSystem.UI.ViewModels
{
    public class TrendsViewModel
    {
        IRegionManager regionManager;

        public TrendsViewModel(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
        }

        private void NavigateToOtherPage(string pageName)
        {
            regionManager.RequestNavigate("SelectedViewPane", pageName);
        }

        private ICommand _navigateCommand;
        public ICommand NavigateCommand
        {
            get => _navigateCommand ?? (_navigateCommand = new DelegateCommand<string>(NavigateToOtherPage));
            set => _navigateCommand = value;
        }
    }
}
