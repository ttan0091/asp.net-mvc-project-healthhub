using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HealthHub2.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

namespace HealthHub2.Controllers
{
    public class RatingController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Rating
        public ActionResult Index()
        {
            if (TempData["Message"]!= null)
            {
                ViewBag.Message = TempData["Message"];
            }
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var allUsers = userManager.Users.ToList();
            var doctorRates = new List<DoctorRateViewModel>();

            

            foreach (var user in allUsers)
            {
                if (userManager.IsInRole(user.Id, "doctor"))
                {
                    // 获取该医生的所有评分
                    var ratings = db.Rating.Where(r => r.DoctorId == user.Id).ToList();

                    // 计算平均评分
                    double averageRate = 0;
                    if (ratings.Count > 0)
                    {
                        averageRate = ratings.Average(r => r.Score);
                    }

                    doctorRates.Add(new DoctorRateViewModel
                    {
                        DoctorId = user.Id,
                        FullName = user.FirstName + " " + user.LastName,
                        Email = user.Email,
                        Score = Math.Round(averageRate, 1) // 保留两位小数
                    });
                }
            }

            // 判断当前登录用户的角色
            var currentUserId = User.Identity.GetUserId();
            var IsPatient = userManager.IsInRole(currentUserId, "patient");
            ViewBag.IsPatient = IsPatient;


            return View(doctorRates); // 此处传入doctorRates列表
        }


        // GET: Rating/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rating rating = db.Rating.Find(id);
            if (rating == null)
            {
                return HttpNotFound();
            }
            return View(rating);
        }








        // GET: Rating/Create
        public ActionResult Create(string email, string fullName,string DoctorId)
        {
            ViewBag.Email = email;
            ViewBag.FullName = fullName;
            ViewBag.DoctorId = DoctorId;


            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string DoctorId, int Rating)
        {
            if (ModelState.IsValid) // 检查模型状态是否有效
            {
                // 获取当前用户的ID
                string userId = User.Identity.GetUserId();

                // 创建新的Rating对象
                Rating newRating = new Rating
                {
                    PatientId = userId,
                    DoctorId = DoctorId,
                    Score = Rating,
                    RatingDate = DateTime.Now // 设置为当前日期和时间
                };

                // 将新的Rating对象添加到数据库中
                db.Rating.Add(newRating);

                // 保存更改
                db.SaveChanges();

                TempData["Message"] = "Your rating has been submitted successfully!";

                // 重定向到某个页面，例如Index页面
                return RedirectToAction("Index");
            }

            // 如果模型状态无效，返回到相同的视图（或其他视图）并显示错误
            return View();
        }

        // GET: Rating/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rating rating = db.Rating.Find(id);
            if (rating == null)
            {
                return HttpNotFound();
            }
            return View(rating);
        }

        // POST: Rating/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RatingId,PatientId,DoctorId,Score,RatingDate")] Rating rating)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rating).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rating);
        }

        // GET: Rating/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rating rating = db.Rating.Find(id);
            if (rating == null)
            {
                return HttpNotFound();
            }
            return View(rating);
        }

        // POST: Rating/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Rating rating = db.Rating.Find(id);
            db.Rating.Remove(rating);
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
