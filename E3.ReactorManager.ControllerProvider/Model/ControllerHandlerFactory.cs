using E3.ReactorManager.ControllerProvider.Model.Interfaces;
using Unity;

namespace E3.ReactorManager.ControllerProvider.Model
{
    public class ControllerHandlerFactory
    {
        IUnityContainer unityContainer;

        public ControllerHandlerFactory(IUnityContainer containerProvider)
        {
            unityContainer = containerProvider;
        }

        public IController CreateControllerObject(string controllerProviderName)
        {
            return unityContainer.Resolve<IController>(controllerProviderName);
        }
    }
}
