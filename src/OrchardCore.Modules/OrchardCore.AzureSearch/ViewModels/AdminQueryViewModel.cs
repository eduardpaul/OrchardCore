using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace OrchardCore.AzureSearch.ViewModels
{
    public class AdminQueryViewModel
    {
        public string DecodedQuery { get; set; }
        public string IndexName { get; set; }
        public string Parameters { get; set; }

        [BindNever]
        public string[] Indices { get; set; }

        [BindNever]
        public TimeSpan Elapsed { get; set; } = TimeSpan.Zero;

        [BindNever]
        public IEnumerable<object> Documents { get; set; } = Enumerable.Empty<object>();
    }
}
