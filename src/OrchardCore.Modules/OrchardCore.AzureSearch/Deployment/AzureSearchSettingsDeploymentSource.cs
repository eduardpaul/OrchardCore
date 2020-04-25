using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using OrchardCore.Deployment;
using OrchardCore.Settings;

namespace OrchardCore.AzureSearch.Deployment
{
    public class AzureSearchSettingsDeploymentSource : IDeploymentSource
    {
        private readonly AzureSearchIndexingService _AzureSearchIndexingService;
        private readonly ISiteService _siteService;

        public AzureSearchSettingsDeploymentSource(AzureSearchIndexingService AzureSearchIndexingService, ISiteService siteService)
        {
            _AzureSearchIndexingService = AzureSearchIndexingService;
            _siteService = siteService;
        }

        public async Task ProcessDeploymentStepAsync(DeploymentStep step, DeploymentPlanResult result)
        {
            var AzureSearchSettingsStep = step as AzureSearchSettingsDeploymentStep;

            if (AzureSearchSettingsStep == null)
            {
                return;
            }

            var AzureSearchSettings = await _AzureSearchIndexingService.GetAzureSearchSettingsAsync();

            // Adding AzureSearch settings
            result.Steps.Add(new JObject(
                new JProperty("name", "Settings"),
                new JProperty("AzureSearchSettings", JObject.FromObject(AzureSearchSettings))
            ));
        }
    }
}
