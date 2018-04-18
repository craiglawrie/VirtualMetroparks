using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;
using Capstone.Web.DAL;
using System.Data.SqlClient;

namespace Capstone.Web.DAL
{
    public class LastSeenVideosSqlDAL : ILastSeenVideosDAL
    {
        string connectionString;

        public LastSeenVideosSqlDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<LastSeenVideosModel> GetAllLastSeenVideos()
        {
            List<LastSeenVideosModel> lastSeenVideos = new List<LastSeenVideosModel>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"SELECT * FROM last_seen_videos", conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        LastSeenVideosModel lastSeenVideo = MapRowToLastSeenVideos(reader);
                        lastSeenVideos.Add(lastSeenVideo);
                    }

                    return lastSeenVideos;
                }

            }
            catch (SqlException)
            {
                throw;
            };
        }

        public LastSeenVideosModel GetLastSeenVideosById(int id)
        {
            LastSeenVideosModel lastSeenVideo = new LastSeenVideosModel();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"SELECT * FROM last_seen_videos WHERE last_seen_videos.last_seen_videos_id = @id;", conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        lastSeenVideo = MapRowToLastSeenVideos(reader);
                    }
                    else
                    {
                        lastSeenVideo = null;
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return lastSeenVideo;
        }

        public List<LastSeenVideosModel> GetLastSeenVideosByParkId(int parkId)
        {
            List<LastSeenVideosModel> lastSeenVideos = new List<LastSeenVideosModel>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"SELECT * FROM last_seen_videos 
                                                      INNER JOIN panoramic_images 
                                                      ON last_seen_videos.panoramic_image_id = panoramic_images.panoramic_image_id 
                                                      INNER JOIN trails 
                                                      ON panoramic_images.trail_id = trails.trail_id
                                                      INNER JOIN parks 
                                                      ON trails.park_id = parks.park_id
                                                      WHERE parks.park_id = @parkId;", conn);
                    cmd.Parameters.AddWithValue("@parkId", parkId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        LastSeenVideosModel lastSeenVideo = MapRowToLastSeenVideos(reader);

                        lastSeenVideos.Add(lastSeenVideo);
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
            return lastSeenVideos;
        }

        public List<LastSeenVideosModel> GetLastSeenVideosByTrailId(int trailId)
        {
            List<LastSeenVideosModel> lastSeenVideos = new List<LastSeenVideosModel>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"SELECT * FROM last_seen_videos 
                                                      INNER JOIN panoramic_images 
                                                      ON last_seen_videos.panoramic_image_id = panoramic_images.panoramic_image_id  
                                                      WHERE panoramic_images.trail_id = @trailId;", conn);
                    cmd.Parameters.AddWithValue("@trailId", trailId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        LastSeenVideosModel lastSeenVideo = MapRowToLastSeenVideos(reader);

                        lastSeenVideos.Add(lastSeenVideo);
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
            return lastSeenVideos;
        }

        public List<LastSeenVideosModel> GetLastSeenVideosByPanoramicId(int panoramicId)
        {
            List<LastSeenVideosModel> lastSeenVideos = new List<LastSeenVideosModel>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"SELECT * FROM last_seen_videos                                                         
                                                      WHERE last_seen_videos.panoramic_image_id = @panoramicId;", conn);
                    cmd.Parameters.AddWithValue("@panoramicId", panoramicId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        LastSeenVideosModel lastSeenVideo = MapRowToLastSeenVideos(reader);

                        lastSeenVideos.Add(lastSeenVideo);
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
            return lastSeenVideos;
        }

        private static LastSeenVideosModel MapRowToLastSeenVideos(SqlDataReader reader)
        {
            return new LastSeenVideosModel
            {
                LastSeenVideosId = Convert.ToInt32(reader["last_seen_videos_id"]),
                Description = Convert.ToString(reader["description"]),
                VideoAddress = Convert.ToString(reader["video_address"]),
                Title = Convert.ToString(reader["title"]),
                Pitch = Convert.ToInt32(reader["pitch"]),
                Yaw = Convert.ToInt32(reader["yaw"]),
                HasSound = Convert.ToBoolean((reader["has_sound"] as bool?) ?? false),
                Duration = Convert.ToDouble(reader["duration"] as double? ?? 0),
                Volume = Convert.ToInt32(reader["volume"] as int? ?? 0)
            };
        }
    }
}