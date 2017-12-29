using EdicolaMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EdicolaMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var dashboard = new List<RivistaModel>();

            return View(dashboard);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}