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
using HealthHub2.Utility;

namespace HealthHub2.Controllers
{
    public class AdminController : Controller
    {
        private HealthHubContext db = new HealthHubContext();

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Admin admin)
        {
            string hashedPassword = PasswordHelper.HashPassword(admin.Password);
            var user = db.Admin.FirstOrDefault(u => u.Email == admin.Email && u.Password == hashedPassword);

            if (user != null)
            {
                Session.Clear();
                // set session variables
                Session["AdminId"] = user.AdminId.ToString();
                Session["Email"] = user.Email.ToString();

                TempData["SuccessMessage"] = "Login Succeed！";
                return RedirectToAction("Index", "Doctor");
            }
            else
            {
                ModelState.AddModelError("", "Email or Password is wrong.");
                return View(admin);
            }
        }

        // GET: Admin
        //public ActionResult Index()
        //{
        //    return View(db.Admin.ToList());
        //}

        //// GET: Admin/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Admin admin = db.Admin.Find(id);
        //    if (admin == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(admin);
        //}

        //// GET: Admin/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Admin/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "AdminId,Password,Email")] Admin admin)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // 使用PasswordHelper来加密密码
        //        string hashedPassword = PasswordHelper.HashPassword(admin.Password);
        //        admin.Password = hashedPassword;

        //        db.Admin.Add(admin);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(admin);
        //}

        //// GET: Admin/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Admin admin = db.Admin.Find(id);
        //    if (admin == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(admin);
        //}

        //// POST: Admin/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "AdminId,Password,Email")] Admin admin)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(admin).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(admin);
        //}

        //// GET: Admin/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Admin admin = db.Admin.Find(id);
        //    if (admin == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(admin);
        //}

        //// POST: Admin/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Admin admin = db.Admin.Find(id);
        //    db.Admin.Remove(admin);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
