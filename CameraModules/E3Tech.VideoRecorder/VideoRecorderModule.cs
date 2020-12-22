using E3Tech.Navigation;
using E3Tech.VideoRecorder.Model;
using E3Tech.VideoRecorder.ViewModels;
using E3Tech.VideoRecorder.Views;
using NLogger;
using Prism.Events;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Unity;
using MyVideoRecorder = E3Tech.VideoRecorder.Model.VideoRecorder;

namespace E3Tech.VideoRecorder
{
    public class VideoRecorderModule : IModule
    {
        private readonly IUnityContainer container;
        private readonly IRegionManager regionManager;
        private readonly IEventAggregator eventAggregator;
        private readonly ILogger logger;
        private readonly IViewManager viewManager;

        public VideoRecorderModule(IUnityContainer container, IRegionManager manager, IEventAggregator eventAggregator, ILogger logger)
        {
            this.container = container;
            this.regionManager = manager;
            this.eventAggregator = eventAggregator;
            this.logger = logger;
            viewManager = container.Resolve<IViewManager>();
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //Register video recorder as singleton to preserve the recording instance at multiple views
            //as we will be using the video recorder views at multiple times so there
            containerRegistry.RegisterSingleton<E3VideoRecorderViewModel>();
            containerRegistry.Register<IVideoRecorder, MyVideoRecorder>();
            regionManager.RegisterViewWithRegion("E3VideoRecorder", typeof(E3VideoRecorder));
            viewManager.AddView("E3VideoRecorder");
        }
    }
}