using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Capstone.Web.Models;
using Capstone.Web.DAL;
using System.Transactions;
using System.Data.SqlClient;


namespace Capstone.Tests
{
    [TestClass]
    public class PanoramicSqlDALTests
    {
        public static string connection = @"Server=.\SqlExpress;Database=ParkInfo;Trusted_Connection=true";
        [TestMethod]
        public void GetAllPanoramics()
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                PanoramicSqlDALTests.InsertFakePanoramic(10, 19, "test", 8.8, 100.1, 33, 56);
                PanoramicSqlDAL testClass = new PanoramicSqlDAL(connection);
                List<PanoramicModel> panoramic = testClass.GetAllPanoramics();
                Assert.AreEqual(10, panoramic.Count);
            }
        }

        [TestMethod]
        public void GetPanoramicById()
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                PanoramicSqlDALTests.InsertFakePanoramic(10, 19, "test", 8.8, 100.1, 33, 56);
                PanoramicSqlDAL testClass = new PanoramicSqlDAL(connection);
                PanoramicModel panoramic = testClass.GetPanoramicById(10);
                Assert.AreEqual(10, panoramic.PanoramicId);
            }
        }

        private static int InsertFakePanoramic(int panoramicId, int trailId, string imageAddress, double latitude, double longitude, int nextPanoramic, int prevPanoramic)
        {
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO panoramics VALUES (@trailId, @imageAddress, @latitude, @longitude, @nextPanoramic, @prevPanoramic)", conn);
                cmd.Parameters.AddWithValue("@trail_id", trailId);
                cmd.Parameters.AddWithValue("@image_Address", imageAddress);
                cmd.Parameters.AddWithValue("@latitude", latitude);
                cmd.Parameters.AddWithValue("@longitude", longitude);
                cmd.Parameters.AddWithValue("@nextPanoramic", nextPanoramic);
                cmd.Parameters.AddWithValue("@prevPanoramic", prevPanoramic);

                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("SELECT MAX(panoramic_id) FROM trails", conn);
                int result = Convert.ToInt32(cmd.ExecuteScalar());
                return result;
            }

        }
    }
}
