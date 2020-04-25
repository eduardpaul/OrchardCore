using System;
using System.Threading;
using OrchardCore.BackgroundTasks;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace OrchardCore.AzureSearch
{
    /// <summary>
    /// This background task will index content items using.
    /// </summary>
    /// <remarks>
    /// This services is only registered from OrchardCore.AzureSearch.Worker feature.
    /// </remarks>
    [BackgroundTask(Schedule = "* * * * *", Description = "Update AzureSearch indexes.")]
    public class IndexingBackgroundTask : IBackgroundTask
    {
        public Task DoWorkAsync(IServiceProvider serviceProvider, CancellationToken cancellationToken)
        {
            var indexingService = serviceProvider.GetService<AzureSearchIndexingService>();
            return indexingService.ProcessContentItemsAsync();
        }
    }
}
