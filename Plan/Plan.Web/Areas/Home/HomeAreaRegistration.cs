using System.Web.Mvc;

namespace Plan.Web.Areas.Home
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
        }
    }
}
