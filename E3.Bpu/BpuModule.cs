using E3.Bpu.Services;
using E3.Bpu.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace E3.Bpu
{
    public class BpuModule : IModule
    {
        private readonly IRegionManager regionManager;

        public BpuModule(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            containerProvider.Resolve<IDevicesHandler>();
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IBusinessProcessingUnit, BusinessProcessingUnit>();
            containerRegistry.RegisterSingleton<IDevicesHandler, RegisteredDevicesHandler>();
            regionManager.RegisterViewWithRegion("RegisteredDevices", typeof(RegisteredDevicesView));
        }
    }
}