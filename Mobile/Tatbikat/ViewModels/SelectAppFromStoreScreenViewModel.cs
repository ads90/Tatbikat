using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tatbikat.Models;
using Tatbikat.Operations;
using Tatbikat.UI.Enums;
using Tatbikat.UI.Interfaces;
using Xamarin.Forms;

namespace Tatbikat.ViewModels
{
    public class SelectAppFromStoreScreenViewModel : ViewModelsBase, ICallbackEnabledScreen<TatbikatApp>
    {
        PlatformType _platformType;
        TaskCompletionSource<TatbikatApp> _pageTcs;
        public Command<string> SearchForAppCommand
        {
            get;
            set;
        }
        public SelectAppFromStoreScreenViewModel(PlatformType platformType)
        {
            SearchForAppCommand = new Command<string>(SearchForAppCommandFunction); 
            _pageTcs = new TaskCompletionSource<TatbikatApp>();
            _platformType = platformType;
        }

        private string _appSearchText;
        public string AppSearchText
        {
            get { return _appSearchText; }
            set { RefreshUIProperty(ref _appSearchText, value); }
        }
        private List<TatbikatApp> _appSearchResult = new List<TatbikatApp>();
        public List<TatbikatApp> AppSearchResult
        {
            get { return _appSearchResult; }
            set { RefreshUIProperty(ref _appSearchResult, value); }
        }
        private TatbikatApp _selectedApp;
        public TatbikatApp SelectedApp
        {
            get { return _selectedApp; }
            set
            {
                RefreshUIProperty(ref _selectedApp, value);
                OnSelectedApp();
            }
        }

        private void OnSelectedApp()
        {
            if (SelectedApp == null)
            {
                return;
            }
            _pageTcs?.TrySetResult((TatbikatApp)SelectedApp);
        }

        private async void SearchForAppCommandFunction(string appname)
        {
            if (string.IsNullOrWhiteSpace(appname))
            {
                return;
            }
            IsLoading = true;
            var searchParams = $"/search?term={appname}&country=sa&entity=software";
            var result = await Connector.Current.GetAppsFromiOSStore(searchParams);
            List<TatbikatApp> castedResult = (List<TatbikatApp>)result;
            AppSearchResult = castedResult;
            IsLoading = false;
        }

        public Task<TatbikatApp> Wait()
        {
            return _pageTcs.Task;
        }
    }
}
