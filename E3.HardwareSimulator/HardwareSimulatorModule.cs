using E3.HardwareSimulator.Services;
using E3.HardwareSimulator.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace E3.HardwareSimulator
{
    public class HardwareSimulatorModule : IModule
    {
        private readonly IRegionManager regionManager;

        public HardwareSimulatorModule(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            containerProvider.Resolve<IHardwareSimulator>();
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IHardwareSimulator, InMemoryHardwareSimulator>();
            regionManager.RegisterViewWithRegion("HardwareSimulator", typeof(HardwareSimulatorView));
        }
    }
}