using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mvc.Code
{
    // sets selected menu item
    
    public class MenuAttribute : ActionFilterAttribute
    {
        MenuItem selectedMenu;

        public MenuAttribute(MenuItem selectedMenu)
        {
            this.selectedMenu = selectedMenu;
        }

        // sets selected menu in ViewData

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.Controller.ViewData["SelectedMenu"] = selectedMenu.ToString().ToLower();
        }
    }
}