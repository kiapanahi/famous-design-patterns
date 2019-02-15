using ActionService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebForms.Code;

namespace WebForms.WebAuth
{
    public partial class Logout : PageBase
    {
        // performs logout operation.
        
        protected void Page_Load(object sender, EventArgs e)
        {
            var service = new Service();
            service.Logout();
        }
    }
}