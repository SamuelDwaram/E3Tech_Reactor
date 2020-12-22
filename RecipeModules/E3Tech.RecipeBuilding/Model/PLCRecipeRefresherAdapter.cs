using E3.ReactorManager.Interfaces.HardwareAbstractionLayer;
using E3Tech.RecipeBuilding.Model;
using System;
using Unity;

namespace E3Tech.RecipeBuilding.Model
{
    public class PLCRecipeRefresherAdapter
    {
        IUnityContainer containerProvider;
        private readonly IRecipeRefresher recipeRefresher;
        IFieldDevicesCommunicator fieldDevicesCommunicator;

        public PLCRecipeRefresherAdapter(IUnityContainer containerProvider)
        {
            this.containerProvider = containerProvider;
            if (containerProvider.IsRegistered<IRecipeRefresher>())
            {
                this.recipeRefresher = containerProvider.Resolve<IRecipeRefresher>();
            }

            if (containerProvider.IsRegistered<IFieldDevicesCommunicator>())
            {
                this.fieldDevicesCommunicator = containerProvider.Resolve<IFieldDevicesCommunicator>();

                fieldDevicesCommunicator.FieldPointDataReceived += CommunicatorInstance_FieldPointDataReceived;
            }
        }

        private void CommunicatorInstance_FieldPointDataReceived(object sender, E3.ReactorManager.Interfaces.HardwareAbstractionLayer.Data.FieldPointDataReceivedArgs e)
        {
            if (recipeRefresher != null)
            {
                try
                {
                    string blocParameterName = e.FieldPointDescription;
                    if (blocParameterName.Contains("Recipe"))
                    {
                        blocParameterName = blocParameterName.Substring(blocParameterName.IndexOf("_") + 1);
                        if (blocParameterName.IndexOf("_") > 0)
                        {
                            string blockName = blocParameterName.Substring(0, blocParameterName.IndexOf("_"));
                            string parameterNameWithStepNumber = blocParameterName.Substring(blocParameterName.IndexOf("_") + 1);
                            if (parameterNameWithStepNumber.IndexOf("_") > 0)
                            {
                                string parameterName = parameterNameWithStepNumber.Substring(0, parameterNameWithStepNumber.IndexOf("_"));
                                int stepIndex = Convert.ToInt32(parameterNameWithStepNumber.Substring(parameterNameWithStepNumber.IndexOf("_") + 1));
                                if (!string.IsNullOrEmpty(blockName))
                                {
                                    recipeRefresher.RefreshBlock(e.FieldDeviceIdentifier, stepIndex, blockName, parameterName, e.NewFieldPointData);
                                }
                            }
                        }
                    }
                }
                catch (Exception)
                {

                }
            }
        }
    }
}
