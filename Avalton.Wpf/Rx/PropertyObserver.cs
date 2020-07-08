
using System.ComponentModel;

namespace Avalton.Wpf.Rx
{
    public class PropertyObserver : ObserverProduct<PropertyChangedEventArgs,PropertyChangedEventArgs>
    {
        private readonly INotifyPropertyChanged _propertyOwner;

        public PropertyObserver(INotifyPropertyChanged propertyOwner) : base(x=>x)
        {
            _propertyOwner = propertyOwner;
            _propertyOwner.PropertyChanged += OnPropertyChanged;
        }
        
        private void OnPropertyChanged(object s, PropertyChangedEventArgs e)
        {
            OnNext(e);
        }
        
        protected override void Dispose(bool disposed)
        {
            if (disposed is false)
            {
                _propertyOwner.PropertyChanged -= OnPropertyChanged;
            }
        }
    }
}