using System;
using Plugin.Connectivity;
using Tatbikat.iOS.PlatformServices;
using Tatbikat.UI.Interfaces;
[assembly: Xamarin.Forms.Dependency(typeof(InternetStatusService))]

namespace Tatbikat.iOS.PlatformServices
{
    public class InternetStatusService: IInternetStatusService
    {

        public bool IsConnected()
        {

            return CrossConnectivity.Current.IsConnected;

        }

    }
}