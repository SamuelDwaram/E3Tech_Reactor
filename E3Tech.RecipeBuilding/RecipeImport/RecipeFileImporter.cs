using E3Tech.IO.FileAccess;
using E3Tech.RecipeBuilding.Helpers;
using E3Tech.RecipeBuilding.Model;
using E3Tech.RecipeBuilding.Model.Blocks;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using Unity;

namespace E3Tech.RecipeBuilding.RecipeImport
{
    public class RecipeFileImporter : IRecipeImporter
    {
        private readonly IFileBrowser fileBrowser;
        private readonly IUnityContainer unityContainer;

        public RecipeFileImporter(IUnityContainer unityContainer, IFileBrowser fileBrowser)
        {
            this.unityContainer = unityContainer;
            this.fileBrowser = fileBrowser;
        }

        public IList<RecipeStep> Import()
        {
            string fileName = fileBrowser.OpenFile(".json");

            if (!string.IsNullOrWhiteSpace(fileName))
            {
                StreamReader sr = new StreamReader(fileName);
                string json = sr.ReadToEnd();
                var settings = new JsonSerializerSettings();
                settings.NullValueHandling = NullValueHandling.Ignore;
                settings.Converters.Add(new BlockCreationConverter<ParameterizedRecipeBlock<Object>>(unityContainer));

                return JsonConvert.DeserializeObject<IList<RecipeStep>>(json, settings);
            }

            return null;
        }
    }
}
