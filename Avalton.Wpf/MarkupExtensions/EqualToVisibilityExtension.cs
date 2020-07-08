using System;
using System.Windows.Markup;
using Avalton.Wpf.Converters;

namespace Avalton.Wpf.MarkupExtensions
{
    public class IntEqualToVisibleExtension : MarkupExtension
    {
        public int? Value { get; set; }
        
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return new EqualToVisibleConverter()
            {
                Value = Value,
            };
        }
    }
}