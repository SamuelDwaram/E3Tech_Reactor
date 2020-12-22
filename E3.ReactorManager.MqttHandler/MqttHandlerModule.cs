using E3.ReactorManager.MqttHandler.Interfaces;
using Prism.Ioc;
using Prism.Modularity;

namespace E3.ReactorManager.MqttHandler
{
    public class MqttHandlerModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IMqttClientHandler, MqttClientHandler>();
        }
    }
}
