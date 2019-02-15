using System.Web.Mvc;

namespace Art.Web.Areas.Shop
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
            context.MapRoute("", "products/{id}", defaults: new { controller = "Product", action = "Product", area = "Shop" });
            context.MapRoute("", "products", defaults: new { controller = "Product", action = "Products", area = "Shop" });
            context.MapRoute("", "search", defaults: new { controller = "Product", action = "Search", area = "Shop" });

            context.MapRoute("", "cart", defaults: new { controller = "Cart", action = "Cart", area = "Shop" });
            context.MapRoute("", "checkout", defaults: new { controller = "Cart", action = "Checkout", area = "Shop" });
            context.MapRoute("", "confirm", defaults: new { controller = "Cart", action = "Confirm", area = "Shop" });
        }
    }
}
