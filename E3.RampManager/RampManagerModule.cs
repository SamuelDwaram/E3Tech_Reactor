using E3.RampManager.Services;
using E3.RampManager.ViewModels;
using E3.RampManager.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace E3.RampManager
{
    public class RampManagerModule : IModule
    {
        private readonly IRegionManager regionManager;

        public RampManagerModule(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            containerProvider.Resolve<IRampManager>();
            containerProvider.Resolve<RampViewModel>();
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            regionManager.RegisterViewWithRegion("Ramp", typeof(RampView));
            containerRegistry.RegisterSingleton<IRampManager, Services.RampManager>();
            containerRegistry.RegisterSingleton<RampViewModel>();
        }
    }
}