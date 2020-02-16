using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Text;
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
            get { return (Type)GetValue(ViewProperty); }
            set { SetValue(ViewProperty, value); }
        }

        protected override void Invoke(object parameter)
        {
            var window = Activator.CreateInstance(View) as Window;
            window.DataContext = parameter;
        }
    }
}
