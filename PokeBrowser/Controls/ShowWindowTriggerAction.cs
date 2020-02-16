using Microsoft.Xaml.Behaviors;
using System;
using System.Windows;

namespace PokeBrowser.Controls
{
    public class ShowWindowTriggerAction : TriggerAction<DependencyObject>
    {
        public static readonly DependencyProperty ViewProperty =
            DependencyProperty.Register(
                nameof(View),
                typeof(Type),
                typeof(ShowWindowTriggerAction),
                new PropertyMetadata(default));

        public Type View
        {
            get => (Type)GetValue(ViewProperty);
            set => SetValue(ViewProperty, value);
        }

        protected override void Invoke(object parameter)
        {
            var window = Activator.CreateInstance(View) as Window;
            if (window != null) 
                window.DataContext = parameter;
        }
    }
}
