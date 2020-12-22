using E3.MqttProvider.Services;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace E3.MqttProvider
{
    public class MqttProviderModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            containerProvider.Resolve<IMqttManager>();
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<ISerializer, DefaultSerializer>();
            containerRegistry.RegisterSingleton<IMqttManager, MqttManager>();
        }
    }
}