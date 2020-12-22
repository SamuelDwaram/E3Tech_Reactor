using E3.ReactorManager.VideoCaptureDevicesHandler.Model;
using E3.ReactorManager.VideoCaptureDevicesHandler.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace E3.ReactorManager.VideoCaptureDevicesHandler
{
    public class VideoCaptureDevicesHandlerModule : IModule
    {
        IRegionManager regionManager;

        public VideoCaptureDevicesHandlerModule(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            containerProvider.Resolve<IVideoCaptureDevicesHandler>();
            containerProvider.Resolve<CapturedImageHandler>();
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IVideoCaptureDevicesHandler, Model.VideoCaptureDevicesHandler>();

            regionManager.RegisterViewWithRegion("SelectedCaptureDeviceView", typeof(SelectedCaptureDeviceView));
        }
    }
}