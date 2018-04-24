using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class ParkModel
    {
        public int ParkId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Zoom { get; set; }

        public List<TrailModel> Trails { get; set; }
        public List<PanoramicModel> UserVisitedPanoramics { get; set; }
    }
}