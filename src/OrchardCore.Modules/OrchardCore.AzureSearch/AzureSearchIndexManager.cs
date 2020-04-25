using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OrchardCore.Environment.Shell;
using OrchardCore.Indexing;
using OrchardCore.AzureSearch.Services;
using OrchardCore.Modules;

namespace OrchardCore.AzureSearch
{
    /// <summary>
    /// Provides methods to manage physical AzureSearch indices.
    /// This class is provided as a singleton to that the index searcher can be reused across requests.
    /// </summary>
    public class AzureSearchIndexManager : IDisposable
    {
        private readonly IClock _clock;
        private readonly ILogger<AzureSearchIndexManager> _logger;
        private readonly string _rootPath;
        private readonly DirectoryInfo _rootDirectory;
        private bool _disposing;
        private ConcurrentDictionary<string, DateTime> _timestamps = new ConcurrentDictionary<string, DateTime>(StringComparer.OrdinalIgnoreCase);
       
        private static object _synLock = new object();

        public AzureSearchIndexManager(
            IClock clock,
            IOptions<ShellOptions> shellOptions,
            ShellSettings shellSettings,
            ILogger<AzureSearchIndexManager> logger
            )
        {
            _clock = clock;
            _logger = logger;
            _rootPath = PathExtensions.Combine(
                shellOptions.Value.ShellsApplicationDataPath,
                shellOptions.Value.ShellsContainerName,
                shellSettings.Name, "AzureSearch");

            _rootDirectory = Directory.CreateDirectory(_rootPath);
        }

        public void Dispose()
        {
            if (_disposing)
            {
                return;
            }

            _disposing = true;
        }

        ~AzureSearchIndexManager()
        {
            Dispose();
        }
    }
}
