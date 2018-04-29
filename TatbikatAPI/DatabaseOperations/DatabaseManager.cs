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
            GetSpeceficAppFromiOSStore =>parms(appname,platform)
            GetSpeceficAppFromiOSAndroidStore(appname, platform)
            PostNewApp
            */
        private List<TatbikatApp> _tatbikatApps;

        public List<TatbikatApp> TatbikatApps
        {
            get { return _tatbikatApps; }
            set { _tatbikatApps = value; }
        }

        public List<TatbikatApp> GetAllApps()
        {
            //List<TatbikatApp> _tatbikatApp = new List<TatbikatApp>();
            using (SqlConnection sqlConn = new SqlConnection(_mainDatabaseConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand("[dbo].[GetAllAppsWithCategories]", sqlConn))
                {

                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlConn.Open();
                    sqlCommand.ExecuteNonQuery();

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        string JSONString = string.Empty;
                        while (reader.Read())
                        { 
                            JSONString += reader.GetString(0);
                        }

                        TatbikatApps = string.IsNullOrEmpty(JSONString) ? new List<TatbikatApp>() : JsonConvert.DeserializeObject<List<TatbikatApp>>(JSONString).Where(a=>a.IsVerified).ToList();
                    }
                    sqlConn.Close();
                }
            }

            return TatbikatApps;
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
        public void PostNewApp(TatbikatApp app)
        {
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
                        if (TatbikatApps.Exists(t => t.IDForIOSApp== app.IDForIOSApp ||t.IDForAndroidApp==app.IDForAndroidApp))
                        {
                            transaction.Rollback();
                            sqlconn.Close();
                            return;
                        }
                        sqlCommand.CommandText = $"insert into app values('{app.Name}','{app.Image}','{app.IosUrl}','{app.AndroidUrl}',GETDATE(),0,null)";
                        sqlCommand.ExecuteNonQuery();

                        sqlCommand.CommandText = "select SCOPE_IDENTITY()";
                        var uniqueAppID = sqlCommand.ExecuteScalar();

                        foreach (var category in app.Category)
                        {
                            sqlCommand.CommandText = $"insert into appcategory values({uniqueAppID},{category.Id})";
                        }

                        // Attempt to commit the transaction.
                        transaction.Commit();
                        Console.WriteLine("successed written to database.");

                        sqlconn.Close();
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
                        sqlconn.Close();
                    }

                    sqlconn.Open();
                    sqlCommand.ExecuteNonQuery();
                }
            }
        }
    }
}
