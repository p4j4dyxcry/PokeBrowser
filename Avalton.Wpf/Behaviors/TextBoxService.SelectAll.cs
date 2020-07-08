using System.Windows;
using System.Windows.Threading;

namespace Avalton.Wpf.Behaviors
{
    public partial class TextBoxService
    {
        public static bool GetSelectAllOnGotFocus(DependencyObject obj)
        {
            return (bool)obj.GetValue(SelectAllOnGotFocusProperty);
        }

        public static void SetSelectAllOnGotFocus(DependencyObject obj, bool value)
        {
            obj.SetValue(SelectAllOnGotFocusProperty, value);
        }

        private static void SelectAllOnGotFocusChanged(DependencyObject sender, DependencyPropertyChangedEventArgs evt)
        {
            if (TryGetTextBox(sender , out var textBox))
            {
                textBox.GotFocus -= OnTextBoxGotFocus;
                if ((bool)evt.NewValue)
                    textBox.GotFocus += OnTextBoxGotFocus;
            }
        }

        private static void OnTextBoxGotFocus(object sender, RoutedEventArgs e)
        {
            if (TryGetTextBox(sender as FrameworkElement, out var textBox))
            {
                textBox.Dispatcher?.InvokeAsync(() => textBox.SelectAll(), DispatcherPriority.Background);
            }
        }
    }
}