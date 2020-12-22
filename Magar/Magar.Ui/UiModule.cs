using Magar.Ui.ViewModels;
using Magar.Ui.Views;
using Prism.Ioc;
using Prism.Modularity;

namespace Magar.Ui
{
    public class UiModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            containerProvider.Resolve<TanksViewModel>();
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<TanksViewModel>();
            containerRegistry.RegisterForNavigation(typeof(TanksView), "TanksView");
        }
    }
}