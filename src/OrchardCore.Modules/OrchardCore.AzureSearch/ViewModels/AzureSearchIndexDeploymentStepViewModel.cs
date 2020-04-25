using System;

namespace OrchardCore.AzureSearch.ViewModels
{
    public class AzureSearchIndexDeploymentStepViewModel
    {
        public bool IncludeAll { get; set; }
        public string[] IndexNames { get; set; }
        public string[] AllIndexNames { get; set; }
    }
}
