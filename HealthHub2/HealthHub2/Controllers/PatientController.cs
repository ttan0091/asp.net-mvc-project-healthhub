using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using HealthHub2.Context;
using HealthHub2.Models;
using HealthHub2.Utility;
//using Microsoft.Owin.Security;
//using Microsoft.Owin.Security.Cookies;
//using Microsoft.Owin;
//using Microsoft.Owin.Security.Google;
//using Owin;
//using Microsoft.Owin.Security.OAuth;
//using Microsoft.Owin.Host.SystemWeb;

namespace HealthHub2.Controllers
{
    public class PatientController : Controller
    {
        private HealthHubContext db = new HealthHubContext();

        // GET: Patient
        public ActionResult Index()
        {
            return View(db.Patient.ToList());
        }

        // GET: Patient/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null) 
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patient.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        // GET: Patient/Create
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PatientId,FirstName,LastName,Password,Email,Address,Status")] Patient patient)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // 使用PasswordHelper来加密密码
                    string hashedPassword = PasswordHelper.HashPassword(patient.Password);
                    patient.Password = hashedPassword;

                    db.Patient.Add(patient);
                    db.SaveChanges();  
                    TempData["SuccessMessage"] = "Register Succeed！";
                    return RedirectToAction("Login", "Patient");
                }
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException e)
            {
                
                ModelState.AddModelError("Email", "This Email has already been registered");
            }
            return View(patient);
        }








        // GET: Patient/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patient.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PatientId,FirstName,LastName,Password,Email,Address")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                db.Entry(patient).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(patient);
        }

        // GET: Patient/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patient.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        // POST: Patient/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Patient patient = db.Patient.Find(id);
            db.Patient.Remove(patient);
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

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Patient patient)
        {

            string hashedPassword = PasswordHelper.HashPassword(patient.Password);

            var user = db.Patient.FirstOrDefault(u => u.Email == patient.Email && u.Password == hashedPassword);


            if (user != null)
            {
                Session["PatientId"] = user.PatientId.ToString();
                Session["FirstName"] = user.FirstName;
                Session["Email"] = user.Email.ToString();
                Session["LastName"] = user.LastName;
                Session["Address"] = user.Address;
                Session["Status"] = user.Status;

                TempData["SuccessMessage"] = "Login Succeed！";

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Email or Password is wrong.");
                return View(patient);
            }
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login", "Patient");
        }

        public ActionResult EditProfile()
        {
            var patientId = Session["PatientId"];
            if (patientId == null)
            {
                return RedirectToAction("Login");
            }

            var patient = db.Patient.Find(Convert.ToInt32(patientId));
            if (patient == null)
            {
                return HttpNotFound();
            }

            // 将找到的 Patient 对象传递给 View
            return View(patient);
        }

        [HttpPost]
        public ActionResult EditProfile(Patient patient)
        {
            if (ModelState.IsValid)
            {
                var existingPatient = db.Patient.Find(patient.PatientId);
                if (existingPatient != null)
                {
                    existingPatient.FirstName = patient.FirstName;
                    existingPatient.LastName = patient.LastName;
                    existingPatient.Address = patient.Address;
                    existingPatient.Address = patient.Status;
                    Session["FirstName"] = existingPatient.FirstName;
                    Session["LastName"] = existingPatient.LastName;
                    Session["Address"] = existingPatient.Address;
                    Session["Status"] = existingPatient.Status;
                    try
                    {
                        db.SaveChanges();
                        TempData["UpdateSuccessMessage"] = "Profile updated successfully!";
                    }
                    catch (Exception ex)
                    {
                        // Handle the exception (log it, show an error message, etc.)
                        TempData["UpdateErrorMessage"] = "An error occurred: " + ex.Message;
                    }
                }
                else
                {
                    TempData["UpdateErrorMessage"] = "No matching patient found.";
                }
            }
            else
            {
                TempData["UpdateErrorMessage"] = "Model validation failed.";
            }

            return View(patient);
        }

















        //// 这个方法用于重定向用户到 Google 的登录页
        //public ActionResult ExternalLogin(string provider)
        //{
        //    return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Patient"));
        //}


        ////private IAuthenticationManager AuthenticationManager
        ////{
        ////    get { return HttpContext.GetOwinContext().Authentication; }
        ////}
        //// 这个方法是 Google 登录成功后的回调
        //public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        //{
        //    // 你可以在这里获取来自 Google 的用户信息
        //    // 然后，你可以检查数据库中是否有匹配的用户，并进行相应的操作（创建新用户，或者更新现有用户等）

        //    // 这里是一个简单示例：
        //    var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
        //    if (loginInfo == null)
        //    {
        //        return RedirectToAction("Login");
        //    }

        //    // 以下为模拟代码，你需要根据你的数据库模型进行适当的更改
        //    var user = db.Patient.FirstOrDefault(u => u.Email == loginInfo.Email);
        //    if (user != null)
        //    {
        //        // 用户存在，登录用户
        //        // （这里应该设置相应的 Session 等）
        //        Session["PatientId"] = user.PatientId.ToString();
        //        Session["FirstName"] = user.FirstName;
        //        // ... 其他 Session 设置
        //        return RedirectToAction("Index", "Home");
        //    }
        //    else
        //    {
        //        // 用户不存在，创建新用户
        //        // （这里应该保存用户到数据库，并设置相应的 Session 等）
        //        return RedirectToAction("Create", "Patient");
        //    }
        //}

        //private IAuthenticationManager AuthenticationManager
        //{
        //    get { return HttpContext.GetOwinContext().Authentication; }
        //}

        //private class ChallengeResult : HttpUnauthorizedResult
        //{
        //    public ChallengeResult(string provider, string redirectUri)
        //    {
        //        LoginProvider = provider;
        //        RedirectUri = redirectUri;
        //    }

        //    public string LoginProvider { get; set; }
        //    public string RedirectUri { get; set; }

        //    public override void ExecuteResult(ControllerContext context)
        //    {
        //        var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
        //        context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
        //    }
        //}





    }
}
