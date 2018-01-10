using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tatbikat.Models;
using Tatbikat.Models.Enums;
using Tatbikat.UI.Interfaces;
using Tatbikat.Views;
using Xamarin.Forms;

namespace Tatbikat.ViewModels
{
    class AddAppScreenViewModel : ViewModelsBase
    {
        public Command AddiOSAppCommand { get; set; }
        public Command AddAndroidAppCommand { get; set; }
        public Command SelectCategoriesCommand { get; set; }
        public AddAppScreenViewModel()
        {
            AddAndroidAppCommand=new Command(AddAndroidAppCommandFunction);
            AddiOSAppCommand = new Command(AddiOSAppCommandFunction);
            SelectCategoriesCommand = new Command(SelectCategoriesCommandFunction);
        }
        private string _appSearchiOSText;
        public string AppSearchiOSText
        {
            get { return _appSearchiOSText; }
            set { RefreshUIProperty(ref _appSearchiOSText, value); }

        }
        private string _appSearchAndroidText;
        public string AppSearchAndroidText
        {
            get { return _appSearchAndroidText; }
            set { RefreshUIProperty(ref _appSearchAndroidText, value); }

        }
        private List<Category> _appCategories;
        public List<Category> AppCategories
        {
            get { return _appCategories; }
            set { RefreshUIProperty(ref _appCategories, value); }

        }
        
        private async void AddAndroidAppCommandFunction()
        {
            Page page = new SelectAppFromStoreScreen(PlatformType.Android);
            var result = await NavigateForResultAsync<TatbikatApp>(page);
            AppSearchAndroidText = result == null ? "" : result.Name;
        }
       
        private async void AddiOSAppCommandFunction()
        {
            Page page = new SelectAppFromStoreScreen(PlatformType.iOS);
            var result=await NavigateForResultAsync<TatbikatApp>(page);
            AppSearchiOSText = result == null ? "" : result.Name;
        }
        //async Task<TatbikatApp> NavigateForResultAsync(Page page)
        //{
        //    IsLoading = true;
        //    await Application.Current.MainPage.Navigation.PushModalAsync(page);
        //    TatbikatApp result = await (page.BindingContext as ICallbackEnabledScreen<TatbikatApp>).Wait();
        //    await Application.Current.MainPage.Navigation.PopModalAsync();
        //    IsLoading = false;
        //    return result;
        //}
        private async void SelectCategoriesCommandFunction()
        {
            //await Application.Current.MainPage.Navigation.PushAsync(new CategoriesSelectionScreen(true));
            Page page = new CategoriesSelectionScreen(true);
            var result = await NavigateForResultAsync<List<Category>>(page);
            AppCategories = result;
        }

       
    }
}
