using Microsoft.Xaml.Behaviors;
using System.Windows;

namespace PokeBrowser.Controls
{
    public class MessageInvoker : TriggerAction<DependencyObject>
    {
        public static readonly DependencyProperty TargetProperty =
            DependencyProperty.Register(
            nameof(Target),
            typeof(ITriggerMessageAction),
            typeof(MessageInvoker),
            new PropertyMetadata(default));

        public ITriggerMessageAction Target
        {
            get => (ITriggerMessageAction)GetValue(TargetProperty);
            set => SetValue(TargetProperty, value);
        }


        protected override void Invoke(object parameter)
        {
            Target?.Invoke();
        }
    }
}
