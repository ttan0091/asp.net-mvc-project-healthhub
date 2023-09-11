using HealthHub2.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;


namespace HealthHub2.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> ManageDoctors()
        {

            // 使用 RoleManager 获取 doctor 角色的 Id
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var role = await roleManager.FindByNameAsync("doctor");
            if (role == null)
            {
                // 如果没有找到 "doctor" 角色，请先创建该角色
                return View("Error"); // 或者其他逻辑
            }

            // 使用 UserManager 获取所有具有 doctor 角色的用户
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var doctors = await userManager.Users.Where(u => u.Roles.Any(r => r.RoleId == role.Id)).ToListAsync();

            // 将医生列表传递给视图
            return View(doctors);
        }

        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(string id)
        //{
        //    ApplicationUser doctor = db.ApplicationUsers.Find(id);
        //    db.ApplicationUsers.Remove(doctor);
        //    db.SaveChanges();
        //    return RedirectToAction("ManageDoctors");
        //}

    }
}