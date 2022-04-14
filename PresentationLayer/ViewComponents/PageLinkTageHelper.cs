using Microsoft.AspNetCore.Razor.TagHelpers;
using PresentationLayer.Models;
using System.Text;

namespace PresentationLayer.ViewComponents
{
    [HtmlTargetElement("div", Attributes = "page-model")]
    public class PageLinkTageHelper:TagHelper
    {
        public PageInfo pageModel { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("<ul class='pagination'>");
            for (int i = 0; i <= pageModel.TotalPages(); i++)
            {
                stringBuilder.AppendFormat("<li class='page-item{0}'>", i == pageModel.CurrentPage ? "active" : " ");

                if (string.IsNullOrEmpty(pageModel.CurrentCategory))
                {
                    stringBuilder.AppendFormat("<a class='page-link'href='/products?page={0}'>{0}</a>",i);
                }
                else
                {
                    stringBuilder.AppendFormat("<a class='page-link'href='/products/?page={1}'>{0}</a>",i, pageModel.CurrentCategory);
                }
                stringBuilder.Append("</li>");
            }
            output.Content.SetHtmlContent(stringBuilder.ToString());
            base.Process(context, output);
        }

    }
}
