using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;
using System.Data.SqlClient;

/*Check the database before executing this code*/

namespace Capstone.Web.DAL
{
    public class AdminNewHotspotSqlDAL : IAdminNewHotspotDAL
    {
        string connectionString;

        public AdminNewHotspotSqlDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public bool SaveNewHotspot(AdminNewHotspotModel addHotspot)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO add_hotspots VALUES (@title, @description, @mediaTypes, @url);", conn);
                    cmd.Parameters.AddWithValue("@title", addHotspot.Title);
                    cmd.Parameters.AddWithValue("@description", addHotspot.Description);
                    cmd.Parameters.AddWithValue("@mediaType", addHotspot.MediaType);
                    cmd.Parameters.AddWithValue("@url", addHotspot.URL);

                    cmd.ExecuteNonQuery();

                    return true;
                }
            }
            catch(SqlException)
            {
                return false;
            }
        }
    }
}