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
    public class PanoramicSqlDALTests
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        ParkModel park;
        TrailModel trail;
        PanoramicModel panoramicImage;

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

            panoramicImage = new PanoramicModel()
            {
                ImageAddress = "address",
                Latitude = 100,
                Longitude = 42,
            };
        }

        [TestMethod]
        public void GetAllPanoramics()
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                int newParkId = ParkSqlDALTests.InsertFakePark(park);
                int newTrailId = TrailSqlDALTests.InsertFakeTrail(trail, newParkId);
                int newPanoramicId = PanoramicSqlDALTests.InsertFakePanoramic(panoramicImage, newTrailId);
                PanoramicSqlDAL testClass = new PanoramicSqlDAL(connectionString);
                List<PanoramicModel> newPanoramicImages = testClass.GetAllPanoramics();
                Assert.IsTrue(newPanoramicImages.Select(panoramic => panoramic.PanoramicId).Contains(newPanoramicId));
            }
        }

        [TestMethod]
        public void GetPanoramicById()
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                int newParkId = ParkSqlDALTests.InsertFakePark(park);
                int newTrailId = TrailSqlDALTests.InsertFakeTrail(trail, newParkId);
                int newPanoramicId = PanoramicSqlDALTests.InsertFakePanoramic(panoramicImage, newTrailId);
                PanoramicSqlDAL testClass = new PanoramicSqlDAL(connectionString);
                PanoramicModel newPanoramicImages = testClass.GetPanoramicById(newPanoramicId);
                Assert.AreEqual(newPanoramicImages.PanoramicId, newPanoramicId);
            }
        }

        [TestMethod]
        public void GetPanoramicsByTrailId()
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                int newParkId = ParkSqlDALTests.InsertFakePark(park);
                int newTrailId = TrailSqlDALTests.InsertFakeTrail(trail, newParkId);
                int newPanoramicId = PanoramicSqlDALTests.InsertFakePanoramic(panoramicImage, newTrailId);
                PanoramicSqlDAL testClass = new PanoramicSqlDAL(connectionString);
                List<PanoramicModel> newPanoramicImages = testClass.GetPanoramicsByTrailId(newTrailId);
                Assert.IsTrue(newPanoramicImages.Select(panoramic => panoramic.PanoramicId).Contains(newPanoramicId));
                Assert.AreEqual(1, newPanoramicImages.Count);
            }
        }

        private static int InsertFakePanoramic(PanoramicModel panoramicImage, int trailId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO panoramic_images " +
                                              " (trail_id, image_address, image_latitude, image_longitude, is_trail_head) " +
                                              "VALUES " +
                                              " (@trailId, @imageAddress, @latitude, @longitude, @isTrailHead)", conn);
                cmd.Parameters.AddWithValue("@trailId", trailId);
                cmd.Parameters.AddWithValue("@imageAddress", panoramicImage.ImageAddress);
                cmd.Parameters.AddWithValue("@latitude", panoramicImage.Latitude);
                cmd.Parameters.AddWithValue("@longitude", panoramicImage.Longitude);
                cmd.Parameters.AddWithValue("@isTrailHead", true);

                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("SELECT panoramic_image_id FROM panoramic_images " +
                                     "WHERE panoramic_images.image_address = @imageAddress " +
                                     "AND panoramic_images.image_latitude = @latitude " +
                                     "AND panoramic_images.image_longitude = @longitude " +
                                     "AND panoramic_images.is_trail_head = @isTrailHead ", conn);
                cmd.Parameters.AddWithValue("@imageAddress", panoramicImage.ImageAddress);
                cmd.Parameters.AddWithValue("@latitude", panoramicImage.Latitude);
                cmd.Parameters.AddWithValue("@longitude", panoramicImage.Longitude);
                cmd.Parameters.AddWithValue("@isTrailHead", true);

                int result = Convert.ToInt32(cmd.ExecuteScalar());
                return result;
            }

        }

    }
}

