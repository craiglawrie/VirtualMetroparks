using Capstone.Web.DAL;
using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Capstone.Web.Controllers
{
    public class ParkInfoController : ApiController
    {
        IParkDAL parkDAL;
        ITrailDAL trailDAL;
        IPanoramicDAL panoramicDAL;
        ILastSeenImagesDAL lastSeenImagesDAL;
        ILastSeenVideosDAL lastSeenVideosDAL;

        public ParkInfoController(IParkDAL parkDAL, ITrailDAL trailDAL, IPanoramicDAL panoramicDAL, ILastSeenImagesDAL lastSeenImagesDAL, ILastSeenVideosDAL lastSeenVideosDAL)
        {
            this.parkDAL = parkDAL;
            this.trailDAL = trailDAL;
            this.panoramicDAL = panoramicDAL;
            this.lastSeenImagesDAL = lastSeenImagesDAL;
            this.lastSeenVideosDAL = lastSeenVideosDAL;
        }

        [HttpGet]
        [Route("api/parks")]
        public IHttpActionResult GetParks()
        {
            List<ParkModel> parks = parkDAL.GetAllParks();

            return Ok(parks);
        }

        [HttpGet]
        [Route("api/park/{parkName}")]
        public IHttpActionResult GetParkByParkName(string parkName)
        {
            ParkModel park = parkDAL.GetParkByParkName(parkName);
            park.Trails = trailDAL.GetTrailsByParkName(parkName);
            park.UserVisitedPanoramics = new List<PanoramicModel>();
            park.Trails.ForEach(
                trail =>
                {
                    trail.TrailHead = panoramicDAL.GetTrailHeadByTrailId(trail.TrailId);
                    trail.PointsOfInterest = panoramicDAL.GetPointsOfInterestByTrailId(trail.TrailId);
                    trail.PanoramicsInTrail = panoramicDAL.GetPanoramicsByTrailId(trail.TrailId);
                    trail.PanoramicsInTrail.ForEach(
                        panoramic => panoramic.Connections = panoramicDAL.GetConnectionsByPanoramicId(panoramic.PanoramicId)
                    );

                    if (User.Identity.IsAuthenticated)
                    {
                        park.UserVisitedPanoramics.AddRange(panoramicDAL.GetVisitedPanoramicsByUsername(User.Identity.Name));
                    }
                }
            );


            return Ok(park);
        }

        [HttpGet]
        [Route("api/trail/{trailName}")]
        public IHttpActionResult GetTrailByTrailName(string trailName)
        {
            TrailModel trail = trailDAL.GetTrailByTrailName(trailName);
            trail.PanoramicsInTrail = panoramicDAL.GetPanoramicsByTrailName(trailName);
            trail.PanoramicsInTrail.ForEach(panoramic => panoramic.Connections = panoramicDAL.GetConnectionsByPanoramicId(panoramic.PanoramicId));
            trail.PanoramicsInTrail.ForEach(panoramic => panoramic.LastSeenImages = lastSeenImagesDAL.GetLastSeenImagesByPanoramicId(panoramic.PanoramicId));
            trail.PanoramicsInTrail.ForEach(panoramic => panoramic.LastSeenVideos = lastSeenVideosDAL.GetLastSeenVideosByPanoramicId(panoramic.PanoramicId));
            trail.PanoramicsInTrail.ForEach(panoramic => panoramic.BackgroundSoundClips = panoramicDAL.GetAllBackgroundSoundClips());
            trail.TrailHead = panoramicDAL.GetTrailHeadByTrailId(trail.TrailId);
            trail.TrailHead = trail.PanoramicsInTrail.First(panoramic => panoramic.PanoramicId == trail.TrailHead.PanoramicId);
            return Ok(trail);
        }

        [HttpGet]
        [Route("api/panoramic/{panoramicId}")]
        public IHttpActionResult GetPanoramicById(int panoramicId)
        {
            PanoramicModel panoramic = panoramicDAL.GetPanoramicById(panoramicId);
            panoramic.BackgroundSoundClips = panoramicDAL.GetBackgroundSoundClipsByPanoramicId(panoramicId);
            return Ok(panoramic);
        }

        [HttpPost]
        [Route("api/visited/{panoramicId}")]
        public IHttpActionResult RecordUserVisitedPanoramic(int panoramicId)
        {
            if (User.Identity.IsAuthenticated)
            {
                panoramicDAL.AddVisitedPanoramicByUsername(panoramicId, User.Identity.Name);
            }
            return Ok();
        }

        [HttpGet]
        [Route("api/visited/{trailId}")]
        public IHttpActionResult GetUserVisitedPanoramicsByTrailId(int trailId)
        {
            return Ok();
        }
    }
}
