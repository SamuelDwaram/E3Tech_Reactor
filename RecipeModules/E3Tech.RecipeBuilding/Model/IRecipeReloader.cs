using System.Collections.Generic;
using System.Threading.Tasks;

namespace E3Tech.RecipeBuilding.Model
{
    public interface IRecipeReloader
    {
        IList<RecipeStep> ReloadRecipe(object deviceId);

        bool GetRecipeStatus(string deviceId);

        bool GetRecipeEndedStatus(string deviceId);

        int GetNumberOfRecipeSteps(string deviceId);

        bool CheckIfRecipeStepContainsRecipeBlock(string deviceId, string blockName, int stepIndex);

        IRecipeBlock GetRecipeBlockInstance(int stepIndex, string blockName, string deviceId);
    }
}
