using System;
using System.Collections.Generic;
using Tatbikat.Models.Enums;
using Tatbikat.ViewModels;
using Xamarin.Forms;

namespace Tatbikat.Views
{
    public partial class SelectAppFromStoreScreen : ContentPage
    {
        public SelectAppFromStoreScreen(PlatformType platformType, string appName = null)
        {
            InitializeComponent();
            this.BindingContext = new SelectAppFromStoreScreenViewModel(platformType,appName);
            appBar.PageTitleText = platformType == PlatformType.Android ? "تأكيد اختيار التطبيق" : "ابحث عن التطبيق";
        }
    }
}
