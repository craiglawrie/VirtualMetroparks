using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Web.DAL
{
    public interface IPanoramicDAL
    {
        PanoramicModel GetPanoramicById(int id);
        List<PanoramicModel> GetPanoramicsByTrailId(int trailId);
        List<PanoramicModel> GetAllPanoramics();
    }
}
