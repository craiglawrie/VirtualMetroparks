using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;

namespace Capstone.Web.DAL
{
    public interface ILastSeenImagesDAL
    {
        LastSeenImagesModel GetLastSeenImagesById(int id);
        List<LastSeenImagesModel> GetLastSeenImagesByTrailId(int trailId);
        List<LastSeenImagesModel> GetAllLastSeenImages();
        List<LastSeenImagesModel> GetLastSeenImagesByParkId(int parkId);
        List<LastSeenImagesModel> GetLastSeenImagesByPanoramicId(int panoramicId);
    }
}