using Agilisium.TalentManager.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Agilisium.TalentManager.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [ChildActionOnly]
        public ActionResult EmployeesDashboard()
        {
            return PartialView(new EmployeeWidgetModel());
        }

        [ChildActionOnly]
        public ActionResult AllocationsDashboard()
        {
            return PartialView(new List<AllocationModel>());
        }
    }
}