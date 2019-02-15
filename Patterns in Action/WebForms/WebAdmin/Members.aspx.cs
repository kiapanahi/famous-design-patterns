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
    public partial class Members : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "Member List";
            Page.MetaKeywords = "Member List, Patterns in Action";
            Page.MetaDescription = "Member List at Patterns in Action";

            if (!IsPostBack)
            {
                // set the selected menu item in the Master page

                SelectedMenu = "members";

                // set default sort settings

                SortColumn = "MemberId";
                SortDirection = "DESC";

                Bind();
            }
        }

        
        // sets datasources and bind data. 
        
        private void Bind()
        {
            var service = new Service();
            GridViewMembers.DataSource = service.GetMembers(SortExpression);
            GridViewMembers.DataBind();

            // display potential add or update message
            if (Session["message"] != null)
            {
                LabelError.Text = Session["message"].ToString();
                Session["message"] = null;
            }
        }

        #region Sorting

        
        // sets sort order and re-binds page.
        
        protected void GridViewMembers_Sorting(object sender, GridViewSortEventArgs e)
        {
            SortDirection = (SortDirection == "ASC") ? "DESC" : "ASC";
            SortColumn = e.SortExpression;

            Bind();
        }

        
        // adds glyphs to gridview header according to sort order.
        
        protected void GridViewMembers_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                AddGlyph(this.GridViewMembers, e.Row);
            }
        }

        protected void GridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton linkButton = e.Row.Cells[4].Controls[0] as LinkButton;
                
                // escape single quotes in Javascript. 

                string company = DataBinder.Eval(e.Row.DataItem, "CompanyName").ToString().Replace("'", "\\'");
                linkButton.Attributes.Add("onclick", "javascript:return " +
                "confirm('OK to delete \"" + company + "\"?')");
            }
        }

        #endregion

        // deletes member record
        
        protected void GridViewMembers_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var row = GridViewMembers.Rows[e.RowIndex];
            int memberId = int.Parse(row.Cells[0].Text);

            var service = new Service();

            // member with orders cannot be deleted

            var orders = service.GetOrdersByMember(memberId);
            if (orders.Count > 0)
            {
                string memberName = row.Cells[1].Text;
                LabelError.Text = "Cannot delete " + memberName + " because they have existing orders!";
            }
            else
            {
                var member = service.GetMember(memberId);
                service.DeleteMember(member);
                Session["message"] = "Member successfully deleted";

                Bind();
            }
        }
    }
}