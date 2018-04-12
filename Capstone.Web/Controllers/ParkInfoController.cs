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
        IParkDAL dal;

        public ParkInfoController(IParkDAL dal)
        {
            this.dal = dal;
        }

        [HttpGet]
        [Route("api/parks")]
        public IHttpActionResult GetParks()
        {
            List<ParkModel> parks = dal.GetAllParks();

            return Ok(parks);
        }

        [HttpGet]
        [Route("api/park/{name}")]
        public IHttpActionResult GetParkByParkName(string name)
        {
            ParkModel park = dal.GetParkByParkName(name);

            return Ok(park);
        }
    }
}
