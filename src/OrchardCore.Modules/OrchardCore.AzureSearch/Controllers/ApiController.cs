using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using YesSql;

namespace OrchardCore.AzureSearch.Controllers
{
    [Route("api/AzureSearch")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Api"), IgnoreAntiforgeryToken, AllowAnonymous]
    public class ApiController : Controller
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly AzureSearchQuerySource _AzureSearchQuerySource;
        private readonly ISession _session;

        public ApiController(
            IAuthorizationService authorizationService,
            AzureSearchQuerySource AzureSearchQuerySource,
            ISession session)
        {
            _authorizationService = authorizationService;
            _AzureSearchQuerySource = AzureSearchQuerySource;
            _session = session;
        }

        [HttpPost, HttpGet]
        [Route("content")]
        public async Task<IActionResult> Content(
            string indexName,
            string query,
            string parameters)
        {
            if (!await _authorizationService.AuthorizeAsync(User, Permissions.QueryAzureSearchApi))
            {
                return Unauthorized();
            }

            var AzureSearchQuery = new AzureSearchQuery
            {
                Index = indexName,
                Template = query,
                ReturnContentItems = true
            };

            var queryParameters = parameters != null ?
                JsonConvert.DeserializeObject<Dictionary<string, object>>(parameters)
                : new Dictionary<string, object>();

            var result = await _AzureSearchQuerySource.ExecuteQueryAsync(AzureSearchQuery, queryParameters);

            return new ObjectResult(result);
        }

        [HttpPost, HttpGet]
        [Route("documents")]
        public async Task<IActionResult> Documents(
            string indexName,
            string query,
            string parameters)
        {
            if (!await _authorizationService.AuthorizeAsync(User, Permissions.QueryAzureSearchApi))
            {
                return Unauthorized();
            }

            var AzureSearchQuery = new AzureSearchQuery
            {
                Index = indexName,
                Template = query
            };

            var queryParameters = parameters != null ?
                JsonConvert.DeserializeObject<Dictionary<string, object>>(parameters)
                : new Dictionary<string, object>();

            var result = await _AzureSearchQuerySource.ExecuteQueryAsync(AzureSearchQuery, queryParameters);

            return new ObjectResult(result);
        }
    }
}
