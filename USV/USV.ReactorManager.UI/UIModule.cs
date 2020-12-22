using E3Tech.IO.FileAccess;
using E3Tech.Navigation;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using USV.ReactorManager.UI.Views;

namespace USV.ReactorManager.UI
{
    public class UIModule : IModule
    {
        IViewManager viewManager;
        IRegionManager regionManager;

        public UIModule(IViewManager viewManager, IRegionManager regionManager)
        {
            this.viewManager = viewManager;
            this.regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IFileBrowser, DefaultFileBrowser>();
            regionManager.RegisterViewWithRegion("FieldDeviceParameters", typeof(FieldDeviceParametersView));

            viewManager.AddView("Login");
            containerRegistry.RegisterForNavigation(typeof(LoginView), "Login");

            viewManager.AddView("Dashboard");
            containerRegistry.RegisterForNavigation(typeof(DashboardView), "Dashboard");

            viewManager.AddView("ReactorControl");
            containerRegistry.RegisterForNavigation(typeof(ReactorControlView), "ReactorControl");

            viewManager.AddView("RecipeDesigner");
            containerRegistry.RegisterForNavigation(typeof(RecipeDesignerView), "RecipeDesigner");

            viewManager.AddView("Reports");
            containerRegistry.RegisterForNavigation(typeof(ReportsView), "Reports");
        }
    }
}
