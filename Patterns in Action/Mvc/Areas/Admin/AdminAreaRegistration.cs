using System.Web.Http.Routing;
using System.Web.Mvc;

namespace Mvc.Areas.Admin
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
            context.MapRoute("", "administration", new { area = "Admin", controller = "Admin", action = "Administration" });
            context.MapRoute("", "members/delete/{id}", new { area = "Admin", controller = "Admin", action = "MemberDelete" });
            context.MapRoute("", "members/{id}/orders", new { area = "Admin", controller = "Admin", action = "MemberOrders" });
            context.MapRoute("", "members/{id}/orders/{oid}", new { area = "Admin", controller = "Admin", action = "Order" });
            context.MapRoute("", "members/{id}", new { area = "Admin", controller = "Admin", action = "Member" });
            context.MapRoute("", "members", new { area = "Admin", controller = "Admin", action = "Members" });
            context.MapRoute("", "orders", new { area = "Admin", controller = "Admin", action = "Orders" });
        }
    }
}
