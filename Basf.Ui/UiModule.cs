using Basf.Ui.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace Basf.Ui
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
            regionManager.RegisterViewWithRegion("Logo", typeof(Logo));
            containerRegistry.RegisterForNavigation(typeof(InitializeView), "Initialize");
            containerRegistry.RegisterForNavigation(typeof(DashboardView), "Dashboard");
            containerRegistry.RegisterForNavigation(typeof(ReactorControlView), "Reactor");
            containerRegistry.RegisterForNavigation(typeof(ReportsView), "Reports");
        }
    }
}