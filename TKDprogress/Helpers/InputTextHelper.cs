using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace TKDprogress.Helpers
{
    [HtmlTargetElement("custom-input")]
    public class InputTextHelper : TagHelper
    {
        public string Label { get; set; }
        public string Name { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";

            var labelHtml = $@"<label asp-for=""{Name}"" class=""block mb-2 text-lg font-medium text-gray-900"">{Label}</label>";
            var inputHtml = $@"<input type=""text"" asp-for=""{Name}"" name=""{Name}"" class=""bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5"" />";
            var validationHtml = $@"<span asp-validation-for=""{Name}"" class=""text-red-700""></span>";

            var combinedHtml = $"{labelHtml}{inputHtml}{validationHtml}";

            output.Content.SetHtmlContent(combinedHtml);
        }
    }
}
