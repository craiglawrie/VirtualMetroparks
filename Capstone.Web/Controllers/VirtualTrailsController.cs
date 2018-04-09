using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Capstone.Web.Controllers
{
    public class VirtualTrailsController : Controller
    {
        // GET: VirtualTrails
        public ActionResult Index()
        {
            return RedirectToAction("ChoosePark");
        }

        public ActionResult ChoosePark()
        {
            return View();
        }
    }
}