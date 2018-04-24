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
        List<PanoramicModel> GetPanoramicsByTrailName(string name);
        PanoramicModel GetTrailHeadByTrailId(int trailId);
        List<PanoramicModel> GetPointsOfInterestByTrailId(int trailId);
        List<TourConnection> GetConnectionsByPanoramicId(int panoramicId);
        List<BackgroundSoundClip> GetBackgroundSoundClipsByPanoramicId(int panoramicId);
        List<BackgroundSoundClip> GetAllBackgroundSoundClips();
        bool AddVisitedPanoramicByUsername(int panoramicId, string userName);
        List<PanoramicModel> GetVisitedPanoramicsByUsername(string userName);
    }
}
