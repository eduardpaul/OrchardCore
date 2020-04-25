using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace OrchardCore.AzureSearch
{
    public interface IAzureSearchQueryService
    {
        Task<object> SearchAsync(AzureSearchQueryContext context, JObject queryObj);
        object CreateQueryFragment(AzureSearchQueryContext context, JObject queryObj);
    }
}