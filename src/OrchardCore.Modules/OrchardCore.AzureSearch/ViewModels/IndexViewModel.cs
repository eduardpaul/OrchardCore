﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrchardCore.AzureSearch.ViewModels
{
    public class IndexViewModel
    {
        public string Name { get; set; }
        public DateTime LastUpdateUtc { get; set; }
        // public IndexingStatus IndexingStatus { get; set; }
    }
}