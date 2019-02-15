using ActionService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebForms.Code;

namespace WebForms.WebAdmin
{
    public partial class OrderDetails : PageBase
    {
        int orderId; 

        // returns custom breadcrumb chain for this page. 

        public override SiteMapNode SiteMapResolve(object sender, SiteMapResolveEventArgs e)
        {
            var home = new SiteMapNode(e.Provider, "Home", "~/", "home");
            var admin = new SiteMapNode(e.Provider, "Admin", "~/admin", "administration");
            var orders = new SiteMapNode(e.Provider, "Orders", "~/admin/members/orders", "orders");

            var memberId = Page.RouteData.Values["memberid"].ToString();
            var member = new SiteMapNode(e.Provider, "Member", "~/admin/members/" + memberId + "/orders", "member orders");
            var details = new SiteMapNode(e.Provider, "Details", null, "line items");

            admin.ParentNode = home;
            orders.ParentNode = admin;
            member.ParentNode = orders;
            details.ParentNode = member;

            return details;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "Order Details";
            Page.MetaKeywords = "Members, Order Details, Patterns in Action";
            Page.MetaDescription = "Member's order details at Patterns in Action";

            if (!IsPostBack)
            {
                // set the selected menu item in Master page

                SelectedMenu = "orders";

                // save off OrderId for this page

                orderId = int.Parse(Page.RouteData.Values["orderid"].ToString());

                Bind();
            }
        }

        
        // sets datasources and bind data to controls.
        
        private void Bind()
        {
            var service = new Service();
            var order = service.GetOrder(orderId);

            // sets the date

            LabelHeader.Text = "Order Line Items";
            LabelOrderDate.Text = "Order date: " + order.OrderDate.ToShortDateString();
            HyperLinkBack.Text = "< back to orders ";


            var orderDetails = service.GetOrderDetails(orderId);
            foreach (var detail in orderDetails)
            {
                // caching products would be more effective. however, # of details is usually fairly small.

                var product = service.GetProduct(detail.ProductId);
                detail.ProductName = product.ProductName;
            }

            GridViewOrderDetails.DataSource = orderDetails;
            GridViewOrderDetails.DataBind();
        }
    }
}