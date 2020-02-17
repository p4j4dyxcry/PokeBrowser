using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace PokeBrowser.Controls
{
    public class PercentConverter : IValueConverter
    {
        public double MaxValue { get; set; }

        public double Value { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (double.TryParse(value?.ToString(),out var result))
            {
                return Lerp((result / MaxValue)) * Value;
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public double Lerp(double t)
        {
            return 1 - ((1 - t) * (1 - t));
        }
    }
}