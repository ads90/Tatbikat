using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public MainPageViewModel()
        {

            AddAppCommand = new Command(AddAppCommandFunction);
            //Apps = new ObservableCollection<TatbikatApp>()
            //{
            //    new TatbikatApp(){ ID=1,ImageSource="https://png.icons8.com/ios/50/000000/delivery-filled.png",Name="tawseel",IOSStoreLink="https://itunes.apple.com/sa/app/tawseel-twsyl/id1070766519?mt=8",AndroidStoreLink="https://play.google.com/store/apps/details?id=sa.tawseel.client",
            //        AppCategories=new List<Category>(){new Category(){ID=1,ImageSource="https://image.flaticon.com/icons/svg/45/45880.svg",Name="توصيل",SubCategories=new List<Category>(){new Category(){ID=1,ImageSource="https://image.flaticon.com/icons/svg/45/45880.svg",Name="توصيل"}}}}},
            //    new TatbikatApp(){ ID=1,ImageSource="http://is1.mzstatic.com/image/thumb/Purple128/v4/a5/a2/23/a5a223b0-e5ff-d701-5703-d7a04c794f6b/source/175x175bb.jpg",Name="tawseel",IOSStoreLink="https://itunes.apple.com/sa/app/tawseel-twsyl/id1070766519?mt=8",AndroidStoreLink="https://play.google.com/store/apps/details?id=sa.tawseel.client",
            //        AppCategories=new List<Category>(){new Category(){ID=1,ImageSource="https://image.flaticon.com/icons/svg/45/45880.svg",Name="توصيل",SubCategories=new List<Category>(){new Category(){ID=1,ImageSource="https://image.flaticon.com/icons/svg/45/45880.svg",Name="توصيل"}}}}},
            //    new TatbikatApp(){ ID=1,ImageSource="https://lh3.googleusercontent.com/E0FnB74k8TkuBbp7LkHY6ZRm9vgVtTgbKku2ZCf0ea7yGTE9Kgc6sjf01QCeJOY2L_M=w300-rw",Name="tawseel",IOSStoreLink="https://itunes.apple.com/sa/app/tawseel-twsyl/id1070766519?mt=8",AndroidStoreLink="https://play.google.com/store/apps/details?id=sa.tawseel.client",
            //        AppCategories=new List<Category>(){new Category(){ID=1,ImageSource="https://image.flaticon.com/icons/svg/45/45880.svg",Name="توصيل",SubCategories=new List<Category>(){new Category(){ID=1,ImageSource="https://image.flaticon.com/icons/svg/45/45880.svg",Name="توصيل"}}}}},


            //};
            GetApps();
        }

        private async void GetApps()
        {
            Apps = await Connector.Current.GetApps();
        }

        private async void AddAppCommandFunction()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new AddAppScreen());
        }

        public List<TatbikatApp> Apps
        {
            get;
            set;
        }
    }
}
