﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace Avalton.Wpf.Converters
{
    public class BoolToAnyConverter<T> : IValueConverter
    {
        public T True { get; set; } = default;
        public T False { get; set; } = default;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is true ? True : False;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Equals(value,True);
        }
    }
}