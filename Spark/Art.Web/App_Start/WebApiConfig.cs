using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Art.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // note the inclusion of the v1 folder

            var r = config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/v1/{controller}/{id}",              
                defaults: new { id = RouteParameter.Optional }
            );

            // note: MapHttpRoute does not currently support namespaces.
            // hopefully in the next .NET release so that our next API version is easily added.
            // alternatively, you could do things manually or try to set a default namespace like so: 
  
            // r.DataTokens["Namespaces"] = new string[] { "Art.Rest.v1" };
        }
    }
}
