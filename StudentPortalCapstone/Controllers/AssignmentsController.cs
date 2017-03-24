using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StudentPortalCapstone.Models;

namespace StudentPortalCapstone.Controllers
{
    public class AssignmentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Assignments
        public ActionResult Index()
        {
            var userEmail = User.Identity.Name;

            if (userEmail == "") { return View(); }
            var person = db.Peoples.Single(a => a.Email == userEmail);
            //return Content("This is my Role: " + person.Role);
            if (person.Role == "Teacher")
            {
                return View();
            }

            return View("IndexStudent");
        }
        public ActionResult UploadRequest()
        {
            return View("UploadRequest");
        }
        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            string path = Server.MapPath("~/Assignments/" + FileNameSaver.FileName);
            file.SaveAs(path); // saving file

            return RedirectToAction("FindAssignmentForClass");
            
        }

        // GET: Assignments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assignments assignments = db.Assignments.Find(id);
            if (assignments == null)
            {
                return HttpNotFound();
            }
            return View(assignments);
        }

        // GET: Assignments/Create
        public ActionResult Create()
        {
            ViewBag.RosterId = new SelectList(db.Rosters, "Id", "ClassName");
            return View();
        }

        public ActionResult FindAssignmentForClass()
        {
            ViewBag.RosterId = new SelectList(db.Rosters, "Id", "ClassName");
            return View();
        }

        public ActionResult FindAssignStudent()
        {
            ViewBag.RosterId = new SelectList(db.Rosters, "Id", "ClassName");
            return View("FindAssignStudent");
        }

        public ActionResult DisplayAssignments(Assignments course)
        {
            var assignmentForClass = db.Assignments.Include(y => y.Roster).Where(y => y.Roster.Id == course.RosterId).ToList();
            var userEmail = User.Identity.Name;
  
            var person = db.Peoples.Single(a => a.Email == userEmail);
            //return Content("This is my Role: " + person.Role);
            if (person.Role == "Teacher")
            {
                return View(assignmentForClass);
            }

            return View("DisplayAssignStudent", assignmentForClass);

        }

        

        // POST: Assignments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,AssignmentName,RosterId,MaxPts")] Assignments assignments)
        {
            if (ModelState.IsValid)
            {
                db.Assignments.Add(assignments);
                db.SaveChanges();
                FileNameSaver.FileName = assignments.AssignmentName;
                return RedirectToAction("UploadRequest");
            }

            ViewBag.RosterId = new SelectList(db.Rosters, "Id", "ClassName", assignments.RosterId);
            return View(assignments);
        }

        // GET: Assignments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assignments assignments = db.Assignments.Find(id);
            if (assignments == null)
            {
                return HttpNotFound();
            }
            ViewBag.RosterId = new SelectList(db.Rosters, "Id", "ClassName", assignments.RosterId);
            return View(assignments);
        }

        // POST: Assignments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,AssignmentName,RosterId,MaxPts")] Assignments assignments)
        {
            if (ModelState.IsValid)
            {
                db.Entry(assignments).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RosterId = new SelectList(db.Rosters, "Id", "ClassName", assignments.RosterId);
            return View(assignments);
        }

        // GET: Assignments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assignments assignments = db.Assignments.Find(id);
            if (assignments == null)
            {
                return HttpNotFound();
            }
            return View(assignments);
        }

        // POST: Assignments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Assignments assignments = db.Assignments.Find(id);
            db.Assignments.Remove(assignments);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
