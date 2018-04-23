using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class TrailModel
    {
        public int TrailId { get; set; }
        public int ParkId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInMiles { get; set; }
        public int EstimatedWalkTimeinMinutes { get; set; }
        public string Image { get; set; }
        public int Zoom { get; set; }

        public PanoramicModel TrailHead { get; set; }
        public List<PanoramicModel> PointsOfInterest { get; set; }
        public List<PanoramicModel> PanoramicsInTrail { get; set; }
    }
}