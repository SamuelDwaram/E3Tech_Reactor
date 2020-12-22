using E3.ReactorManager.TrendsManager.Model.Interfaces;
using E3.ReactorManager.TrendsManager.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Unity;

namespace E3.ReactorManager.TrendsManager
{
    public class TrendsManagerModule : IModule
    {
        private readonly IRegionManager regionManager;

        public TrendsManagerModule(IUnityContainer containerProvider, IRegionManager manager)
        {
            regionManager = manager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<ITrendsManager, Model.Implementations.TrendsManager>();
            containerRegistry.RegisterForNavigation(typeof(TrendsView), "Trends");

            regionManager.RegisterViewWithRegion("Trends", typeof(TrendsView));
        }
    }
}
