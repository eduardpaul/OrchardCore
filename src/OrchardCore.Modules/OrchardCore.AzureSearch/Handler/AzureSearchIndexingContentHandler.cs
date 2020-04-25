using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OrchardCore.ContentManagement.Handlers;
using OrchardCore.Indexing;
using OrchardCore.Modules;

namespace OrchardCore.AzureSearch.Handlers
{
    public class AzureSearchIndexingContentHandler : ContentHandlerBase
    {
        private readonly AzureSearchIndexManager _AzureSearchIndexManager;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<AzureSearchIndexingContentHandler> _logger;

        public AzureSearchIndexingContentHandler(
            AzureSearchIndexManager AzureSearchIndexManager,
            IServiceProvider serviceProvider,
            ILogger<AzureSearchIndexingContentHandler> logger)
        {
            _AzureSearchIndexManager = AzureSearchIndexManager;
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public override async Task PublishedAsync(PublishContentContext context)
        {
            // TODO: ignore if this index is not configured for the content type

            var buildIndexContext = new BuildIndexContext(new DocumentIndex(context.ContentItem.ContentItemId), context.ContentItem, new string[] { context.ContentItem.ContentType });
            // Lazy resolution to prevent cyclic dependency 
            var contentItemIndexHandlers = _serviceProvider.GetServices<IContentItemIndexHandler>();
            await contentItemIndexHandlers.InvokeAsync(x => x.BuildIndexAsync(buildIndexContext), _logger);
            /*
            foreach (var index in _AzureSearchIndexManager.List())
            {
                //_AzureSearchIndexManager.DeleteDocuments(index, new string[] { context.ContentItem.ContentItemId });
                //_AzureSearchIndexManager.StoreDocuments(index, new DocumentIndex[] { buildIndexContext.DocumentIndex });
            }
            */
        }

        public override Task RemovedAsync(RemoveContentContext context)
        {
            // TODO: ignore if this index is not configured for the content type
            /*
            foreach (var index in _AzureSearchIndexManager.List())
            {
                _AzureSearchIndexManager.DeleteDocuments(index, new string[] { context.ContentItem.ContentItemId });
            }
            */
            return Task.CompletedTask;
        }

        public override Task UnpublishedAsync(PublishContentContext context)
        {
            // TODO: ignore if this index is not configured for the content type
/*
            foreach (var index in _AzureSearchIndexManager.List())
            {
                //_AzureSearchIndexManager.DeleteDocuments(index, new string[] { context.ContentItem.ContentItemId });
            }
            */
            return Task.CompletedTask;
        }
    }
}