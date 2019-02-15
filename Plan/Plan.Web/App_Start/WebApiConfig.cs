using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Plan.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // note the inclusion of the v1 folder

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/v1/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
