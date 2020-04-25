using OrchardCore.Deployment;

namespace OrchardCore.AzureSearch.Deployment
{
    /// <summary>
    /// Adds layers to a <see cref="DeploymentPlanResult"/>. 
    /// </summary>
    public class AzureSearchSettingsDeploymentStep : DeploymentStep
    {
        public AzureSearchSettingsDeploymentStep()
        {
            Name = "AzureSearchSettings";
        }
    }
}
