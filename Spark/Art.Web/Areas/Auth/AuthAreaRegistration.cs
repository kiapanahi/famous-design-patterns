using System.Web.Mvc;

namespace Art.Web.Areas.Auth
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
            context.MapRoute("", "signup", defaults: new { controller = "Auth", action = "Signup", area = "Auth" });
            context.MapRoute("", "login", defaults: new { controller = "Auth", action = "Login", area = "Auth" });
            context.MapRoute("", "logout", defaults: new { controller = "Auth", action = "Logout", area = "Auth" });
            context.MapRoute("", "logout/confirm", defaults: new { controller = "Auth", action = "LogoutConfirm", area = "Auth" });
            context.MapRoute("", "account", defaults: new { controller = "Auth", action = "Account", area = "Auth" });
            context.MapRoute("", "account/change", defaults: new { controller = "Auth", action = "ChangeAccount", area = "Auth" });
            context.MapRoute("", "account/password", defaults: new { controller = "Auth", action = "ChangePassword", area = "Auth" });

            context.MapRoute("", "external/login", defaults: new { controller = "Auth", action = "ExternalLogin" });
            context.MapRoute("", "external/logincallback", defaults: new { controller = "Auth", action = "ExternalLoginCallback", area = "Auth" });
            context.MapRoute("", "external/loginfailure", defaults: new { controller = "Auth", action = "ExternalLoginFailure", area = "Auth" });
        }
    }
}
