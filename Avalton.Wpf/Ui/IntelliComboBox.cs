using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Avalton.Wpf.Extensions;

namespace Avalton.Wpf.Ui
{
    public class IntelliComboBox : Control
    {
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text", typeof(string), typeof(IntelliComboBox), new FrameworkPropertyMetadata(default(string),FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public static readonly DependencyProperty HitSourceProperty = DependencyProperty.Register(
            nameof(HitSource), typeof(IEnumerable<string>), typeof(IntelliComboBox), new PropertyMetadata(default(IEnumerable<string>)));

        public IEnumerable<string> HitSource
        {
            get => (IEnumerable<string>)GetValue(HitSourceProperty);
            set => SetValue(HitSourceProperty, value);
        }

        private TextBox _textBox;
        public void FocusTextBox()
        {
            if(_textBox is null)
                _textBox = this.FindChild<TextBox>();

            _textBox?.Focus();
        }
    }
}