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
    public class TrailSqlDALTests
    {
        public static string connection = @"Server=.\SqlExpress;Database=ParkInfo;Trusted_Connection=true";

        [TestMethod]
        public void GetAllTrails()
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                TrailSqlDALTests.InsertFakeTrail(0, 19, "test", "Beautiful", "image");
                TrailSqlDAL testClass = new TrailSqlDAL(connection);
                List<TrailModel> trail = testClass.GetAllTrails();
                Assert.AreEqual(19, trail.Count);
            }
        }

        [TestMethod]
        public void GetTrailById()
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                TrailSqlDALTests.InsertFakeTrail(21, 19, "test", "Beautiful", "image");
                TrailSqlDAL testClass = new TrailSqlDAL(connection);
                TrailModel trail = testClass.GetTrailById(21);
                Assert.AreEqual(21, trail.TrailId);
            }
        }

        [TestMethod]
        public void GetTrailByTrailName()
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                TrailSqlDALTests.InsertFakeTrail(21, 19, "test", "Beautiful", "image");
                TrailSqlDAL testClass = new TrailSqlDAL(connection);
                TrailModel trail = testClass.GetTrailByTrailName("test");
                Assert.AreEqual("test", trail.Name);
            }
        }

        public static int InsertFakeTrail(int trailId, int parkId, string name, string description, string image)
        {
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO parks VALUES (@name, @description, @image)", conn);
                cmd.Parameters.AddWithValue("@park_id", parkId);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@description", description);
                cmd.Parameters.AddWithValue("@image", image);

                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("SELECT MAX(trail_id) FROM trails", conn);
                int result = Convert.ToInt32(cmd.ExecuteScalar());
                return result;
            }
        }
    }
}
