using Unity;

namespace E3.RemoteClient.Ui.Helpers
{
    public class AnyInstanceExtractor
    {
        private static IUnityContainer unityContainer;

        public AnyInstanceExtractor(IUnityContainer containerProvider)
        {
            unityContainer = containerProvider;
        }

        public static T GetInstance<T>()
        {
            return unityContainer.Resolve<T>();
        }
    }
}
