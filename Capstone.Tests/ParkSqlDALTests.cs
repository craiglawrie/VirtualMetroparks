using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Capstone.Web.Models;
using Capstone.Web.DAL;
using System.Transactions;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;

namespace Capstone.Tests
{
    [TestClass]
    public class ParkSqlDALTests
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        ParkModel park;
        ParkImagesModel parkImage;

        [TestInitialize]
        public void Initialize()
        {
            park = new ParkModel()
            {
                Name = "testPark",
                Description = "testDescription",
                Latitude = 41,
                Longitude = 100,
                Zoom = 200
            };

            parkImage = new ParkImagesModel()
            {
                Bit = true,
                ImageAddress = "address"
            };
        }

        [TestMethod]
        public void GetAllParks()
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                int newParkId = ParkSqlDALTests.InsertFakePark(park);
                ParkSqlDAL testClass = new ParkSqlDAL(connectionString);
                List<ParkModel> parks = testClass.GetAllParks();
                Assert.IsTrue(parks.Select(park => park.ParkId).Contains(newParkId));
            }
        }

        [TestMethod]
        public void GetParkById()
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                int newParkId = ParkSqlDALTests.InsertFakePark(park);
                ParkSqlDAL testClass = new ParkSqlDAL(connectionString);
                ParkModel newPark = testClass.GetParkById(newParkId);
                Assert.AreEqual(newPark.ParkId, newParkId);
            }
        }

        [TestMethod]
        public void GetParkByParkName()
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                int newParkId = ParkSqlDALTests.InsertFakePark(park);
                ParkSqlDAL testClass = new ParkSqlDAL(connectionString);
                ParkModel newPark = testClass.GetParkByParkName(park.Name);
                Assert.AreEqual(newPark.ParkId, newParkId);
            }
        }

        [TestMethod]
        public void GetImageByParkId()
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                int newParkId = ParkSqlDALTests.InsertFakePark(park);
                int newParkImageId = ParkSqlDALTests.InsertFakeParkImage(parkImage, newParkId);
                ParkSqlDAL testClass = new ParkSqlDAL(connectionString);
                string newParkImageAddress = testClass.GetImageByParkId(newParkId);
                Assert.AreEqual(parkImage.ImageAddress, newParkImageAddress);
            }
        }

        public static int InsertFakePark(ParkModel park)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO parks " +
                                              " (park_name, park_description, park_latitude, park_longitude, default_zoom) " +
                                              "VALUES " +
                                              " (@name, @description, @latitude, @longitude, @zoom)", conn);
                cmd.Parameters.AddWithValue("@name", park.Name);
                cmd.Parameters.AddWithValue("@description", park.Description);
                cmd.Parameters.AddWithValue("@latitude", park.Latitude);
                cmd.Parameters.AddWithValue("@longitude", park.Longitude);
                cmd.Parameters.AddWithValue("@zoom", park.Zoom);


                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("SELECT park_id FROM parks " +
                                     "WHERE parks.park_name = @name " +
                                     "AND parks.park_description = @description " +
                                     "AND parks.park_latitude = @latitude " +
                                     "AND parks.park_longitude = @longitude " +
                                     "AND parks.default_zoom = @zoom", conn);
                cmd.Parameters.AddWithValue("@name", park.Name);
                cmd.Parameters.AddWithValue("@description", park.Description);
                cmd.Parameters.AddWithValue("@latitude", park.Latitude);
                cmd.Parameters.AddWithValue("@longitude", park.Longitude);
                cmd.Parameters.AddWithValue("@zoom", park.Zoom);
                int result = Convert.ToInt32(cmd.ExecuteScalar());
                return result;


            }
        }

        public static int InsertFakeParkImage(ParkImagesModel parkImage, int parkId)
        {
            int parkImageId = -1;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO park_images " +
                                              "(park_id, park_image_address, local) " +
                                              "VALUES (@parkId, @imageAddress, @bit)", conn);
                cmd.Parameters.AddWithValue("@parkId", parkId);
                cmd.Parameters.AddWithValue("@imageAddress", parkImage.ImageAddress);
                cmd.Parameters.AddWithValue("@bit", parkImage.Bit);


                cmd.ExecuteNonQuery();

                return parkImageId;
            }
        }
    }
}
