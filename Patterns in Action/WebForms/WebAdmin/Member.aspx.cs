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
    public partial class Member : PageBase
    {
        // returns custom breadcrumb chain for this page. 

        public override SiteMapNode SiteMapResolve(object sender, SiteMapResolveEventArgs e)
        {
            var home = new SiteMapNode(e.Provider, "Home", "~/", "home");
            var admin = new SiteMapNode(e.Provider, "Admin", "~/admin", "administration");
            var members = new SiteMapNode(e.Provider, "Members", "~/admin/members", "members");
            var member = new SiteMapNode(e.Provider, "Member", null, "member details");

            admin.ParentNode = home;
            members.ParentNode = admin;
            member.ParentNode = members;

            return member;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "Member Details";
            Page.MetaKeywords = "Member Details, Patterns in Action";
            Page.MetaDescription = "Member Details at Patterns in Action";

            if (!IsPostBack)
            {
                // sets the selected menu item in the Master page

                SelectedMenu = "members";

                MemberId = int.Parse(Page.RouteData.Values["memberId"].ToString());

                // set DetailsView control in Add or Edit mode

                if (MemberId == 0)
                    DetailsViewMember.ChangeMode(DetailsViewMode.Insert);
                else
                    DetailsViewMember.ChangeMode(DetailsViewMode.Edit);

                // if no image is available display silhouette

                int photoId = (MemberId > 0 && MemberId < 92) ? MemberId : 0;
                ImageMember.ImageUrl = "/images/members/large/" + photoId + ".jpg";
            }
        }

        
        // gets or sets the memberId for this page.
        
        private int MemberId
        {
            get { return int.Parse(Session["MemberId"].ToString()); }
            set { Session["MemberId"] = value; }
        }

        
        // saves data for new or edited member to database.
        
        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            var service = new Service();

            var member  = (MemberId == 0) ? new BusinessObjects.Member() : service.GetMember(MemberId);

            // get email name from page

            var row = DetailsViewMember.Rows[1];
            var textBox = row.Cells[1].Controls[0] as TextBox;
            member.Email = textBox.Text.Trim();

            // get Company name from page.

            row = DetailsViewMember.Rows[2];
            textBox = row.Cells[1].Controls[0] as TextBox;
            member.CompanyName = textBox.Text.Trim();

            // get City from page

            row = DetailsViewMember.Rows[3];
            textBox = row.Cells[1].Controls[0] as TextBox;
            member.City = textBox.Text.Trim();

            // get Country from page

            row = DetailsViewMember.Rows[4];
            textBox = row.Cells[1].Controls[0] as TextBox;
            member.Country = textBox.Text.Trim();

            // validate using business rules engine

            if (member.IsValid())
            {
                if (MemberId == 0)
                {
                    service.InsertMember(member);
                    Session["message"] = "New member successfully added";
                }
                else
                {
                    service.UpdateMember(member);
                    Session["message"] = "Member successfully updated";
                }
            }
            else
            {
                LabelError.Text = member.Errors.Aggregate((current, next) => current + "</br>" + next);
                PanelError.Visible = true;
                return;
            }

            // return to list of members

            Response.Redirect(UrlMaker.ToMembers());
        }

        
        // cancel the page and redirect user to page with list of members
        
        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(UrlMaker.ToMembers());
        }

        // executed only once. places cursor in first editable field
        
        protected void DetailsView_OnDataBound(object sender, EventArgs e)
        {
            if (DetailsViewMember.Rows.Count < 1) return;

            var row = DetailsViewMember.Rows[1];
            var textBox = row.Cells[1].Controls[0] as TextBox;
            textBox.Focus();
        }
    }
}