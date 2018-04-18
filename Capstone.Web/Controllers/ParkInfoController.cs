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
            park.Trails.ForEach(trail => trail.TrailHead = panoramicDAL.GetTrailHeadByTrailId(trail.TrailId));
            park.Trails.ForEach(trail => trail.PointsOfInterest = panoramicDAL.GetPointsOfInterestByTrailId(trail.TrailId));

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
    }
}
