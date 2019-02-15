using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Text;

namespace Art.Web
{
    // generates pagination control

    public static class PagerHelpers
    {
        public static HtmlString Pager<T>(this HtmlHelper html, PagedList<T> list)
        {
            if (list == null) return new HtmlString("");

            int window = 5;  // moving window size. this is adjustable
            int halfwindow = window / 2;
            bool leftEllipsis = list.Page >= window;
            bool rightEllipsis = (list.TotalPages - list.Page + 1) >= window;

            var sb = new StringBuilder();
            sb.AppendLine("<div class='pagination'>");
            sb.AppendLine("<ul>");

            sb.AppendLine("<li " + (!list.HasPreviousPage ? "class='disabled'" : "") + "><a name='page0' href='#" + (list.Page - 1) + "'>&laquo;</a></li>");
            for (int i = 1; i <= list.TotalPages; i++)
            {
                if ((i == 2 && leftEllipsis) || (i == (list.TotalPages - 1) && rightEllipsis))
                {
                    sb.AppendLine("<li class='disabled'><a name='page0' href='#'>&hellip;</a></li>");
                }
                else if (i == 1 || i == list.TotalPages ||
                    (!leftEllipsis && (i <= window + 1)) ||
                    (!rightEllipsis && (i >= list.TotalPages - window)) ||
                    (i >= (list.Page - halfwindow) && i <= (list.Page + halfwindow)))
                {
                    sb.AppendLine("<li " + (list.Page == i ? "class='active'" : "") + "><a name='page" + i + "' href='#" + i + "'>" + i + "</a></li>");
                }
            }
            sb.AppendLine("<li " + (!list.HasNextPage ? "class='disabled'" : "") + "><a name='page" + (list.TotalPages + 1) + "' href='#" + (list.Page + 1) + "'>&raquo;</a></li>");

            sb.AppendLine("</ul>");
            sb.AppendLine("</div>");

            return new HtmlString(sb.ToString());
        }

        // textual display control of 'page of pages'
        public static HtmlString PagerText<T>(this HtmlHelper html, PagedList<T> list, string things)
        {
            if (list.TotalRows <= 0) return new HtmlString("");
            return new HtmlString(list.Range + "&nbsp;&nbsp;of&nbsp;&nbsp;" + list.TotalRows + " " + things);
        }
    }
}