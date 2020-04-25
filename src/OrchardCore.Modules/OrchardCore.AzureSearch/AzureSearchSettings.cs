using System.Collections.Generic;

namespace OrchardCore.AzureSearch
{
    public class AzureSearchSettings
    {
        public string SearchIndex { get; set; }

        public string[] DefaultSearchFields { get; set; } = new string[0];

        /// <summary>
        /// Gets the list of indices and their settings.
        /// </summary>
        public Dictionary<string, IndexSettings> IndexSettings { get; } = new Dictionary<string, IndexSettings>();
    }

    public class IndexSettings
    {
        public string Name { get; set; }
        public string Analyzer { get; set; }
    }
}
