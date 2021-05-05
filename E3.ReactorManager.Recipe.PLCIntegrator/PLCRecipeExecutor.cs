using E3.ReactorManager.Interfaces.HardwareAbstractionLayer;
using E3Tech.RecipeBuilding.Model;
using E3Tech.RecipeBuilding.Model.Blocks;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity;

namespace E3.ReactorManager.Recipe.PLCIntegrator
{
    public class PLCRecipeExecutor : IRecipeExecutor
    {
        IFieldDevicesCommunicator fieldDevicesCommunicator;

        public PLCRecipeExecutor(IUnityContainer containerProvider)
        {
            fieldDevicesCommunicator = containerProvider.Resolve<IFieldDevicesCommunicator>();
        }

        public void ClearRecipe(string deviceId)
        {
            fieldDevicesCommunicator
                .SendCommandToDevice(deviceId,
                                          "ClearRecipeStatus",
                                          "bool",
                                          bool.TrueString);
        }
        public void Execute(string deviceId, IList<RecipeStep> recipeSteps)
        {
            foreach (var (step, stepIndex) in recipeSteps.Select((v, i) => (v, i)))
            {
                SendBlock(step.BlockOne, stepIndex, deviceId);
                SendBlock(step.BlockTwo, stepIndex, deviceId);
                SendBlock(step.BlockThree, stepIndex, deviceId);
                SendBlock(step.BlockFour, stepIndex, deviceId);
            }
            /*
             * After sending all the recipe blocks data to plc send the end block
             * Decrease the recipe steps count by 1 as last recipe block is End block 
             * and it does not contain any parameters for sending to the plc
             */
            SendEndBlock(recipeSteps.Count - 1, deviceId);
        }

        private void SendBlock(IRecipeBlock block, int stepIndex, string deviceId)
        {
            if (block == null)
                return;
            switch (block.Name)
            {
                case "Start":
                    SendStartBlock(block as ParameterizedRecipeBlock<StartBlockParameters>, deviceId);
                    break;
                case "HeatCool":
                    SendHeatCoolBlock(block as ParameterizedRecipeBlock<HeatCoolBlockParameters>, stepIndex, deviceId);
                    break;
                case "Stirrer":
                    SendStirrerBlock(block as ParameterizedRecipeBlock<StirrerBlockParameters>, stepIndex, deviceId);
                    break;
                case "Dosing":
                    SendDosingBlock(block as ParameterizedRecipeBlock<DosingBlockParameters>, stepIndex, deviceId);
                    break;
                case "Wait":
                    SendWaitBlock(block as ParameterizedRecipeBlock<WaitBlockParameters>, stepIndex, deviceId);
                    break;
                case "Fill":
                    SendFillBlock(block as ParameterizedRecipeBlock<FillBlockParameters>, stepIndex, deviceId);
                    break;
                case "Transfer":
                    SendTransferBlock(block as ParameterizedRecipeBlock<TransferBlockParameters>, stepIndex, deviceId);
                    break;
                default:
                    break;
            }
        }

        private void SendTransferBlock(ParameterizedRecipeBlock<TransferBlockParameters> transferBlock, int stepIndex, string deviceId)
        {
            string volume = transferBlock.Parameters.Volume;
            string sourceItemIndex = transferBlock.Parameters.SourceItemIndex;
            string targetItemIndex = transferBlock.Parameters.TargetItemIndex;

            fieldDevicesCommunicator
                .SendCommandToDevice(deviceId,
                                          "TransferEnabled_" + stepIndex,
                                          "bool",
                                          bool.TrueString);

            fieldDevicesCommunicator
                .SendCommandToDevice(deviceId,
                                          "TransferSourceItemIndex_" + stepIndex,
                                          "int",
                                          sourceItemIndex);

            fieldDevicesCommunicator
                .SendCommandToDevice(deviceId,
                                          "TransferTargetItemIndex_" + stepIndex,
                                          "int",
                                          targetItemIndex);

            fieldDevicesCommunicator
                .SendCommandToDevice(deviceId,
                                          "TransferVolume_" + stepIndex,
                                          "int",
                                          volume != null ? (int.Parse(Math.Floor(Convert.ToDouble(volume)).ToString())).ToString() : "0");
        }

        private void SendFillBlock(ParameterizedRecipeBlock<FillBlockParameters> fillBlock, int stepIndex, string deviceId)
        {
            string volume = fillBlock.Parameters.Volume;
            string targetItemIndex = fillBlock.Parameters.TargetItemIndex;

            fieldDevicesCommunicator
                .SendCommandToDevice(deviceId,
                                          "FillEnabled_" + stepIndex,
                                          "bool",
                                          bool.TrueString);

            fieldDevicesCommunicator
                .SendCommandToDevice(deviceId,
                                          "FillTargetItemIndex_" + stepIndex,
                                          "int",
                                          targetItemIndex);

            fieldDevicesCommunicator
                .SendCommandToDevice(deviceId,
                                          "FillVolume_" + stepIndex,
                                          "int",
                                          volume != null ? int.Parse(Math.Floor(Convert.ToDouble(volume)).ToString()).ToString() : "0");
        }

        private void SendEndBlock(int numberOfRecipeSteps, string deviceId)
        {
            /*
             * Send the Recipe Status and the Number of Recipe Steps to the plc
             * after sending all the recipe blocks data to the plc
             */
            fieldDevicesCommunicator
                .SendCommandToDevice(deviceId,
                                          "NumberOfRecipeSteps",
                                          "int",
                                          numberOfRecipeSteps.ToString());
            fieldDevicesCommunicator
                .SendCommandToDevice(deviceId,
                                          "RecipeStatus",
                                          "bool",
                                          bool.TrueString);
        }

        private void SendWaitBlock(ParameterizedRecipeBlock<WaitBlockParameters> waitBlock, int stepIndex, string deviceId)
        {
            string duration = waitBlock.Parameters.Duration;

            fieldDevicesCommunicator
                .SendCommandToDevice(deviceId,
                                          "WaitEnabled_" + stepIndex,
                                          "bool",
                                          bool.TrueString);

            fieldDevicesCommunicator
                .SendCommandToDevice(deviceId,
                                          "WaitDuration_" + stepIndex,
                                          "int",
                                          duration != null ? (int.Parse(Math.Floor(Convert.ToDouble(duration)).ToString())).ToString() : "0");
        }

        private void SendDosingBlock(ParameterizedRecipeBlock<DosingBlockParameters> dosingBlock, int stepIndex, string deviceId)
        {
            string dosingMaxAmount = dosingBlock.Parameters.MaxAmount;
            string dosingStopTemperature = dosingBlock.Parameters.StopTemperature;
            string dosingResumeTemperature = dosingBlock.Parameters.ResumeTemperature;
            string dosingRateSetPointExpression = dosingBlock.Parameters.SetPoint;
            string dosingMinRate = dosingBlock.Parameters.MinRate;
            string dosingMaxRate = dosingBlock.Parameters.MaxRate;
            string dosingSettlingTime = dosingBlock.Parameters.SettlingTime;
            string dosingMin_pH = dosingBlock.Parameters.MinPh;
            string dosingMax_pH = dosingBlock.Parameters.MaxPh;
            string dosingOperatingMode = dosingBlock.Parameters.OperatingMode;

            fieldDevicesCommunicator
                .SendCommandToDevice(deviceId,
                                          "DosingEnabled_" + stepIndex,
                                          "bool",
                                          bool.TrueString);

            fieldDevicesCommunicator
                .SendCommandToDevice(deviceId,
                                          "DosingMaxAmount_" + stepIndex,
                                          "float",
                                          dosingMaxAmount ?? "0");
            fieldDevicesCommunicator
                .SendCommandToDevice(deviceId,
                                          "DosingStopTemperature_" + stepIndex,
                                          "int",
                                          dosingStopTemperature != null ? (int.Parse(Math.Floor(Convert.ToDouble(dosingStopTemperature)).ToString())).ToString() : "0");
            fieldDevicesCommunicator
                .SendCommandToDevice(deviceId,
                                          "DosingResumeTemperature_" + stepIndex,
                                          "int",
                                          dosingResumeTemperature != null ? (int.Parse(Math.Floor(Convert.ToDouble(dosingResumeTemperature)).ToString())).ToString() : "0");
            fieldDevicesCommunicator
                .SendCommandToDevice(deviceId,
                                          "DosingSetPoint_" + stepIndex,
                                          "float",
                                          dosingRateSetPointExpression ?? "0");
            fieldDevicesCommunicator
                .SendCommandToDevice(deviceId,
                                          "DosingMinRate_" + stepIndex,
                                          "float",
                                          dosingMinRate ?? "0");
            fieldDevicesCommunicator
                .SendCommandToDevice(deviceId,
                                          "DosingMaxRate_" + stepIndex,
                                          "float",
                                          dosingMaxRate ?? "0");
            fieldDevicesCommunicator
                .SendCommandToDevice(deviceId,
                                          "DosingSettlingTime_" + stepIndex,
                                          "int",
                                          dosingSettlingTime != null ? (int.Parse(Math.Floor(Convert.ToDouble(dosingSettlingTime)).ToString())).ToString() : "0");
            fieldDevicesCommunicator
                .SendCommandToDevice(deviceId,
                                          "DosingMinPh_" + stepIndex,
                                          "int",
                                          dosingMin_pH != null ? (int.Parse(Math.Floor(Convert.ToDouble(dosingMin_pH)).ToString())).ToString() : "0");
            fieldDevicesCommunicator
                .SendCommandToDevice(deviceId,
                                          "DosingMaxPh_" + stepIndex,
                                          "int",
                                          dosingMax_pH != null ? (int.Parse(Math.Floor(Convert.ToDouble(dosingMax_pH)).ToString())).ToString() : "0");
            fieldDevicesCommunicator
                .SendCommandToDevice(deviceId,
                                          "DosingOperatingMode_" + stepIndex,
                                          "bool",
                                          dosingOperatingMode != null ? bool.Parse(dosingOperatingMode).ToString() : bool.FalseString);
        }

        private void SendHeatCoolBlock(ParameterizedRecipeBlock<HeatCoolBlockParameters> heatCoolBlock, int stepIndex, string deviceId)
        {
            string operatingMode = heatCoolBlock.Parameters.OperatingMode;
            string setPoint = heatCoolBlock.Parameters.SetPoint;
            string duration = heatCoolBlock.Parameters.Duration;

            fieldDevicesCommunicator
                .SendCommandToDevice(deviceId,
                                          "HeatCoolEnabled_" + stepIndex,
                                          "bool",
                                          bool.TrueString);

            fieldDevicesCommunicator
                .SendCommandToDevice(deviceId,
                                          "HeatCoolOperatingMode_" + stepIndex,
                                          "bool",
                                          operatingMode != null ? bool.Parse(operatingMode).ToString() : bool.FalseString);

            fieldDevicesCommunicator
                .SendCommandToDevice(deviceId,
                                          "HeatCoolSetPoint_" + stepIndex,
                                          "int",
                                          setPoint != null ? (int.Parse(Math.Floor(Convert.ToDouble(setPoint)).ToString())).ToString() : "0");

            fieldDevicesCommunicator
                .SendCommandToDevice(deviceId,
                                          "HeatCoolDuration_" + stepIndex,
                                          "int",
                                          duration != null ? (int.Parse(Math.Floor(Convert.ToDouble(duration)).ToString())).ToString() : "0");
        }

        private void SendStirrerBlock(ParameterizedRecipeBlock<StirrerBlockParameters> stirrerBlock, int stepIndex, string deviceId)
        {
            string setPoint = stirrerBlock.Parameters.SetPoint;

            fieldDevicesCommunicator
                .SendCommandToDevice(deviceId,
                                          "StirrerEnabled_" + stepIndex,
                                          "bool",
                                          bool.TrueString);

            fieldDevicesCommunicator
                .SendCommandToDevice(deviceId,
                                          "StirrerSetPoint_" + stepIndex,
                                          "int",
                                          setPoint != null ? (int.Parse(Math.Floor(Convert.ToDouble(setPoint)).ToString())).ToString() : "0");
        }

        private void SendStartBlock(ParameterizedRecipeBlock<StartBlockParameters> startBlock, string deviceId)
        {
            string heatCoolModeSelection = startBlock.Parameters.HeatCoolModeSelection;
            string heatCoolDeltaTemp = startBlock.Parameters.HeatCoolDeltaTemp;

            fieldDevicesCommunicator
                .SendCommandToDevice(deviceId, "Started_0", "bool", bool.TrueString);

            fieldDevicesCommunicator
                .SendCommandToDevice(deviceId,
                                          "HeatCoolModeSelection_0",
                                          "bool",
                                          heatCoolModeSelection != null ? bool.Parse(heatCoolModeSelection).ToString() : bool.FalseString);

            fieldDevicesCommunicator
                .SendCommandToDevice(deviceId,
                                          "HeatCoolDeltaTemp_0",
                                          "int",
                                          heatCoolDeltaTemp != null ? (int.Parse(Math.Floor(Convert.ToDouble(heatCoolDeltaTemp)).ToString())).ToString() : "0");
        }

        public void UpdateBlock(int stepIndex, IRecipeBlock block, string deviceId)
        {
            SendBlock(block, stepIndex, deviceId);
        }

        public void AbortBlockExecution(int stepIndex, IRecipeBlock block, string deviceId)
        {
            string blockName = default;

            if (block.Name.Contains("HeatCool"))
            {
                blockName = "HeatCool";
            }
            else if (block.Name.Contains("Stirrer"))
            {
                blockName = "Stirrer";
            }
            else if (block.Name.Contains("Dosing"))
            {
                blockName = "Dosing";
            }
            else if (block.Name.Contains("Wait"))
            {
                blockName = "Wait";
            }

            fieldDevicesCommunicator
                .SendCommandToDevice(deviceId,
                                          blockName + "Ended_" + stepIndex,
                                          "bool",
                                          bool.TrueString);
        }

        public bool GetRecipeStatus(string deviceId)
        {
            var recipeStatus
                = fieldDevicesCommunicator.ReadFieldPointValue<bool>(deviceId, "RecipeStatus");

            if (recipeStatus)
            {
                return recipeStatus;
            }
            else
            {
                return false;
            }
        }

        public void AbortRecipeExecution(string deviceId)
        {
            fieldDevicesCommunicator
                .SendCommandToDevice(deviceId,
                                          "AbortRecipeStatus",
                                          "bool",
                                          bool.TrueString);
        }

        public void UpdateRecipeStepsFromGivenIndex(int toBeUpdatedFromIndex, string deviceId, IList<RecipeStep> recipeSteps)
        {
            for (int stepIndex = toBeUpdatedFromIndex; stepIndex < recipeSteps.Count; stepIndex++)
            {
                var step = recipeSteps[stepIndex];

                SendBlock(step.BlockOne, stepIndex, deviceId);
                SendBlock(step.BlockTwo, stepIndex, deviceId);
                SendBlock(step.BlockThree, stepIndex, deviceId);
                SendBlock(step.BlockFour, stepIndex, deviceId);
            }
            
            /*
             * After sending all the recipe blocks data to plc send the end block
             * Decrease the recipe steps count by 1 as last recipe block is End block 
             * and it does not contain any parameters for sending to the plc
             */
            SendEndBlock(recipeSteps.Count - 1, deviceId);
        }
    }
}
