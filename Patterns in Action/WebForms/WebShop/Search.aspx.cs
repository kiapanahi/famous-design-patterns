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
    public partial class Search : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "Search Products";
            Page.MetaKeywords = "Search, Electronic Products";
            Page.MetaDescription = "Search for Electronic Products at Patterns in Action";

            if (!IsPostBack)
            {
                // set the selected menu item in Master page

                SelectedMenu = "search";

                // set default button on page

                Form.DefaultButton = this.ButtonSearch.UniqueID;

                // default sort order

                SortColumn = "UnitPrice";
                SortDirection = "ASC";

                Bind();
            }
        }

        // takes search criteria, sets datasource, and bind data to controls.

        private void Bind()
        {
            // validate price range

            int priceRangeId;
            if (!int.TryParse(DropDownListRange.SelectedValue, out priceRangeId))
                priceRangeId = 0;

            // get product name entered

            string productName = this.TextBoxProductName.Text.Trim();

            // retrieve list of products.

            var service = new Service();

            double priceFrom = new PriceRange().GetList()[priceRangeId].RangeFrom;
            double priceThru = new PriceRange().GetList()[priceRangeId].RangeThru;
            if (priceThru == 0) priceThru = 5000;  // in case no range is set
            GridViewProducts.DataSource = service.SearchProducts(productName, priceFrom, priceThru, SortExpression);
            GridViewProducts.DataBind();

            PanelSearchResults.Visible = true;
        }

        
        // sets sort order and re-binds page
        
        protected void GridViewProducts_Sorting(object sender, GridViewSortEventArgs e)
        {
            SortDirection = (SortDirection == "ASC") ? "DESC" : "ASC";
            SortColumn = e.SortExpression;

            Bind();
        }

        
        // adds glyph to header according to current sort settings
        
        protected void GridViewProducts_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                AddGlyph(this.GridViewProducts, e.Row);
            }
        }

        
        // databinds page with selected search criteria
        
        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            Bind();
        }
    }
}