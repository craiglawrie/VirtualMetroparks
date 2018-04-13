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
    public class ParkSqlDALTests
    {
        public static string connection = @"Server=.\SqlExpress;Database=ParkInfo;Trusted_Connection=true";

        [TestMethod]
        public void GetAllParks()
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                ParkSqlDALTests.InsertFakePark(0, "test", "Beautiful", "image", 40, 80, 100);
                ParkSqlDAL testClass = new ParkSqlDAL(connection);
                List<ParkModel> park = testClass.GetAllParks();
                Assert.AreEqual(19, park.Count);
            }
        }

        [TestMethod]
        public void GetParkById()
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                ParkSqlDALTests.InsertFakePark(19, "test", "Beautiful", "image", 40, 80, 100);
                ParkSqlDAL testClass = new ParkSqlDAL(connection);
                ParkModel park = testClass.GetParkById(19);
                Assert.AreEqual(19, park.ParkId);
            }
        }

        [TestMethod]
        public void GetParkByParkName()
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                ParkSqlDALTests.InsertFakePark(19, "test", "Beautiful", "image", 40, 80, 100);
                ParkSqlDAL testClass = new ParkSqlDAL(connection);
                ParkModel park = testClass.GetParkByParkName("test");
                Assert.AreEqual("test", park.Name);
            }
        }

        public static int InsertFakePark(int parkId, string name, string description, string image, double latitude, double longitude, int zoom)
        {
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO parks VALUES (@name, @description, @image, @latitude, @longitude, @zoom)", conn);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@description", description);
                cmd.Parameters.AddWithValue("@image", image);
                cmd.Parameters.AddWithValue("@latitude", latitude);
                cmd.Parameters.AddWithValue("@longitude", longitude);

                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("SELECT MAX(park_id) FROM parks", conn);
                int result = Convert.ToInt32(cmd.ExecuteScalar());
                return result;
            }
        }
    }
}
