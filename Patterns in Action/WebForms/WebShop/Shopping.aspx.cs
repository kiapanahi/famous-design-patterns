using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebForms.Code;

namespace WebForms.WebShop
{
    public partial class Shopping : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "Shopping";
            Page.MetaKeywords = "Shopping, Electronic Products";
            Page.MetaDescription = "Start your Shopping for Electronic Products at Patterns in Action";

            if (!IsPostBack)
            {
                // sets menu item

                SelectedMenu = "shopping";
            }
        }
    }
}