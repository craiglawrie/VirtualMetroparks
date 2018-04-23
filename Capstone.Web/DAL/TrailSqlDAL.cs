using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Capstone.Web.Models;
using Capstone.Web.DAL;

namespace Capstone.Web.DAL
{
    public class TrailSqlDAL : ITrailDAL
    {
        string connectionString;

        public TrailSqlDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<TrailModel> GetAllTrails()
        {
            List<TrailModel> trails = new List<TrailModel>();
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                    try
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand(@"SELECT * FROM trails;", conn);
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            TrailModel trail = MapRowToTrail(reader);
                            trails.Add(trail);
                        }
                        return trails;
                    }
                    catch (SqlException)
                    {
                        throw;
                    }
            }

        }

        public TrailModel GetTrailById(int id)
        {
            TrailModel trail = new TrailModel();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"SELECT * FROM trails WHERE trails.trail_id = @id;", conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        trail = MapRowToTrail(reader);
                    }
                    else
                    {
                        trail = null;
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return trail;
        }

        public TrailModel GetTrailByTrailName(string name)
        {
            TrailModel trail = new TrailModel();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"SELECT * FROM trails WHERE trails.trail_name = @name;", conn);
                    cmd.Parameters.AddWithValue("@name", name);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        trail = MapRowToTrail(reader);
                    }
                    else
                    {
                        trail = null;
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
            return trail;
        }

        public List<TrailModel> GetTrailsByParkId(int id)
        {
            List<TrailModel> trails = new List<TrailModel>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"SELECT * FROM trails WHERE trails.park_id = @parkId", conn);
                    cmd.Parameters.AddWithValue("@parkId", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        TrailModel trail = MapRowToTrail(reader);

                        trails.Add(trail);
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
            return trails;
        }

        public List<TrailModel> GetTrailsByParkName(string name)
        {
            List<TrailModel> trails = new List<TrailModel>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"SELECT trails.* FROM trails INNER JOIN parks ON trails.park_id = parks.park_id WHERE parks.park_name = @parkName", conn);
                    cmd.Parameters.AddWithValue("@parkName", name);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        TrailModel trail = MapRowToTrail(reader);

                        trails.Add(trail);
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
            return trails;
        }

        public string GetImageByTrailId(int id)
        {
            string imageAddress = "";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"SELECT * FROM trail_images WHERE trail_id = @id;", conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        imageAddress = Convert.ToString(reader["trail_image_address"]);
                    }

                }
            }
            catch (SqlException)
            {
                throw;
            }
            return imageAddress;
        }

        public List<string> GetTrailDescriptionByTrailId(int id)
        {
            List<string> NameAndDescription = new List<string>();
            string description = "";
            string name = "";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"SELECT trail_name, trail_description FROM trails WHERE trail_id = @id;", conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                       description = Convert.ToString(reader["trail_description"]);
                        name = Convert.ToString(reader["trail_name"]);
                       
                    }

                    NameAndDescription.Add(name);
                    NameAndDescription.Add(description);

                }
            }
            catch (SqlException)
            {
                throw;
            }
            return NameAndDescription;

        }
        

        private static TrailModel MapRowToTrail(SqlDataReader reader)
        {
            return new TrailModel
            {
                TrailId = Convert.ToInt32(reader["trail_id"]),
                ParkId = Convert.ToInt32(reader["park_id"]),
                Name = Convert.ToString(reader["trail_name"]),
                Description = Convert.ToString(reader["trail_description"])
            };
        }
    }
}