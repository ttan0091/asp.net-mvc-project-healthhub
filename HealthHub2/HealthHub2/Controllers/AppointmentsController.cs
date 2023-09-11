using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using System.Web.UI;
using HealthHub2.Models;
using Microsoft.AspNet.Identity;
using PagedList;
using PagedList.Mvc;
namespace HealthHub2.Controllers
{
    public class AppointmentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Appointments
        public ActionResult Index()
        {
            var appointment = db.Appointment.Include(a => a.GeoLocation);
            return View(appointment.ToList());
        }

        // GET: Appointments/Details/5
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

        // GET: Appointments/Create
        public ActionResult Create()
        {
            ViewBag.LocationId = new SelectList(db.GeoLocation, "LocationId", "PlaceName");
            return View();
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "AppointmentId,PatientId,PatientName,DoctorId,LocationId,ServiceType,Date,Status,Note,Gender,ImageUrl,UploadDate")] Appointment appointment)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Appointment.Add(appointment);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.LocationId = new SelectList(db.GeoLocation, "LocationId", "PlaceName", appointment.LocationId);
        //    return View(appointment);
        //}










        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            string PatientName,
          string Gender,
          int LocationId,
          string MRI,
          DateTime Date,
          string DoctorId,
          string NoteToDoctor)
        {
            try
            {

                //检查是否有在同一天、同一医生和同一医院的预约
                var existingAppointments = db.Appointment.Where(a => a.DoctorId == DoctorId
                                                                       && a.LocationId == LocationId
                                                                       && a.Date == Date).ToList();

                if (existingAppointments.Any())
                {
                    var doctor = db.Users.Find(DoctorId);
                    string firstname = doctor?.FirstName;
                    string lastname = doctor?.LastName;

                    string errorMessage = $"The doctor ({firstname + " " + lastname}) is already booked for this day ({Date.ToShortDateString()}) at this location. Please choose another day or doctor.";
                    TempData["Error"] = errorMessage;

                    return RedirectToAction("MriServiceTable", "Mri", new { clinicLocation = LocationId });
                }


                // 创建一个新的 Appointment 对象
                Appointment appointment = new Appointment();

                // 设置 Appointment 的属性
                appointment.PatientId = User.Identity.GetUserId();
                appointment.DoctorId = DoctorId;
                appointment.ServiceType = MRI;
                appointment.Date = Date;
                appointment.Status = "unchecked";
                appointment.Note = NoteToDoctor;
                appointment.LocationId = LocationId;
                appointment.Gender = Gender;
                appointment.PatientName = PatientName;

                db.Appointment.Add(appointment);
                db.SaveChanges();

                TempData["AppointmentData"] = appointment;

                return RedirectToAction("AppointmentDetails", "Appointments");

            }
            catch (Exception ex)
            {
                ViewBag.Error = "An error occurred while submitting the appointment.";
                return View("Error");
            }
        }

        public ActionResult AppointmentDetails()
        {
            var appointment = TempData["AppointmentData"] as Appointment;
            if (appointment == null)
            {
                return RedirectToAction("Index", "Home");
            }
            // 获取医生的 DoctorId
            string doctorId = appointment.DoctorId;

            // 查询医生的信息
            var doctor = db.Users.Find(doctorId);

            // 存储医生的 full name 到 ViewBag
            ViewBag.DoctorFullName = $"{doctor.FirstName} {doctor.LastName}";
            ViewBag.DoctorEmail = doctor.Email;
            ViewBag.Appointment= appointment;
            ViewBag.PlaceName = db.GeoLocation.Find(appointment.LocationId).PlaceName;
            return View();

        }


        public ActionResult CheckImages(string sortOrder, int? page)
        {
            string currentUserId = User.Identity.GetUserId();

            var appointments = db.Appointment
                                .Where(a => a.PatientId == currentUserId)
                                .Include(a => a.GeoLocation)
                                .AsQueryable();

            switch (sortOrder)
            {
                case "date_desc":
                    appointments = appointments.OrderByDescending(a => a.Date);
                    break;
                case "date_asc":
                    appointments = appointments.OrderBy(a => a.Date);
                    break;
                default:
                    appointments = appointments.OrderBy(a => a.Date);
                    break;
            }
            ViewBag.CurrentSort = sortOrder;

            int pageSize = 5;  
            int pageNumber = (page ?? 1); 

            // 创建一个新的列表，将医生名和其他信息绑定在一起
            var combinedAppointments = appointments.Select(a => new AppointmentViewModel
            {
                PatientName = a.PatientName, 
                ServiceType = a.ServiceType,
                Date = a.Date,
                Status = a.Status,
                Note = a.Note,
                Gender = a.Gender,
                ImageUrl = a.ImageUrl,
                UploadDate = a.UploadDate,
                DoctorName = db.Users.Where(u => u.Id == a.DoctorId).Select(u => u.FirstName + " " + u.LastName).FirstOrDefault(),
                ClinicName = a.GeoLocation.PlaceName 
            }).ToPagedList(pageNumber, pageSize);


            // 将新的列表传递到视图
            return View(combinedAppointments);
        }









        // GET: Appointments/Edit/5
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

        // POST: Appointments/Edit/5
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

        // GET: Appointments/Delete/5
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

        // POST: Appointments/Delete/5
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
