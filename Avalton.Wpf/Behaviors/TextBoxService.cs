using System.Windows;
using System.Windows.Controls;
using Avalton.Wpf.Extensions;

namespace Avalton.Wpf.Behaviors
{
    public partial class TextBoxService
    {
        public static readonly DependencyProperty SelectAllOnGotFocusProperty =
            DependencyProperty.RegisterAttached(
                "SelectAllOnGotFocus",
                typeof(bool),
                typeof(TextBoxService),
                new UIPropertyMetadata(false, SelectAllOnGotFocusChanged)
            );

        public static readonly DependencyProperty WatermarkProperty = DependencyProperty.RegisterAttached(
            "Watermark",
            typeof(object),
            typeof(TextBoxService),
            new FrameworkPropertyMetadata(null, OnWatermarkChanged));

        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.RegisterAttached(
            "CornerRadius",
            typeof(CornerRadius),
            typeof(TextBoxService),
            new PropertyMetadata(new CornerRadius(0, 0, 0, 0), CornerRadiusChanged));

        static bool TryGetTextBox(DependencyObject control, out TextBox output)
        {
            output = null;
            
            if (control is TextBox textBox)
            {
                output = textBox;
                return true;
            }

            if (control is FrameworkElement element)
            {
                output = element.FindChild<TextBox>();                
            }

            return output != null;
        }
    }
}