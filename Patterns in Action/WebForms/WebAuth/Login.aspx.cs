using ActionService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebForms.Code;

namespace WebForms.WebAuth
{
    public partial class Login : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "Login";
            Page.MetaKeywords = "Login, Administration";
            Page.MetaDescription = "Login to Administrative Area in Patterns in Action";

            if (!IsPostBack)
            {
                // set the selected menu item in Master page

                SelectedMenu = "login";

                Tries = 0;

                // places cursor in first field (better done in javascript).

                TextboxEmail.Focus();
            }
        }

        protected void ButtonSubmit_Click(object sender, System.EventArgs e)
        {
            string email = TextboxEmail.Text.Trim();
            string password = TextboxPassword.Text.Trim();

            var service = new Service();

            if (service.Login(email, password))
            {
                string redirectUrl = FormsAuthentication.GetRedirectUrl(email, false);
                if (redirectUrl != null && redirectUrl.IndexOf("admin") >= 0)
                    FormsAuthentication.RedirectFromLoginPage(email, false);
                else
                    Response.Redirect(UrlMaker.ToAdmin()); 
            }
            else
            {
                if (Tries >= 5)
                    Response.Redirect(UrlMaker.ToDefault());
                else
                {
                    Tries += 1;
                    this.LiteralError.Text = "Invalid Username or Password. Please try again.";
                }
            }
        }

        // counter for number of login attempts

        private int Tries
        {
            get { return int.Parse(ViewState["Tries"].ToString()); }
            set { ViewState["Tries"] = value; }
        }
    }
}