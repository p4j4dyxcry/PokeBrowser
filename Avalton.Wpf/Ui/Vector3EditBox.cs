using System;
using System.Numerics;
using System.Windows;
using System.Windows.Controls;

namespace Avalton.Wpf.Ui
{
    public class Vector3EditBox : Control
    {
        public static readonly DependencyProperty ValueProperty = 
            DependencyProperty.Register(
                nameof(Value),
                typeof(Vector3), 
                typeof(Vector3EditBox),
                new FrameworkPropertyMetadata(
                    default(Vector3),
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    ValueChanged));

        public Vector3 Value
        {
            get => (Vector3) GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public static readonly DependencyProperty XProperty =
            DependencyProperty.Register(
                nameof(X),
                typeof(float), 
                typeof(Vector3EditBox),
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
                typeof(Vector3EditBox),
                new PropertyMetadata(
                    default(float),
                    ValueElementChanged));

        public float Y
        {
            get => (float) GetValue(YProperty);
            set => SetValue(YProperty, value);
        }

        public static readonly DependencyProperty ZProperty =
            DependencyProperty.Register(
                nameof(Z),
                typeof(float), 
                typeof(Vector3EditBox),
                new PropertyMetadata(
                    default(float),
                    ValueElementChanged));

        public float Z
        {
            get => (float) GetValue(ZProperty);
            set => SetValue(ZProperty, value);
        }

        private static void ValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if(d is Vector3EditBox v)
                v.OnValueChanged();
        }
        
        private static void ValueElementChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Vector3EditBox vector3EditBox)
            {
                vector3EditBox.SetCurrentValue(ValueProperty,
                    new Vector3(vector3EditBox.Value.X,vector3EditBox.Value.Y,vector3EditBox.Z));
            }
        }

        private void OnValueChanged()
        {
            if (Math.Abs(X - Value.X) > 0.0001d)
                SetCurrentValue(XProperty,Value.X);

            if (Math.Abs(Y - Value.Y) > 0.0001d)
                SetCurrentValue(YProperty,Value.Y);

            if (Math.Abs(Z - Value.Z) > 0.0001d)
                SetCurrentValue(ZProperty,Value.Z);
        }

        public Vector3EditBox()
        {
            SetCurrentValue(FocusableProperty,false);
        }
    }
}