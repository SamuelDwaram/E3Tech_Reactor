using E3.SessionManager.Services;
using Prism.Ioc;
using Prism.Modularity;

namespace E3.SessionManager
{
    public class SessionManagerModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
 
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<ISessionManager, Services.SessionManager>();
        }
    }
}