using System;
using System.Collections.Generic;
using Tatbikat.Operations;
using Xamarin.Forms;

namespace Tatbikat.ViewModels
{
    public class AddAppScreenViewModel : ViewModelsBase
    {
        public Command<string> SearchForAppCommand
        {
            get;
            set;
        }
        public AddAppScreenViewModel()
        {
            SearchForAppCommand = new Command<string>(SearchForAppCommandFunction);
        }

        private string _appSearchText;
        public string AppSearchText
        {
            get { return _appSearchText; }
            set { RefreshUIProperty(ref _appSearchText, value); }
        }

        private async void SearchForAppCommandFunction(string appname)
        {
            if (string.IsNullOrWhiteSpace(appname))
            {
                return;
            }
            var searchParams = $"/search?term={appname}&country=sa&entity=software";
            Models.InternalIOSApp app =await Connector.Current.GetAppsFromiOSStore(searchParams);
            
        }
    }
}
