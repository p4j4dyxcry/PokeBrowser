using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using Avalton.Wpf.Extensions;

namespace Avalton.Wpf.Converters
{
    /// <summary>
    /// 色からブラシに変換するコンバーター
    /// </summary>
    public class ColorToBrushConverter : IValueConverter
    {
        private readonly Dictionary<Color, Brush> _brushes = new Dictionary<Color, Brush>();
        private static readonly Brush Default = Brushes.Transparent;
        
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Color color)
            {
                if (_brushes.TryGetValue(color, out var result))
                {
                    return result;
                }

                var brush = new SolidColorBrush(color).DoFreeze();
                return _brushes[color] = brush;
            }

            return Default;
        }

        // ユースケースが無いので one way only
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}