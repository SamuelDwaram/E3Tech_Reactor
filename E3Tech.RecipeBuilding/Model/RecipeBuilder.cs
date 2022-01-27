using E3Tech.RecipeBuilding.Model.Blocks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace E3Tech.RecipeBuilding.Model
{
    internal class RecipeBuilder : IRecipeBuilder, INotifyPropertyChanged
    {
        private IRecipeExporter recipeExporter;
        private IRecipeImporter recipeImporter;
        private readonly IRecipeRulesValidator recipeRulesValidator;
        private readonly IRecipeRefresher recipeRefresher;
        private readonly IRecipeReloader recipeReloader;
        private List<RecipeStep> recipeSteps;

        public RecipeBuilder(IRecipeRulesValidator recipeRulesValidator, IRecipeExporter recipeExporter, IRecipeImporter recipeImporter, IRecipeRefresher recipeRefresher, IRecipeReloader recipeReloader)
        {
            this.recipeExporter = recipeExporter;
            this.recipeImporter = recipeImporter;
            this.recipeRulesValidator = recipeRulesValidator;
            this.recipeRefresher = recipeRefresher;
            this.recipeReloader = recipeReloader;
            this.recipeRefresher.RefreshBlockRecieved += RecipeRefresher_RefreshBlock;
            recipeSteps = new List<RecipeStep>();
        }

        private void UpdateRecipeSteps(Task<IList<RecipeStep>> task)
        {
            if (task.IsCompleted)
            {
                recipeSteps = new List<RecipeStep>(task.Result);
            }
            else
            {
                if (task.IsFaulted)
                {
                    // display error to user
                }
            }
        }

        #region Recipe Refreshers
        private void RecipeRefresher_RefreshBlock(object sender, RefreshBlockEventArgs e)
        {
            if (e.Id == DeviceId)
            {
                RefreshBlock(e.StepIndex, e.BlockName, e.ParameterName, e.ParameterValue);
            }
        }

        public void RefreshBlock(int stepIndex, string blockName, string parameterName, string paremeterValue)
        {
            if (recipeSteps.Count < 1 || recipeSteps.Count <= stepIndex)
            {
                return;
            }

            IRecipeBlock block = null;
            block = GetBlockByName(blockName, recipeSteps[stepIndex], block);

            if (block != null)
            {
                block.UpdateParameterValue(parameterName, paremeterValue);
            }
        }

        private static IRecipeBlock GetBlockByName(string blockName, RecipeStep updateStep, IRecipeBlock block)
        {
            if (updateStep.BlockOne != null && updateStep.BlockOne.Name == blockName)
            {
                block = updateStep.BlockOne;
            }
            else if (updateStep.BlockTwo != null && updateStep.BlockTwo.Name == blockName)
            {
                block = updateStep.BlockTwo;
            }
            else if (updateStep.BlockThree != null && updateStep.BlockThree.Name == blockName)
            {
                block = updateStep.BlockThree;
            }
            else if (updateStep.BlockFour != null && updateStep.BlockFour.Name == blockName)
            {
                block = updateStep.BlockFour;
            }

            return block;
        }

        #endregion

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool UpdateStep(RecipeStep recipeStep, int blockIndex, IRecipeBlock block)
        {
            if (recipeRulesValidator.ValidateAddingNewBlockToStep(recipeStep, block, blockIndex))
            {
                switch (blockIndex)
                {
                    case 0:
                    default:
                        recipeStep.BlockOne = block;
                        break;
                    case 1:
                        recipeStep.BlockTwo = block;
                        break;
                    case 2:
                        recipeStep.BlockThree = block;
                        break;
                    case 3:
                        recipeStep.BlockFour = block;
                        break;
                }
                return true;
            }
            return false;
        }

        public void Export()
        {
            recipeExporter.Export(this.RecipeSteps);
        }

        public bool AddNewStep(RecipeStep step, IRecipeBlock recipeBlock)
        {
            if (recipeRulesValidator.ValidateAddingNewStep(recipeSteps.ToArray(), recipeBlock, 0))
            {
                step.BlockOne = recipeBlock;
                step.Name = "Step " + recipeSteps.Count;
                recipeSteps.Add(step);
                return true;
            }
            return false;
        }

        public bool AddNewStepWhileRunningRecipe(RecipeStep currentRecipeStep, RecipeStep newRecipeStep, IRecipeBlock block, int toBeAddedStepIndex)
        {
            if (recipeRulesValidator.ValidateAddingNewStepWhileRunningRecipe(recipeSteps.ToArray(), currentRecipeStep, newRecipeStep, block, recipeSteps.Count, toBeAddedStepIndex))
            {
                newRecipeStep.BlockOne = block;
                newRecipeStep.Name = "Step " + toBeAddedStepIndex;
                recipeSteps.Insert(toBeAddedStepIndex, newRecipeStep);
                UpdateRecipeStepNames();
                return true;
            }

            return false;
        }

        private void UpdateRecipeStepNames()
        {
            int stepIndex = 0;
            foreach (RecipeStep step in recipeSteps)
            {
                step.Name = "Step " + stepIndex;
                stepIndex += 1;
            }
        }

        public bool RemoveStep(RecipeStep step)
        {
            if (recipeRulesValidator.ValidateDelete(recipeSteps, step))
            {
                if (recipeSteps.Remove(step))
                {
                    RaisePropertyChanged("RecipeSteps");
                    return true;
                }
            }
            return false;
        }

        public void RemoveBlockFromStep(RecipeStep step, IRecipeBlock block)
        {
            if (recipeRulesValidator.ValidateDeleteBlock(step, block))
            {
                if (step.BlockOne?.Name == block.Name)
                {
                    step.BlockOne = null;
                }
                else if (step.BlockTwo?.Name == block.Name)
                {
                    step.BlockTwo = null;
                }
                else if (step.BlockThree?.Name == block.Name)
                {
                    step.BlockThree = null;
                }
                else if (step.BlockFour?.Name == block.Name)
                {
                    step.BlockFour = null;
                }
            }
        }

        public bool CheckEndBlockInRecipe(IList<RecipeStep> recipeSteps)
        {
            return recipeRulesValidator.CheckEndBlockInRecipe(recipeSteps);
        }

        public void Clear()
        {
            recipeSteps.Clear();
        }

        public RecipeStep[] Import()
        {
            IList<RecipeStep> importedRecipeSteps = recipeImporter.Import();

            if (importedRecipeSteps != null)
            {
                recipeSteps = importedRecipeSteps.ToList();
                return recipeSteps.ToArray();
            }
            return null;
        }

        public IList<RecipeStep> ReturnRecipeSteps()
        {
            return RecipeSteps;
        }

        public void ReloadRecipeSteps(Action<Task> action, TaskScheduler taskScheduler)
        {
            Task.Factory.StartNew(new Func<object, IList<RecipeStep>>(recipeReloader.ReloadRecipe), DeviceId)
                .ContinueWith(new Action<Task<IList<RecipeStep>>>(UpdateRecipeSteps))
                .ContinueWith(new Action<Task>(action), taskScheduler);
        }

        public bool CheckIfRecipeStepContainsAnyExecutingBlock(RecipeStep recipeStep)
        {
            if (recipeStep.BlockOne != null)
            {
                if (!GetBlockEndedStatus(recipeStep.BlockOne))
                {
                    return true;
                }
            }

            if (recipeStep.BlockTwo != null)
            {
                if (!GetBlockEndedStatus(recipeStep.BlockTwo))
                {
                    return true;
                }
            }

            if (recipeStep.BlockThree != null)
            {
                if (!GetBlockEndedStatus(recipeStep.BlockThree))
                {
                    return true;
                }
            }

            if (recipeStep.BlockFour != null)
            {
                if (!GetBlockEndedStatus(recipeStep.BlockFour))
                {
                    return true;
                }
            }

            return false;
        }

        public bool GetBlockEndedStatus(IRecipeBlock block)
        {
            string blockEndedStatus;

            switch (block.Name)
            {
                case "Start":
                    blockEndedStatus = (block as ParameterizedRecipeBlock<StartBlockParameters>).Parameters.Ended;
                    break;
                case "HeatCool":
                    blockEndedStatus = (block as ParameterizedRecipeBlock<HeatCoolBlockParameters>).Parameters.Ended;
                    break;
                case "Stirrer":
                    blockEndedStatus = (block as ParameterizedRecipeBlock<StirrerBlockParameters>).Parameters.Ended;
                    break;
                case "N2Purge":
                    blockEndedStatus = (block as ParameterizedRecipeBlock<N2PurgeBlockParameters>).Parameters.Ended;
                    break;
                case "Wait":
                    blockEndedStatus = (block as ParameterizedRecipeBlock<WaitBlockParameters>).Parameters.Ended;
                    break;
                case "Transfer":
                    blockEndedStatus = (block as ParameterizedRecipeBlock<TransferBlockParameters>).Parameters.Ended;
                    break;
                case "Drain":
                    blockEndedStatus = (block as ParameterizedRecipeBlock<DrainBlockParameters>).Parameters.Ended;
                    break;
                case "Flush":
                    blockEndedStatus = (block as ParameterizedRecipeBlock<FlushBlockParameters>).Parameters.Ended;
                    break;
                default:
                    blockEndedStatus = string.Empty;
                    break;
            }

            if (string.IsNullOrWhiteSpace(blockEndedStatus))
            {
                return false;
            }
            else
            {
                return bool.Parse(blockEndedStatus);
            }
        }

        public RecipeStep[] RecipeSteps => recipeSteps.ToArray();

        public string Id { get; set; }

        public string DeviceId { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
