using System;
using System.Globalization;
using System.Windows.Data;

namespace Avalton.Wpf.Converters
{
    /// <summary>
    /// 数値からテキストに変換するコンバーター
    /// 例えばテキストボックスにfloatをそのままBinding UpdateSourceTriggerがPropertyChangedだと「.」入力がはじかれる。
    /// StringFormatには未対応
    /// </summary>
    public abstract class NumberToStringConverter<T> : IValueConverter
    {
        private T _latestValue;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is T tValue)
                return _latestValue = tValue;
            return default;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string stringValue)
            {
                if (TryConvert(stringValue, out var result))
                    return result;
            }
            
            // パースに失敗したら前回の値を返す。
            return _latestValue;
        }

        protected abstract bool TryConvert(string @string, out T result);

    }

    // 特殊化
    
    public class IntToStringConverter : NumberToStringConverter<int>
    {
        protected override bool TryConvert(string @string, out int result) => int.TryParse(@string, out result);
    }
    
    public class FloatToStringConverter : NumberToStringConverter<float>
    {
        protected override bool TryConvert(string @string, out float result) => float.TryParse(@string, out result);
    }
    
    public class DoubleToStringConverter : NumberToStringConverter<double>
    {
        protected override bool TryConvert(string @string, out double result) => double.TryParse(@string, out result);
    }
    
    public class DecimalToStringConverter : NumberToStringConverter<decimal>
    {
        protected override bool TryConvert(string @string, out decimal result) => decimal.TryParse(@string, out result);
    }
}