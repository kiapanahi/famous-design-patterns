using System.Web.Mvc;

namespace Art.Web.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute("", "users", defaults: new { controller = "Admin", action = "Users", area = "Admin" });
            context.MapRoute("", "user/{id}", defaults: new { controller = "Admin", action = "User", area = "Admin", id = UrlParameter.Optional }); // GET, POST, PUT OR DELETE

            context.MapRoute("", "reports", defaults: new { controller = "Report", action = "Reports", area = "Admin" });
            context.MapRoute("", "reports/users", defaults: new { controller = "Report", action = "Users", area = "Admin" });
            context.MapRoute("", "reports/orders", defaults: new { controller = "Report", action = "Orders", area = "Admin" });
            context.MapRoute("", "reports/orders/{id}", defaults: new { controller = "Report", action = "OrderDetails", area = "Admin" });
            context.MapRoute("", "reports/products", defaults: new { controller = "Report", action = "Products", area = "Admin" });

            context.MapRoute("", "dashboard", defaults: new { controller = "Dashboard", action = "Dashboard", area = "Admin" });

            context.MapRoute("", "adhoc", defaults: new { controller = "Adhoc", action = "Adhoc", area = "Admin" });
        }
    }
}
