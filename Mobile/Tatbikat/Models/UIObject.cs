using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Tatbikat.Models
{
    public class UIObject : INotifyPropertyChanged
    {
        public void Notify<T>(ref T backing, T val, [CallerMemberName] string propName = "")
        {
            backing = val;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        protected void InvokePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}