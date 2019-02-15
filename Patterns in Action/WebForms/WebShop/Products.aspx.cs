using ActionService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebForms.Code;

namespace WebForms.WebShop
{
    public partial class Products : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "Products Page";
            Page.MetaKeywords = "Product List, Electronic Products";
            Page.MetaDescription = "Product List for Electronic Products at Patterns In Action";

            if (!IsPostBack)
            {
                // set the selected menu

                SelectedMenu = "products";

                // default sort order

                SortColumn = "UnitPrice";
                SortDirection = "ASC";

                Bind();
            }
        }
        
        // sets datasources and bind data to controls.
        
        private void Bind()
        {
            // validate selected CategoryId

            int categoryId;
            if (!int.TryParse(DropDownListCategories.SelectedValue, out categoryId))
                categoryId = 1;

            // gets list of products

            var service = new Service();
            GridViewProducts.DataSource = service.GetProductsByCategory(categoryId, SortExpression);
            GridViewProducts.DataBind();
        }

        
        // sets sort order and re-binds page.
        
        protected void GridViewProducts_Sorting(object sender, GridViewSortEventArgs e)
        {
            SortDirection = (SortDirection == "ASC") ? "DESC" : "ASC";
            SortColumn = e.SortExpression;

            Bind();
        }

        
        // adds glyph to header according to current sort settings.
        
        protected void GridViewProducts_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                AddGlyph(this.GridViewProducts, e.Row);
            }
        }
        
        // rebinds page following category change.
        
        protected void DropDownListCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            Bind();
        }
    }
}