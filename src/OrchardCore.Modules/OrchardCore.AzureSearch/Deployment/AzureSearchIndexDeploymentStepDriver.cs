using System;
using System.Linq;
using System.Threading.Tasks;
using OrchardCore.Deployment;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.AzureSearch.ViewModels;

namespace OrchardCore.AzureSearch.Deployment
{
    public class AzureSearchIndexDeploymentStepDriver : DisplayDriver<DeploymentStep, AzureSearchIndexDeploymentStep>
    {
        private readonly AzureSearchIndexManager _AzureSearchIndexManager;

        public AzureSearchIndexDeploymentStepDriver(AzureSearchIndexManager AzureSearchIndexManager)
        {
            _AzureSearchIndexManager = AzureSearchIndexManager;
        }

        public override IDisplayResult Display(AzureSearchIndexDeploymentStep step)
        {
            return
                Combine(
                    View("AzureSearchIndexDeploymentStep_Fields_Summary", step).Location("Summary", "Content"),
                    View("AzureSearchIndexDeploymentStep_Fields_Thumbnail", step).Location("Thumbnail", "Content")
                );
        }

        public override IDisplayResult Edit(AzureSearchIndexDeploymentStep step)
        {
            return Initialize<AzureSearchIndexDeploymentStepViewModel>("AzureSearchIndexDeploymentStep_Fields_Edit", model =>
            {
                model.IncludeAll = step.IncludeAll;
                model.IndexNames = step.IndexNames;
                //model.AllIndexNames = _AzureSearchIndexManager.List().ToArray();
            }).Location("Content");
        }

        public override async Task<IDisplayResult> UpdateAsync(AzureSearchIndexDeploymentStep step, IUpdateModel updater)
        {
            step.IndexNames = Array.Empty<string>();

            await updater.TryUpdateModelAsync(step,
                                              Prefix,
                                              x => x.IndexNames,
                                              x => x.IncludeAll);

            // don't have the selected option if include all
            if (step.IncludeAll)
            {
                step.IndexNames = Array.Empty<string>();
            }

            return Edit(step);
        }
    }
}
