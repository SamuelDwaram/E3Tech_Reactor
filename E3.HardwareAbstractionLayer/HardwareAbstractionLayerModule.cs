using E3.HardwareAbstractionLayer.Model;
using E3.ReactorManager.Interfaces.HardwareAbstractionLayer;
using Prism.Ioc;
using Prism.Modularity;

namespace E3.HardwareAbstractionLayer
{
    public class HardwareAbstractionLayerModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            containerProvider.Resolve<FieldDevicesCommunicator>();
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<FieldDevicesWrapper>();
            containerRegistry.RegisterSingleton<IFieldDevicesCommunicator, FieldDevicesCommunicator>();
        }
    }
}
