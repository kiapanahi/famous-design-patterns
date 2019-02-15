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
    public partial class Orders : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "Members and Orders";
            Page.MetaKeywords = "Members, Order Count, Patterns In Action";
            Page.MetaDescription = "Members and their Order Count at Patterns in Action";

            if (!IsPostBack)
            {
                // set the selected menu

                SelectedMenu = "orders";

                // set the default sort settings

                SortColumn = "MemberId";
                SortDirection = "ASC";

                Bind();
            }
        }
        
        // sets datasources and bind data to controls.
        
        private void Bind()
        {
            var service = new Service();
            GridViewOrders.DataSource = service.GetMembersWithOrderStatistics(SortExpression);
            GridViewOrders.DataBind();
        }

        #region Sorting

        
        // sets sort order and re-binds page.
        
        protected void GridViewOrders_Sorting(object sender, GridViewSortEventArgs e)
        {
            SortDirection = (SortDirection == "ASC") ? "DESC" : "ASC";
            SortColumn = e.SortExpression;

            Bind();
        }

        // adds glyph to header according to current sort settings.
        
        protected void GridViewOrders_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                AddGlyph(this.GridViewOrders, e.Row);
            }
        }

        #endregion
    }
}