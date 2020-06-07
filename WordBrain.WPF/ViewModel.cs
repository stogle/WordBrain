using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WordBrain.WPF
{
    public class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected bool SetValue<T>(ref T property, T value, [CallerMemberName]string? propertyName = null)
        {
            if (!EqualityComparer<T>.Default.Equals(property, value))
            {
                property = value;
                InvokePropertyChanged(propertyName);
                return true;
            }

            return false;
        }

        protected void InvokePropertyChanged([CallerMemberName]string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
