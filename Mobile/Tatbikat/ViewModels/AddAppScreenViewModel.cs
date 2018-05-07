using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tatbikat.Models;
using Tatbikat.Models.Enums;
using Tatbikat.Operations;
using Tatbikat.UI.Interfaces;
using Tatbikat.Views;
using Xamarin.Forms;

namespace Tatbikat.ViewModels
{
    class AddAppScreenViewModel : ViewModelsBase
    {
        public Command AddiOSAppCommand { get; set; }
        public Command SelectCategoriesCommand { get; set; }
        public Command SumbitNewAppCommand { get; set; }
        public AddAppScreenViewModel()
        {
            AddiOSAppCommand = new Command(AddiOSAppCommandFunction);
            SelectCategoriesCommand = new Command(SelectCategoriesCommandFunction);
            SumbitNewAppCommand = new Command(SumbitNewAppCommandFunction);
        }
        public TatbikatApp AndroidApp { get; set; }
        public TatbikatApp IOSApp { get; set; }

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
            set
            {
                RefreshUIProperty(ref _appCategories, value);
                if (value != null && value.Count > 0)
                {
                    ShouldReceiveTouch = false;
                }
            }
        }

        private bool _shouldReceiveTouch = true;
        public bool ShouldReceiveTouch
        {
            get { return _shouldReceiveTouch; }
            set { RefreshUIProperty(ref _shouldReceiveTouch, value); }
        }

        private async void SumbitNewAppCommandFunction()
        {
            if (IOSApp?.IosAppID == null || AndroidApp?.AndroidAppID == null || AppCategories == null)
            {
                await App.Current.MainPage.DisplayAlert("خطا", "الرجاء ادخال كافة البيانات", "موافق");
                return;
            }
            IsLoading = true;
            IOSApp.AppCategories = new List<Category>();
            foreach(var category in AppCategories)
            {
                IOSApp.AppCategories.Add(category);
            }
            //note here i'm taking iOS app to be submitted as final app and add android line to it
            IOSApp.AndroidAppID = AndroidApp.AndroidAppID;
            //here must add the datetime from back end
            IOSApp.DateAdded = DateTime.Now;
            await Connector.Current.PostNewApp(IOSApp);
            IsLoading = false;
            await App.Current.MainPage.DisplayAlert("تم ارسال التطبيق", "سيتم عرض التطبيق لدينا بعد التاكد من صحة البيانات", "موافق");
            await App.Current.MainPage.Navigation.PopAsync();
        }

        private async Task AddAndroidAppCommandFunction(string appName)
        {
            Page page = new SelectAppFromStoreScreen(PlatformType.Android,appName);
            AndroidApp = await NavigateForResultAsync<TatbikatApp>(page);
            if(AndroidApp==null)
            {
                AppSearchiOSText = "";
                IOSApp = null;
            }
        }

        private async void AddiOSAppCommandFunction()
        {
            Page page = new SelectAppFromStoreScreen(PlatformType.iOS);
            IOSApp = await NavigateForResultAsync<TatbikatApp>(page);
            if (IOSApp != null)
            {
                await AddAndroidAppCommandFunction(IOSApp.Name);
            }
            AppSearchiOSText = IOSApp == null ? "" : IOSApp.Name;
        }

        private async void SelectCategoriesCommandFunction()
        {
            Page page = new CategoriesSelectionScreen(true);
            var result = await NavigateForResultAsync<List<Category>>(page);
            AppCategories = result;
        }


    }
}
