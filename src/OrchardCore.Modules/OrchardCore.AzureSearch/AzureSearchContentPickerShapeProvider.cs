using Microsoft.AspNetCore.Html;
using Microsoft.Extensions.Localization;
using OrchardCore.DisplayManagement;
using OrchardCore.DisplayManagement.Descriptors;
using OrchardCore.DisplayManagement.Shapes;

namespace OrchardCore.AzureSearch
{
    public class AzureSearchContentPickerShapeProvider : IShapeAttributeProvider
    {
        public AzureSearchContentPickerShapeProvider(IStringLocalizer<AzureSearchContentPickerShapeProvider> stringLocalizer)
        {
            T = stringLocalizer;
        }

        private IStringLocalizer T { get; }

        [Shape]
        public IHtmlContent ContentPickerField_Option__AzureSearch(dynamic shape)
        {
            var selected = shape.Editor == "AzureSearch";
            if (selected)
            {
                return new HtmlString($"<option value=\"AzureSearch\" selected=\"selected\">{T["AzureSearch"]}</option>");
            }
            return new HtmlString($"<option value=\"AzureSearch\">{T["AzureSearch"]}</option>");
        }
    }
}
