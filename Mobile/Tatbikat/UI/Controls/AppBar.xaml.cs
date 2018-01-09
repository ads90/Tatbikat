using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Tatbikat.UI.Controls
{
    [ContentProperty("Title")]
    public partial class AppBar : ContentView
    {
        public AppBar()
        {
            InitializeComponent();
            IsAddIconVisible = false;
            IsCategoryIconVisible = false;
        }
        private ContentView _title;
        public ContentView Title
        {
            get { return _title; }
            set
            {
                _title = value;
                Root.Children.Add(_title, 1, 0);
            }
        }
        public string PageTitleText
        {
            get { return PageTitle.Text; }
            set { PageTitle.Text = value; }
        }
        #region Back Icon Properties
        public bool IsBackIconVisible
        {
            get { return BackIconContainer.IsVisible; }
            set
            {
                BackIconContainer.IsVisible = value;
            }
        }


        public static readonly BindableProperty OnBackPressedCommandProperty =
            BindableProperty.Create("OnBackPressedCommand", typeof(Command), typeof(AppBar), null);

        public Command OnBackPressedCommand
        {
            get { return (Command)GetValue(OnBackPressedCommandProperty); }
            set { SetValue(OnBackPressedCommandProperty, value); }
        }

        private void OnBackIconTapped(object sender, System.EventArgs e)
        {
            OnBackPressedCommand?.Execute(null);
        }

        #endregion
        #region category Icon Properties
        public bool IsCategoryIconVisible
        {
            get { return CategoryIconContainer.IsVisible; }
            set
            {
                CategoryIconContainer.IsVisible = value;
            }
        }
        public static readonly BindableProperty OnCategoryPressedCommandProperty =
          BindableProperty.Create("OnCategoryPressedCommand", typeof(Command), typeof(AppBar), null);

        public Command OnCategoryPressedCommand
        {
            get { return (Command)GetValue(OnCategoryPressedCommandProperty); }
            set { SetValue(OnCategoryPressedCommandProperty, value); }
        }
        void OnCategoryIconTapped(object sender, System.EventArgs e)
        {
            OnCategoryPressedCommand?.Execute(null);
        }

        #endregion
        #region Add Icon Properties
        public bool IsAddIconVisible
        {
            get { return AddIconContainer.IsVisible; }
            set
            {
                AddIconContainer.IsVisible = value;
            }
        }

        public static readonly BindableProperty OnAddPressedCommandProperty =
            BindableProperty.Create("OnAddPressedCommand", typeof(Command), typeof(AppBar), null);

        public Command OnAddPressedCommand
        {
            get { return (Command)GetValue(OnAddPressedCommandProperty); }
            set { SetValue(OnAddPressedCommandProperty, value); }
        }

        void OnAddIconTapped(object sender, System.EventArgs e)
        {
            OnAddPressedCommand?.Execute(null);
        }

        #endregion
    }
}
