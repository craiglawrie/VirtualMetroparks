using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;

namespace Capstone.Web.DAL
{
    public class FakeParkDAL : IParkDAL
    {
        public List<ParkModel> GetAllParks()
        {
            return parks.Values.ToList();
        }

        public ParkModel GetParkById(int id)
        {
            return parks[id];
        }

        private Dictionary<int, ParkModel> parks = new Dictionary<int, ParkModel>()
        {
            {1, new ParkModel() {Name = "Acacia"} },
            {2, new ParkModel() {Name = "Bedford"} },
            {3, new ParkModel() {Name = "Big Creek"} },
            {4, new ParkModel() {Name = "Bradley Woods"} },
            {5, new ParkModel() {Name = "Brecksville"} },
            {6, new ParkModel() {Name = "Brookside"} },
            {7, new ParkModel() {Name = "Euclid Creek"} },
            {8, new ParkModel() {Name = "Garfield Park"} },
            {9, new ParkModel() {Name = "Hinckley"} },
            {10, new ParkModel() {Name = "Huntington"} },
            {11, new ParkModel() {Name = "Lakefront"} },
            {12, new ParkModel() {Name = "Mill Stresm Run"} },
            {13, new ParkModel() {Name = "North Chagrin"} },
            {14, new ParkModel() {Name = "Ohio and Erie Canal"} },
            {15, new ParkModel() {Name = "Rocky River"} },
            {16, new ParkModel() {Name = "South Chagrin"} },
            {17, new ParkModel() {Name = "Washington"} },
            {18, new ParkModel() {Name = "West Creek"} }
        };
    }
}