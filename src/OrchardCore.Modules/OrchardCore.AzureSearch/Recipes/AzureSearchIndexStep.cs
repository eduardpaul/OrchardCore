using System;
using System.Threading.Tasks;
using OrchardCore.Recipes.Models;
using OrchardCore.Recipes.Services;

namespace OrchardCore.AzureSearch.Recipes
{
    /// <summary>
    /// This recipe step creates a AzureSearch index.
    /// </summary>
    public class AzureSearchIndexStep : IRecipeStepHandler
    {
        private readonly AzureSearchIndexManager _AzureSearchIndexManager;

        public AzureSearchIndexStep(AzureSearchIndexManager AzureSearchIndexManager)
        {
            _AzureSearchIndexManager = AzureSearchIndexManager;
        }

        public Task ExecuteAsync(RecipeExecutionContext context)
        {
            if (!String.Equals(context.Name, "AzureSearch-index", StringComparison.OrdinalIgnoreCase))
            {
                return Task.CompletedTask;
            }

            var model = context.Step.ToObject<AzureSearchIndexModel>();

            foreach(var index in model.Indices)
            {
                /*
                if (!_AzureSearchIndexManager.Exists(index))
                {
                    _AzureSearchIndexManager.CreateIndex(index);
                }
                */
            }

            return Task.CompletedTask;
        }

        private class AzureSearchIndexModel
        {
            public string[] Indices { get; set; }
        }
    }
}
