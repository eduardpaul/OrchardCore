using System.ComponentModel.DataAnnotations;

namespace OrchardCore.AzureSearch.ViewModels
{
    public class AzureSearchQueryViewModel
    {
        public string[] Indices { get; set; }
        public string Index { get; set; }
        public string Query { get; set; }
        public bool ReturnContentItems { get; set; }
    }
}
