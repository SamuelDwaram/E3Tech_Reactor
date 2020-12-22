using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Windows.Input;

namespace Magar.Shell.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IRegionManager regionManager;

        public MainWindowViewModel(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
        }

        public ICommand LoadedCommand
        {
            get => new DelegateCommand(() => regionManager.RequestNavigate("SelectedViewPane", "TanksView"));
        }
    }
}
