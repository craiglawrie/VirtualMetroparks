using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;

namespace Capstone.Web.DAL
{
    public interface IParkImagesDAL
    {
        ParkImagesModel GetParkImagesById(int id);
        List<ParkImagesModel> GetParkImagesByParkId(int parkId);
        List<ParkImagesModel> GetAllParkImages();
    }
}