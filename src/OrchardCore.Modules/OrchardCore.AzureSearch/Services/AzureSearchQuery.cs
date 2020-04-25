using OrchardCore.Queries;

namespace OrchardCore.AzureSearch
{
    public class AzureSearchQuery : Query
    {
        public AzureSearchQuery() : base("AzureSearch")
        {
        }

        public string Index { get; set; }
        public string Template { get; set; }
        public bool ReturnContentItems { get; set; }
    }
}
