using HealthHub2.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HealthHub2.Controllers
{

    public class ChatHubController : Controller
    {

      
        public ActionResult ChatCommunity()
        {
            
            return View();
        }
    }
}