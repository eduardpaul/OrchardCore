using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace OrchardCore.AzureSearch.Settings
{
    public class ContentPickerFieldAzureSearchEditorSettings
    {
        public string Index { get; set; }

        [BindNever]
        public string[] Indices { get; set; }
    }
}