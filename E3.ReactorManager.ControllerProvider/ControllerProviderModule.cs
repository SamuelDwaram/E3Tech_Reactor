using E3.ReactorManager.ControllerProvider.ControllerType;
using E3.ReactorManager.ControllerProvider.Model;
using E3.ReactorManager.ControllerProvider.Model.Enums;
using E3.ReactorManager.ControllerProvider.Model.Interfaces;
using Prism.Ioc;
using Prism.Modularity;

namespace E3.ReactorManager.ControllerProvider
{
    public class ControllerProviderModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<ControllerHandlerFactory>();
            containerRegistry.Register<IController, ModbusRtuController>(ControllerTypeEnum.ModbusRtu.ToString());
        }
    }
}
