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
    public class ReportCardsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ReportCards
        public ActionResult Index()
        {
            var reportCards = db.ReportCards.Include(r => r.Assignments).Include(r => r.User);
            return View(reportCards.ToList());
        }

        public ActionResult FindClassForAssignments()
        {
            ViewBag.RosterId = new SelectList(db.Rosters, "Id", "ClassName");
            return View();
        }

        public ActionResult FindClassToEditGrade()
        {
            ViewBag.RosterId = new SelectList(db.Rosters, "Id", "ClassName");
            return View();
        }

        public ActionResult DisplayAssignments(Assignments course)
        {
            var assignmentForClass = db.Assignments.Include(y => y.Roster).Where(y => y.Roster.Id == course.RosterId).ToList();

            ClassList.Students = db.Enrollments.Include(y => y.User).Where(y => y.RosterId == course.RosterId).ToList();
            return View(assignmentForClass);
        }

        public ActionResult DisplayAssignmentsForEdit(Assignments course)
        {
            var assignmentForClass = db.Assignments.Include(y => y.Roster).Where(y => y.Roster.Id == course.RosterId).ToList();

            ClassList.Students = db.Enrollments.Include(y => y.User).Where(y => y.RosterId == course.RosterId).ToList();
            return View(assignmentForClass);
        }

        public ActionResult ShowStudentsAssignments(int? id)
        {
            if (db.ReportCards.Include(y => y.User).Where(y => y.AssignmentsId == id).Count() > 0)
            {
                var peeps = db.ReportCards.Include(y => y.User).Include(y => y.Assignments).Where(y => y.AssignmentsId == id && y.HasBeenGraded == false).ToList();
                ClassList.Students.Clear();

                return View(peeps);
            }

            var reportCard = new ReportCard();

            foreach (var a in ClassList.Students)
            {
                reportCard.UserId = a.UserId;
                reportCard.AssignmentsId = Convert.ToInt32(id);
                reportCard.StudentPts = 0;
                reportCard.HasBeenGraded = false;
                db.ReportCards.Add(reportCard);
                db.SaveChanges();
            }
            ClassList.Students.Clear();

            var students = db.ReportCards.Include(y => y.User).Include(y => y.Assignments).Where(y => y.AssignmentsId == id);

            return View(students);
        }

        public ActionResult ShowStudentsAssignmentsForEdit(int? id)
        {
            if (db.ReportCards.Include(y => y.User).Where(y => y.AssignmentsId == id).Count() > 0)
            {
                var peeps = db.ReportCards.Include(y => y.User).Include(y => y.Assignments).Where(y => y.AssignmentsId == id).ToList();
                ClassList.Students.Clear();

                return View(peeps);
            }

            var reportCard = new ReportCard();

            foreach (var a in ClassList.Students)
            {
                reportCard.UserId = a.UserId;
                reportCard.AssignmentsId = Convert.ToInt32(id);
                reportCard.StudentPts = 0;
                reportCard.HasBeenGraded = false;
                db.ReportCards.Add(reportCard);
                db.SaveChanges();
            }
            ClassList.Students.Clear();

            var students = db.ReportCards.Include(y => y.User).Include(y => y.Assignments).Where(y => y.AssignmentsId == id);

            return View(students);
        }


        public ActionResult AddGrade(int? id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReportCard reportCard = db.ReportCards.Find(id);
            if (reportCard == null)
            {
                return HttpNotFound();
            }
            ViewBag.AssignmentsId = new SelectList(db.Assignments, "Id", "AssignmentName", reportCard.AssignmentsId);
            ViewBag.UserId = new SelectList(db.Peoples, "Id", "FirstName", reportCard.UserId);
            return View(reportCard);
        }

        public ActionResult SubmitGrade([Bind(Include = "Id,StudentPts,MaxPts,UserId,AssignmentsId")] ReportCard reportCard)
        {
            reportCard.HasBeenGraded = true;
            if (ModelState.IsValid)
            {
                db.Entry(reportCard).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("FindClassForAssignments");
            }
            ViewBag.AssignmentId = new SelectList(db.Assignments, "Id", "AssignmentName", reportCard.AssignmentsId);
            ViewBag.UserId = new SelectList(db.Peoples, "Id", "FirstName", reportCard.UserId);
            return RedirectToAction("FindClassForAssignments");
            //return View(reportCard);
        }

        // GET: ReportCards/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReportCard reportCard = db.ReportCards.Find(id);
            if (reportCard == null)
            {
                return HttpNotFound();
            }
            return View(reportCard);
        }

        // GET: ReportCards/Create
        public ActionResult Create()
        {
            ViewBag.AssignmentsId = new SelectList(db.Assignments, "Id", "AssignmentName");
            ViewBag.UserId = new SelectList(db.Peoples, "Id", "FirstName");
            return View();
        }

        // POST: ReportCards/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,StudentPts,UserId,AssignmentsId,HasBeenGraded")] ReportCard reportCard)
        {
            if (ModelState.IsValid)
            {
                db.ReportCards.Add(reportCard);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AssignmentsId = new SelectList(db.Assignments, "Id", "AssignmentName", reportCard.AssignmentsId);
            ViewBag.UserId = new SelectList(db.Peoples, "Id", "FirstName", reportCard.UserId);
            return View(reportCard);
        }

        // GET: ReportCards/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReportCard reportCard = db.ReportCards.Find(id);
            if (reportCard == null)
            {
                return HttpNotFound();
            }
            ViewBag.AssignmentsId = new SelectList(db.Assignments, "Id", "AssignmentName", reportCard.AssignmentsId);
            ViewBag.UserId = new SelectList(db.Peoples, "Id", "FirstName", reportCard.UserId);
            return View(reportCard);
        }

        // POST: ReportCards/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,StudentPts,UserId,AssignmentsId,HasBeenGraded")] ReportCard reportCard)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reportCard).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AssignmentsId = new SelectList(db.Assignments, "Id", "AssignmentName", reportCard.AssignmentsId);
            ViewBag.UserId = new SelectList(db.Peoples, "Id", "FirstName", reportCard.UserId);
            return View(reportCard);
        }

        // GET: ReportCards/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReportCard reportCard = db.ReportCards.Find(id);
            if (reportCard == null)
            {
                return HttpNotFound();
            }
            return View(reportCard);
        }

        // POST: ReportCards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ReportCard reportCard = db.ReportCards.Find(id);
            db.ReportCards.Remove(reportCard);
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
