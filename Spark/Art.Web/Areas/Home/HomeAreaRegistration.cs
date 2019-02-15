using System.Web.Mvc;

namespace Art.Web.Areas.Home
{
    public class HomeAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Home";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute("", "", defaults: new { controller = "Home", action = "Index", area = "Home" });
            context.MapRoute("", "about", defaults: new { controller = "Home", action = "About", area = "Home" });
            context.MapRoute("", "error", defaults: new { controller = "Home", action = "Error", area = "Home" });
        }
    }
}
