using System;
using System.Windows.Markup;
using Avalton.Wpf.Converters;

namespace Avalton.Wpf.MarkupExtensions
{
    public class IntToStringExtension: MarkupExtension
    {
        public override object ProvideValue( IServiceProvider serviceProvider ) => new  IntToStringConverter();
    }
    public class FloatToStringExtension: MarkupExtension
    {
        public override object ProvideValue( IServiceProvider serviceProvider ) => new  FloatToStringConverter();
    }
    public class DoubleToStringExtension: MarkupExtension
    {
        public override object ProvideValue( IServiceProvider serviceProvider ) => new  DoubleToStringConverter();
    }
    public class DecimalToStringExtension: MarkupExtension
    {
        public override object ProvideValue( IServiceProvider serviceProvider ) => new  DecimalToStringConverter();
    }
}