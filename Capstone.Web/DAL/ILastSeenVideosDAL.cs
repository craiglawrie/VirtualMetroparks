using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;

namespace Capstone.Web.DAL
{
    public interface ILastSeenVideosDAL
    {
        LastSeenVideosModel GetLastSeenVideosById(int id);
        List<LastSeenVideosModel> GetLastSeenVideosByTrailId(int trailId);
        List<LastSeenVideosModel> GetAllLastSeenVideos();
        List<LastSeenVideosModel> GetLastSeenVideosByParkId(int parkId);
        List<LastSeenVideosModel> GetLastSeenVideosByPanoramicId(int panoramicId);
    }
}