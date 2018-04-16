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
            List<TrailModel> trails = trailDAL.GetAllTrails();
            List<PanoramicModel> panoramics = panoramicDAL.GetAllPanoramics();

            if (!trails.Select(trail => trail.Name).Contains(trailName) ||
                (panoramics.FirstOrDefault(panoramic => panoramic.PanoramicId == panoramicId) == null) && panoramicId != null)
            {
                return new HttpStatusCodeResult(404);
            } 

            if (panoramicId == null)
            {
                return RedirectToAction("ViewTrail", new { trailName = trailName, panoramicId = trails.First(trail => trail.Name == trailName).TrailHead.PanoramicId });
            }

            return View("ViewTrail");
        }
    }
}