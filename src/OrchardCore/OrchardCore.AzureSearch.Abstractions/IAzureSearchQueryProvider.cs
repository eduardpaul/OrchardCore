using Newtonsoft.Json.Linq;

namespace OrchardCore.AzureSearch
{
    public interface IAzureSearchQueryProvider
    {
        object CreateQuery(IAzureSearchQueryService builder, AzureSearchQueryContext context, string type, JObject query);
    }
}
