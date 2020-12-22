using E3.ReactorManager.ParametersProvider.Model;
using Unity;

namespace E3.ReactorManager.ParametersProvider
{
    public class ParametersProviderModuleInstanceProvider
    {
        static IUnityContainer unityContainer;

        public ParametersProviderModuleInstanceProvider(IUnityContainer containerProvider)
        {
            unityContainer = containerProvider;
        }

        public static IParametersProvider GetInstance()
        {
            return unityContainer.Resolve<IParametersProvider>();
        }
    }
}
