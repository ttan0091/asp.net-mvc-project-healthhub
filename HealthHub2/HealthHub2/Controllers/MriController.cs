using HealthHub2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;


namespace HealthHub2.Controllers
{
 
    public class MriController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Mri
        [Authorize (Roles = "patient")]
        public ActionResult Index(string viewName)
        {
            ViewBag.ViewName = viewName ?? "BookMriService"; // 默认为 "BookMriService"
            return View();
        }

        [Authorize(Roles = "patient")]
        public ActionResult BookMriService()
        {
            // 获取医院数据
            var hospitals = db.GeoLocation.ToList(); // 从数据库中获取所有地理位置信息

            // 将数据传递给视图
            ViewBag.Hospitals = hospitals;
            return PartialView();
        }

        [Authorize(Roles = "patient")]
        [HttpGet]
        //[HttpPost]
        public ActionResult MriServiceTable(int clinicLocation) 
        {
            if (TempData["Error"] != null)
            {
                ViewBag.Error = TempData["Error"];
            }
            try
            {
                // 从 OWIN 上下文中获取 RoleManager 和 UserManager
                var roleManager = HttpContext.GetOwinContext().Get<RoleManager<IdentityRole>>();
                var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

                // 获取所有用户
                var allUsers = userManager.Users.ToList();

                // 过滤出属于 "doctor" 角色的用户
                var doctors = allUsers.Where(u => userManager.IsInRole(u.Id, "doctor")).ToList();

                ViewBag.Doctors = doctors;

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

                return View();
            
            }
            catch (Exception ex)
            {
                // 处理异常
                return Content("Error：" + ex.Message);
            }
        }



    }
}