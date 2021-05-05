using Anathem.Ui.Views;
using Prism.Ioc;
using Prism.Modularity;

namespace Anathem.Ui
{
    public class UiModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
 
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation(typeof(DashboardView), "Dashboard");
            containerRegistry.RegisterForNavigation(typeof(ReactorControlView), "ReactorControl");
            containerRegistry.RegisterForNavigation(typeof(InitializeView), "Initialize");
            containerRegistry.RegisterForNavigation(typeof(RecipeBuilderView), "RecipeDesigner");
        }
    }
}