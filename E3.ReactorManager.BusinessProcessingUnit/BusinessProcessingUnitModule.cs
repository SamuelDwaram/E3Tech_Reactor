using E3.ReactorManager.BusinessProcessingUnit.Model.Helpers;
using E3.ReactorManager.BusinessProcessingUnit.Model.Interfaces;
using Prism.Ioc;
using Prism.Modularity;
using Unity;

namespace E3.ReactorManager.BusinessProcessingUnit
{
    public class BusinessProcessingUnitModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            containerProvider.Resolve<IBusinessProcessingUnit>();
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<QueueMessageParser>();
            containerRegistry.RegisterSingleton<IBusinessProcessingUnit, Model.Implementations.BusinessProcessingUnit>();
        }
    }
}
