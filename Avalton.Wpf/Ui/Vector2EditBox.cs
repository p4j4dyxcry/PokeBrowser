using System;
using System.Numerics;
using System.Windows;
using System.Windows.Controls;

namespace Avalton.Wpf.Ui
{
    public class Vector2EditBox : Control
    {
        public static readonly DependencyProperty ValueProperty = 
            DependencyProperty.Register(
                nameof(Value),
                typeof(Vector2), 
                typeof(Vector2EditBox),
                new FrameworkPropertyMetadata(
                    default(Vector2),
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    ValueChanged));

        public Vector2 Value
        {
            get => (Vector2) GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public static readonly DependencyProperty XProperty =
            DependencyProperty.Register(
                nameof(X),
                typeof(float), 
                typeof(Vector2EditBox),
                new PropertyMetadata(
                    default(float),
                    ValueElementChanged));

        public float X
        {
            get => (float) GetValue(XProperty);
            set => SetValue(XProperty, value);
        }

        public static readonly DependencyProperty YProperty =
            DependencyProperty.Register(
                nameof(Y),
                typeof(float), 
                typeof(Vector2EditBox),
                new PropertyMetadata(
                    default(float),
                    ValueElementChanged));

        public float Y
        {
            get => (float) GetValue(YProperty);
            set => SetValue(YProperty, value);
        }
        private static void ValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if(d is Vector2EditBox v)
                v.OnValueChanged();
        }
        
        private static void ValueElementChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Vector2EditBox vector2EditBox)
            {
                vector2EditBox.SetCurrentValue(ValueProperty,
                    new Vector2(vector2EditBox.Value.X,vector2EditBox.Value.Y));
            }
        }

        private void OnValueChanged()
        {
            if (Math.Abs(X - Value.X) > 0.0001d)
                SetCurrentValue(XProperty,Value.X);

            if (Math.Abs(Y - Value.Y) > 0.0001d)
                SetCurrentValue(YProperty,Value.Y);
        }

        public Vector2EditBox()
        {
            SetCurrentValue(FocusableProperty,false);
        }
    }
}