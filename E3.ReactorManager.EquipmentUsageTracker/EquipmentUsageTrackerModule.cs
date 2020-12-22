using E3.ReactorManager.EquipmentUsageTracker.Model.Interfaces;
using Prism.Ioc;
using Prism.Modularity;
using Unity;

namespace E3.ReactorManager.EquipmentUsageTracker
{
    public class EquipmentUsageTrackerModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            containerProvider.Resolve<IEquipmentUsageTracker>();
            containerProvider.Resolve<EquipmentUsageTrackerInstanceProvider>();
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IEquipmentUsageTracker, EquipmentUsageTracker>();
        }
    }
}
