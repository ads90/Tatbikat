using System;
namespace Tatbikat.Operations
{
    public static class Endpoints
    {
        public const string IOSStoreSearch = "https://itunes.apple.com";
        public const string API_BaseUrl = "http://tatbikat.azurewebsites.net/api/";

        public class Apps
        {
            public const string GetAllApps = "GetAllApps";
            public const string GetAllCategories = "GetAllCategortries";
            public const string PostNewApp = "AddApp";

        }
    }
}
