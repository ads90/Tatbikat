using System;
using Android.Net;
using Tatbikat.Droid.PlatformServices;
using Tatbikat.UI.Interfaces;
using Android.Content;
using Xamarin.Forms;
[assembly: Xamarin.Forms.Dependency(typeof(InternetStatusService))]
namespace Tatbikat.Droid.PlatformServices
{
    public class InternetStatusService : IInternetStatusService
    {
        private ConnectivityManager _connectivityManager;

        public InternetStatusService()
        {

            if (_connectivityManager == null)
            {
                _connectivityManager = (ConnectivityManager)Android.App.Application.Context.GetSystemService(Context.ConnectivityService);
            }

        }

        public bool IsConnected()
        {
            NetworkInfo activeConnection = _connectivityManager.ActiveNetworkInfo;
            return (activeConnection != null) && activeConnection.IsConnected;

        }

    }
}
