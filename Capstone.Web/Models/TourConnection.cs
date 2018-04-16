using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class TourConnection
    {
        public int DestinationId { get; set; }
        public int HotspotPitch { get; set; }
        public int HotspotYaw { get; set; }
        public int DestinationStartPitch { get; set; }
        public int DestinationStartYaw { get; set; }
    }
}