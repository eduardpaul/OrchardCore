using System;
using System.Threading.Tasks;
using OrchardCore.DisplayManagement.Entities;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.AzureSearch.ViewModels;
using OrchardCore.Settings;

namespace OrchardCore.AzureSearch.Drivers
{
    public class AzureSearchSiteSettingsDisplayDriver : SectionDisplayDriver<ISite, AzureSearchSettings>
    {
        private readonly AzureSearchIndexManager _AzureSearchIndexProvider;

        public AzureSearchSiteSettingsDisplayDriver(AzureSearchIndexManager AzureSearchIndexProvider)
        {
            _AzureSearchIndexProvider = AzureSearchIndexProvider;
        }

        public override IDisplayResult Edit(AzureSearchSettings section, BuildEditorContext context)
        {
            return Initialize<AzureSearchSettingsViewModel>("AzureSearchSettings_Edit", model =>
                {
                    model.SearchIndex = section.SearchIndex;
                    model.SearchFields = String.Join(", ", section.DefaultSearchFields ?? new string[0]);
                    //model.SearchIndexes = _AzureSearchIndexProvider.List();
                }).Location("Content:2").OnGroup("search");
        }

        public override async Task<IDisplayResult> UpdateAsync(AzureSearchSettings section,  BuildEditorContext context)
        {
            if (context.GroupId == "search")
            {
                var model = new AzureSearchSettingsViewModel();

                await context.Updater.TryUpdateModelAsync(model, Prefix);

                section.SearchIndex = model.SearchIndex;
                section.DefaultSearchFields = model.SearchFields?.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            }

            return await EditAsync(section, context);
        }
    }
}
