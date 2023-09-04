using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HealthHub2.Context;
using HealthHub2.Models;
using HealthHub2.Utility;

namespace HealthHub2.Controllers
{
    public class DoctorController : Controller
    {
        private HealthHubContext db = new HealthHubContext();

        // GET: Doctor
        public ActionResult Index()
        {
            if (Session["AdminId"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            return View(db.Doctor.ToList());
        }

        // GET: Doctor/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doctor doctor = db.Doctor.Find(id);
            if (doctor == null)
            {
                return HttpNotFound();
            }
            return View(doctor);
        }

        // GET: Doctor/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Doctor/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DoctorID,FirstName,LastName,Password,Email,Department,Status")] Doctor doctor)
        {
            if (Session["AdminId"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            if (ModelState.IsValid)
            {
                // 使用PasswordHelper来加密密码
                string hashedPassword = PasswordHelper.HashPassword(doctor.Password);
                doctor.Password = hashedPassword;

                db.Doctor.Add(doctor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(doctor);
        }

        // GET: Doctor/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["AdminId"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doctor doctor = db.Doctor.Find(id);
            if (doctor == null)
            {
                return HttpNotFound();
            }
            return View(doctor);
        }

        // POST: Doctor/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DoctorID,FirstName,LastName,Password,Email,Department,Status")] Doctor doctor)
        {
            if (Session["AdminId"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }

            if (ModelState.IsValid)
            {
                string hashedPassword = PasswordHelper.HashPassword(doctor.Password);
                doctor.Password = hashedPassword;

                db.Entry(doctor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(doctor);
        }

        // GET: Doctor/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["AdminId"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doctor doctor = db.Doctor.Find(id);
            if (doctor == null)
            {
                return HttpNotFound();
            }
            return View(doctor);
        }

        // POST: Doctor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["AdminId"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            Doctor doctor = db.Doctor.Find(id);
            db.Doctor.Remove(doctor);
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


        public ActionResult Login()
        {
              return View();
        }

        [HttpPost]
        public ActionResult Login(Doctor doctor)
        {
            string hashedPassword = PasswordHelper.HashPassword(doctor.Password);
            var user = db.Doctor.FirstOrDefault(u => u.Email == doctor.Email && u.Password == hashedPassword);

            if (user != null)
            {
                Session.Clear();
                // set session variables
                Session["DoctorId"] = user.DoctorId.ToString();
                Session["FirstName"] = user.FirstName;
                Session["Email"] = user.Email.ToString();
                Session["LastName"] = user.LastName;
                Session["Status"] = user.Status;

                TempData["SuccessMessage"] = "Login Succeed！";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Email or Password is wrong.");
                return View(doctor);
            }
        }



    }
}
