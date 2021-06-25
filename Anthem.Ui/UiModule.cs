using Anathem.Ui.Helpers;
using Anathem.Ui.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace Anathem.Ui
{
    public class UiModule : IModule
    {
        private readonly IRegionManager regionManager;

        public UiModule(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation(typeof(DashboardView), "Dashboard");
            containerRegistry.RegisterForNavigation(typeof(ReactorControlView), "ReactorControl");
            containerRegistry.RegisterForNavigation(typeof(InitializeView), "Initialize");
            regionManager.RegisterViewWithRegion("ParametersHost", typeof(ParametersHostView));
        }
    }
}