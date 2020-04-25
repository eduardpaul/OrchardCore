using System.Linq;
using System.Threading.Tasks;
using OrchardCore.ContentManagement.Metadata.Models;
using OrchardCore.ContentTypes.Editors;
using OrchardCore.DisplayManagement.Views;

namespace OrchardCore.AzureSearch.Settings
{
    public class ContentPickerFieldAzureSearchEditorSettingsDriver : ContentPartFieldDefinitionDisplayDriver
    {
        private readonly AzureSearchIndexManager _AzureSearchIndexManager;

        public ContentPickerFieldAzureSearchEditorSettingsDriver(AzureSearchIndexManager AzureSearchIndexManager)
        {
            _AzureSearchIndexManager = AzureSearchIndexManager;
        }

        public override IDisplayResult Edit(ContentPartFieldDefinition partFieldDefinition)
        {
            return Initialize<ContentPickerFieldAzureSearchEditorSettings>("ContentPickerFieldAzureSearchEditorSettings_Edit", model =>
            {
                partFieldDefinition.PopulateSettings<ContentPickerFieldAzureSearchEditorSettings>(model);
                //model.Indices = _AzureSearchIndexManager.List().ToArray();
            }).Location("Editor");
        }

        public override async Task<IDisplayResult> UpdateAsync(ContentPartFieldDefinition partFieldDefinition, UpdatePartFieldEditorContext context)
        {
            if (partFieldDefinition.Editor() == "AzureSearch")
            {
                var model = new ContentPickerFieldAzureSearchEditorSettings();

                await context.Updater.TryUpdateModelAsync(model, Prefix);

                context.Builder.WithSettings(model);
            }

            return Edit(partFieldDefinition);
        }

        public override bool CanHandleModel(ContentPartFieldDefinition model)
        {
            return string.Equals("ContentPickerField", model.FieldDefinition.Name);
        }
    }
}
