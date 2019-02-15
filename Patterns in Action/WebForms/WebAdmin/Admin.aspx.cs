using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebForms.Code;

namespace WebForms.WebAdmin
{
    public partial class Admin : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "Administration";
            Page.MetaKeywords = "Administration, Members, Orders";
            Page.MetaDescription = "Administer Members and Orders";

            if (!IsPostBack)
            {
                // set the selected menu item in the Master page

                SelectedMenu = "administration";
            }
        }
    }
}