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

namespace HealthHub2.Controllers
{
    public class AppointmentController : Controller
    {
        private HealthHubContext db = new HealthHubContext();

        // GET: Appointment
        public ActionResult Index()
        {
            var appointment = db.Appointment.Include(a => a.Doctor).Include(a => a.GeoLocation).Include(a => a.Patient);
            return View(appointment.ToList());
        }

        // GET: Appointment/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointment.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        // GET: Appointment/Create
        public ActionResult Create()
        {
            ViewBag.DoctorId = new SelectList(db.Doctor, "DoctorId", "FirstName");
            ViewBag.LocationId = new SelectList(db.GeoLocation, "LocationId", "PlaceName");
            ViewBag.PatientId = new SelectList(db.Patient, "PatientId", "FirstName");
            return View();
        }

        // POST: Appointment/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AppointmentId,PatientId,DoctorId,LocationId,ServiceType,Date,Status,Note,Gender,ImageUrl,UploadDate")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                db.Appointment.Add(appointment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DoctorId = new SelectList(db.Doctor, "DoctorId", "FirstName", appointment.DoctorId);
            ViewBag.LocationId = new SelectList(db.GeoLocation, "LocationId", "PlaceName", appointment.LocationId);
            ViewBag.PatientId = new SelectList(db.Patient, "PatientId", "FirstName", appointment.PatientId);
            return View(appointment);
        }

        // GET: Appointment/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointment.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            ViewBag.DoctorId = new SelectList(db.Doctor, "DoctorId", "FirstName", appointment.DoctorId);
            ViewBag.LocationId = new SelectList(db.GeoLocation, "LocationId", "PlaceName", appointment.LocationId);
            ViewBag.PatientId = new SelectList(db.Patient, "PatientId", "FirstName", appointment.PatientId);
            return View(appointment);
        }

        // POST: Appointment/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AppointmentId,PatientId,DoctorId,LocationId,ServiceType,Date,Status,Note,Gender,ImageUrl,UploadDate")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(appointment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DoctorId = new SelectList(db.Doctor, "DoctorId", "FirstName", appointment.DoctorId);
            ViewBag.LocationId = new SelectList(db.GeoLocation, "LocationId", "PlaceName", appointment.LocationId);
            ViewBag.PatientId = new SelectList(db.Patient, "PatientId", "FirstName", appointment.PatientId);
            return View(appointment);
        }

        // GET: Appointment/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointment.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        // POST: Appointment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Appointment appointment = db.Appointment.Find(id);
            db.Appointment.Remove(appointment);
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
