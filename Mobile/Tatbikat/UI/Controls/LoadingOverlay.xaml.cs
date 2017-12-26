using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Tatbikat.UI.Controls
{
    public partial class LoadingOverlay : ContentView
    {
        public LoadingOverlay()
        {
            IsVisible = false;
            InitializeComponent();
            PropertyChanged += LoadingOverlay_PropertyChanged;
        }
        public static BindableProperty IsRunningProperty = BindableProperty.Create(nameof(IsRunning), typeof(bool), typeof(LoadingOverlay), false);

        public bool IsRunning
        {
            get => (bool)GetValue(IsRunningProperty);
            set => SetValue(IsRunningProperty, value);
        }

        private void LoadingOverlay_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == IsRunningProperty.PropertyName)
            {
                Device.BeginInvokeOnMainThread(() =>
                {

                    if (IsRunning)
                    {
                        IsVisible = true;
                        Task.Run(() => { StartAnimation(); });
                    }
                    else
                    {
                        IsVisible = false;
                        Task.Run(() => { StopAnimation(); });

                    }
                });

            }
        }

        bool _isAnimationRunning;
        async void StartAnimation()
        {

            if (_isAnimationRunning)
            {
                return;
            }

            _isAnimationRunning = true;

            try
            {
                while (_isAnimationRunning)
                {

                    if (__loadingImage == null)
                    {
                        _isAnimationRunning = false;
                    }

                    await __loadingImage.RotateTo(360, 300);
                    await Task.Delay(900);
                    await __loadingImage.RotateTo(0);
                    await Task.Delay(500);

                }
            }
            catch (Exception ex)
            {

            }
        }

        void StopAnimation()
        {
            _isAnimationRunning = false;
        }

    }
}