using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Avalton.Wpf.Behaviors
{
    public partial class TextBoxService
    {
        public static object GetWatermark(DependencyObject d)
        {
            return d.GetValue(WatermarkProperty);
        }

        public static void SetWatermark(DependencyObject d, object value)
        {
            d.SetValue(WatermarkProperty, value);
        }

        private static void OnWatermarkChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Control control)
            {
                control.Loaded += (s, e2) =>
                {
                    if (TryGetTextBox(control, out var textBox))
                    {
                        control.GotKeyboardFocus += WaterMarkTarget_GotKeyboardFocus;
                        control.LostKeyboardFocus += WaterMarkTarget_Loaded;
                        textBox.TextChanged += WaterMarkTarget_OnTextChanged;

                        // 初回
                        if (ShouldShowWatermark(textBox))
                        {
                            ShowWatermark(textBox);
                        }
                    }
                };
            }
        }

        private static void WaterMarkTarget_GotKeyboardFocus(object sender, RoutedEventArgs e)
        {
            if (TryGetTextBox(sender as FrameworkElement, out var textBox))
            {
                RemoveWatermark(textBox);
            }
        }

        private static void WaterMarkTarget_OnTextChanged(object sender, RoutedEventArgs e)
        {
            if (TryGetTextBox(sender as FrameworkElement , out var textBox))
            {
                if(textBox.Text == string.Empty)
                    ShowWatermark(textBox);
                else
                    RemoveWatermark(textBox);
            }
        }

        private static void WaterMarkTarget_Loaded(object sender, RoutedEventArgs e)
        {
            if (TryGetTextBox(sender as FrameworkElement, out var textBox))
            {
                if (ShouldShowWatermark(textBox))
                {
                    ShowWatermark(textBox);
                }
            }
        }


        private static void RemoveWatermark(TextBox control)
        {
            var layer = AdornerLayer.GetAdornerLayer(control);

            if (layer is null)
                return;
            
            var layers = layer.GetAdorners(control)?.ToArray() ?? Array.Empty<Adorner>();

            foreach (var adorner in layers.OfType<WatermarkAdorner>())
            {
                adorner.Visibility = Visibility.Hidden;
                layer.Remove(adorner);
            }
        }

        private static void ShowWatermark(TextBox control)
        {
            var layer = AdornerLayer.GetAdornerLayer(control);
            layer?.Add(new WatermarkAdorner(control, GetWatermark(control)));
        }

        private static bool ShouldShowWatermark(TextBox textBox)
        {
            return textBox.Text == string.Empty;
        }
    }
}