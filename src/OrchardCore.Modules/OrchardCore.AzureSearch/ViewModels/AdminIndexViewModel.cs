using System.Collections.Generic;

namespace OrchardCore.AzureSearch.ViewModels
{
    public class AdminIndexViewModel
    {
        public IEnumerable<IndexViewModel> Indexes { get; set; }
    }
}
