﻿using System;
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
    public class EnrollmentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Enrollments
        public ActionResult Index()
        {
            var enrollments = db.Enrollments.Include(e => e.Roster).Include(e => e.User);
            return View(enrollments.ToList());
        }

        public ActionResult Search()
        {
            return View();
        }

        public ActionResult FindPerson(User person)
        {

            string[] words = person.FirstName.Split(' ');
            var firstName = words[0];
            var personRecord = db.Peoples.Where(a => a.FirstName.Contains(firstName) || a.LastName.Contains(firstName)).ToList();
            //if (personRecord == null && words.Length > 1)
            //{
            //    var lastName = words[1];
            //    var personRecordFullName = db.Users.Where(a => a.FirstName.Contains(firstName) || a.LastName.Contains(lastName)).ToList();
            //    return View("FindPerson", personRecordFullName);
            //}
          
            return View("FindPerson",personRecord);

        }
        public ActionResult UploadPic()
        {
            ViewBag.RosterId = new SelectList(db.Rosters, "Id", "ClassName");
            return View();
        }

        public ActionResult UploadPicture(int? id)
        {
            var person = db.Peoples.Find(id);

            return View(person);
        }

        public ActionResult ShowList(Enrollment enrollment)
        {
            var classList = db.Enrollments.Include(a => a.Roster).Include(y => y.User).Where(y => y.RosterId == enrollment.RosterId);
            return View(classList);
        }

        public ActionResult UploadToProfile(HttpPostedFileBase file)
        {           
            string path = Server.MapPath("~/ProfilePics/" + file.FileName);
            file.SaveAs(path);
            return RedirectToAction("UploadPic");
        }

        // GET: Enrollments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollment enrollment = db.Enrollments.Find(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            return View(enrollment);
        }

        // GET: Enrollments/Create
        public ActionResult Create()
        {
            ViewBag.RosterId = new SelectList(db.Rosters, "Id", "ClassName");
            ViewBag.UserId = new SelectList(db.Peoples, "Id", "FirstName");
            return View();
        }

        // POST: Enrollments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,RosterId")] Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                db.Enrollments.Add(enrollment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RosterId = new SelectList(db.Rosters, "Id", "ClassName", enrollment.RosterId);
            ViewBag.UserId = new SelectList(db.Peoples, "Id", "FirstName", enrollment.UserId);
            return View(enrollment);
        }

        // GET: Enrollments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollment enrollment = db.Enrollments.Find(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            ViewBag.RosterId = new SelectList(db.Rosters, "Id", "ClassName", enrollment.RosterId);
            ViewBag.UserId = new SelectList(db.Peoples, "Id", "FirstName", enrollment.UserId);
            return View(enrollment);
        }

        // POST: Enrollments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,RosterId")] Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(enrollment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RosterId = new SelectList(db.Rosters, "Id", "ClassName", enrollment.RosterId);
            ViewBag.UserId = new SelectList(db.Peoples, "Id", "FirstName", enrollment.UserId);
            return View(enrollment);
        }

        // GET: Enrollments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollment enrollment = db.Enrollments.Find(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            return View(enrollment);
        }

        // POST: Enrollments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Enrollment enrollment = db.Enrollments.Find(id);
            db.Enrollments.Remove(enrollment);
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
