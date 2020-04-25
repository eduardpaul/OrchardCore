using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OrchardCore.ContentManagement;
using OrchardCore.AzureSearch.Services;
using OrchardCore.AzureSearch.ViewModels;
using OrchardCore.Navigation;
using OrchardCore.Settings;

namespace OrchardCore.AzureSearch.Controllers
{
    public class SearchController : Controller
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly ISiteService _siteService;
        private readonly AzureSearchIndexManager _AzureSearchIndexProvider;
        private readonly AzureSearchIndexingService _AzureSearchIndexingService;
        private readonly ISearchQueryService _searchQueryService;
        private readonly IContentManager _contentManager;

        public SearchController(
              IAuthorizationService authorizationService,
            ISiteService siteService,
            AzureSearchIndexManager AzureSearchIndexProvider,
            AzureSearchIndexingService AzureSearchIndexingService,
            ISearchQueryService searchQueryService,
            IContentManager contentManager,
            ILogger<SearchController> logger
            )
        {
            _authorizationService = authorizationService;
            _siteService = siteService;
            _AzureSearchIndexProvider = AzureSearchIndexProvider;
            _AzureSearchIndexingService = AzureSearchIndexingService;
            _searchQueryService = searchQueryService;
            _contentManager = contentManager;

            Logger = logger;
        }

        ILogger Logger { get; set; }

        public async Task<IActionResult> Index(string id, string q, PagerParameters pagerParameters)
        {
            if (!await _authorizationService.AuthorizeAsync(User, Permissions.QueryAzureSearchSearch))
            {
                return NotFound();
            }

            var siteSettings = await _siteService.GetSiteSettingsAsync();
            var pager = new Pager(pagerParameters, siteSettings.PageSize);

            var indexName = "Search";

            if (!string.IsNullOrWhiteSpace(id))
            {
                indexName = id;
            }
            /*
            if (!_AzureSearchIndexProvider.Exists(indexName))
            {
                return NotFound();
            }
            */
            if (string.IsNullOrWhiteSpace(q))
            {
                return View(new SearchIndexViewModel
                {
                    Pager = pager,
                    IndexName = id,
                    ContentItems = Enumerable.Empty<ContentItem>()
                });
            }

            var AzureSearchSettings = await _AzureSearchIndexingService.GetAzureSearchSettingsAsync();

            if (AzureSearchSettings == null || AzureSearchSettings?.DefaultSearchFields == null)
            {
                Logger.LogInformation("Couldn't execute search. No AzureSearch settings was defined.");

                return View(new SearchIndexViewModel
                {
                    HasMoreResults = false,
                    Query = q,
                    Pager = pager,
                    IndexName = id,
                    ContentItems = Enumerable.Empty<ContentItem>()
                });
            }

            /*
            var queryParser = new MultiFieldQueryParser(AzureSearchSettings.DefaultVersion, AzureSearchSettings.DefaultSearchFields, new StandardAnalyzer(AzureSearchSettings.DefaultVersion));
            var query = queryParser.Parse(QueryParser.Escape(q));
            */

            int start = pager.GetStartIndex(), size = pager.PageSize, end = size + 1;// Fetch one more result than PageSize to generate "More" links
            var contentItemIds = await _searchQueryService.ExecuteQueryAsync(null, indexName, start, end);

            var contentItems = new List<ContentItem>();
            foreach (var contentItemId in contentItemIds.Take(size))
            {
                var contentItem = await _contentManager.GetAsync(contentItemId);
                if (contentItem != null)
                {
                    contentItems.Add(contentItem);
                }
            }

            var model = new SearchIndexViewModel
            {
                /*HasMoreResults = contentItemIds.Count > size,*/
                Query = q,
                Pager = pager,
                IndexName = id,
                ContentItems = contentItems
            };

            return View(model);
        }
    }
}