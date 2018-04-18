using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;
using Capstone.Web.DAL;
using System.Data.SqlClient;

namespace Capstone.Web.DAL
{
    public class LastSeenImagesSqlDAL : ILastSeenImagesDAL
    {
        string connectionString;

        public LastSeenImagesSqlDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<LastSeenImagesModel> GetAllLastSeenImages()
        {
            List<LastSeenImagesModel> lastSeenImages = new List<LastSeenImagesModel>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"SELECT * FROM last_seen_images", conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        LastSeenImagesModel lastSeenImage = MapRowToLastSeenImages(reader);
                        lastSeenImages.Add(lastSeenImage);
                    }

                    return lastSeenImages;
                }

            }
            catch (SqlException)
            {
                throw;
            };
        }

        public LastSeenImagesModel GetLastSeenImagesById(int id)
        {
            LastSeenImagesModel lastSeenImage = new LastSeenImagesModel();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"SELECT * FROM last_seen_images WHERE last_seen_images.last_seen_images_id = @id;", conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        lastSeenImage = MapRowToLastSeenImages(reader);
                    }
                    else
                    {
                        lastSeenImage = null;
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return lastSeenImage;
        }

        public List<LastSeenImagesModel> GetLastSeenImagesByParkId(int parkId)
        {
            List<LastSeenImagesModel> lastSeenImages = new List<LastSeenImagesModel>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"SELECT * FROM last_seen_images 
                                                      INNER JOIN panoramic_images 
                                                      ON last_seen_images.panoramic_image_id = panoramic_images.panoramic_image_id 
                                                      INNER JOIN trails 
                                                      ON panoramic_images.trail_id = trails.trail_id
                                                      INNER JOIN parks 
                                                      ON trails.park_id = parks.park_id
                                                      WHERE parks.park_id = @parkId;", conn);
                    cmd.Parameters.AddWithValue("@parkId", parkId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        LastSeenImagesModel lastSeenImage = MapRowToLastSeenImages(reader);

                        lastSeenImages.Add(lastSeenImage);
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
            return lastSeenImages;
        }

        public List<LastSeenImagesModel> GetLastSeenImagesByTrailId(int trailId)
        {
            List<LastSeenImagesModel> lastSeenImages = new List<LastSeenImagesModel>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"SELECT * FROM last_seen_images 
                                                      INNER JOIN panoramic_images 
                                                      ON last_seen_images.panoramic_image_id = panoramic_images.panoramic_image_id  
                                                      WHERE panoramic_images.trail_id = @trailId;", conn);
                    cmd.Parameters.AddWithValue("@trailId", trailId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        LastSeenImagesModel lastSeenImage = MapRowToLastSeenImages(reader);

                        lastSeenImages.Add(lastSeenImage);
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
            return lastSeenImages;
        }

        public List<LastSeenImagesModel> GetLastSeenImagesByPanoramicId(int panoramicId)
        {
            List<LastSeenImagesModel> lastSeenImages = new List<LastSeenImagesModel>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"SELECT * FROM last_seen_images                                                         
                                                      WHERE last_seen_images.panoramic_image_id = @panoramicId;", conn);
                    cmd.Parameters.AddWithValue("@panoramicId", panoramicId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        LastSeenImagesModel lastSeenImage = MapRowToLastSeenImages(reader);

                        lastSeenImages.Add(lastSeenImage);
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
            return lastSeenImages;
        }

        private static LastSeenImagesModel MapRowToLastSeenImages(SqlDataReader reader)
        {
            return new LastSeenImagesModel
            {
                LastSeenImagesId = Convert.ToInt32(reader["last_seen_images_id"]),
                Description = Convert.ToString(reader["description"]),
                ImageAddress = Convert.ToString(reader["image_address"]),
                Title = Convert.ToString(reader["title"]),
                Pitch = Convert.ToInt32(reader["pitch"]),
                Yaw = Convert.ToInt32(reader["yaw"])
            };
        }
    }

}