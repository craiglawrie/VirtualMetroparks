using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;

namespace Capstone.Web.DAL
{
    public class FakePanoramicDAL : IPanoramicDAL
    {
        public PanoramicModel GetPanoramicById(int id)
        {
            return panoramics[id];
        }

        public List<PanoramicModel> GetPanoramicsByTrailId(int trailId)
        {
            return panoramics.Values.ToList();
        }

        public List<PanoramicModel> GetAllPanoramics()
        {
            return panoramics.Values.ToList();
        }

        public List<PanoramicModel> GetPanoramicsByTrailName(string name)
        {
            throw new NotImplementedException();
        }

        public PanoramicModel GetTrailHeadByTrailId(int trailId)
        {
            throw new NotImplementedException();
        }

        public List<PanoramicModel> GetPointsOfInterestByTrailId(int trailId)
        {
            throw new NotImplementedException();
        }

        public List<TourConnection> GetConnectionsByPanoramicId(int panoramicId)
        {
            throw new NotImplementedException();
        }

        public List<BackgroundSoundClip> GetBackgroundSoundClipsByPanoramicId(int panoramicId)
        {
            throw new NotImplementedException();
        }

        public List<BackgroundSoundClip> GetAllBackgroundSoundClips()
        {
            throw new NotImplementedException();
        }

        private Dictionary<int, PanoramicModel> panoramics = new Dictionary<int, PanoramicModel>()
        {
            {0, new PanoramicModel() { PanoramicId = 0, Latitude = 41.416917, Longitude = -81.415312 } },
            {1, new PanoramicModel() { PanoramicId = 1, Latitude = 41.413674, Longitude = -81.415001} },
            {2, new PanoramicModel() { PanoramicId = 2, Latitude = 41.41, Longitude = -81.41 } }
        };
    }
}