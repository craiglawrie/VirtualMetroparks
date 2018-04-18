using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;
using System.Data.SqlClient;

namespace Capstone.Web.DAL
{
    public class ParkSqlDAL : IParkDAL
    {
        string connectionString;

        public ParkSqlDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }


        public List<ParkModel> GetAllParks()
        {
            List<ParkModel> parks = new List<ParkModel>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"SELECT * FROM parks ORDER BY park_name;", conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        ParkModel park = MapRowToPark(reader);
                        parks.Add(park);
                    }

                    return parks;
                }

            }
            catch (SqlException)
            {
                throw;
            }
        }

        public ParkModel GetParkById(int id)
        {
            ParkModel park = new ParkModel();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"SELECT * FROM parks WHERE parks.park_id = @id;", conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        park = MapRowToPark(reader);
                    }
                    else
                    {
                        park = null;
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return park;
        }

        public ParkModel GetParkByParkName(string name)
        {
            ParkModel park = new ParkModel();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"SELECT * FROM parks WHERE parks.park_name = @name;", conn);
                    cmd.Parameters.AddWithValue("@name", name);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        park = MapRowToPark(reader);
                    }
                    else
                    {
                        park = null;
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
            return park;
        }

        public string GetImageByParkId(int id)
        {
            string imageAddress = "";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"SELECT * FROM park_images WHERE park_id = @id;", conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if(reader.Read())
                    {
                        imageAddress = Convert.ToString(reader["park_image_address"]);
                    }
                        
                }
            }
            catch(SqlException)
            {
                throw;
            }
            return imageAddress;
        }

        private static ParkModel MapRowToPark(SqlDataReader reader)
        {
            return new ParkModel()
            {
                ParkId = Convert.ToInt32(reader["park_id"]),
                Name = Convert.ToString(reader["park_name"]),
                Description = Convert.ToString(reader["park_description"]),                
                Latitude = Convert.ToDouble(reader["park_latitude"]),
                Longitude = Convert.ToDouble(reader["park_longitude"]),
                Zoom = Convert.ToInt32(reader["default_zoom"])
            };
        }

        
    }
}