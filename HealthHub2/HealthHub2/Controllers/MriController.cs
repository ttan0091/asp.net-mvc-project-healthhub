using HealthHub2.Context;
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

        [HttpPost]
        public ActionResult BookMriService(string clinicLocation)
        {
            

            //return Content(clinicLocation);
            return View();
        }

        public ActionResult BookConsultation()
        {
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