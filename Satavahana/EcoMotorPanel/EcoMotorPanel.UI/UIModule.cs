using E3Tech.Navigation;
using EcoMotorPanel.UI.ViewModels;
using EcoMotorPanel.UI.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace EcoMotorPanel.UI
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
            containerProvider.Resolve<EcoMotorPanelViewModel>();
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton(typeof(EcoMotorPanelViewModel));

            containerRegistry.RegisterForNavigation(typeof(DashboardView), "Dashboard");
            containerRegistry.RegisterForNavigation(typeof(EcoMotor), "EcoMotor");
            containerRegistry.RegisterForNavigation(typeof(NavigationControlsView), "NavigationControls");
            containerRegistry.RegisterForNavigation(typeof(HomeView), "Home");
            containerRegistry.RegisterForNavigation(typeof(StartSessionView), "StartSession");
            containerRegistry.RegisterForNavigation(typeof(ExistingSessionView), "ExistingSession");
            containerRegistry.RegisterForNavigation(typeof(ReportPreviewView), "ReportPreview");

            viewManager.AddView("EcoMotor");

            regionManager.RegisterViewWithRegion("RegisteredViewsPane", typeof(NavigationControlsView));
        }
    }
}
