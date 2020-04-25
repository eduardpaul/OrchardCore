using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrchardCore.ContentManagement;
using OrchardCore.AzureSearch.Settings;

namespace OrchardCore.AzureSearch.Services
{
    public class AzureSearchContentPickerResultProvider : IContentPickerResultProvider
    {
        private readonly AzureSearchIndexManager _AzureSearchIndexProvider;

        public AzureSearchContentPickerResultProvider(AzureSearchIndexManager AzureSearchIndexProvider)
        {
            _AzureSearchIndexProvider = AzureSearchIndexProvider;
        }

        public string Name => "AzureSearch";

        public async Task<IEnumerable<ContentPickerResult>> Search(ContentPickerSearchContext searchContext)
        {
            var indexName = "Search";

            var fieldSettings = searchContext.PartFieldDefinition?.GetSettings<ContentPickerFieldAzureSearchEditorSettings>();
            if (!string.IsNullOrWhiteSpace(fieldSettings?.Index))
            {
                indexName = fieldSettings.Index;
            }

            await Task.Delay(100);

            /*
            if (!_AzureSearchIndexProvider.Exists(indexName))
            {
                return new List<ContentPickerResult>();
            }
            */
            var results = new List<ContentPickerResult>();
            /*
            await _AzureSearchIndexProvider.SearchAsync(indexName, searcher =>
            {
                Query query = null;

                if (string.IsNullOrWhiteSpace(searchContext.Query))
                {
                    query = new MatchAllDocsQuery();
                }
                else
                {
                    query = new WildcardQuery(new Term("Content.ContentItem.DisplayText.Analyzed", searchContext.Query.ToLowerInvariant() + "*"));
                }

                var filter = new FieldCacheTermsFilter("Content.ContentItem.ContentType", searchContext.ContentTypes.ToArray());

                var docs = searcher.Search(query, filter, 50, Sort.RELEVANCE);

                foreach (var hit in docs.ScoreDocs)
                {
                    var doc = searcher.Doc(hit.Doc);

                    results.Add(new ContentPickerResult
                    {
                        ContentItemId = doc.GetField("ContentItemId").GetStringValue(),
                        DisplayText = doc.GetField("Content.ContentItem.DisplayText").GetStringValue(),
                        HasPublished = doc.GetField("Content.ContentItem.Published").GetStringValue() == "true" ? true : false 
                    });
                }
                
                return Task.CompletedTask;
            });

            */
            return results.OrderBy(x => x.DisplayText);
        }
    }
}
