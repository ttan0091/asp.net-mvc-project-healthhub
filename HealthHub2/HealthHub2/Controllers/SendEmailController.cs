using HealthHub2.Models;
using HealthHub2.Utils;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HealthHub2.Controllers
{
    public class SendEmailController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Send_Email
        public ActionResult Index()
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var allUsers = userManager.Users.ToList();
            var doctors = new List<DoctorViewModel>();

            foreach (var user in allUsers)
            {
                if (userManager.IsInRole(user.Id, "doctor"))
                {
                    doctors.Add(new DoctorViewModel
                    {
                        FullName = user.FirstName + " " + user.LastName,
                        Email = user.Email
                    });
                }
            }

            return View(doctors);
        }
        [HttpGet]
        public ActionResult SendEmail()
        {
            if (TempData.ContainsKey("Result"))
            {
                ViewBag.Result = TempData["Result"];
            }
            return View(new SendEmailViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SendEmail(string ToEmail) 
        {
            ViewBag.ToEmail = ToEmail;
            return View(new SendEmailViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FinishSendEmail(SendEmailViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    String toEmail = model.ToEmail;
                    String subject = model.Subject;
                    String contents = model.Contents;

                    EmailSender es = new EmailSender();
                    es.Send(toEmail, subject, contents);

                    TempData["Result"] = "Email has been send.";

                    ModelState.Clear();

                    return RedirectToAction("SendEmail");
                }
                catch
                {
                    return View();
                }
            }

            return View();
        }
    }
}