using Unity;

namespace E3.ReactorManager.EquipmentUsageTracker
{
    public class EquipmentUsageTrackerInstanceProvider
    {
        static IUnityContainer unityContainer;

        public EquipmentUsageTrackerInstanceProvider(IUnityContainer containerProvider)
        {
            unityContainer = containerProvider;
        }

        public static T GetInstance<T>()
        {
            return unityContainer.Resolve<T>();
        }
    }
}
