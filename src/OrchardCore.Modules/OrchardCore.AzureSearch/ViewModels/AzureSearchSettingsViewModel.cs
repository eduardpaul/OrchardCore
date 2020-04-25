using System.Collections.Generic;

namespace OrchardCore.AzureSearch.ViewModels
{
    public class AzureSearchSettingsViewModel
    {
        public string Analyzer { get; set; }
        public string SearchIndex { get; set; }
        public IEnumerable<string> SearchIndexes { get; set; }
        public string SearchFields { get; set; }
    }
}
