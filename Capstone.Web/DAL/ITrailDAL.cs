using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Web.DAL
{
    public interface ITrailDAL
    {
        TrailModel GetTrailById(int id);
        TrailModel GetTrailByTrailName(string name);
        List<TrailModel> GetTrailsByParkId(int id);
        List<TrailModel> GetTrailsByParkName(string name);
        List<TrailModel> GetAllTrails();
        string GetImageByTrailId(int id);
        List<string> GetTrailDescriptionByTrailId(int id);
    }
}