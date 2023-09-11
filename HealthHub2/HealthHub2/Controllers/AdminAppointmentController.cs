using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HealthHub2.Models;

namespace HealthHub2.Controllers
{
    public class AdminAppointmentController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AdminAppointment
        public ActionResult Index()
        {
            var appointment = db.Appointment.Include(a => a.GeoLocation);
            return View(appointment.ToList());
        }

        // GET: AdminAppointment/Details/5
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

        // GET: AdminAppointment/Create
        public ActionResult Create()
        {
            ViewBag.LocationId = new SelectList(db.GeoLocation, "LocationId", "PlaceName");
            return View();
        }

        // POST: AdminAppointment/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AppointmentId,PatientId,PatientName,DoctorId,LocationId,ServiceType,Date,Status,Note,Gender,ImageUrl,UploadDate")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                db.Appointment.Add(appointment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LocationId = new SelectList(db.GeoLocation, "LocationId", "PlaceName", appointment.LocationId);
            return View(appointment);
        }

        // GET: AdminAppointment/Edit/5
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
            ViewBag.LocationId = new SelectList(db.GeoLocation, "LocationId", "PlaceName", appointment.LocationId);
            return View(appointment);
        }

        // POST: AdminAppointment/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AppointmentId,PatientId,PatientName,DoctorId,LocationId,ServiceType,Date,Status,Note,Gender,ImageUrl,UploadDate")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(appointment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LocationId = new SelectList(db.GeoLocation, "LocationId", "PlaceName", appointment.LocationId);
            return View(appointment);
        }

        // GET: AdminAppointment/Delete/5
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

        // POST: AdminAppointment/Delete/5
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
