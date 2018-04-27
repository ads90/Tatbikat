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
        public Command AddAppCommand
        {
            get;
            set;
        }
        public Command SelectCategoriesCommand { get; set; }
        public MainPageViewModel()
        {
            SelectCategoriesCommand = new Command(SelectCategoriesCommandFunctionAsync);
            AddAppCommand = new Command(AddAppCommandFunction);
            GetAppsAsync();


        }

        private async void SelectCategoriesCommandFunctionAsync()
        {
            Page page = new CategoriesSelectionScreen(false);
            List<Category> result = await NavigateForResultAsync<List<Category>>(page);
            FilteredApps = new List<TatbikatApp>();
            if(result==null||result.Count()==0)
            {
                return;
            }
            foreach (Category cat in result)
            {
                //    FilteredApps = this.Apps.ta(a=>a.AppCategories.Exists(app=>app.ID==cat.ID)).ToList();
                var item = Apps.Where((a) => a.AppCategories.Any(c => c.ID ==cat.ID)).FirstOrDefault();
                if (item != null)
                {
                    FilteredApps.Add(item);
                }
            }
        }

        private async void GetAppsAsync()
        {
            IsLoading = true;
            Apps = await Connector.Current.GetApps();
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
