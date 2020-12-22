using E3Tech.Camera.BaslerCamera.Model;
using E3Tech.Camera.BaslerCamera.ViewModels;
using E3Tech.Camera.BaslerCamera.Views;
using E3Tech.Navigation;
using E3Tech.Pylon.Wpf.SDK;
using E3Tech.VideoRecorder.StreamSource;
using NLogger;
using Prism.Events;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity.Ioc;
using System;
using Unity;

namespace E3Tech.Camera.BaslerCamera
{
    public class BaslerCameraModule : IModule
    {

        private readonly IUnityContainer container;
        private readonly IRegionManager regionManager;
        private readonly IEventAggregator eventAggregator;
        private readonly ILogger logger;
        private readonly IViewManager viewManager;

        public BaslerCameraModule(IUnityContainer container, IRegionManager manager, IEventAggregator eventAggregator, ILogger logger)
        {
            this.container = container;
            this.regionManager = manager;
            this.eventAggregator = eventAggregator;
            this.logger = logger;
            viewManager = container.Resolve<IViewManager>();
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            containerProvider.Resolve<BaslerCameraInstanceProvider>();
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // We need to register the model before the view due to its dependecy. 
            // We need only one instance of the BaslerViewer to ensure stability and good performance.
            containerRegistry.RegisterSingleton<BaslerViewer>();
            // register the same instance of BaslerViewer under 2 different interfaces for for display and one for recording.
            containerRegistry.RegisterInstance<IVideoStreamSource>(container.Resolve<BaslerViewer>());
            containerRegistry.RegisterInstance<IBaslerViewer>(container.Resolve<BaslerViewer>());

            // We need to register the viewmodel before the view due to its dependecy. 
            container.RegisterType<PylonViewerModel>();
            
            // navigate to the view.
            regionManager.RegisterViewWithRegion("PylonViewer", typeof(PylonViewer));
            viewManager.AddView("PylonViewer");
        }

    }
}