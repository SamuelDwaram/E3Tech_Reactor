using System.Collections.Generic;

namespace E3Tech.RecipeBuilding.Model
{
    public interface IRecipeExporter
    {
        void Export(IList<RecipeStep> recipe);
    }
}
