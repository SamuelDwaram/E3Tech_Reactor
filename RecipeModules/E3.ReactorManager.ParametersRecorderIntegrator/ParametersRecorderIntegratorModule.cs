using Prism.Ioc;
using Prism.Modularity;
using E3.ReactorManager.ParametersRecorderIntegrator.Model;

namespace E3.ReactorManager.ParametersRecorderIntegrator
{
    public class ParametersRecorderIntegratorModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IParametersRecorderIntegrator, Model.ParametersRecorderIntegrator>();
        }
    }
}
