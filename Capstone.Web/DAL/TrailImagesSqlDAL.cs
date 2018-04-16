using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;
using System.Data.SqlClient;

namespace Capstone.Web.DAL
{
    public class TrailImagesSqlDAL : ITrailImagesDAL
    {
        string connectionString;

        public TrailImagesSqlDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<TrailImagesModel> GetAllTrailImages()
        {
            List<TrailImagesModel> trailImages = new List<TrailImagesModel>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"SELECT * FROM trail_images", conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        TrailImagesModel trailImage = MapRowToTrailImages(reader);
                        trailImages.Add(trailImage);
                    }

                    return trailImages;
                }

            }
            catch (SqlException)
            {
                throw;
            }
        }

        public TrailImagesModel GetTrailImagesById(int id)
        {
            TrailImagesModel trailImage = new TrailImagesModel();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"SELECT * FROM trail_images WHERE trail_images.trail_image_id = @id;", conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        trailImage = MapRowToTrailImages(reader);
                    }
                    else
                    {
                        trailImage = null;
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return trailImage;
        }

        public List<TrailImagesModel> GetTrailImagesByTrailId(int trailId)
        {
            List<TrailImagesModel> trailImages = new List<TrailImagesModel>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"SELECT * FROM trail_images WHERE trail_images.trail_id = @trailId;", conn);
                    cmd.Parameters.AddWithValue("@trailId", trailId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        TrailImagesModel trailImage = MapRowToTrailImages(reader);

                        trailImages.Add(trailImage);
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
            return trailImages;
        }

        private static TrailImagesModel MapRowToTrailImages(SqlDataReader reader)
        {
            return new TrailImagesModel
            {
                TrailImageId = Convert.ToInt32(reader["trail_image_id"]),
                Bit = Convert.ToBoolean(reader["bit"]),
                ImageAddress = Convert.ToString(reader["image_address"])
            };
        }
    }
}