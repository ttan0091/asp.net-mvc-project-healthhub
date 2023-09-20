using HealthHub2.Models;
using HealthHub2.Utils;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace HealthHub2.Controllers
{
    public class SendEmailController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            if (TempData["Result"] != null)
            {
                ViewBag.Result = TempData["Result"];
            }

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var allUsers = userManager.Users.ToList();

            // 判断当前登录用户的角色
            var currentUserId = User.Identity.GetUserId();
            var isDoctor = userManager.IsInRole(currentUserId, "doctor");
    
            if(isDoctor)
            {
                var patients = new List<DoctorViewModel>(); 
                foreach (var user in allUsers)
                {
                    if (userManager.IsInRole(user.Id, "patient"))
                    {
                        patients.Add(new DoctorViewModel
                        {
                            FullName = "Unknown",
                            Email = user.Email
                        });
                    }
                }
                return View( patients); 
            }
            else
            {
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
                return View( doctors);
            }
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


        public async Task<ActionResult> FinishSendEmail(SendEmailViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    String toEmail = model.ToEmail;
                    String subject = model.Subject;
                    String contents = model.Contents;
                    EmailSender es = new EmailSender();

                    if (model.Attachment != null && model.Attachment.ContentLength > 0)
                    {
                        Stream stream = model.Attachment.InputStream;
                        string fileName = Path.GetFileName(model.Attachment.FileName);
                        await es.SendWithAttachment(toEmail, subject, contents, stream, fileName);
                    }
                    else
                    {
                        es.Send(toEmail, subject, contents);
                    }

                    TempData["Result"] = "Email has been sent.";
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



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BulkSendEmail(string bulkEmails)
        {
            
            string[] emailArray = bulkEmails.Split(',');

            string emailList = string.Join(", ", emailArray);

            ViewBag.ToEmail = emailList;
         

            // 返回群发邮件的视图
            return View(new SendEmailViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> FinishBulkSendEmail(SendEmailViewModel model)
        {
            try
            {
                // 分解ToEmail字符串，将其转换为电子邮件地址数组
                String subject = model.Subject;
                String contents = model.Contents;
                string emailAddresses = Request.Form["emailAddresses"];
                string[] emailArray = emailAddresses.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(email => email.Trim())
                    .ToArray();
           

                EmailSender es = new EmailSender();

                // 检查是否有附件
                if (model.Attachment != null && model.Attachment.ContentLength > 0)
                {
                    // 将附件转换为字节流或存储为文件
                    Stream stream = model.Attachment.InputStream;
                    string fileName = Path.GetFileName(model.Attachment.FileName);

                    foreach (var toEmail in emailArray)
                    {
                        stream.Position = 0;
                        await es.SendWithAttachment(toEmail.Trim(), subject, contents, stream, fileName);
                    }
                }
                else
                {
                    foreach (var toEmail in emailArray)
                    {
                        es.Send(toEmail.Trim(), subject, contents);
                    }
                }

                TempData["Result"] = "Emails have been sent.";

                return RedirectToAction("Index"); // 重定向到群发电子邮件的视图
            }
            catch (Exception ex)
            {
                TempData["Result"] = "An error occurred: " + ex.Message; // 存储异常信息
                return Content(ex.Message); // 返回异常信息
            }
        }


    }
}