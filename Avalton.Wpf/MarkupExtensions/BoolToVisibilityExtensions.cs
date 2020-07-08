using System;
using System.Windows;
using System.Windows.Markup;
using Avalton.Wpf.Converters;

namespace Avalton.Wpf.MarkupExtensions
{
    public class TrueToVisibleExtension: MarkupExtension
    {
        private static BoolToAnyConverter<Visibility> _booleanToVisibilityConverter;
        public override object ProvideValue( IServiceProvider serviceProvider )
        {
            if (_booleanToVisibilityConverter is null)
            {
                _booleanToVisibilityConverter = new BoolToAnyConverter<Visibility>()
                {
                    True = Visibility.Visible,
                    False = Visibility.Collapsed,
                };
            }
            return _booleanToVisibilityConverter;
        }
    }

    public class FalseToVisibleExtension: MarkupExtension
    {
        private static BoolToAnyConverter<Visibility> _booleanToVisibilityConverter;
        public override object ProvideValue( IServiceProvider serviceProvider )
        {
            if (_booleanToVisibilityConverter is null)
            {
                _booleanToVisibilityConverter = new BoolToAnyConverter<Visibility>()
                {
                    True = Visibility.Collapsed,
                    False = Visibility.Visible,
                };
            }
            return _booleanToVisibilityConverter;
        }
    }
}