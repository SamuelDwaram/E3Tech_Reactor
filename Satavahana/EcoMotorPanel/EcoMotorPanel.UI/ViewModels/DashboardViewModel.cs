using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Windows.Input;

namespace EcoMotorPanel.UI.ViewModels
{
    public class DashboardViewModel : BindableBase
    {
        IRegionManager regionManager;

        public DashboardViewModel(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
        }

        public void PageLoaded()
        {
            regionManager.RequestNavigate("SelectedViewPane", "EcoMotor");
        }

        private ICommand _loadedCommand;
        public ICommand LoadedCommand
        {
            get => _loadedCommand ?? (_loadedCommand = new DelegateCommand(PageLoaded));
            set => SetProperty(ref _loadedCommand, value);
        }

    }
}
