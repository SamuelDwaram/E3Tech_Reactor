using E3Tech.IO.FileAccess;
using E3Tech.RecipeBuilding.Helpers;
using E3Tech.RecipeBuilding.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using Unity;

namespace E3Tech.RecipeBuilding.RecipceExport
{
    public class RecipeFileExporter : IRecipeExporter
    {
        private readonly IFileBrowser fileBrowser;
        private readonly IUnityContainer unityContainer;

        public RecipeFileExporter(IUnityContainer unityContainer, IFileBrowser fileBrowser)
        {
            this.fileBrowser = fileBrowser;
            this.unityContainer = unityContainer;
        }

        public void Export(IList<RecipeStep> recipe)
        {
            string fileName = fileBrowser.SaveFile("Recipe-1", ".json");
            if (!string.IsNullOrWhiteSpace(fileName))
            {
                JsonSerializerSettings settings = new JsonSerializerSettings();
                settings.NullValueHandling = NullValueHandling.Ignore;
                settings.Converters.Add(new BlockCreationConverter<ParameterizedRecipeBlock<Object>>(unityContainer));

                File.WriteAllText(fileName, JsonConvert.SerializeObject(recipe, settings));
            }
        }
    }
}
