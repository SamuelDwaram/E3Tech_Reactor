using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Anathem.Ui.ViewModels
{

    public class ConfigurationViewModel : BindableBase
    {
        private readonly IRegionManager regionManager;
        public ConfigurationViewModel(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
        }

        public ICommand NavigateCommand
        {
            get => new DelegateCommand<string>(str => regionManager.RequestNavigate("SelectedViewPane", str));
        }
    }
}
