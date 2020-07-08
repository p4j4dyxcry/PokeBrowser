using System;
using System.Windows.Data;
using System.Windows.Markup;
using Avalton.Wpf.Converters;

namespace Avalton.Wpf.MarkupExtensions
{
    public class ColorToBrushExtensions : MarkupExtension
    {
        private static IValueConverter _converter;
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (_converter is null)
            {
                _converter = new ColorToBrushConverter();
            }
            return _converter;
        }
    }
}