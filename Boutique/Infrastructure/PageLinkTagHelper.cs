using Boutique.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Boutique.Infrastructure
{
    [HtmlTargetElement("ul", Attributes = "page-model")]
    public class PageLinkTagHelper : TagHelper
    {
        private IUrlHelperFactory urlHelperFactory;

        public PageLinkTagHelper(IUrlHelperFactory _urlHelperFactory)
        {
            urlHelperFactory = _urlHelperFactory;
        }
        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }
        public PagingInfo PageModel { get; set; }
        public string PageAction { get; set; }
        public override void  Process(TagHelperContext context, TagHelperOutput output)
        {
            var urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);

            var result = new TagBuilder("ul");
            result.AddCssClass("pagination justify-content-center justify-content-lg-end");
            for (int i = 1; i <= PageModel.TotalPages(); i++)
            {
                var childTag = new TagBuilder("a");
                var parentTag = new TagBuilder("li");
                childTag.Attributes["href"] = urlHelper.Action(PageAction, new { page = i });
                childTag.InnerHtml.Append(i.ToString());
                childTag.AddCssClass("page-link");
                if (i == PageModel.CurrentPage)
                {
                    parentTag.AddCssClass("page-item mx-1 active");
                }
                else
                {
                    parentTag.AddCssClass("page-item mx-1 ");
                }
                parentTag.InnerHtml.AppendHtml(childTag);
                result.InnerHtml.AppendHtml(parentTag);
            }
            output.Content.AppendHtml(result.InnerHtml);
        }
    }
}
