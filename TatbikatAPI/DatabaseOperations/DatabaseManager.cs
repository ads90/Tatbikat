using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;  
using TatbikatAPI.Models;

namespace TatbikatAPI.DatabaseOperations
{
    public partial class DatabaseManager
    {
        private readonly string _mainDatabaseConnectionString;

        public DatabaseManager(string MainDatabaseConnectionString)
        {
            _mainDatabaseConnectionString = MainDatabaseConnectionString;

        }
        /*
            TODOs
            GetAllApps Done
            GetAllCategories
            GetSpeceficAppFromiOSStore =>parms(appname,platform)
            GetSpeceficAppFromiOSAndroidStore(appname, platform)
            PostNewApp
            */
        public List<TatbikatApp> GetAllApps()
        {
            List<TatbikatApp> _tatbikatApp = new List<TatbikatApp>();
            using (SqlConnection sqlConn = new SqlConnection(_mainDatabaseConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand("[dbo].[GetAllAppsWithCategories]", sqlConn))
                {

                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlConn.Open();
                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string JSONString = reader.GetString(0);
                            _tatbikatApp = JsonConvert.DeserializeObject<IList<TatbikatApp>>(JSONString).ToList();
                        }
                    }
                    sqlConn.Close();
                }
            }

            return _tatbikatApp;
        }
        public List<Category> GetAllCategortries()
        {
            List<Category> _categortries = new List<Category>();
            using (SqlConnection sqlConn = new SqlConnection(_mainDatabaseConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand("[dbo].[GetAllCategortries]", sqlConn))
                {

                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlConn.Open();
                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string JSONString = string.Empty;
                            _categortries = JsonConvert.DeserializeObject<IList<Category>>(reader.GetString(0)).ToList();//.Where(c=>c.SubCategory.ToList());
                        }
                    }
                    sqlConn.Close();
                }
            }

            return _categortries;
        }
    }
}
