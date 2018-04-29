using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Tatbikat.Models;
using Tatbikat.Operations;
using Tatbikat.Views;
using Xamarin.Forms;

namespace Tatbikat.ViewModels
{
    public class MainPageViewModel : ViewModelsBase
    {
        public Command AddAppCommand { get; set; }
        public Command SelectCategoriesCommand { get; set; }
        public Command RefreshAppsListCommand { get; set; }

        public MainPageViewModel()
        {
            SelectCategoriesCommand = new Command(SelectCategoriesCommandFunctionAsync);
            AddAppCommand = new Command(AddAppCommandFunction);
            GetAppsAsync();
            RefreshAppsListCommand = new Command(() => GetAppsAsync());

        }



        private async void SelectCategoriesCommandFunctionAsync()
        {
            Page page = new CategoriesSelectionScreen(false);
            List<Category> result = await NavigateForResultAsync<List<Category>>(page);

            if (result == null || result.Count() == 0)
            {
                //return to select all apps
                FilteredApps = this.Apps;
                return;
            }
            FilteredApps = new List<TatbikatApp>();
            foreach (Category cat in result)
            {
                //    FilteredApps = this.Apps.ta(a=>a.AppCategories.Exists(app=>app.ID==cat.ID)).ToList();
                var items = Apps.Where((a) => a.AppCategories.Any(c => c.ID == cat.ID));
                if (items != null)
                {
                    FilteredApps.AddRange(items);
                }
            }
        }

        private void GetAppsAsync()
        {
            IsLoading = true;
            Device.BeginInvokeOnMainThread(async () =>
            {
                Apps = await Connector.Current.GetApps();
            });
            IsLoading = false;

        }

        private async void AddAppCommandFunction()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new AddAppScreen());
        }

        private List<TatbikatApp> _apps;
        public List<TatbikatApp> Apps
        {
            get
            {
                return _apps;
            }
            set
            {
                RefreshUIProperty(ref _apps, value);
                FilteredApps = Apps;
            }
        }
        private TatbikatApp _selectedApp;

        public TatbikatApp SelectedApp
        {
            get { return _selectedApp; }
            set
            {
                RefreshUIProperty(ref _selectedApp, value);
                if (_selectedApp != null)
                {
                    string urlStore;
                    if (Device.RuntimePlatform == Device.Android)
                    {
                        urlStore = "https://play.google.com/store/apps/details?id=" + SelectedApp.AndroidAppID; 
                    }
                    else
                    {
                        urlStore = "https://itunes.apple.com/sa/app/id" + SelectedApp.IosAppID;
                    }
                    //https://play.google.com/store/apps/details?id=com.tantumsoft.zind
                    //https://itunes.apple.com/sa/app/optio-win-vouchers-and-deals/id1189168698?mt=8&uo=4

                    Device.OpenUri(new Uri(urlStore));
                }
            }
        }

        private List<TatbikatApp> _filteredApps;
        public List<TatbikatApp> FilteredApps
        {
            get
            {
                return _filteredApps;
            }
            set
            {
                RefreshUIProperty(ref _filteredApps, value);
            }
        }
    }
}
