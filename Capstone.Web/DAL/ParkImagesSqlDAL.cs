using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;
using System.Data.SqlClient;

namespace Capstone.Web.DAL
{
    public class ParkImagesSqlDAL : IParkImagesDAL
    {
        string connectionString;

        public ParkImagesSqlDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<ParkImagesModel> GetAllParkImages()
        {
            List<ParkImagesModel> parkImages = new List<ParkImagesModel>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"SELECT * FROM park_images", conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        ParkImagesModel parkImage = MapRowToParkImages(reader);
                        parkImages.Add(parkImage);
                    }

                    return parkImages;
                }

            }
            catch (SqlException)
            {
                throw;
            }
        }

        public ParkImagesModel GetParkImagesById(int id)
        {
            ParkImagesModel parkImage = new ParkImagesModel();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"SELECT * FROM park_images WHERE park_images.park_images_id = @id;", conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        parkImage = MapRowToParkImages(reader);
                    }
                    else
                    {
                        parkImage = null;
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return parkImage;
        }

        public List<ParkImagesModel> GetParkImagesByParkId(int parkId)
        {
            List<ParkImagesModel> parkImages = new List<ParkImagesModel>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"SELECT * FROM park_images WHERE park_images.park_id = @parkId;", conn);
                    cmd.Parameters.AddWithValue("@parkId", parkId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        ParkImagesModel parkImage = MapRowToParkImages(reader);

                        parkImages.Add(parkImage);
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
            return parkImages;
        }

        private static ParkImagesModel MapRowToParkImages(SqlDataReader reader)
        {
            return new ParkImagesModel
            {
                ParkImageId = Convert.ToInt32(reader["park_image_id"]),
                Bit = Convert.ToBoolean(reader["bit"]),
                ImageAddress = Convert.ToString(reader["image_address"])
            };
        }
    }
}