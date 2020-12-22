using Unity;

namespace E3Tech.Camera.BaslerCamera
{
    public class BaslerCameraInstanceProvider
    {
        static IUnityContainer unityContainer;

        public BaslerCameraInstanceProvider(IUnityContainer containerProvider)
        {
            unityContainer = containerProvider;
        }

        public static T GetInstance<T>()
        {
            if (unityContainer.IsRegistered<T>())
            {
                return unityContainer.Resolve<T>();
            }

            return default;
        }
    }
}
