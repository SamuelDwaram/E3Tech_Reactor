using Prism.Ioc;
using Prism.Modularity;
using E3.ReactorManager.LiveGraphManager.Model.Interfaces;
using Unity;
using Prism.Regions;
using E3.ReactorManager.LiveGraphManager.Views;
using E3.ReactorManager.LiveGraphManager.ViewModels;
using E3.ReactorManager.LiveGraphManager.Model.Implementations;

namespace E3.ReactorManager.LiveGraphManager
{
    public class LiveGraphManagerModule : IModule
    {
        private readonly IRegionManager regionManager;

        public LiveGraphManagerModule(IUnityContainer containerProvider, IRegionManager manager)
        {
            this.regionManager = manager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<LiveGraphViewModel>();
            containerRegistry.Register<ILiveGraphManager, Model.Implementations.LiveGraphManager>();
            containerRegistry.RegisterSingleton<ILiveGraphViewsContainerManager, LiveGraphViewsContainerManager>();

            regionManager.RegisterViewWithRegion("LiveGraphViewsContainerManagerView", typeof(LiveGraphViewsContainerManagerView));
        }
    }
}
