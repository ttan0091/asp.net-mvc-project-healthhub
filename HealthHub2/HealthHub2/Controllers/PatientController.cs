using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;
using HealthHub2.Context;
using HealthHub2.Models;
using HealthHub2.Utility;

namespace HealthHub2.Controllers
{
    public class PatientController : Controller
    {
        private HealthHubContext db = new HealthHubContext();

        // GET: Patient
        public ActionResult Index()
        {
            return View(db.Patient.ToList());
        }

        // GET: Patient/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null) 
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patient.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        // GET: Patient/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Patient/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PatientId,FirstName,LastName,Password,Email,Address,Status")] Patient patient)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // 使用PasswordHelper来加密密码
                    string hashedPassword = PasswordHelper.HashPassword(patient.Password);
                    patient.Password = hashedPassword;

                    db.Patient.Add(patient);
                    db.SaveChanges();  
                    TempData["SuccessMessage"] = "Register Succeed！";
                    return RedirectToAction("Login", "Patient");
                }
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException e)
            {
                
                ModelState.AddModelError("Email", "This Email has already been registered");
            }
            return View(patient);
        }








        // GET: Patient/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patient.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        // POST: Patient/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PatientId,FirstName,LastName,Password,Email,Address")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                db.Entry(patient).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(patient);
        }

        // GET: Patient/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patient.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        // POST: Patient/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Patient patient = db.Patient.Find(id);
            db.Patient.Remove(patient);
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
        public ActionResult Login(Patient patient)
        {
            string hashedPassword = PasswordHelper.HashPassword(patient.Password);
            var user = db.Patient.FirstOrDefault(u => u.Email == patient.Email && u.Password == hashedPassword);

            if (user != null)
            {
                // set session variables
                Session["PatientId"] = user.PatientId.ToString();
                Session["FirstName"] = user.FirstName;
                Session["Email"] = user.Email.ToString();
                Session["LastName"] = user.LastName;
                Session["Address"] = user.Address;
                Session["Status"] = user.Status;

                TempData["SuccessMessage"] = "Login Succeed！";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Email or Password is wrong.");
                return View(patient);
            }
          
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login", "Patient");
        }

        public ActionResult EditProfile()
        {
            var patientId = Session["PatientId"];
            if (patientId == null)
            {
                return RedirectToAction("Login");
            }

            var patient = db.Patient.Find(Convert.ToInt32(patientId));
            if (patient == null)
            {
                return HttpNotFound();
            }

            // 将找到的 Patient 对象传递给 View
            return View(patient);
        }

        [HttpPost]
        public ActionResult EditProfile(Patient patient)
        {
            if (ModelState.IsValid)
            {
                var existingPatient = db.Patient.Find(patient.PatientId);
                if (existingPatient != null)
                {
                    existingPatient.FirstName = patient.FirstName;
                    existingPatient.LastName = patient.LastName;
                    existingPatient.Address = patient.Address;
                    existingPatient.Address = patient.Status;
                    Session["FirstName"] = existingPatient.FirstName;
                    Session["LastName"] = existingPatient.LastName;
                    Session["Address"] = existingPatient.Address;
                    Session["Status"] = existingPatient.Status;
                    try
                    {
                        db.SaveChanges();
                        TempData["UpdateSuccessMessage"] = "Profile updated successfully!";
                    }
                    catch (Exception ex)
                    {
                        // Handle the exception (log it, show an error message, etc.)
                        TempData["UpdateErrorMessage"] = "An error occurred: " + ex.Message;
                    }
                }
                else
                {
                    TempData["UpdateErrorMessage"] = "No matching patient found.";
                }
            }
            else
            {
                TempData["UpdateErrorMessage"] = "Model validation failed.";
            }

            return View(patient);
        }

    }
}
