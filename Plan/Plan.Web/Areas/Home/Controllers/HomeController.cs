using Plan.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using db = Plan.Domain.PlanContext;

namespace Plan.Web.Areas.Home.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/Home/

        public ActionResult Index()
        {
            ViewBag.Count = db.Users.Count();
            return View();
        }
    }
}
