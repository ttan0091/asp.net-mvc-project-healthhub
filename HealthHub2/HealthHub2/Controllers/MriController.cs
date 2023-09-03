﻿using HealthHub2.Context;
using HealthHub2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HealthHub2.Controllers
{
    public class MriController : Controller
    {
        private HealthHubContext db = new HealthHubContext();
        // GET: Mri
        public ActionResult Index(string viewName)
        {
            ViewBag.ViewName = viewName ?? "BookMriService"; // 默认为 "BookMriService"
       

            if (Session["Status"] != null && Session["Status"].ToString() == "patient") // 检查用户是否登录
            {
                return View();
            }
            else
            {
                // 如果用户没有登录或者状态不是 "patient"，重定向到 Login 方法
                return RedirectToAction("Login", "Patient");
            }

            
            
        }

        public ActionResult BookMriService()
        {
            // 获取医院数据
            var hospitals = db.GeoLocation.ToList(); // 从数据库中获取所有地理位置信息

            // 将数据传递给视图
            ViewBag.Hospitals = hospitals;
            return PartialView();
        }

        //[HttpPost]
        //public ActionResult BookMriService(string clinicLocation)
        //{


        //    //return Content(clinicLocation);
        //    return View();
        //}


        [HttpPost]
        public ActionResult MriServiceTable(string clinicLocation)
        {
            try
            {
                var doctors = db.Doctor.ToList();

                // 根据 clinicLocation 从数据库中获取医院地址
                var hospital = db.GeoLocation.Find(Convert.ToInt32(clinicLocation));
                if (hospital != null)
                {
                    ViewBag.HospitalAddress = hospital.PlaceName;
                }
                else
                {
                    // Log: Hospital not found for given location
                    return Content("No Hospital found"); ; // You can direct to an error page
                }

                return View(doctors);
            }
            catch (Exception ex)
            {
                // Log: ex.Message
                return Content("Error");
            }
        }


        [HttpPost]
        public ActionResult SubmitAppointment(
          string Gender,
          int HospitalAddress,
          string MRI,
          DateTime Date,
          int DoctorName,
          string NoteToDoctor)
        {
            try
            {
                // 创建一个新的 Appointment 对象
                Appointment appointment = new Appointment();

                // 设置 Appointment 的属性
                appointment.PatientId = Convert.ToInt32(Session["PatientId"]);
                appointment.DoctorId = DoctorName;
                appointment.ServiceType = MRI;
                appointment.Date = Date;
                appointment.Status = "unchecked";
                appointment.Note = NoteToDoctor;
                appointment.LocationId = HospitalAddress;
                appointment.Gender = Gender;

                db.Appointment.Add(appointment);
                db.SaveChanges();

                TempData["AppointmentData"] = appointment;
                return RedirectToAction("AppointmentDetail", "Mri");

            }
            catch (Exception ex)
            {
                ViewBag.Error = "An error occurred while submitting the appointment.";
                return View("Error"); 
            }
        }

        public ActionResult AppointmentDetail()
        {
            try
            {
                // 获取 TempData 中的 Appointment 对象
                Appointment appointment = TempData["AppointmentData"] as Appointment;

                if (appointment != null)
                {
                    // 根据 PatientId 查询 Patient 表获取全名和邮箱
                    var patient = db.Patient.Find(appointment.PatientId);
                    string patientFullName = patient.FirstName + " " + patient.LastName;
                    string patientEmail = patient.Email;

                    // 根据 DoctorId 查询 Doctor 表获取全名和邮箱
                    var doctor = db.Doctor.Find(appointment.DoctorId);
                    string doctorFullName = doctor.FirstName + " " + doctor.LastName;
                    string doctorEmail = doctor.Email;

                    // 根据 LocationId 查询 GeoLocation 表获取 PlaceName
                    var geoLocation = db.GeoLocation.Find(appointment.LocationId);
                    string placeName = geoLocation.PlaceName;

                    // 使用 ViewBag 或 ViewData 或 ViewModel 传递信息到视图
                    ViewBag.PatientFullName = patientFullName;
                    ViewBag.PatientEmail = patientEmail;
                    ViewBag.DoctorFullName = doctorFullName;
                    ViewBag.DoctorEmail = doctorEmail;  
                    ViewBag.PlaceName = placeName;
                    ViewBag.Appointment = appointment;

                    return View();
                }
                else
                {
                    return View("Error");
                }
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }



        [HttpPost]
        public ActionResult BookConsultation(string clinicLocation)
        {
            ViewBag.ClinicLocation = clinicLocation;

            return PartialView();
        }

        public ActionResult CheckImages()
        {
            return PartialView();
        }

        public ActionResult CheckConsultation()
        {
            return PartialView();
        }

        public ActionResult CheckBills()
        {
            return PartialView();
        }

        public ActionResult SendEmail()
        {
            return PartialView();
        }

        public ActionResult RateDoctor()
        {
            return PartialView();
        }
    }
}