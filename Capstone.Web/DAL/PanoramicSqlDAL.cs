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
                    SqlCommand cmd = new SqlCommand(@"SELECT * FROM panoramic_images;", conn);
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
                    SqlCommand cmd = new SqlCommand(@"SELECT * FROM panoramic_images WHERE panoramic_id = @id;", conn);
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

            return panoramic;
        }

        public List<PanoramicModel> GetPanoramicsByTrailId(int trailId)
        {
            List<PanoramicModel> panoramics = new List<PanoramicModel>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"SELECT * FROM panoramic_images WHERE trail_id = @trailId;", conn);
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

        public List<PanoramicModel> GetPanoramicsByTrailName(string name)
        {
            List<PanoramicModel> panoramics = new List<PanoramicModel>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"SELECT p.* 
                                                      FROM panoramic_images p 
                                                      INNER JOIN trails 
                                                        ON p.trail_id = trails.trail_id 
                                                      WHERE trails.trail_name = @trailName;", conn);
                    cmd.Parameters.AddWithValue("@trailName", name);
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

        public PanoramicModel GetTrailHeadByTrailId(int trailId)
        {
            PanoramicModel panoramic = new PanoramicModel();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"SELECT * FROM panoramic_images p WHERE p.trail_id = @id AND p.is_trail_head = 1;", conn);
                    cmd.Parameters.AddWithValue("@id", trailId);
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

            return panoramic;
        }

        public List<PanoramicModel> GetPointsOfInterestByTrailId(int trailId)
        {
            List<PanoramicModel> panoramics = new List<PanoramicModel>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"SELECT pi.* 
                                                      FROM panoramic_images pi 
                                                      JOIN points_of_interest poi 
                                                          ON pi.panoramic_image_id = poi.panoramic_image_id 
                                                      WHERE poi.trail_id = @trail_id", conn);
                    cmd.Parameters.AddWithValue("@trail_id", trailId);
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

        public List<TourConnection> GetConnectionsByPanoramicId(int panoramicId)
        {
            List<TourConnection> connections = new List<TourConnection>();

            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"SELECT pl.* 
                                                      FROM panoramic_linking pl 
                                                      WHERE pl.source_panoramic_id = @panoramicId", conn);
                    cmd.Parameters.AddWithValue("@panoramicId", panoramicId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while(reader.Read())
                    {
                        TourConnection connection = new TourConnection()
                        {
                            DestinationId = Convert.ToInt32(reader["destination_panoramic_id"]),
                            HotspotPitch = Convert.ToInt32(reader["hotspot_pitch"]),
                            HotspotYaw = Convert.ToInt32(reader["hotspot_yaw"]),
                            DestinationStartYaw = Convert.ToInt32(reader["destination_start_yaw"])
                        };

                        connections.Add(connection);
                    }
                }
            }
            catch (SqlException e)
            {
                throw;
            }

            return connections;
        }

        private static PanoramicModel MapRowToPanoramic(SqlDataReader reader)
        {
            return new PanoramicModel
            {
                PanoramicId = Convert.ToInt32(reader["panoramic_image_id"]),
                ImageAddress = Convert.ToString(reader["image_address"]),
                Latitude = Convert.ToDouble(reader["image_latitude"]),
                Longitude = Convert.ToDouble(reader["image_longitude"])
            };
        }
    }
}