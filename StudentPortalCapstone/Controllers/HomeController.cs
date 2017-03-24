using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudentPortalCapstone.Models;

namespace StudentPortalCapstone.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db;

        public HomeController()
        {
            db = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            var userEmail = User.Identity.Name;
            
            if (userEmail == "") { return View(); }
            var person = db.Peoples.Single(a => a.Email == userEmail);
            //return Content("This is my Role: " + person.Role);
            if (person.Role == "Teacher") { 
                return View ("Teacher");
            }

            return View();
        }

        public ActionResult Download()
        {
            Response.ContentType = "application/octect-stream";
            Response.AppendHeader("content-disposition", "filename=Olivia.rtf");
            Response.TransmitFile(Server.MapPath("~/Files/Olivia.rtf"));
            Response.End();
            return View();
        }

       

        public ActionResult Attendance()
        {
            var userEmail = User.Identity.Name;

            if (userEmail == "") { return View(); }
            var person = db.Peoples.Single(a => a.Email == userEmail);
            //return Content("This is my Role: " + person.Role);
            if (person.Role == "Teacher")
            {
                return View();
            }

            return View("AttendanceStudent");
            //ViewBag.Message = "Manage Attendance";

            //return View();
        }

        public ActionResult Assignments()
        {
            ViewBag.Message = "Manage Assignments";

            return View();
        }
    }
}