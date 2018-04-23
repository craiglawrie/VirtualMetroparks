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
        ILastSeenImagesDAL lastSeenImagesDAL;
        ILastSeenVideosDAL lastSeenVideosDAL;

        public VirtualTrailsController(IParkDAL parkDAL, ITrailDAL trailDAL, IPanoramicDAL panoramicDAL, ILastSeenImagesDAL lastSeenImagesDAL, ILastSeenVideosDAL lastSeenVideosDAL)
        {
            this.parkDAL = parkDAL;
            this.trailDAL = trailDAL;
            this.panoramicDAL = panoramicDAL;
            this.lastSeenImagesDAL = lastSeenImagesDAL;
            this.lastSeenVideosDAL = lastSeenVideosDAL;
        }

        public ActionResult Index()
        {
            return RedirectToAction("ChoosePark");
        }

        public ActionResult ChoosePark()
        {
            List<ParkModel> parks = parkDAL.GetAllParks();
            parks.ForEach(park => park.Image = parkDAL.GetImageByParkId(park.ParkId));
            return View("ChoosePark", parks);
        }
        
        public ActionResult ChooseTrail(string id)
        {
            ParkModel park = parkDAL.GetParkByParkName(id);
            park.Trails = trailDAL.GetTrailsByParkName(id);
            park.Trails.ForEach(trail => trail.Image = trailDAL.GetImageByTrailId(trail.TrailId));

            return View("ChooseTrail", park);
        }

        public ActionResult ViewTrail(string trailName, int? panoramicId)
        {
            List<TrailModel> trails = trailDAL.GetAllTrails();
            trails.ForEach(trail => trail.TrailHead = panoramicDAL.GetTrailHeadByTrailId(trail.TrailId));
            List<PanoramicModel> panoramics = panoramicDAL.GetAllPanoramics();
            List<LastSeenImagesModel> lastSeenImages = lastSeenImagesDAL.GetAllLastSeenImages();
            List<LastSeenVideosModel> lastSeenVideos = lastSeenVideosDAL.GetAllLastSeenVideos();
            LastSeenModel superModel = new LastSeenModel()
                {
                    Images = lastSeenImages,
                    Videos = lastSeenVideos
                };

            if (!trails.Select(trail => trail.Name).Contains(trailName) ||
                (panoramics.FirstOrDefault(panoramic => panoramic.PanoramicId == panoramicId) == null) && panoramicId != null)
            {
                return new HttpStatusCodeResult(404);
            }

            int selectedParkId = trails.First(trail => trail.Name == trailName).ParkId;
            ParkModel selectedPark = parkDAL.GetParkById(selectedParkId);

            int selectedTrailId = trails.First(trail => trail.Name == trailName).TrailId;
            List<string> selectedTrail = trailDAL.GetTrailDescriptionByTrailId(selectedTrailId);

            if (panoramicId == null)
            {
                return RedirectToAction("ViewTrail", new { trailName = trailName, panoramicId = trails.First(trail => trail.Name == trailName).TrailHead.PanoramicId });
            }
            ViewBag.TrailName = trailName;
            ViewBag.ParkName = selectedPark.Name;
            ViewBag.NameAndDescription = selectedTrail;

            return View("ViewTrail", superModel);
        }
    }
}