using E3Tech.Navigation;
using MainMotorPanel.UI.ViewModels;
using MainMotorPanel.UI.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace MainMotorPanel.UI
{
    public class UIModule : IModule
    {
        IRegionManager regionManager;
        IViewManager viewManager;

        public UIModule(IRegionManager regionManager, IViewManager viewManager)
        {
            this.regionManager = regionManager;
            this.viewManager = viewManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            containerProvider.Resolve<MainMotorPanelViewModel>();
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton(typeof(MainMotorPanelViewModel));

            containerRegistry.RegisterForNavigation(typeof(DashboardView), "Dashboard");
            containerRegistry.RegisterForNavigation(typeof(MainMotor), "MainMotor");
            containerRegistry.RegisterForNavigation(typeof(NavigationControlsView), "NavigationControls");
            containerRegistry.RegisterForNavigation(typeof(HomeView), "Home");
            containerRegistry.RegisterForNavigation(typeof(StartSessionView), "StartSession");
            containerRegistry.RegisterForNavigation(typeof(ExistingSessionView), "ExistingSession");
            containerRegistry.RegisterForNavigation(typeof(ReportPreviewView), "ReportPreview");

            viewManager.AddView("MainMotor");

            regionManager.RegisterViewWithRegion("RegisteredViewsPane", typeof(NavigationControlsView));
        }
    }
}
