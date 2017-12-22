using System;
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
            //if(string.IsNullOrWhiteSpace(appname))
            //{
            //    return;
            //}
            var app =await Connector.Current.GetAppsFromiOSStore();
            
        }
    }
}
