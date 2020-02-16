using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace PokeBrowser.Controls
{
    public class OffsetValueConverter : IValueConverter
    {
        public int Offset { get; set; }

        public bool ClampMinus { get; set; } = true;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (int.TryParse(value?.ToString(), out var result))
            {
                result = result + Offset;
                if(ClampMinus)
                {
                    if (result <= 0)
                        result = 0;
                }
                value = result;
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (int.TryParse(value?.ToString(), out var result))
            {
                result = result - Offset;
                if (ClampMinus)
                {
                    if (result <= 0)
                        result = 0;
                }
                value = result;
            }

            return value;
        }
    }
}
