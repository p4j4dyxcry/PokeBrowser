using System;
using System.Windows.Markup;
using Avalton.Wpf.Converters;

namespace Avalton.Wpf.MarkupExtensions
{
    public class InverseBoolExtension: MarkupExtension
    {
        private static BoolToAnyConverter<bool> _converter;
        public override object ProvideValue( IServiceProvider serviceProvider )
        {
            if (_converter is null)
            {
                _converter = new BoolToAnyConverter<bool>()
                {
                    True = false,
                    False = true,
                };
            }
            return _converter;
        }
    }
}