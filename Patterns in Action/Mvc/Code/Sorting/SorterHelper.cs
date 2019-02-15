using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Mvc.Code
{
    // sorter helper class. Holds extension methods.
    
    public static class SorterHelper
    {
        // extension method. Returns anchor element (a) that contains the virtual path for sort action.
        // this is where column sort headers are created.
        public static MvcHtmlString Sorter<T>(this HtmlHelper html, SortedList<T> list, string linkText, string sort, string order, object htmlAttributes = null)
        {
            if (list == null) return null;

            var tag = new TagBuilder("a");
            tag.InnerHtml = linkText;

            // set Css class to selected if indeed selected

            if (list.Sort.Equals(sort, StringComparison.InvariantCultureIgnoreCase))
                tag.AddCssClass("selected-" + list.Order);

            // Onclick: submit back and sort by same column but in reverse order. Uses jQuery.

            tag.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            tag.MergeAttribute("onclick", "$('#sort').val('" + sort + "');$('#order').val('" + list.Order.ReverseOrder() + "');$('form').submit();return false;");

            // set the correct url to anchor tag. 

            var urlHelper = new UrlHelper(html.ViewContext.RequestContext);
            var dictionary = html.ViewContext.RequestContext.RouteData.Values;
            string url = urlHelper.RouteUrl(dictionary);
            tag.MergeAttribute("href", url);

            return MvcHtmlString.Create(tag.ToString());
        }

        // reverses sort order

        private static string ReverseOrder(this string order)
        {
            return order.ToLower() == "asc" ? "desc" : "asc";
        }
    }
}