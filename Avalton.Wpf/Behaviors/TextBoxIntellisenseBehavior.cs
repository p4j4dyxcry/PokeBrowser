using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using Microsoft.Xaml.Behaviors;

namespace Avalton.Wpf.Behaviors
{
    public class TextBoxIntellisenseBehavior : Behavior<TextBox>
    {
        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register(
            nameof(ItemsSource), typeof(IEnumerable<string>), typeof(TextBoxIntellisenseBehavior), new PropertyMetadata(default(IEnumerable<string>)));

        public IEnumerable<string> ItemsSource
        {
            get => (IEnumerable<string>)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        public static readonly DependencyProperty IntellisenseMaxHeightProperty = DependencyProperty.Register(
            nameof(IntellisenseMaxHeight), typeof(double), typeof(TextBoxIntellisenseBehavior), new PropertyMetadata(360d, IntellisenseSizeChanged));

        public static readonly DependencyProperty MaxRecodeProperty = DependencyProperty.Register(
            "MaxRecode", typeof(int), typeof(TextBoxIntellisenseBehavior), new PropertyMetadata(-1));

        public int MaxRecode
        {
            get => (int)GetValue(MaxRecodeProperty);
            set => SetValue(MaxRecodeProperty, value);
        }

        public double IntellisenseMaxHeight
        {
            get => (double) GetValue(IntellisenseMaxHeightProperty);
            set => SetValue(IntellisenseMaxHeightProperty, value);
        }

        public static readonly DependencyProperty IntellisenseMinHeightProperty = DependencyProperty.Register(
            nameof(IntellisenseMinHeight), typeof(double), typeof(TextBoxIntellisenseBehavior), new PropertyMetadata(20d, IntellisenseSizeChanged));

        public double IntellisenseMinHeight
        {
            get => (double) GetValue(IntellisenseMinHeightProperty);
            set => SetValue(IntellisenseMinHeightProperty, value);
        }
        private static void IntellisenseSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TextBoxIntellisenseBehavior textBoxIntellisenseBehavior)
            {
                textBoxIntellisenseBehavior._listBox.MaxHeight = textBoxIntellisenseBehavior.IntellisenseMaxHeight;
                textBoxIntellisenseBehavior._listBox.MinHeight = textBoxIntellisenseBehavior.IntellisenseMinHeight;
            }
        }

        public static readonly DependencyProperty IsOpenProperty = DependencyProperty.Register(
            "IsOpen", typeof(bool), typeof(TextBoxIntellisenseBehavior), new PropertyMetadata(default(bool),
                (o, args) =>
                {
                    if (o is TextBoxIntellisenseBehavior i)
                    {
                        if(i.IsOpen && i._intellisense.IsOpen is false)
                            i.TryOpenPopup(true);
                        else if(i._intellisense.IsOpen is true)
                            i.CloseIntellisense();
                    }
                }));

        public bool IsOpen
        {
            get => (bool)GetValue(IsOpenProperty);
            set => SetValue(IsOpenProperty, value);
        }

        public static readonly DependencyProperty LostFocusClosedProperty = DependencyProperty.Register(
            "LostFocusClosed", typeof(bool), typeof(TextBoxIntellisenseBehavior), new PropertyMetadata(true));

        public bool LostFocusClosed
        {
            get => (bool)GetValue(LostFocusClosedProperty);
            set => SetValue(LostFocusClosedProperty, value);
        }


        private readonly ListBox _listBox = new ListBox();
        private readonly Popup _intellisense = new Popup();
        public TextBoxIntellisenseBehavior()
        {
            _listBox.PreviewMouseDown += (s, e) =>
            {
                var target = _listBox.InputHitTest(e.GetPosition(_listBox));
                if (target is FrameworkElement dependencyObject)
                {
                    var item = dependencyObject.GetSelfAndAncestors().FirstOrDefault(x=> x is ListBoxItem);
                    if (item is FrameworkElement frameworkElement)
                    {
                        _listBox.SelectedItem = frameworkElement.DataContext;
                    }
                    else
                    {
                        if (dependencyObject.GetSelfAndAncestors().FirstOrDefault(x => x is ScrollBar) != null)
                            return;
                    }
                }
                e.Handled = true;
            };
            VirtualizingPanel.SetScrollUnit(_listBox, ScrollUnit.Pixel);

            var itemStyle = new Style(typeof(ListBoxItem));
            itemStyle.Setters.Add(new EventSetter(UIElement.MouseUpEvent,
                new MouseButtonEventHandler(
                (s, e) =>
                {
                    if (s is FrameworkElement frameworkElement)
                    {
                        AssociatedObject.Text = frameworkElement.DataContext?.ToString() ?? string.Empty;
                        AssociatedObject.CaretIndex = AssociatedObject.Text.Length;
                        CloseIntellisense();
                        AssociatedObject.GetBindingExpression(TextBox.TextProperty)?.UpdateSource();
                    }
                })));
            _listBox.ItemContainerStyle = itemStyle;
            _listBox.MaxHeight = IntellisenseMaxHeight;
            _listBox.MinHeight = IntellisenseMinHeight;

            _intellisense.StaysOpen = false;
            _intellisense.Child = _listBox;
        }

        protected override void OnAttached()
        {
            AssociatedObject.KeyDown          += OnKeyDown;
            AssociatedObject.PreviewTextInput += OnPreviewTextInput;
            AssociatedObject.PreviewKeyDown   += OnPreviewKeyDown;
            AssociatedObject.TextChanged      += OnTextChanged;
            AssociatedObject.LostFocus        += OnLostFocus;
            AssociatedObject.MouseDoubleClick += OnDoubleClick;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.KeyDown          -= OnKeyDown;
            AssociatedObject.PreviewTextInput -= OnPreviewTextInput;
            AssociatedObject.PreviewKeyDown   -= OnPreviewKeyDown;
            AssociatedObject.TextChanged      -= OnTextChanged;
            AssociatedObject.LostFocus        -= OnLostFocus;
            AssociatedObject.MouseDoubleClick -= OnDoubleClick;
        }

        private void TryOpenPopup(bool showAll)
        {
            if (ItemsSource?.Any() is true)
            {
                var defaultIndex = -1;
                var targetItems = ItemsSource.ToArray();
                if (showAll is false)
                {
                    targetItems = targetItems
                        .Where(item => item.ToLower()
                            .Contains(AssociatedObject.Text.ToLower()))
                        .Distinct()
                        .ToArray();
                }

                if (targetItems.Any() is false)
                {
                    CloseIntellisense();
                    return;
                }
                _listBox.MinWidth = AssociatedObject.ActualWidth;
                _intellisense.PlacementTarget = AssociatedObject;
                OpenIntellisense();

                foreach (var target in targetItems.Select((item, index) => new { item, index }))
                {
                    if (target.item == AssociatedObject.Text)
                    {
                        defaultIndex = target.index;
                        break;
                    }
                }

                if (defaultIndex < 0 && targetItems.Any())
                    defaultIndex = 0;

                if (MaxRecode > 0)
                {
                    targetItems = targetItems.Take(MaxRecode).ToArray();
                }

                _listBox.ItemsSource   = targetItems;
                _listBox.SelectedIndex = defaultIndex;
                if (defaultIndex >= 0)
                    _listBox.ScrollIntoView(_listBox.SelectedItem);
            }
            else
            {
                CloseIntellisense();
            }
        }

        private void OpenIntellisense()
        {
            if(IsOpen is false)
                SetValue(IsOpenProperty,true);
            _intellisense.IsOpen = true;
        }

        private void CloseIntellisense()
        {
            if(IsOpen is true)
                SetValue(IsOpenProperty,false);
            _intellisense.IsOpen = false;
        }

        private bool _isRequestIntellisense;
        private void RequestIntellisense()
        {
            _isRequestIntellisense = true;
        }

        private void CancelRequestIntellisense()
        {
            _isRequestIntellisense = false;
        }


        private void ApplySelectedText()
        {
            if (_listBox.SelectedItem != null)
            {
                AssociatedObject.Text = _listBox.SelectedItem.ToString() ?? string.Empty;
                AssociatedObject.CaretIndex = AssociatedObject.Text.Length;
                CloseIntellisense();
            }
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.Space)
            {
                TryOpenPopup(false);
                e.Handled = true;
            }
        }

        private void OnDoubleClick(object sender, MouseButtonEventArgs e)
        {
            TryOpenPopup(false);
            e.Handled = true;
        }

        private Dictionary<Key,Action> KeyboardActions { get; set; }

        private Dictionary<Key, Action> MakeKeyboardActions()
        {
            var dictionary = new Dictionary<Key, Action>();

            dictionary[Key.Down] = () =>
            {
                if (_listBox.SelectedIndex < ItemsSource.Count())
                {
                    _listBox.SelectedIndex++;
                    _listBox.ScrollIntoView(_listBox.SelectedItem);
                }
            };

            dictionary[Key.Up] = () =>
            {
                if (_listBox.SelectedIndex > 0)
                {
                    _listBox.SelectedIndex--;
                    _listBox.ScrollIntoView(_listBox.SelectedItem);
                }
            };

            dictionary[Key.Enter]  =
            dictionary[Key.Tab]    = ApplySelectedText;

            dictionary[Key.Escape] = CloseIntellisense;

            return dictionary;
        }

        private Action GetOrCreateKeyboardActions(Key key)
        {
            if (KeyboardActions is null)
                KeyboardActions = MakeKeyboardActions();

            if (KeyboardActions.ContainsKey(key))
                return KeyboardActions[key];

            return null;
        }


        private void OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (_intellisense.IsOpen)
            {
                var action = GetOrCreateKeyboardActions(e.Key);

                if (action != null)
                {
                    action.Invoke();
                    e.Handled = true;
                }
            }
        }

        private void OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            RequestIntellisense();
        }

        private void OnLostFocus(object sender, RoutedEventArgs e)
        {
            if(LostFocusClosed)
                CloseIntellisense();
        }

        private bool _isInitText = true;
        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (_isInitText)
                _isInitText = false;
            else
                RequestIntellisense();

            if (_isRequestIntellisense)
            {
                if (string.IsNullOrEmpty(AssociatedObject.Text))
                {
                    CloseIntellisense();
                    CancelRequestIntellisense();
                    _isInitText = true;
                }
                else
                {
                    TryOpenPopup(false);
                }
                CancelRequestIntellisense();
            }
        }
    }
}