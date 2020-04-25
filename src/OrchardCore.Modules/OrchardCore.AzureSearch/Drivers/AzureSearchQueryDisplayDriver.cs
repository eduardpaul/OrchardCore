using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.AzureSearch.ViewModels;
using OrchardCore.Queries;

namespace OrchardCore.AzureSearch.Drivers
{
    public class AzureSearchQueryDisplayDriver : DisplayDriver<Query, AzureSearchQuery>
    {
        private IStringLocalizer S;
        private AzureSearchIndexManager _AzureSearchIndexManager;

        public AzureSearchQueryDisplayDriver(
            IStringLocalizer<AzureSearchQueryDisplayDriver> stringLocalizer,
            AzureSearchIndexManager AzureSearchIndexManager)
        {
            _AzureSearchIndexManager = AzureSearchIndexManager;
            S = stringLocalizer;
        }

        public override IDisplayResult Display(AzureSearchQuery query, IUpdateModel updater)
        {
            return Combine(
                Dynamic("AzureSearchQuery_SummaryAdmin", model => { model.Query = query; }).Location("Content:5"),
                Dynamic("AzureSearchQuery_Buttons_SummaryAdmin", model => { model.Query = query; }).Location("Actions:2")
            );
        }

        public override IDisplayResult Edit(AzureSearchQuery query, IUpdateModel updater)
        {
            return Initialize<AzureSearchQueryViewModel>("AzureSearchQuery_Edit", model =>
            {
                model.Query = query.Template;
                model.Index = query.Index;
                model.ReturnContentItems = query.ReturnContentItems;
                //model.Indices = _AzureSearchIndexManager.List().ToArray();

                // Extract query from the query string if we come from the main query editor
                if (string.IsNullOrEmpty(query.Template))
                {
                    updater.TryUpdateModelAsync(model, "", m => m.Query);
                }
            }).Location("Content:5");
        }

        public override async Task<IDisplayResult> UpdateAsync(AzureSearchQuery model, IUpdateModel updater)
        {
            var viewModel = new AzureSearchQueryViewModel();
            if (await updater.TryUpdateModelAsync(viewModel, Prefix, m => m.Query, m => m.Index, m => m.ReturnContentItems))
            {
                model.Template = viewModel.Query;
                model.Index = viewModel.Index;
                model.ReturnContentItems = viewModel.ReturnContentItems;
            }

            if (String.IsNullOrWhiteSpace(model.Template))
            {
                updater.ModelState.AddModelError(nameof(model.Template), S["The query field is required"]);
            }

            if (String.IsNullOrWhiteSpace(model.Index))
            {
                updater.ModelState.AddModelError(nameof(model.Index), S["The index field is required"]);
            }

            return Edit(model, updater);
        }
    }
}
