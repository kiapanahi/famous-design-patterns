using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;

namespace Mvc.Code
{
    // static breadcrumb helper class.
    
    public static class BreadCrumbHelper
    {
        // extension method. builds entire breadcrumbs string

        public static MvcHtmlString BreadCrumbs(this HtmlHelper html)
        {
            var breadCrumbs = html.ViewContext.ViewBag.Crumbs as List<BreadCrumb>;

            var crumbs = new StringBuilder();
            for (int i = 0; i < breadCrumbs.Count; i++)
            {
                var crumb = breadCrumbs[i];

                if (string.IsNullOrEmpty(crumb.Url))
                    crumbs.Append(" " + crumb.Title);
                else
                    crumbs.AppendFormat("<a href='{0}'>{1}</a>", crumb.Url, crumb.Title);


                if (i < breadCrumbs.Count - 1) crumbs.Append(" &nbsp;|&nbsp; ");
            }

            return MvcHtmlString.Create(crumbs.ToString());
        }
    }
}