using Capstone.Web.DAL;
using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Capstone.Web.Controllers
{
    public class VirtualTrailsController : Controller
    {
        IParkDAL dal;

        public VirtualTrailsController(IParkDAL dal)
        {
            this.dal = dal;
        }

        public ActionResult Index()
        {
            return RedirectToAction("ChoosePark");
        }

        public ActionResult ChoosePark()
        {
            List<ParkModel> parks = dal.GetAllParks();
            return View("ChoosePark", parks);
        }

        public ActionResult ChooseTrail(string id)
        {
            ParkModel park = dal.GetParkByParkName(id);

            return View("ChooseTrail", park);
        }

        public ActionResult ViewTrail(int id)
        {
            ParkModel park = dal.GetParkById(id);
            return View("ViewTrail", park);
        }
    }
}