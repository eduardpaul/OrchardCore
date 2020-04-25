using System.Collections.Generic;

namespace OrchardCore.AzureSearch
{
    public class AzureSearchOptions
    {
        public IList<IAzureSearchAnalyzer> Analyzers { get; } = new List<IAzureSearchAnalyzer>();
    }
}