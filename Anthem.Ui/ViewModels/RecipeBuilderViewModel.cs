using E3Tech.RecipeBuilding.Model;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anathem.Ui.ViewModels
{
    public class RecipeBuilderViewModel : BindableBase
    {
        private readonly IRecipesManager recipesManager;

        public RecipeBuilderViewModel(IRecipesManager recipesManager)
        {
            this.recipesManager = recipesManager;
            LoadRecipes();
        }

        private void LoadRecipes()
        {
            recipesManager.AddRecipe("Reactor_1");
        }
    }
}
