using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fluid;
using Newtonsoft.Json.Linq;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Records;
using OrchardCore.Liquid;
using OrchardCore.AzureSearch.Services;
using OrchardCore.Queries;
using YesSql;
using YesSql.Services;

namespace OrchardCore.AzureSearch
{
    public class AzureSearchQuerySource : IQuerySource
    {
        private readonly AzureSearchIndexManager _AzureSearchIndexProvider;
        private readonly AzureSearchIndexingService _AzureSearchIndexingService;
        private readonly IAzureSearchQueryService _queryService;
        private readonly ILiquidTemplateManager _liquidTemplateManager;
        private readonly ISession _session;

        public AzureSearchQuerySource(
            AzureSearchIndexManager AzureSearchIndexProvider,
            AzureSearchIndexingService AzureSearchIndexingService,
            IAzureSearchQueryService queryService,
            ILiquidTemplateManager liquidTemplateManager,
            ISession session)
        {
            _AzureSearchIndexProvider = AzureSearchIndexProvider;
            _AzureSearchIndexingService = AzureSearchIndexingService;
            _queryService = queryService;
            _liquidTemplateManager = liquidTemplateManager;
            _session = session;
        }

        public string Name => "AzureSearch";

        public Query Create()
        {
            return new AzureSearchQuery();
        }

        public async Task<object> ExecuteQueryAsync(Query query, IDictionary<string, object> parameters)
        {
            var AzureSearchQuery = query as AzureSearchQuery;
            object result = null;

            await Task.Delay(1000);

            /*
            await _AzureSearchIndexProvider.SearchAsync (AzureSearchQuery.Index, async searcher =>
            {
                var templateContext = new TemplateContext();

                if (parameters != null)
                {
                    foreach (var parameter in parameters)
                    {
                        templateContext.SetValue(parameter.Key, parameter.Value);
                    }
                }

                var tokenizedContent = await _liquidTemplateManager.RenderAsync(AzureSearchQuery.Template, System.Text.Encodings.Web.JavaScriptEncoder.Default, templateContext);
                var parameterizedQuery = JObject.Parse(tokenizedContent);

                var analyzer = _AzureSearchAnalyzerManager.CreateAnalyzer(AzureSearchSettings.StandardAnalyzer);
                var context = new AzureSearchQueryContext(searcher, AzureSearchSettings.DefaultVersion, analyzer);
                var docs = await _queryService.SearchAsync(context, parameterizedQuery);

                if (AzureSearchQuery.ReturnContentItems)
                {
                    // Load corresponding content item versions
                    var contentItemVersionIds = docs.ScoreDocs.Select(x => searcher.Doc(x.Doc).Get("Content.ContentItem.ContentItemVersionId")).ToArray();
                    var contentItems = await _session.Query<ContentItem, ContentItemIndex>(x => x.ContentItemVersionId.IsIn(contentItemVersionIds)).ListAsync();

                    // Reorder the result to preserve the one from the AzureSearch query
                    var indexed = contentItems.ToDictionary(x => x.ContentItemVersionId, x => x);
                    result = contentItemVersionIds.Select(x => indexed[x]).ToArray();
                }
                else
                {
                    var results = new List<JObject>();
                    foreach (var document in docs.ScoreDocs.Select(hit => searcher.Doc(hit.Doc)))
                    {
                        results.Add(new JObject(document.Select(x => new JProperty(x.Name, x.GetStringValue()))));
                    }

                    result = results;
                }
            });
            */
            return result;
        }
    }
}
