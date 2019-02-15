using System.Web.Mvc;

namespace Mvc.Areas.Auth
{
    public class AuthAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Auth";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute("", "login", new { area = "Auth", controller = "Auth", action = "Login" });
            context.MapRoute("", "logout", new { area = "Auth", controller = "Auth", action = "Logout" });
        }
    }
}
