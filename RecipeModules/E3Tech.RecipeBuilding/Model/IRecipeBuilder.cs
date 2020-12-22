using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace E3Tech.RecipeBuilding.Model
{
    public interface IRecipeBuilder
    {
        RecipeStep[] RecipeSteps { get; }

        string DeviceId { get; set; }

        bool UpdateStep(RecipeStep recipeStep, int blockIndex, IRecipeBlock block);        

        void Export();

        IList<RecipeStep> ReturnRecipeSteps();

        bool AddNewStep(RecipeStep step, IRecipeBlock recipeBlock);

        bool AddNewStepWhileRunningRecipe(RecipeStep currentRecipeStep, RecipeStep newRecipeStep, IRecipeBlock block, int toBeAddedStepIndex);

        bool RemoveStep(RecipeStep step);

        void RemoveBlockFromStep(RecipeStep step, IRecipeBlock obj);

        bool CheckEndBlockInRecipe(IList<RecipeStep> recipeSteps);

        void Clear();

        bool CheckIfRecipeStepContainsAnyExecutingBlock(RecipeStep recipeStep);

        RecipeStep[] Import();

        void ReloadRecipeSteps(Action<Task> action, TaskScheduler taskScheduler);
    }
}
