using Mvc.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mvc.Controllers
{
    // renders home page only

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Crumbs = new List<BreadCrumb>();
            ViewBag.Crumbs.Add(new BreadCrumb { Title = "home" });

            ViewBag.Menu = "home";

            return View();
        }
    }
}
