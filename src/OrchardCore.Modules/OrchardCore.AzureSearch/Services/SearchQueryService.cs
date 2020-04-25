using System.Collections.Generic;
using System.Threading.Tasks;
using OrchardCore.ContentManagement;

namespace OrchardCore.AzureSearch.Services
{
    public class SearchQueryService : ISearchQueryService
    {
        private readonly AzureSearchIndexManager _AzureSearchIndexManager;
        private readonly IContentManager _contentManager;

        private static HashSet<string> IdSet = new HashSet<string>(new string[] { "ContentItemId" });

        public SearchQueryService(
            IContentManager contentManager,
            AzureSearchIndexManager AzureSearchIndexManager)
        {
            _AzureSearchIndexManager = AzureSearchIndexManager;
            _contentManager = contentManager;

        }
        public async Task<IList<string>> ExecuteQueryAsync(object query, string indexName, int start, int end)
        {
            var contentItemIds = new List<string>();
            await Task.Delay(1000);
            /*
            await _AzureSearchIndexManager.SearchAsync(indexName, searcher =>
            {                
                var collector = TopScoreDocCollector.Create(end, true);

                searcher.Search(query, collector);
                var hits = collector.GetTopDocs(start, end);

                foreach (var hit in hits.ScoreDocs)
                {
                    var d = searcher.Doc(hit.Doc, IdSet);
                    contentItemIds.Add(d.GetField("ContentItemId").GetStringValue());
                }

                return Task.CompletedTask;
            });
            */
            return contentItemIds;

        }
    }
}