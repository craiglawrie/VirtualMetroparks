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
                    SqlCommand cmd = new SqlCommand(@"SELECT * FROM panoramic_images WHERE panoramic_image_id = @id;", conn);
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
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"SELECT pl.* 
                                                      FROM panoramic_linking pl 
                                                      WHERE pl.source_panoramic_id = @panoramicId", conn);
                    cmd.Parameters.AddWithValue("@panoramicId", panoramicId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
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

        public List<BackgroundSoundClip> GetBackgroundSoundClipsByPanoramicId(int panoramicId)
        {
            List<BackgroundSoundClip> backgroundSoundClips = new List<BackgroundSoundClip>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"SELECT ts.* 
                                                      FROM trail_sounds ts 
                                                      INNER JOIN sound_categories sc ON ts.sound_category_id = sc.sound_category_id
                                                      INNER JOIN trail_sounds_associative tsa ON tsa.sound_category_id = sc.sound_category_id 
                                                      WHERE tsa.panoramic_image_id = @panoramicId", conn);
                    cmd.Parameters.AddWithValue("@panoramicId", panoramicId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        BackgroundSoundClip soundClip = new BackgroundSoundClip()
                        {
                            AudioId = Convert.ToInt32(reader["trail_sound_id"]),
                            AudioAddress = Convert.ToString(reader["trail_sound_file"])

                        };

                        backgroundSoundClips.Add(soundClip);
                    }
                }
            }
            catch (SqlException e)
            {
                throw;
            }

            return backgroundSoundClips;
        }

        public List<BackgroundSoundClip> GetAllBackgroundSoundClips()
        {
            List<BackgroundSoundClip> backgroundSoundClips = new List<BackgroundSoundClip>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"SELECT * FROM trail_sounds", conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        BackgroundSoundClip soundClip = new BackgroundSoundClip()
                        {
                            AudioId = Convert.ToInt32(reader["trail_sound_id"]),
                            AudioAddress = Convert.ToString(reader["trail_sound_file"])
                        };

                        backgroundSoundClips.Add(soundClip);
                    }
                }
            }
            catch (SqlException e)
            {
                throw;
            }

            return backgroundSoundClips;

        }

        public bool AddVisitedPanoramicByUsername(int panoramicId, string userName)
        {
            bool result = false;

            if (!GetVisitedPanoramicsByUsername(userName).Select(pan => pan.PanoramicId).Contains(panoramicId))
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand(@"INSERT INTO user_panoramic_associative (panoramic_image_id, UserId) 
                                                      VALUES(@panoramicId, (SELECT UserId FROM Users WHERE UserName = @userName));", conn);
                        cmd.Parameters.AddWithValue("@panoramicId", panoramicId);
                        cmd.Parameters.AddWithValue("@userName", userName);

                        result = cmd.ExecuteNonQuery() == 1;
                    }
                }
                catch (SqlException)
                {
                    throw;
                }
            }

            return result;
        }

        public List<PanoramicModel> GetVisitedPanoramicsByUsername(string userName)
        {
            List<PanoramicModel> panoramics = new List<PanoramicModel>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"SELECT pi.* 
                                                      FROM panoramic_images pi 
                                                      JOIN user_panoramic_associative upa
                                                        ON upa.panoramic_image_id = pi.panoramic_image_id
                                                      JOIN Users u
                                                        ON u.UserId = upa.UserId
                                                      WHERE u.UserName = @userName;", conn);
                    cmd.Parameters.AddWithValue("@userName", userName);

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