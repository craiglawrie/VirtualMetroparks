﻿using System;
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

        public ParkModel GetParkByParkName(string name)
        {
            return parks.Values.ToList().First(p => p.Name == name);
        }

        public string GetImageByParkId(int id)
        {
            throw new NotImplementedException();
        }

        private Dictionary<int, ParkModel> parks = new Dictionary<int, ParkModel>()
        {
            {1, new ParkModel() {ParkId = 1, Name = "Acacia Reservation", Description="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.", Image="HenryChurchRock.jpg", Latitude = 41.505898, Longitude = -81.492452, Zoom = 15} },
            {2, new ParkModel() {ParkId = 2, Name = "Bedford Reservation", Description="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.", Image="HenryChurchRock.jpg", Latitude = 41.377143, Longitude = -81.565660, Zoom = 13} },
            {3, new ParkModel() {ParkId = 3, Name = "Big Creek Reservation", Description="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.", Image="HenryChurchRock.jpg", Latitude = 41.412679, Longitude = -81.754273, Zoom = 15} },
            {4, new ParkModel() {ParkId = 4, Name = "Bradley Woods Reservation", Description="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.", Image="HenryChurchRock.jpg", Latitude = 41.412114, Longitude = -81.957769, Zoom = 14} },
            {5, new ParkModel() {ParkId = 5, Name = "Brecksville Reservation", Description="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.", Image="HenryChurchRock.jpg", Latitude = 41.309311, Longitude = -81.608507, Zoom = 13} },
            {6, new ParkModel() {ParkId = 6, Name = "Brookside Reservation", Description="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.", Image="HenryChurchRock.jpg", Latitude = 41.448763, Longitude = -81.723073, Zoom = 15} },
            {7, new ParkModel() {ParkId = 7, Name = "Euclid Creek Reservation", Description="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.", Image="HenryChurchRock.jpg", Latitude = 41.550857, Longitude = -81.529233, Zoom = 14} },
            {8, new ParkModel() {ParkId = 8, Name = "Garfield Park Reservation", Description="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.", Image="HenryChurchRock.jpg", Latitude = 41.433910, Longitude = -81.611864, Zoom = 14} },
            {9, new ParkModel() {ParkId = 9, Name = "Hinckley Reservation", Description="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.", Image="HenryChurchRock.jpg", Latitude = 41.216352, Longitude = -81.709617, Zoom = 14} },
            {10, new ParkModel() {ParkId = 10, Name = "Huntington Reservation", Description="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.", Image="HenryChurchRock.jpg", Latitude = 41.487740, Longitude = -81.933973, Zoom = 15} },
            {11, new ParkModel() {ParkId = 11, Name = "Lakefront Reservation", Description="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.", Image="HenryChurchRock.jpg", Latitude = 41.488620, Longitude = -81.737879, Zoom = 15} },
            {12, new ParkModel() {ParkId = 12, Name = "Mill Stream Run Reservation", Description="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.", Image="HenryChurchRock.jpg", Latitude = 41.325285, Longitude = -81.816950, Zoom = 12} },
            {13, new ParkModel() {ParkId = 13, Name = "North Chagrin Reservation", Description="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.", Image="HenryChurchRock.jpg", Latitude = 41.571339, Longitude = -81.428583, Zoom = 13} },
            {14, new ParkModel() {ParkId = 14, Name = "Ohio and Erie Canal Reservation", Description="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.", Image="HenryChurchRock.jpg", Latitude = 41.433929, Longitude = -81.666145, Zoom = 13} },
            {15, new ParkModel() {ParkId = 15, Name = "Rocky River Reservation", Description="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.", Image="HenryChurchRock.jpg", Latitude = 41.433571, Longitude = -81.846017, Zoom = 13} },
            {16, new ParkModel() {ParkId = 16, Name = "South Chagrin Reservation", Description="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.", Image="HenryChurchRock.jpg", Latitude = 41.416206, Longitude = -81.423261, Zoom = 14} },
            {17, new ParkModel() {ParkId = 17, Name = "Washington Reservation", Description="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.", Image="HenryChurchRock.jpg", Latitude = 41.456177, Longitude = -81.661627, Zoom = 15} },
            {18, new ParkModel() {ParkId = 18, Name = "West Creek Reservation", Description="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.", Image="HenryChurchRock.jpg", Latitude = 41.382914, Longitude = -81.695003, Zoom = 14} }
        };
    }
}