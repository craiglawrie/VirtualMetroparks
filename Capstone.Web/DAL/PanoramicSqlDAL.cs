using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;
using System.Data.SqlClient;

namespace Capstone.Web.DAL
{
    public class PanoramicSqlDAL : IPanoramicDAL
    {
        string connectionString;

        public PanoramicSqlDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<PanoramicModel> GetAllPanoramics()
        {

            List<PanoramicModel> panoramics = new List<PanoramicModel>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"SELECT * FROM panoramics;", conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        PanoramicModel panoramic = MapRowToPanoramic(reader);
                        panoramics.Add(panoramic);
                    }

                    return panoramics;
                }

            }
            catch (SqlException)
            {
                throw;
            }
        }

        public PanoramicModel GetPanoramicById(int id)
        {
            PanoramicModel panoramic = new PanoramicModel();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"SELECT * FROM panoramics WHERE panoramics.panoramic_id = @id;", conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        panoramic = MapRowToPanoramic(reader);
                    }
                    else
                    {
                        panoramic = null;
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return panoramic; ;
        }

        public List<PanoramicModel> GetPanoramicsByTrailId(int trailId)
        {
            List<PanoramicModel> panoramics = new List<PanoramicModel>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"SELECT * FROM panoramics WHERE panoramics.trail_id = @trailId;", conn);
                    cmd.Parameters.AddWithValue("@trailId", trailId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        PanoramicModel panoramic = MapRowToPanoramic(reader);

                        panoramics.Add(panoramic);
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
            return panoramics;
        }
        List<PanoramicModel> GetPanoramicsByTrailName(string name)
        {
            List<PanoramicModel> panoramics = new List<PanoramicModel>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"SELECT panoramics.* FROM panoramics INNER JOIN trails ON panoramics.trail_id = trails.trail_id WHERE trails.trail_name = @trailName;", conn);
                    cmd.Parameters.AddWithValue("@trail_name", name);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while(reader.Read())
                    {
                        PanoramicModel panoramic = MapRowToPanoramic(reader);
                        panoramics.Add(panoramic);
                    }
                }
            }
            catch(SqlException)
            {
                throw;
            }
            return panoramics;
        }
       
       
    

        private static PanoramicModel MapRowToPanoramic(SqlDataReader reader)
        {
            return new PanoramicModel
            {
                PanoramicId = Convert.ToInt32(reader["trail_id"]),
                ImageAddress = Convert.ToString(reader["image_address"]),
                Latitude = Convert.ToDouble(reader["latitude"]),
                Longitude = Convert.ToDouble(reader["longitude"]),
                NextPanoramic = Convert.ToInt32(reader["next_panoramic"]),
                PrevPanoramic = Convert.ToInt32(reader["prev_panoramic"])
            };
        }
    }
}