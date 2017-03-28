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
    public class AttendancesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Attendances
        public ActionResult Index()
        {
            ViewBag.RosterId = new SelectList(db.Rosters, "Id", "ClassName");

            return View();
            //var attendance = db.Attendance.Include(a => a.Roster).Include(a => a.User);
            //return View(attendance.ToList());
        }

        public ActionResult GetClassList(Enrollment course)
        {
            AttendanceViewModel attendanceViewModel = new AttendanceViewModel();
            var attDB = db.Attendance.Include(y => y.User).Include(y => y.Roster).Where(y => y.RosterId == course.RosterId).Where(y => y.Date == DateTime.Today);

            if (attDB.Count() > 0)
            {
                attendanceViewModel.attendances = attDB.ToList();
                return View(attendanceViewModel);
            }

            var courseList = db.Enrollments.Where(y => y.RosterId == course.RosterId).ToList();
            Attendance att = new Attendance();

            if (courseList.Count == 0)
            {

                return Content("No students Enrolled in " + db.Rosters.Find(course.RosterId).ClassName);
            }

            foreach (var student in courseList)
            {
                att.Date = DateTime.Today;
                att.isPresent = false;
                att.RosterId = course.RosterId;
                att.UserId = student.UserId;
                db.Attendance.Add(att);
                db.SaveChanges();
            }

            attendanceViewModel.attendances =
                db.Attendance.Include(y => y.User)
                    .Include(y => y.Roster)
                    .Where(y => y.RosterId == course.RosterId)
                    .Where(y => y.Date == DateTime.Today)
                    .ToList();

            return View(attendanceViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SetAttendance(AttendanceViewModel model)
        {
            var student = new Attendance();
            //return Content(model.attendances[0].Id.ToString());
            foreach (var item in model.attendances)
            {
                //student = db.Attendance.Find(item.Id);
                //student.isPresent = item.isPresent;
                //db.Entry(item).State = EntityState.Modified;

                db.Attendance.Find(item.Id).isPresent = item.isPresent;
                db.SaveChanges();
            }
            model.attendances.Clear();
            //var attDB = db.Attendance.Include(y => y.User).Include(y => y.Roster).Where(y => y.RosterId == ).Where(y => y.Date == DateTime.Today);
            return RedirectToAction("Index");
        }

        public ActionResult CheckAttendance()
        {
            ViewBag.RosterId = new SelectList(db.Rosters, "Id", "ClassName");

            return View();
        }

        [HttpPost]

        public ActionResult GetAttendance(CheckAttendanceViewModel model)
        {
            var attendanceList = db.Attendance.Include(y => y.Roster).Include(y => y.User).Where(y => y.Date == model.Date).Where(y => y.RosterId == model.RosterId).ToList();
            DisplayAttendanceViewModel displayAttendanceVM = new DisplayAttendanceViewModel();
            displayAttendanceVM.Attendances = attendanceList;
            displayAttendanceVM.CourseName = db.Rosters.Find(model.RosterId).ClassName;
            displayAttendanceVM.Date = model.Date;
            return View(displayAttendanceVM);
        }

        public ActionResult FindAttendance()
        {
            return View();
        }


        public ActionResult GetAttendanceForStudent(User person)
        {
            var attendance = db.Attendance.Include(y => y.User).Include(y => y.Roster).Where(y => y.User.Email == person.FirstName).ToList();

            return View(attendance);
        }
        // GET: Attendances/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attendance attendance = db.Attendance.Find(id);
            if (attendance == null)
            {
                return HttpNotFound();
            }
            return View(attendance);
        }

        // GET: Attendances/Create
        public ActionResult Create()
        {
            ViewBag.RosterId = new SelectList(db.Rosters, "Id", "ClassName");
            ViewBag.UserId = new SelectList(db.Peoples, "Id", "FirstName");
            return View();
        }

        // POST: Attendances/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Date,UserId,RosterId,isPresent")] Attendance attendance)
        {
            if (ModelState.IsValid)
            {
                db.Attendance.Add(attendance);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RosterId = new SelectList(db.Rosters, "Id", "ClassName", attendance.RosterId);
            ViewBag.UserId = new SelectList(db.Peoples, "Id", "FirstName", attendance.UserId);
            return View(attendance);
        }

        // GET: Attendances/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attendance attendance = db.Attendance.Find(id);
            if (attendance == null)
            {
                return HttpNotFound();
            }
            ViewBag.RosterId = new SelectList(db.Rosters, "Id", "ClassName", attendance.RosterId);
            ViewBag.UserId = new SelectList(db.Peoples, "Id", "FirstName", attendance.UserId);
            return View(attendance);
        }

        // POST: Attendances/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Date,UserId,RosterId,isPresent")] Attendance attendance)
        {
            if (ModelState.IsValid)
            {
                db.Entry(attendance).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RosterId = new SelectList(db.Rosters, "Id", "ClassName", attendance.RosterId);
            ViewBag.UserId = new SelectList(db.Peoples, "Id", "FirstName", attendance.UserId);
            return View(attendance);
        }

        // GET: Attendances/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attendance attendance = db.Attendance.Find(id);
            if (attendance == null)
            {
                return HttpNotFound();
            }
            return View(attendance);
        }

        // POST: Attendances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Attendance attendance = db.Attendance.Find(id);
            db.Attendance.Remove(attendance);
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
