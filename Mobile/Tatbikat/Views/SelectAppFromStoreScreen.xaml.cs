﻿using System;
using System.Collections.Generic;
using Tatbikat.Models.Enums;
using Tatbikat.ViewModels;
using Xamarin.Forms;

namespace Tatbikat.Views
{
    public partial class SelectAppFromStoreScreen : ContentPage
    {
        public SelectAppFromStoreScreen(PlatformType platformType)
        {
            InitializeComponent();
            this.BindingContext = new SelectAppFromStoreScreenViewModel(platformType);
        }
    }
}