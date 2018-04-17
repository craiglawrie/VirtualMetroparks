using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class LastSeenVideosModel
    {
        public int LastSeenVideosId { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public string VideoAddress { get; set; }
        public int Pitch { get; set; }
        public int Yaw { get; set; }
        public bool HasSound { get; set; }
        public double Duration { get; set; }
        public int Volume { get; set; }
    }
}