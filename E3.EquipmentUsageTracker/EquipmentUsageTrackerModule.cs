using E3.EquipmentUsageTracker.Model;
using E3.EquipmentUsageTracker.Views;
using E3Tech.Navigation;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using System;
using System.Windows;
using Unity;

namespace E3.EquipmentUsageTracker
{
    public class EquipmentUsageTrackerModule : IModule
    {
        IRegionManager regionManager;
        IViewManager viewManager;

        public EquipmentUsageTrackerModule(IUnityContainer containerProvider, IRegionManager regionManager, IViewManager viewManager)
        {
            this.viewManager = viewManager;
            this.regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            containerProvider.Resolve<IEquipmentUsageTracker>();
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IEquipmentUsageTracker, Model.EquipmentUsageTracker>();

            viewManager.AddView("EquipmentUsageTracker");
            containerRegistry.RegisterForNavigation(typeof(EquipmentOccupancyContainerView), "EquipmentUsageTracker");
        }
    }
}
