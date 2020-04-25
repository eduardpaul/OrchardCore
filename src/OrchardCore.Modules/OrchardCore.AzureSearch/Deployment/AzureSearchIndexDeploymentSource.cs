using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using OrchardCore.Deployment;
using OrchardCore.Settings;

namespace OrchardCore.AzureSearch.Deployment
{
    public class AzureSearchIndexDeploymentSource : IDeploymentSource
    {
        private readonly AzureSearchIndexManager _AzureSearchIndexManager;
        private readonly ISiteService _siteService;

        public AzureSearchIndexDeploymentSource(AzureSearchIndexManager AzureSearchIndexManager, ISiteService siteService)
        {
            _AzureSearchIndexManager = AzureSearchIndexManager;
            _siteService = siteService;
        }

        public Task ProcessDeploymentStepAsync(DeploymentStep step, DeploymentPlanResult result)
        {
            var AzureSearchIndexStep = step as AzureSearchIndexDeploymentStep;

            if (AzureSearchIndexStep == null)
            {
                return Task.CompletedTask;
            }

            /*
             var indices = AzureSearchIndexStep.IncludeAll ? _AzureSearchIndexManager.List().ToArray() : AzureSearchIndexStep.IndexNames;
             */

            // Adding AzureSearch settings
            result.Steps.Add(new JObject(
                new JProperty("name", "AzureSearch-index"),
                new JProperty("Indices", JArray.FromObject(null))
            ));

            return Task.CompletedTask;
        }
    }
}
