using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Text;

namespace Art.Web
{
    // generates cart control

    public static class CartHelper
    {
        public static HtmlString Cart(this HtmlHelper html, int count)
        {
            // using simple string concatenation

            var sb = new StringBuilder("<li><a id='thecart' ");
            if (count > 0) sb.Append("style='background-color:orange;color:white;' ");

            sb.Append("href='/cart'><i class='icon-shopping-cart icon-white'></i> Cart<span id='countshow'>");
            if (count > 0) sb.Append("(" + count + ")");

            sb.Append("</span></a></li>");

            return new HtmlString(sb.ToString());
        }
    }
}