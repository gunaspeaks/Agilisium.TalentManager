using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Agilisium.TalentManager.Web.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        [OutputCache(CacheProfile ="AgilisigumTalentMgrCache")]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Index1()
        {
            return View();
        }
    }
}
