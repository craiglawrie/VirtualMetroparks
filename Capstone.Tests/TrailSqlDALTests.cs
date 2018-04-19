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
    public class TrailSqlDALTests
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        ParkModel park;
        TrailModel trail;
        TrailImagesModel trailImage;

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

            trail = new TrailModel()
            {
                Name = "testTrail",
                Description = "testTrailDescription"
            };

            trailImage = new TrailImagesModel()
            {
                Bit = true,
                ImageAddress = "address"
            };
        }

        [TestMethod]
        public void GetAllTrails()
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                int newParkId = ParkSqlDALTests.InsertFakePark(park);
                int newTrailId = TrailSqlDALTests.InsertFakeTrail(trail, newParkId);
                TrailSqlDAL testClass = new TrailSqlDAL(connectionString);
                List<TrailModel> trails = testClass.GetAllTrails();
                Assert.IsTrue(trails.Select(trail => trail.TrailId).Contains(newTrailId));
            }
        }

        [TestMethod]
        public void GetTrailById()
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                int newParkId = ParkSqlDALTests.InsertFakePark(park);
                int newTrailId = TrailSqlDALTests.InsertFakeTrail(trail,newParkId);
                TrailSqlDAL testClass = new TrailSqlDAL(connectionString);
                TrailModel newTrail = testClass.GetTrailById(newTrailId);
                Assert.AreEqual(newTrail.TrailId, newTrailId);
            }
        }

        [TestMethod]
        public void GetTrailByTrailName()
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                int newParkId = ParkSqlDALTests.InsertFakePark(park);
                int newTrailId = TrailSqlDALTests.InsertFakeTrail(trail, newParkId);
                TrailSqlDAL testClass = new TrailSqlDAL(connectionString);
                TrailModel newTrail = testClass.GetTrailByTrailName(trail.Name);
                Assert.AreEqual(newTrail.TrailId, newTrailId);
            }
        }

        [TestMethod]
        public void GetTrailsByParkId()
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                int newParkId = ParkSqlDALTests.InsertFakePark(park);
                int newTrailId = TrailSqlDALTests.InsertFakeTrail(trail, newParkId);
                TrailSqlDAL testClass = new TrailSqlDAL(connectionString);
                List<TrailModel> newTrails = testClass.GetTrailsByParkId(newParkId);
                Assert.IsTrue(newTrails.Select(trail => trail.TrailId).Contains(newTrailId));
            }
        }

        [TestMethod]
        public void GetTrailsByParkName()
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                int newParkId = ParkSqlDALTests.InsertFakePark(park);
                int newTrailId = TrailSqlDALTests.InsertFakeTrail(trail, newParkId);
                TrailSqlDAL testClass = new TrailSqlDAL(connectionString);
                List<TrailModel> newTrails = testClass.GetTrailsByParkName(park.Name);
                Assert.IsTrue(newTrails.Select(trail => trail.TrailId).Contains(newTrailId));
            }
        }

        [TestMethod]
        public void GetImagesByTrailId()
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                int newParkId = ParkSqlDALTests.InsertFakePark(park);
                int newTrailId = TrailSqlDALTests.InsertFakeTrail(trail, newParkId);
                int newTrailImageId = TrailSqlDALTests.InsertFakeTrailImage(trailImage, newTrailId);
                TrailSqlDAL testClass = new TrailSqlDAL(connectionString);
                string newTrailImageAddress = testClass.GetImageByTrailId(newTrailId);
                Assert.AreEqual(trailImage.ImageAddress, newTrailImageAddress);
            }
        }

        public static int InsertFakeTrail(TrailModel trail, int parkId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO trails " +
                                              " (park_id, trail_name, trail_description) " +
                                              "VALUES " +
                                              " (@parkId, @name, @description)", conn);
                cmd.Parameters.AddWithValue("@parkId", parkId);
                cmd.Parameters.AddWithValue("@name", trail.Name);
                cmd.Parameters.AddWithValue("@description", trail.Description);

                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("SELECT trail_id FROM trails " +
                                     "WHERE trails.trail_name = @name " +
                                     "AND trails.trail_description = @description", conn);
                cmd.Parameters.AddWithValue("@name", trail.Name);
                cmd.Parameters.AddWithValue("@description", trail.Description);
               
                int result = Convert.ToInt32(cmd.ExecuteScalar());
                return result;
            }
        }

        public static int InsertFakeTrailImage(TrailImagesModel trailImage, int trailId)
        {
            int trailImageId = -1;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO trail_images " +
                                              "(trail_id, trail_image_address, local) " +
                                              "VALUES (@trailId, @imageAddress, @bit)", conn);
                cmd.Parameters.AddWithValue("@trailId", trailId);
                cmd.Parameters.AddWithValue("@imageAddress", trailImage.ImageAddress);
                cmd.Parameters.AddWithValue("@bit", trailImage.Bit);


                cmd.ExecuteNonQuery();

                return trailImageId;
            }
        }
    }
}
