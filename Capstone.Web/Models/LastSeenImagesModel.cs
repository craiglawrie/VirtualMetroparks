using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class LastSeenImagesModel
    {
        public int LastSeenImagesId { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public string ImageAddress { get; set; }
        public int Pitch { get; set; }
        public int Yaw { get; set; }
    }
}