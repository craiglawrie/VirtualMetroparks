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
                        SqlCommand cmd = new SqlCommand(@"SELECT * FROM trails INNER JOIN parks ON trails.park_id = park.park_id ORDER BY trail_name;", conn);
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
                    SqlCommand cmd = new SqlCommand(@"SELECT trail.trail_id FROM trails INNER JOIN parks ON trails.park_id = park.park_id WHERE trail.trail_id = @id;", conn);
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
                    SqlCommand cmd = new SqlCommand(@"SELECT trail.trail_name FROM trails INNER JOIN parks ON trails.park_id = park.park_id WHERE trail.trail_name = @name;", conn);
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

        private static TrailModel MapRowToTrail(SqlDataReader reader)
        {
            return new TrailModel
            {
                TrailId = Convert.ToInt32(reader["trail_id"]),
                Name = Convert.ToString(reader["trail_name"]),
                Description = Convert.ToString(reader["trail_description"]),
                Image = Convert.ToString(reader["trail_image"])
            };
        }
    }
}