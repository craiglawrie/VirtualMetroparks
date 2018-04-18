using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Web.Models;

namespace Capstone.Web.DAL
{
   public interface IParkDAL
    {
        ParkModel GetParkById(int id);
        List<ParkModel> GetAllParks();
        ParkModel GetParkByParkName(string name);
        string GetImageByParkId(int id);
        
    }
}
