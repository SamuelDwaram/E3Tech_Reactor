using Prism.Ioc;
using Prism.Modularity;

namespace E3.SerializationHandler
{
    public class SerializationHandlerModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<ISerializationHandler, DefaultSerializationHandler>();
        }
    }
}
