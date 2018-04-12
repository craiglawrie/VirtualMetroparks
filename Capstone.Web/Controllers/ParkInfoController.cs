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

        public ParkInfoController(IParkDAL parkDAL, ITrailDAL trailDAL)
        {
            this.parkDAL = parkDAL;
            this.trailDAL = trailDAL;
        }

        [HttpGet]
        [Route("api/parks")]
        public IHttpActionResult GetParks()
        {
            List<ParkModel> parks = parkDAL.GetAllParks();

            return Ok(parks);
        }

        [HttpGet]
        [Route("api/park/{name}")]
        public IHttpActionResult GetParkByParkName(string name)
        {
            ParkModel park = parkDAL.GetParkByParkName(name);
            park.Trails = trailDAL.GetTrailsByParkName(name);

            return Ok(park);
        }
    }
}
