using E3.ReactorManager.ParametersProvider.Model;
using Prism.Ioc;
using Prism.Modularity;

namespace E3.ReactorManager.ParametersProvider
{
    public class ParametersProviderModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IParametersProvider, DefaultParametersProvider>();
        }
    }
}
