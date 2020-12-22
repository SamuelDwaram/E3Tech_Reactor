using E3Tech.Navigation;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Eisai.ReactorManager.UI.Views;
using Eisai.ReactorManager.UI.Models;

namespace Eisai.ReactorManager.UI
{
    public class UiModule : IModule
    {
        private readonly IViewManager viewManager;
        private readonly IRegionManager regionManager;

        public UiModule(IViewManager viewManager, IRegionManager regionManager)
        {
            this.viewManager = viewManager;
            this.regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<DevicesReportHandler>();
            regionManager.RegisterViewWithRegion("FieldDeviceParameters", typeof(FieldDeviceParametersView));
            regionManager.RegisterViewWithRegion("Logo", typeof(Logo));

            viewManager.AddView("Login");

            viewManager.AddView("Dashboard");
            containerRegistry.RegisterForNavigation(typeof(DashboardView), "Dashboard");

            viewManager.AddView("ReactorControl");
            containerRegistry.RegisterForNavigation(typeof(ReactorControlView), "ReactorControl");

            viewManager.AddView("ChillerControl");
            containerRegistry.RegisterForNavigation(typeof(ChillerControlView), "ChillerControl");

            viewManager.AddView("OtherEquipment");
            containerRegistry.RegisterForNavigation(typeof(OtherEquipmentView), "OtherEquipment");

            viewManager.AddView("RecipeDesigner");
            containerRegistry.RegisterForNavigation(typeof(RecipeDesignerView), "RecipeDesigner");

            viewManager.AddView("Reports");
            containerRegistry.RegisterForNavigation(typeof(ReportsView), "Reports");
        }
    }
}
