using System.Windows;
using System.Windows.Controls;
using Avalton.Wpf.Extensions;

namespace Avalton.Wpf.Behaviors
{
    public partial class TextBoxService
    {
        public static object GetCornerRadius(DependencyObject d)
        {
            return d.GetValue(CornerRadiusProperty);
        }

        public static void SetCornerRadius(DependencyObject d, object value)
        {
            d.SetValue(CornerRadiusProperty, value);
        }
        
        private static void CornerRadiusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (TryGetTextBox(d,out var t))
            {
                var border = t.FindVisualParentWithType<Border>();
                if (border != null)
                    border.CornerRadius = (CornerRadius)e.NewValue;
            }
        }
    }
}