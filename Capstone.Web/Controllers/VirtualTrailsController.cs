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
        IParkDAL parkDAL;
        ITrailDAL trailDAL;
        IPanoramicDAL panoramicDAL;

        public VirtualTrailsController(IParkDAL parkDAL, ITrailDAL trailDAL, IPanoramicDAL panoramicDAL)
        {
            this.parkDAL = parkDAL;
            this.trailDAL = trailDAL;
            this.panoramicDAL = panoramicDAL;
        }

        public ActionResult Index()
        {
            return RedirectToAction("ChoosePark");
        }

        public ActionResult ChoosePark()
        {
            List<ParkModel> parks = parkDAL.GetAllParks();
            return View("ChoosePark", parks);
        }
        
        public ActionResult ChooseTrail(string id)
        {
            ParkModel park = parkDAL.GetParkByParkName(id);
            park.Trails = trailDAL.GetTrailsByParkName(id);

            return View("ChooseTrail", park);
        }
        
        public ActionResult ViewTrail(string trailName, int? panoramicId)
        {
            PanoramicModel image;

            if (panoramicId != null)
            {
                image = panoramicDAL.GetPanoramicById((int)panoramicId);
            }
            else if (trailName != null)
            {
                image = trailDAL.GetTrailByTrailName(trailName).TrailHead;
            }
            else
            {
                image = trailDAL.GetTrailByTrailName("Henry Church Rock Loop").TrailHead;
            }

            return View("ViewTrail", image);
        }
    }
}