using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace OrchardCore.AzureSearch
{
    public class AzureSearchQueryService : IAzureSearchQueryService
    {
        private readonly IEnumerable<IAzureSearchQueryProvider> _queryProviders;

        public AzureSearchQueryService(IEnumerable<IAzureSearchQueryProvider> queryProviders)
        {
            _queryProviders = queryProviders;
        }

        public Task<object> SearchAsync(AzureSearchQueryContext context, JObject queryObj)
        {
            /*
            var queryProp = queryObj["query"] as JObject;

            if (queryProp == null)
            {
                throw new ArgumentException("Query DSL requires a [query] property");
            }

            var query = CreateQueryFragment(context, queryProp);

            var sortProperty = queryObj["sort"];
            var fromProperty = queryObj["from"];
            var sizeProperty = queryObj["size"];

            var size = sizeProperty?.Value<int>() ?? 50;
            var from = fromProperty?.Value<int>() ?? 0;

            string sortField = null;
            string sortOrder = null;
            var sortFields = new List<SortField>();

            if (sortProperty != null)
            {
                if (sortProperty.Type == JTokenType.String)
                {
                    sortField = sortProperty.ToString();
                    sortFields.Add(new SortField(sortField, SortFieldType.STRING, sortOrder == "desc"));
                }
                else if (sortProperty.Type == JTokenType.Object)
                {
                    sortField = ((JProperty)sortProperty.First).Name;
                    sortOrder = ((JProperty)sortProperty.First).Value["order"].ToString();
                    sortFields.Add(new SortField(sortField, SortFieldType.STRING, sortOrder == "desc"));
                }
                else if (sortProperty.Type == JTokenType.Array)
                {
                    foreach (var item in sortProperty.Children()) {
                        sortField = ((JProperty)item.First).Name;
                        sortOrder = ((JProperty)item.First).Value["order"].ToString();
                        sortFields.Add(new SortField(sortField, SortFieldType.STRING, sortOrder == "desc"));
                    }
                }
            }

            TopDocs docs = context.IndexSearcher.Search(
                query,
                size + from,
                sortField == null ? Sort.RELEVANCE : new Sort(sortFields.ToArray())
            );

            if (from > 0)
            {
                docs = new TopDocs(docs.TotalHits - from, docs.ScoreDocs.Skip(from).ToArray(), docs.MaxScore);
            }

            return Task.FromResult(docs);
            */
            return null;
        }

        public object CreateQueryFragment(AzureSearchQueryContext context, JObject queryObj)
        {
            /*var first = queryObj.Properties().First();

            Query query = null;

            foreach (var queryProvider in _queryProviders)
            {
                query = queryProvider.CreateQuery(this, context, first.Name, (JObject)first.Value);

                if (query != null)
                {
                    break;
                }
            }

            return query;*/

            return null;
        }

        public static List<string> Tokenize(string fieldName, string text, object analyzer)
        {
            /*
            if (string.IsNullOrEmpty(text))
            {
                return new List<string>();
            }

            var result = new List<string>();
            using (var tokenStream = analyzer.GetTokenStream(fieldName, text))
            {
                tokenStream.Reset();
                while (tokenStream.IncrementToken())
                {
                    var termAttribute = tokenStream.GetAttribute<ICharTermAttribute>();

                    if (termAttribute != null)
                    {
                        result.Add(termAttribute.ToString());
                    }
                }
            }

            return result;
            */
            return null;
        }
    }
}
