using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SportTime.ViewModels
{
    /// <summary>
    /// Barcha ViewModellar uchun asosiy klass
    /// INotifyPropertyChanged interfeysi orqali UI bilan bog'lanish
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Property o'zgarganida UI ga xabar berish
        /// </summary>
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Property qiymatini o'rnatish va o'zgarish haqida xabar berish
        /// </summary>
        protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }
    }
}