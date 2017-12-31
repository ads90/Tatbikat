using System;
namespace Tatbikat.Operations
{
    public static class Endpoints
    {
        public const string IOSStoreSearch = "https://itunes.apple.com";
        public const string API_BaseUrl = "http://tatbikat.azurewebsites.net/api/";

        public class Apps
        {
            public const string GetAllApps = "App/GetAllApps";
            public const string PostNewApp = "App/AddApp";
        }
    }
}
