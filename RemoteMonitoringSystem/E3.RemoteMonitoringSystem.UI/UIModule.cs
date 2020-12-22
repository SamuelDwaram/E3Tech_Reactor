using E3.RemoteMonitoringSystem.UI.Models;
using E3.RemoteMonitoringSystem.UI.Views;
using E3Tech.Navigation;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace E3.RemoteMonitoringSystem.UI
{
    public class UIModule : IModule
    {
        private readonly IRegionManager regionManager;
        private readonly IViewManager viewManager;

        public UIModule(IRegionManager regionManager, IViewManager viewManager)
        {
            this.regionManager = regionManager;
            this.viewManager = viewManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<DevicesReportHandler>();
            regionManager.RegisterViewWithRegion("RegisteredViewsPane", typeof(NavigationControlsView));
            regionManager.RegisterViewWithRegion("DeviceSummaryReport", typeof(DeviceSummaryReportView));
            regionManager.RegisterViewWithRegion("ConsolidatedReport", typeof(ConsolidatedReportView));
            regionManager.RegisterViewWithRegion("DataReport", typeof(DataReportView));
            containerRegistry.RegisterForNavigation(typeof(DashboardView), "Dashboard");
            containerRegistry.RegisterForNavigation(typeof(LoginView), "Login");
            containerRegistry.RegisterForNavigation(typeof(TrendsView), "Trends");
            containerRegistry.RegisterForNavigation(typeof(NavigationControlsView), "NavigationControls");
            containerRegistry.RegisterForNavigation(typeof(ReportsView), "Reports");

            viewManager.AddView("Dashboard");
            viewManager.AddView("Trends");
            viewManager.AddView("Reports");
        }
    }
}
