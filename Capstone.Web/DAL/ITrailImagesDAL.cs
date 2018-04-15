using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;

namespace Capstone.Web.DAL
{
    public interface ITrailImagesDAL
    {
        TrailImagesModel GetTrailImagesById(int id);
        List<TrailImagesModel> GetTrailImagesByTrailId(int trailId);
        List<TrailImagesModel> GetAllTrailImages();
    }
}