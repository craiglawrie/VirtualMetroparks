using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;

namespace Capstone.Web.DAL
{
    public class FakeTrailDAL : ITrailDAL
    {
        public TrailModel GetTrailById(int id)
        {
            throw new NotImplementedException();
        }

        public TrailModel GetTrailByTrailName(string name)
        {
            return trails.Values.First(t => t.Name == name);
        }

        public List<TrailModel> GetTrailsByParkId(int id)
        {
            throw new NotImplementedException();
        }

        public List<TrailModel> GetTrailsByParkName(string name)
        {
            return trails.Values.ToList();
        }

        private Dictionary<int, TrailModel> trails = new Dictionary<int, TrailModel>()
        {
            {1, new TrailModel() {TrailId = 1, Name = "Blue Heron Trail", Description = "See a variety of rich habitats along the Chagrin River, including bottomland forest, wetland, meadow and creek.", LengthInMiles = 4.1, EstimatedWalkTimeinMinutes = 73, TrailHead = new PanoramicModel() {Latitude = 41, Longitude = -81}, PointsOfInterest = new List<PanoramicModel>() { new PanoramicModel() {Latitude = 41, Longitude = -81 } }} },
            {2, new TrailModel() {TrailId = 2, Name = "Chagrin Overlook Trail", Description = "See a variety of rich habitats along the Chagrin River, including bottomland forest, wetland, meadow and creek.", LengthInMiles = 4.1, EstimatedWalkTimeinMinutes = 73, TrailHead = new PanoramicModel() {Latitude = 41, Longitude = -81}, PointsOfInterest = new List<PanoramicModel>() { new PanoramicModel() {Latitude = 41, Longitude = -81 } }} },
            {3, new TrailModel() {TrailId = 3, Name = "Chagrin River Bridle Loop", Description = "See a variety of rich habitats along the Chagrin River, including bottomland forest, wetland, meadow and creek.", LengthInMiles = 4.1, EstimatedWalkTimeinMinutes = 73, TrailHead = new PanoramicModel() {Latitude = 41, Longitude = -81}, PointsOfInterest = new List<PanoramicModel>() { new PanoramicModel() {Latitude = 41, Longitude = -81 } }} },
            {4, new TrailModel() {TrailId = 4, Name = "Forest Loop", Description = "See a variety of rich habitats along the Chagrin River, including bottomland forest, wetland, meadow and creek.", LengthInMiles = 4.1, EstimatedWalkTimeinMinutes = 73, TrailHead = new PanoramicModel() {Latitude = 41, Longitude = -81}, PointsOfInterest = new List<PanoramicModel>() { new PanoramicModel() {Latitude = 41, Longitude = -81 } }} },
            {5, new TrailModel() {TrailId = 5, Name = "Hatchet Ridge Loop", Description = "See a variety of rich habitats along the Chagrin River, including bottomland forest, wetland, meadow and creek.", LengthInMiles = 4.1, EstimatedWalkTimeinMinutes = 73, TrailHead = new PanoramicModel() {Latitude = 41, Longitude = -81}, PointsOfInterest = new List<PanoramicModel>() { new PanoramicModel() {Latitude = 41, Longitude = -81 } }} },
            {6, new TrailModel() {TrailId = 6, Name = "Hatchet Ridge Trail", Description = "See a variety of rich habitats along the Chagrin River, including bottomland forest, wetland, meadow and creek.", LengthInMiles = 4.1, EstimatedWalkTimeinMinutes = 73, TrailHead = new PanoramicModel() {Latitude = 41, Longitude = -81}, PointsOfInterest = new List<PanoramicModel>() { new PanoramicModel() {Latitude = 41, Longitude = -81 } }} },
            {7, new TrailModel() {TrailId = 7, Name = "Henry Church Rock Loop", Description = "A challenging trail that travels the Chagrin River edge to a rock carving by Henry Church.", LengthInMiles = 0.7, EstimatedWalkTimeinMinutes = 15, TrailHead = new PanoramicModel() {PanoramicId = 0, Latitude = 41.416917, Longitude = -81.415312}, PointsOfInterest = new List<PanoramicModel>() { new PanoramicModel() {PanoramicId = 1, Latitude = 41.413674, Longitude = -81.415001 } } } },
            {8, new TrailModel() {TrailId = 8, Name = "Hills and Springs Big Loop", Description = "See a variety of rich habitats along the Chagrin River, including bottomland forest, wetland, meadow and creek.", LengthInMiles = 4.1, EstimatedWalkTimeinMinutes = 73, TrailHead = new PanoramicModel() {Latitude = 41, Longitude = -81}, PointsOfInterest = new List<PanoramicModel>() { new PanoramicModel() {Latitude = 41, Longitude = -81 } }} },
            {9, new TrailModel() {TrailId = 9, Name = "Hills and Springs Loop", Description = "See a variety of rich habitats along the Chagrin River, including bottomland forest, wetland, meadow and creek.", LengthInMiles = 4.1, EstimatedWalkTimeinMinutes = 73, TrailHead = new PanoramicModel() {Latitude = 41, Longitude = -81}, PointsOfInterest = new List<PanoramicModel>() { new PanoramicModel() {Latitude = 41, Longitude = -81 } }} },
            {10, new TrailModel() {TrailId = 10, Name = "Look About Lodge Loop", Description = "See a variety of rich habitats along the Chagrin River, including bottomland forest, wetland, meadow and creek.", LengthInMiles = 4.1, EstimatedWalkTimeinMinutes = 73, TrailHead = new PanoramicModel() {Latitude = 41, Longitude = -81}, PointsOfInterest = new List<PanoramicModel>() { new PanoramicModel() {Latitude = 41, Longitude = -81 } }} },
            {11, new TrailModel() {TrailId = 11, Name = "Persing Loop", Description = "See a variety of rich habitats along the Chagrin River, including bottomland forest, wetland, meadow and creek.", LengthInMiles = 4.1, EstimatedWalkTimeinMinutes = 73, TrailHead = new PanoramicModel() {Latitude = 41, Longitude = -81}, PointsOfInterest = new List<PanoramicModel>() { new PanoramicModel() {Latitude = 41, Longitude = -81 } }} },
            {12, new TrailModel() {TrailId = 12, Name = "Quarry Rock Loop", Description = "See a variety of rich habitats along the Chagrin River, including bottomland forest, wetland, meadow and creek.", LengthInMiles = 4.1, EstimatedWalkTimeinMinutes = 73, TrailHead = new PanoramicModel() {Latitude = 41, Longitude = -81}, PointsOfInterest = new List<PanoramicModel>() { new PanoramicModel() {Latitude = 41, Longitude = -81 } }} },
            {13, new TrailModel() {TrailId = 13, Name = "South Chagrin All Purpose Trail", Description = "See a variety of rich habitats along the Chagrin River, including bottomland forest, wetland, meadow and creek.", LengthInMiles = 4.1, EstimatedWalkTimeinMinutes = 73, TrailHead = new PanoramicModel() {Latitude = 41, Longitude = -81}, PointsOfInterest = new List<PanoramicModel>() { new PanoramicModel() {Latitude = 41, Longitude = -81 } }} },
            {14, new TrailModel() {TrailId = 14, Name = "South Chagrin All Purpose Trail Loop", Description = "See a variety of rich habitats along the Chagrin River, including bottomland forest, wetland, meadow and creek.", LengthInMiles = 4.1, EstimatedWalkTimeinMinutes = 73, TrailHead = new PanoramicModel() {Latitude = 41, Longitude = -81}, PointsOfInterest = new List<PanoramicModel>() { new PanoramicModel() {Latitude = 41, Longitude = -81 } }} },
            {15, new TrailModel() {TrailId = 15, Name = "South Chagrin Bridle Loop 1", Description = "See a variety of rich habitats along the Chagrin River, including bottomland forest, wetland, meadow and creek.", LengthInMiles = 4.1, EstimatedWalkTimeinMinutes = 73, TrailHead = new PanoramicModel() {Latitude = 41, Longitude = -81}, PointsOfInterest = new List<PanoramicModel>() { new PanoramicModel() {Latitude = 41, Longitude = -81 } }} },
            {16, new TrailModel() {TrailId = 16, Name = "South Chagrin Bridle Loop 1", Description = "See a variety of rich habitats along the Chagrin River, including bottomland forest, wetland, meadow and creek.", LengthInMiles = 4.1, EstimatedWalkTimeinMinutes = 73, TrailHead = new PanoramicModel() {Latitude = 41, Longitude = -81}, PointsOfInterest = new List<PanoramicModel>() { new PanoramicModel() {Latitude = 41, Longitude = -81 } }} },
            {17, new TrailModel() {TrailId = 17, Name = "South Chagrin Bridle Trail", Description = "See a variety of rich habitats along the Chagrin River, including bottomland forest, wetland, meadow and creek.", LengthInMiles = 4.1, EstimatedWalkTimeinMinutes = 73, TrailHead = new PanoramicModel() {Latitude = 41, Longitude = -81}, PointsOfInterest = new List<PanoramicModel>() { new PanoramicModel() {Latitude = 41, Longitude = -81 } }} },
            {18, new TrailModel() {TrailId = 18, Name = "Sulphur Springs Loop", Description = "See a variety of rich habitats along the Chagrin River, including bottomland forest, wetland, meadow and creek.", LengthInMiles = 4.1, EstimatedWalkTimeinMinutes = 73, TrailHead = new PanoramicModel() {Latitude = 41, Longitude = -81}, PointsOfInterest = new List<PanoramicModel>() { new PanoramicModel() {Latitude = 41, Longitude = -81 } }} },
            {19, new TrailModel() {TrailId = 19, Name = "The Shelterhouse Loop", Description = "See a variety of rich habitats along the Chagrin River, including bottomland forest, wetland, meadow and creek.", LengthInMiles = 4.1, EstimatedWalkTimeinMinutes = 73, TrailHead = new PanoramicModel() {Latitude = 41, Longitude = -81}, PointsOfInterest = new List<PanoramicModel>() { new PanoramicModel() {Latitude = 41, Longitude = -81 } }} }
        };
    }
}