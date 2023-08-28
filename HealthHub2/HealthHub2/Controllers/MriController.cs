using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HealthHub2.Controllers
{
    public class MriController : Controller
    {
        // GET: Mri
        public ActionResult Index(string viewName)
        {
            ViewBag.ViewName = viewName ?? "BookMriService"; // 默认为 "BookMriService"
            return View();
        }

        public ActionResult BookMriService()
        {
            return PartialView();
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