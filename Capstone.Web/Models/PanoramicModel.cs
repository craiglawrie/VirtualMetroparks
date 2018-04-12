using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class PanoramicModel
    {
        public int PanoramicId { get; set; }
        public string ImageAddress { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int NextPanoramic { get; set; }
        public int PrevPanoramic { get; set; }
    }
}