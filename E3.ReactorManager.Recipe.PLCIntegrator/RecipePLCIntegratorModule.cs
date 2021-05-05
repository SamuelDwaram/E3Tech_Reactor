using E3Tech.RecipeBuilding.Model;
using Prism.Ioc;
using Prism.Modularity;
using Unity;

namespace E3.ReactorManager.Recipe.PLCIntegrator
{
    public class RecipePLCIntegratorModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IRecipeExecutor, PLCRecipeExecutor>();
            containerRegistry.Register<IRecipeReloader, PLCRecipeReloader>();
            containerRegistry.RegisterInstance<IRecipesManager>(RecipesManager.Instance);
        }
    }
}
