using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrchardCore.AzureSearch.Services
{
    public interface ISearchQueryService
    {
        Task<IList<string>> ExecuteQueryAsync(object query, string indexName, int start = 0, int end = 20);
    }
}