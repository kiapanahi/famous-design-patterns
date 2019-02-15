using System.Web.Mvc;

namespace Mvc.Areas.Shop
{
    public class ShopAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Shop";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute("", "shopping", new { area = "Shop", controller = "Shop", action = "Shopping" });
            context.MapRoute("", "products", new { area = "Shop", controller = "Shop", action = "Products" });
            context.MapRoute("", "products/{id}", new { area = "Shop", controller = "Shop", action = "Product" });
            context.MapRoute("", "search", new { area = "Shop", controller = "Shop", action = "Search" });
        }
    }
}
