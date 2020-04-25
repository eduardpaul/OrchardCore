using OrchardCore.Deployment;

namespace OrchardCore.AzureSearch.Deployment
{
    /// <summary>
    /// Adds layers to a <see cref="DeploymentPlanResult"/>. 
    /// </summary>
    public class AzureSearchIndexDeploymentStep : DeploymentStep
    {
        public AzureSearchIndexDeploymentStep()
        {
            Name = "AzureSearchIndex";
        }

        public bool IncludeAll { get; set; } = true;

        public string[] IndexNames { get; set; }
    }
}
