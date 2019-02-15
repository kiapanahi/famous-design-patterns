using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebForms.Code;

namespace WebForms.WebShop
{
    public partial class Product : PageBase
    {
        
        // returns custom breadcrumb chain for this page. 

        public override SiteMapNode SiteMapResolve(object sender, SiteMapResolveEventArgs e)
        {
            var home = new SiteMapNode(e.Provider, "Home", "~/", "home");
            var shopping = new SiteMapNode(e.Provider, "Shopping", "~/shop", "shopping");
            var products = new SiteMapNode(e.Provider, "Products", "~/shop/products", "product catalog");
            var product = new SiteMapNode(e.Provider, "Product", null, "product details");

            shopping.ParentNode = home;
            products.ParentNode = shopping;
            product.ParentNode = products;

            return product;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "Product Details";
            Page.MetaKeywords = "Product Details, Electronic Products";
            Page.MetaDescription = "Product Details for Electronic Products at Patterns in Action";

            if (!IsPostBack)
            {
                // set the selected menu 

                SelectedMenu = "products";

                // sets image

                int productId = int.Parse(Page.RouteData.Values["productid"].ToString());
                ImageProduct.ImageUrl = "/images/products/" + productId + ".jpg";
            }
        }
    }
}