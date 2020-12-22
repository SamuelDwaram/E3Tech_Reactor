using E3.RemoteClient.Ui.Helpers;
using E3.RemoteClient.Ui.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace E3.RemoteClient.Ui
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
            containerProvider.Resolve<AnyInstanceExtractor>();
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            regionManager.RegisterViewWithRegion("TitleBar", typeof(TitleBarView));
            containerRegistry.RegisterForNavigation(typeof(InitializingView), "Initializing");
            containerRegistry.RegisterForNavigation(typeof(DashboardView), "Dashboard");
            containerRegistry.RegisterForNavigation(typeof(ReactorControlView), "Reactor");
            containerRegistry.RegisterForNavigation(typeof(ChillerControlView), "Chiller");
            containerRegistry.RegisterForNavigation(typeof(OtherEquipmentView), "OtherEquipment");
        }
    }
}