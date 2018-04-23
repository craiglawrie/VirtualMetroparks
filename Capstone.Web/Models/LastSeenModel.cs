using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class LastSeenModel
    {
        public List<LastSeenImagesModel> Images { get; set; }
        public List<LastSeenVideosModel> Videos { get; set; }

        public List<TrailModel> Trails { get; set; }
        public List<ParkModel> Parks { get; set; }
    }
}