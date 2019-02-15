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
    public partial class Order : PageBase
    {
        
        // returns custom breadcrumb chain for this page. 
        
        public override SiteMapNode SiteMapResolve(object sender, SiteMapResolveEventArgs e)
        {
            var home = new SiteMapNode(e.Provider, "Home", "~/", "home");
            var admin = new SiteMapNode(e.Provider, "Admin", "~/admin", "administration");
            var orders = new SiteMapNode(e.Provider, "Orders", "~/admin/members/orders", "orders");
            var member = new SiteMapNode(e.Provider, "Orders", null, "member orders");

            admin.ParentNode = home;
            orders.ParentNode = admin;
            member.ParentNode = orders;

            return member;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "Member Orders";
            Page.MetaKeywords = "Member Orders, Electronic Products";
            Page.MetaDescription = "Member Orders at Patterns in Action";

            if (!IsPostBack)
            {
                // set the selected menu item in the Master page.

                SelectedMenu = "orders";

                // save off memberId 

                MemberId = int.Parse(Page.RouteData.Values["memberid"].ToString());

                Bind();
            }
        }

        
        // sets datasources and bind data to controls.
        
        private void Bind()
        {
            // get member 

            var service = new Service();
            var member = service.GetMember(MemberId);

            // set company name

            LabelHeader.Text = "<font color='black'>Orders for:</font> "
                + member.CompanyName + " (" + member.Country + ")";

            GridViewOrders.DataSource = service.GetOrdersByMember(MemberId);
            GridViewOrders.DataBind();
        }

        
        // gets or sets memberId for the page in Session.
        
        private int MemberId
        {
            get { return int.Parse(Session["memberId"].ToString()); }
            set { Session["memberId"] = value; }
        }

        protected void GridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var link = e.Row.Cells[4].Controls[0] as HyperLink;
                link.NavigateUrl = link.NavigateUrl.Replace("memberid", MemberId.ToString());
            }
        }
    }
}