using System;
using System.Collections.Generic;
using System.Text;
using Tatbikat.Models;
using Tatbikat.UI.Interfaces;
using Tatbikat.Views;
using Xamarin.Forms;

namespace Tatbikat.ViewModels
{
    class AddAppScreenViewModel : ViewModelsBase
    {
        public Command AddAppCommand { get; set; }
        public AddAppScreenViewModel()
        {
            AddAppCommand = new Command(AddAppCommandFunction);
        }

        private async void AddAppCommandFunction()
        {
            var page = new SelectAppFromStoreScreen();
            await Application.Current.MainPage.Navigation.PushModalAsync(page);
            TatbikatApp result = await(page.BindingContext as ICallbackEnabledScreen<TatbikatApp>).Wait();
            await Application.Current.MainPage.Navigation.PopModalAsync();

            AppSearchText = result.Name;
        }
        private string _appSearchText;
        public string AppSearchText
        {
            get { return _appSearchText; }
            set { RefreshUIProperty(ref _appSearchText, value); }
        }
    }
}
