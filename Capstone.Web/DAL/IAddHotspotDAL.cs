using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;
using Capstone.Web.DAL;

namespace Capstone.Web.DAL
{
    public interface IAddHotspotDAL
    {
        bool SaveNewHotspot(AddHotspot addHotspot);
    }
}