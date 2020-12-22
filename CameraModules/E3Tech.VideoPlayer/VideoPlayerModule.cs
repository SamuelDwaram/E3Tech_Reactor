using E3Tech.IO.FileAccess;
using E3Tech.Navigation;
using E3Tech.VideoPlayer.Model;
using NLogger;
using Prism.Events;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Unity;
using Player =E3Tech.VideoPlayer.Views.VideoPlayer;

namespace E3Tech.VideoPlayer
{
    public class VideoPlayerModule : IModule
    {
        private readonly IUnityContainer container;
        private readonly IRegionManager regionManager;
        private readonly IEventAggregator eventAggregator;
        private readonly ILogger logger;
        private readonly IViewManager viewManager;

        public VideoPlayerModule(IUnityContainer container, IRegionManager manager, IEventAggregator eventAggregator, ILogger logger)
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
            containerRegistry.Register<IFileBrowser, DefaultFileBrowser>();
            containerRegistry.Register<IMediaPlayerController, MediaPlayerController>();
            regionManager.RegisterViewWithRegion("VideoPlayer", typeof(Player));
            viewManager.AddView("VideoPlayer");
            //Add a new region with the same Type of Player for using two prism views in parallel
            regionManager.RegisterViewWithRegion("VideoPlayer_2", typeof(Player));
            viewManager.AddView("VideoPlayer_2");

        }
    }
}