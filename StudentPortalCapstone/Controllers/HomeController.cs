using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentPortalCapstone.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Attendance()
        {
            ViewBag.Message = "Manage Attendance";

            return View();
        }

        public ActionResult Assignments()
        {
            ViewBag.Message = "Manage Assignments";

            return View();
        }
    }
}