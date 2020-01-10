using Microsoft.AspNetCore.Razor.TagHelpers;

namespace CustomTagHelpers
{
    public class PartyTagHelper : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            //<a asp-action="Index" asp-controller="Party" asp-route-id="@party.Id">@party.Title</a>
            //<a href="/Party/Index/1">Super Party</a>
            output.TagName = "a";
            output.Attributes.Clear();
            output.Attributes.SetAttribute("href", "/Party/Index/" + context.AllAttributes["id"].Value);
            output.Content.SetContent(output.GetChildContentAsync().Result.GetContent());
        }
    }
}
