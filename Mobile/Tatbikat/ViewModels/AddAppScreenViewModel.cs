using System;
using System.Collections.Generic;
using Tatbikat.Models;
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
        private List<TatbikatApp> _appSearchResult=new List<TatbikatApp>();
        public List<TatbikatApp> AppSearchResult
        {
            get { return _appSearchResult; }
            set { RefreshUIProperty(ref _appSearchResult, value); }
        }
        //public List<TatbikatApp> AppSearchResult
        //{
        //    get;
        //    set;
        //}
        private async void SearchForAppCommandFunction(string appname)
        {
            if (string.IsNullOrWhiteSpace(appname))
            {
                return;
            }
            IsLoading=true;
            var searchParams = $"/search?term={appname}&country=sa&entity=software";
            var result =await Connector.Current.GetAppsFromiOSStore(searchParams);
            List<TatbikatApp> castedResult= (List<TatbikatApp>)result;
            AppSearchResult = castedResult;
            IsLoading = false;
        }
    }
}
