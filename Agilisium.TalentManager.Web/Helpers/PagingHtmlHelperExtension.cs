using Agilisium.TalentManager.Web.Models;
using System;
using System.Text;
using System.Web.Mvc;

namespace Agilisium.TalentManager.Web.Helpers
{
    public static class PagingHtmlHelperExtension
    {
        public static MvcHtmlString AddPagination(this HtmlHelper html, PagingInfo pagingInfo, Func<int, string> pageURL, bool isPagingEnabled = true)
        {
            if (isPagingEnabled == false) return MvcHtmlString.Create(string.Empty);

            StringBuilder result = new StringBuilder();

            for (int i = 1; i <= pagingInfo.TotalPageCount; i++)
            {
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageURL(i));
                tag.InnerHtml = i.ToString();
                if (i == pagingInfo.CurentPageNo)
                {
                    tag.AddCssClass("selected");
                    tag.AddCssClass("btn-primary");
                }
                tag.AddCssClass("btn btn-default");
                result.Append(tag.ToString());
            }

            return MvcHtmlString.Create(result.ToString());
        }
    }
}