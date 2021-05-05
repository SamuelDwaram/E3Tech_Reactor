using E3.ReactorManager.Interfaces.HardwareAbstractionLayer;
using E3Tech.RecipeBuilding.Model;
using E3Tech.RecipeBuilding.Model.Blocks;
using E3Tech.RecipeBuilding.ParameterProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity;

namespace E3.ReactorManager.Recipe.PLCIntegrator
{
    public class PLCRecipeReloader : IRecipeReloader
    {
        IFieldDevicesCommunicator fieldDevicesCommunicator;
        IUnityContainer containerProvider;
        public IList<IRecipeBlock> AvailableBlocks { get; private set; }

        public PLCRecipeReloader(IUnityContainer containerProvider)
        {
            this.containerProvider = containerProvider;

            fieldDevicesCommunicator = containerProvider.Resolve<IFieldDevicesCommunicator>();

            LoadRegisteredBlocks();
        }

        public bool CheckIfRecipeStepContainsRecipeBlock(string deviceId, string blockName, int stepIndex)
        {
            if (blockName == "Start")
            {
                if (stepIndex == 0)
                {
                    return true;
                }

                return false;
            }

            if (blockName == "End")
            {
                return false;
            }

            if (stepIndex > 0)
            {
                return fieldDevicesCommunicator.ReadFieldPointValue<bool>(deviceId, blockName + "Enabled_" + stepIndex);
            }

            return false;
        }

        public int GetNumberOfRecipeSteps(string deviceId)
        {
            return fieldDevicesCommunicator.ReadFieldPointValue<int>(deviceId, "NumberOfRecipeSteps");
        }

        public IRecipeBlock GetRecipeBlockInstance(int stepIndex, string blockName, string deviceId)
        {
            switch (blockName)
            {
                case "Start":
                    return GetStartBlockInstance(deviceId, stepIndex);
                case "HeatCool":
                    return GetHeatCoolBlockInstance(deviceId, stepIndex);
                case "Stirrer":
                    return GetStirrerBlockInstance(deviceId, stepIndex);
                case "Dosing":
                    return GetDosingBlockInstance(deviceId, stepIndex);
                case "Wait":
                    return GetWaitBlockInstance(deviceId, stepIndex);
                case "Fill":
                    return GetFillBlockInstance(deviceId, stepIndex);
                case "Transfer":
                    return GetTransferBlockInstance(deviceId, stepIndex);
                default:
                    return null;
            }
        }

        #region Get Recipe Block Instances
        private IRecipeBlock GetTransferBlockInstance(string deviceId, int stepIndex)
        {
            IRecipeBlock transferRecipeBlock = new ParameterizedRecipeBlock<TransferBlockParameters>();

            transferRecipeBlock.UpdateParameterValue("Started", fieldDevicesCommunicator.ReadFieldPointValue<string>(deviceId, "TransferStarted_" + stepIndex));
            transferRecipeBlock.UpdateParameterValue("StartedTime", fieldDevicesCommunicator.ReadFieldPointValue<string>(deviceId, "TransferStartedTime_" + stepIndex));
            transferRecipeBlock.UpdateParameterValue("Ended", fieldDevicesCommunicator.ReadFieldPointValue<string>(deviceId, "TransferEnded_" + stepIndex));
            transferRecipeBlock.UpdateParameterValue("EndedTime", fieldDevicesCommunicator.ReadFieldPointValue<string>(deviceId, "TransferEndedTime_" + stepIndex));
            transferRecipeBlock.UpdateParameterValue("TargetItemIndex", fieldDevicesCommunicator.ReadFieldPointValue<string>(deviceId, "TransferTargetItemIndex_" + stepIndex));
            transferRecipeBlock.UpdateParameterValue("SourceItemIndex", fieldDevicesCommunicator.ReadFieldPointValue<string>(deviceId, "TransferSourceItemIndex_" + stepIndex));
            transferRecipeBlock.UpdateParameterValue("Volume", fieldDevicesCommunicator.ReadFieldPointValue<string>(deviceId, "TransferVolume_" + stepIndex));

            return transferRecipeBlock;
        }

        private IRecipeBlock GetFillBlockInstance(string deviceId, int stepIndex)
        {
            IRecipeBlock fillRecipeBlock = new ParameterizedRecipeBlock<FillBlockParameters>();

            fillRecipeBlock.UpdateParameterValue("Started", fieldDevicesCommunicator.ReadFieldPointValue<string>(deviceId, "FillStarted_" + stepIndex));
            fillRecipeBlock.UpdateParameterValue("StartedTime", fieldDevicesCommunicator.ReadFieldPointValue<string>(deviceId, "FillStartedTime_" + stepIndex));
            fillRecipeBlock.UpdateParameterValue("Ended", fieldDevicesCommunicator.ReadFieldPointValue<string>(deviceId, "FillEnded_" + stepIndex));
            fillRecipeBlock.UpdateParameterValue("EndedTime", fieldDevicesCommunicator.ReadFieldPointValue<string>(deviceId, "FillEndedTime_" + stepIndex));
            fillRecipeBlock.UpdateParameterValue("TargetItemIndex", fieldDevicesCommunicator.ReadFieldPointValue<string>(deviceId, "FillTargetItemIndex_" + stepIndex));
            fillRecipeBlock.UpdateParameterValue("Volume", fieldDevicesCommunicator.ReadFieldPointValue<string>(deviceId, "FillVolume_" + stepIndex));

            return fillRecipeBlock;
        }
        private IRecipeBlock GetStartBlockInstance(string deviceId, int stepIndex)
        {
            IRecipeBlock startRecipeBlock = new ParameterizedRecipeBlock<StartBlockParameters>();

            startRecipeBlock.UpdateParameterValue("Started", fieldDevicesCommunicator.ReadFieldPointValue<string>(deviceId, "Started_" + stepIndex));
            startRecipeBlock.UpdateParameterValue("StartedTime", fieldDevicesCommunicator.ReadFieldPointValue<string>(deviceId, "StartedTime_" + stepIndex));
            startRecipeBlock.UpdateParameterValue("Ended", fieldDevicesCommunicator.ReadFieldPointValue<string>(deviceId, "Ended_" + stepIndex));
            startRecipeBlock.UpdateParameterValue("EndedTime", fieldDevicesCommunicator.ReadFieldPointValue<string>(deviceId, "EndedTime_" + stepIndex));
            startRecipeBlock.UpdateParameterValue("HeatCoolModeSelection", fieldDevicesCommunicator.ReadFieldPointValue<string>(deviceId, "HeatCoolModeSelection_" + stepIndex));
            startRecipeBlock.UpdateParameterValue("HeatCoolDeltaTemp", fieldDevicesCommunicator.ReadFieldPointValue<string>(deviceId, "HeatCoolDeltaTemp_" + stepIndex));

            return startRecipeBlock;
        }

        private IRecipeBlock GetHeatCoolBlockInstance(string deviceId, int stepIndex)
        {
            IRecipeBlock heatCoolRecipeBlock = new ParameterizedRecipeBlock<HeatCoolBlockParameters>();

            heatCoolRecipeBlock.UpdateParameterValue("Started", fieldDevicesCommunicator.ReadFieldPointValue<string>(deviceId, "HeatCoolStarted_" + stepIndex));
            heatCoolRecipeBlock.UpdateParameterValue("StartedTime", fieldDevicesCommunicator.ReadFieldPointValue<string>(deviceId, "HeatCoolStartedTime_" + stepIndex));
            heatCoolRecipeBlock.UpdateParameterValue("Ended", fieldDevicesCommunicator.ReadFieldPointValue<string>(deviceId, "HeatCoolEnded_" + stepIndex));
            heatCoolRecipeBlock.UpdateParameterValue("EndedTime", fieldDevicesCommunicator.ReadFieldPointValue<string>(deviceId, "HeatCoolEndedTime_" + stepIndex));
            heatCoolRecipeBlock.UpdateParameterValue("OperatingMode", fieldDevicesCommunicator.ReadFieldPointValue<string>(deviceId, "HeatCoolOperatingMode_" + stepIndex));
            heatCoolRecipeBlock.UpdateParameterValue("SetPoint", fieldDevicesCommunicator.ReadFieldPointValue<string>(deviceId, "HeatCoolSetPoint_" + stepIndex));
            heatCoolRecipeBlock.UpdateParameterValue("Duration", fieldDevicesCommunicator.ReadFieldPointValue<string>(deviceId, "HeatCoolDuration_" + stepIndex));

            return heatCoolRecipeBlock;
        }

        private IRecipeBlock GetStirrerBlockInstance(string deviceId, int stepIndex)
        {
            IRecipeBlock stirrerRecipeBlock = new ParameterizedRecipeBlock<StirrerBlockParameters>();

            stirrerRecipeBlock.UpdateParameterValue("Started", fieldDevicesCommunicator.ReadFieldPointValue<string>(deviceId, "StirrerStarted_" + stepIndex));
            stirrerRecipeBlock.UpdateParameterValue("StartedTime", fieldDevicesCommunicator.ReadFieldPointValue<string>(deviceId, "StirrerStartedTime_" + stepIndex));
            stirrerRecipeBlock.UpdateParameterValue("Ended", fieldDevicesCommunicator.ReadFieldPointValue<string>(deviceId, "StirrerEnded_" + stepIndex));
            stirrerRecipeBlock.UpdateParameterValue("EndedTime", fieldDevicesCommunicator.ReadFieldPointValue<string>(deviceId, "StirrerEndedTime_" + stepIndex));
            stirrerRecipeBlock.UpdateParameterValue("SetPoint", fieldDevicesCommunicator.ReadFieldPointValue<string>(deviceId, "StirrerSetPoint_" + stepIndex));

            return stirrerRecipeBlock;
        }

        private IRecipeBlock GetDosingBlockInstance(string deviceId, int stepIndex)
        {
            IRecipeBlock dosingRecipeBlock = new ParameterizedRecipeBlock<DosingBlockParameters>();

            dosingRecipeBlock.UpdateParameterValue("Started", fieldDevicesCommunicator.ReadFieldPointValue<string>(deviceId, "DosingStarted_" + stepIndex));
            dosingRecipeBlock.UpdateParameterValue("StartedTime", fieldDevicesCommunicator.ReadFieldPointValue<string>(deviceId, "DosingStartedTime_" + stepIndex));
            dosingRecipeBlock.UpdateParameterValue("Ended", fieldDevicesCommunicator.ReadFieldPointValue<string>(deviceId, "DosingEnded_" + stepIndex));
            dosingRecipeBlock.UpdateParameterValue("EndedTime", fieldDevicesCommunicator.ReadFieldPointValue<string>(deviceId, "DosingEndedTime_" + stepIndex));
            dosingRecipeBlock.UpdateParameterValue("MaxAmount", fieldDevicesCommunicator.ReadFieldPointValue<string>(deviceId, "DosingMaxAmount_" + stepIndex));
            dosingRecipeBlock.UpdateParameterValue("RemainingDosableAmount", fieldDevicesCommunicator.ReadFieldPointValue<string>(deviceId, "DosingRemainingDosableAmount_" + stepIndex));
            dosingRecipeBlock.UpdateParameterValue("StopTemperature", fieldDevicesCommunicator.ReadFieldPointValue<string>(deviceId, "DosingStopTemperature_" + stepIndex));
            dosingRecipeBlock.UpdateParameterValue("ResumeTemperature", fieldDevicesCommunicator.ReadFieldPointValue<string>(deviceId, "DosingResumeTemperature_" + stepIndex));
            dosingRecipeBlock.UpdateParameterValue("RateSetPointExpression", fieldDevicesCommunicator.ReadFieldPointValue<string>(deviceId, "DosingRateSetPointExpression_" + stepIndex));
            dosingRecipeBlock.UpdateParameterValue("ResumeTemperature", fieldDevicesCommunicator.ReadFieldPointValue<string>(deviceId, "DosingResumeTemperature_" + stepIndex));
            dosingRecipeBlock.UpdateParameterValue("MinRate", fieldDevicesCommunicator.ReadFieldPointValue<string>(deviceId, "DosingMinRate_" + stepIndex));
            dosingRecipeBlock.UpdateParameterValue("MaxRate", fieldDevicesCommunicator.ReadFieldPointValue<string>(deviceId, "DosingMaxRate_" + stepIndex));
            dosingRecipeBlock.UpdateParameterValue("SettlingTime", fieldDevicesCommunicator.ReadFieldPointValue<string>(deviceId, "DosingSettlingTime_" + stepIndex));
            dosingRecipeBlock.UpdateParameterValue("MinPh", fieldDevicesCommunicator.ReadFieldPointValue<string>(deviceId, "DosingMinPh_" + stepIndex));
            dosingRecipeBlock.UpdateParameterValue("MaxPh", fieldDevicesCommunicator.ReadFieldPointValue<string>(deviceId, "DosingMaxPh_" + stepIndex));
            dosingRecipeBlock.UpdateParameterValue("OperatingMode", fieldDevicesCommunicator.ReadFieldPointValue<string>(deviceId, "DosingOperatingMode_" + stepIndex));

            return dosingRecipeBlock;
        }

        private IRecipeBlock GetWaitBlockInstance(string deviceId, int stepIndex)
        {
            IRecipeBlock waitRecipeBlock = new ParameterizedRecipeBlock<WaitBlockParameters>();

            waitRecipeBlock.UpdateParameterValue("Started", fieldDevicesCommunicator.ReadFieldPointValue<string>(deviceId, "WaitStarted_" + stepIndex));
            waitRecipeBlock.UpdateParameterValue("StartedTime", fieldDevicesCommunicator.ReadFieldPointValue<string>(deviceId, "WaitStartedTime_" + stepIndex));
            waitRecipeBlock.UpdateParameterValue("Ended", fieldDevicesCommunicator.ReadFieldPointValue<string>(deviceId, "WaitEnded_" + stepIndex));
            waitRecipeBlock.UpdateParameterValue("EndedTime", fieldDevicesCommunicator.ReadFieldPointValue<string>(deviceId, "WaitEndedTime_" + stepIndex));
            waitRecipeBlock.UpdateParameterValue("Duration", fieldDevicesCommunicator.ReadFieldPointValue<string>(deviceId, "WaitDuration_" + stepIndex));
            waitRecipeBlock.UpdateParameterValue("RemainingTime", fieldDevicesCommunicator.ReadFieldPointValue<string>(deviceId, "WaitRemainingTime_" + stepIndex));

            return waitRecipeBlock;
        }

        #endregion

        public bool GetRecipeStatus(string deviceId)
        {
            return fieldDevicesCommunicator.ReadFieldPointValue<bool>(deviceId, "RecipeStatus");
        }

        public bool GetRecipeEndedStatus(string deviceId)
        {
            return fieldDevicesCommunicator.ReadFieldPointValue<bool>(deviceId, "RecipeEnded");
        }

        private void LoadRegisteredBlocks()
        {
            AvailableBlocks = new List<IRecipeBlock>();
            IEnumerable<IRecipeBlock> blocks = containerProvider.ResolveAll<IRecipeBlock>().Where(i => !string.IsNullOrWhiteSpace(i.Name));

            if (blocks.Count() > 0)
            {
                /*
                 * Check if the recipe blocks are loaded in the UnityContainer
                 * and Update Available Blocks with the List returned from the UnityContainer
                 */
                AvailableBlocks = new List<IRecipeBlock>(blocks);
            }
        }

        public IList<RecipeStep> ReloadRecipe(object arg)
        {
            string deviceId = (string)arg;

            var recipeSteps = new List<RecipeStep>();
            var recipeStepsCount = GetNumberOfRecipeSteps(deviceId);

            for (int stepIndex = 0; stepIndex < recipeStepsCount; stepIndex++)
            {
                var step = new RecipeStep();
                foreach (var recipeBlock in AvailableBlocks)
                {
                    if (CheckIfRecipeStepContainsRecipeBlock(deviceId, recipeBlock.Name, stepIndex))
                    {
                        /* Try to fill the recipe blocks in the given recipe step */
                        if (step.BlockOne == null)
                        {
                            step.BlockOne = GetRecipeBlockInstance(stepIndex, recipeBlock.Name, deviceId);
                        }
                        else if (step.BlockTwo == null)
                        {
                            step.BlockTwo = GetRecipeBlockInstance(stepIndex, recipeBlock.Name, deviceId);
                        }
                        else if (step.BlockThree == null)
                        {
                            step.BlockThree = GetRecipeBlockInstance(stepIndex, recipeBlock.Name, deviceId);
                        }
                        else if (step.BlockFour == null)
                        {
                            step.BlockFour = GetRecipeBlockInstance(stepIndex, recipeBlock.Name, deviceId);
                        }
                    }
                }

                /* Add step to recipeSteps */
                recipeSteps.Add(step);
            }

            /* Add a new step at the last containing End Recipe Block as we are not reading it from plc */
            var endStep = new RecipeStep();
            endStep.BlockOne = new ParameterizedRecipeBlock<EndBlockParameters>();
            recipeSteps.Add(endStep);

            return recipeSteps;
        }
    }
}
