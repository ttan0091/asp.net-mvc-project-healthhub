using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
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
    [Authorize(Roles = "doctor")]
    public class DoctorController : Controller 
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Doctor
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CheckMriAppointments(string sortOrder, int? page)
        {
            string currentUserId = User.Identity.GetUserId(); // 获取当前登录用户的ID

            // 从数据库获取满足条件（医生ID等于当前用户ID）的预约记录
            var appointments = db.Appointment
                                .Where(a => a.DoctorId == currentUserId) // 注意这里是根据医生ID过滤
                                .Include(a => a.GeoLocation) // 根据需要包含其他相关表
                                .AsQueryable();

            // 根据排序参数排序
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

            ViewBag.CurrentSort = sortOrder; // 保存当前的排序参数

            int pageSize = 5;
            int pageNumber = (page ?? 1);

            // 创建一个新的 ViewModel 列表
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
                ClinicName = a.GeoLocation.PlaceName,
                AppointmentId = a.AppointmentId
            }).ToPagedList(pageNumber, pageSize);

            return View(combinedAppointments); // 注意这里使用的是 PartialView
        }


        [HttpPost]
        public JsonResult UpdateStatus(int id)
        {
            // 找到相应的预约
            var appointment = db.Appointment.Find(id);

            if (appointment != null)
            {
                // 更新状态
                appointment.Status = appointment.Status == "unchecked" ? "checked" : "unchecked";

                // 保存更改到数据库
                db.SaveChanges();

                return Json(new { success = true, newStatus = appointment.Status });
            }

            return Json(new { success = false });
        }

        [HttpPost]
        public ActionResult UploadImage(HttpPostedFileBase image, int id)
        {
            try
            {
                // 1. 验证文件和ID
                if (image == null || id <= 0)
                {
                    return Json(new { success = false, message = "Invalid file or ID" });
                }

                // 2. 设置保存路径
                string folderPath = Server.MapPath("~/assets/images/MriImage/");
                string fileName = $"image_{id}_{DateTime.UtcNow.Ticks}.jpg"; 
                string fullPath = Path.Combine(folderPath, fileName);

                // 3. 保存文件
                image.SaveAs(fullPath);

                // 4. 更新数据库（根据你的实现可能有所不同）
                var appointment = db.Appointment.Find(id);
                if (appointment != null)
                {
                    // 设置新的图片URL
                    string relativePath = $"~/assets/images/MriImage/{fileName}";
                    appointment.ImageUrl = relativePath;

                    DateTime currentDate = DateTime.UtcNow.Date;
                    appointment.UploadDate = currentDate;

                    db.Entry(appointment).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    return Json(new { success = false, message = "Appointment not found" });
                }

                // 5. 返回成功信息
                return Json(new { success = true, message = "File uploaded successfully", path = fullPath });
            }
            catch (Exception ex)
            {
                // 错误处理
                return Json(new { success = false, message = ex.Message });
            }
        }

    }
}