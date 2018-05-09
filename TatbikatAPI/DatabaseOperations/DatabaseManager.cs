using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
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
            GetSpeceficAppFromiOSStore =>parms(appname,platform)
            GetSpeceficAppFromiOSAndroidStore(appname, platform)
            PostNewApp
            */
        //private List<TatbikatApp> _tatbikatApps;

        //public List<TatbikatApp> TatbikatApps
        //{
        //    get { return _tatbikatApps; }
        //    set { _tatbikatApps = value; }
        //}

        public async System.Threading.Tasks.Task<List<TatbikatApp>> GetAllApps()
        {
            List<TatbikatApp> _tatbikatApp = new List<TatbikatApp>();
            using (SqlConnection sqlConn = new SqlConnection(_mainDatabaseConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand("[dbo].[GetAllAppsWithCategories]", sqlConn))
                {

                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlConn.Open();
                    sqlCommand.ExecuteNonQuery();

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        StringBuilder sb = new StringBuilder(32767);

                        string JSONString = string.Empty;
                        while (await reader.ReadAsync())
                        {
                            sb.Append(reader.GetString(0));
                            JSONString += reader.GetValue(0);
                        }
                        //.Where(a => a.IsVerified)
                        _tatbikatApp = string.IsNullOrEmpty(JSONString) ? new List<TatbikatApp>() : JsonConvert.DeserializeObject<List<TatbikatApp>>(sb.ToString()).ToList();
                    }
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
                        string JSONString = string.Empty;
                        while (reader.Read())
                        {
                            JSONString += reader.GetString(0);
                        }
                        _categortries = JsonConvert.DeserializeObject<IList<Category>>(JSONString).ToList();//.Where(c=>c.SubCategory.ToList());
                    }
                    sqlConn.Close();
                }
            }

            return _categortries;
        }

        public async void PostNewApp(TatbikatApp app)
        {
            var _tatbikatApp = await GetAllApps();
            if (_tatbikatApp.Exists(t => t.IosAppID == app.IosAppID || t.AndroidAppID == app.AndroidAppID))
            {
                return;
            }
            using (SqlConnection sqlconn = new SqlConnection(_mainDatabaseConnectionString))
            {
                //                string inserQuery=$"BEGIN TRANSACTION insert into app values({app.Name},{app.Image},{app.IOSStoreLink},{app.AndroidStoreLink},GETDATE(),0,null)" +
                //                    "declare @lastid int=SCOPE_IDENTITY()
                //insert into appcategory values(SCOPE_IDENTITY(),2)"
                //  using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlconn.Open();
                    SqlCommand sqlCommand = sqlconn.CreateCommand();
                    SqlTransaction transaction;
                    transaction = sqlconn.BeginTransaction("SampleTransaction");

                    sqlCommand.Transaction = transaction;

                    try
                    {
                        sqlCommand.CommandText = "insert into app values(@AppName,@AppImage,null,null,GETDATE(),0,null)";
                        sqlCommand.Parameters.AddWithValue("@AppName", app.Name);
                        sqlCommand.Parameters.AddWithValue("@AppImage", app.Image);
                        sqlCommand.Parameters.AddWithValue("@IosAppID", app.IosAppID);
                        sqlCommand.Parameters.AddWithValue("@AndroidAppID", app.AndroidAppID);
                        sqlCommand.ExecuteNonQuery();
                        //  SCOPE_IDENTITY() returns null after paramerized INSERT so @@IDENTITY fix my issue
                        sqlCommand.CommandText = "select @@IDENTITY";
                        var uniqueAppID = sqlCommand.ExecuteScalar();

                        foreach (var category in app.Category)
                        {
                            sqlCommand.CommandText = $"insert into appcategory values({uniqueAppID},{category.Id})";
                            sqlCommand.ExecuteNonQuery();
                        }

                        // Attempt to commit the transaction.
                        transaction.Commit();
                        Console.WriteLine("successed written to database.");


                    }

                    catch (Exception ex)
                    {
                        Console.WriteLine("Commit Exception Type: {0}", ex.GetType());
                        Console.WriteLine("  Message: {0}", ex.Message);

                        // Attempt to roll back the transaction.
                        try
                        {
                            transaction.Rollback();
                        }
                        catch (Exception ex2)
                        {
                            // This catch block will handle any errors that may have occurred
                            // on the server that would cause the rollback to fail, such as
                            // a closed connection.
                            Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
                            Console.WriteLine("  Message: {0}", ex2.Message);
                        } 
                    }
                }
            }


        }
    }
}
