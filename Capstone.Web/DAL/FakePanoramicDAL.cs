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
            throw new NotImplementedException();
        }

        public List<PanoramicModel> GetPanoramicsByTrailId(int trailId)
        {
            return panoramics.Values.ToList();
        }

        private Dictionary<int, PanoramicModel> panoramics = new Dictionary<int, PanoramicModel>()
        {
            {1, new PanoramicModel() { } },
            {2, new PanoramicModel() { } }
        };
    }
}