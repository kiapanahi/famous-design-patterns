using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Mvc
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // home and error page

            routes.MapRoute("", "", new { area="", controller = "Home", action = "Index" });
            routes.MapRoute("", "error", new { area="", controller = "Home", action = "Error" });
        }
    }
}