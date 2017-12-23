using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace Tatbikat.ViewModels
{
    public class ViewModelsBase : INotifyPropertyChanged
    {
        public Command BackCommand { get; set; }
        public ViewModelsBase()
        {
            BackCommand = new Command(NavigateBackRequested);
        }

        public async void NavigateBackRequested()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private bool _isBusy = false;
        public bool IsLoading
        {
            get { return _isBusy; }
            set
            {
                RefreshUIProperty(ref _isBusy, value);
            }
        }
        protected void RefreshUIProperty<TProp>(ref TProp backedProp, TProp newValue, [CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
