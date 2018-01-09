using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Tatbikat.UI.Interfaces;
using Xamarin.Forms;

namespace Tatbikat.ViewModels
{
    public class ViewModelsBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public Command BackCommand { get; set; }
        public ViewModelsBase()
        {
            BackCommand = new Command(NavigateBackRequested);
        }

        public virtual async void NavigateBackRequested()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        private bool _isLoading = false;
        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                RefreshUIProperty(ref _isLoading, value);
            }
        }
        protected void RefreshUIProperty<TProp>(ref TProp backedProp, TProp newValue, [CallerMemberName] string propertyName = "")
        {
            backedProp = newValue;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        protected void InvokePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public  async Task<T> NavigateForResultAsync<T>(Page page)
        {
            IsLoading = true;
            await Application.Current.MainPage.Navigation.PushModalAsync(page);
            T result = await (page.BindingContext as ICallbackEnabledScreen<T>).Wait();
            await Application.Current.MainPage.Navigation.PopModalAsync();
            IsLoading = false;
            return result;
        }
    }
}
