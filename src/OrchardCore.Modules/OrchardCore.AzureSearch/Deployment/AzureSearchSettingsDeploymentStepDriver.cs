using OrchardCore.Deployment;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.DisplayManagement.Views;

namespace OrchardCore.AzureSearch.Deployment
{
    public class AzureSearchSettingsDeploymentStepDriver : DisplayDriver<DeploymentStep, AzureSearchSettingsDeploymentStep>
    {
        public override IDisplayResult Display(AzureSearchSettingsDeploymentStep step)
        {
            return
                Combine(
                    View("AzureSearchSettingsDeploymentStep_Fields_Summary", step).Location("Summary", "Content"),
                    View("AzureSearchSettingsDeploymentStep_Fields_Thumbnail", step).Location("Thumbnail", "Content")
                );
        }

        public override IDisplayResult Edit(AzureSearchSettingsDeploymentStep step)
        {
            return View("AzureSearchSettingsDeploymentStep_Fields_Edit", step).Location("Content");
        }
    }
}
