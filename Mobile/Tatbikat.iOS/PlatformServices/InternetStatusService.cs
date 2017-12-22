using System;
using Plugin.Connectivity;
using Tatbikat.UI.Interfaces;

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