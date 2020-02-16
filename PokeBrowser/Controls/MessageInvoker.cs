using Microsoft.Xaml.Behaviors;
using System.Windows;

namespace PokeBrowser.Controls
{
    public class MessageInvoker : TriggerAction<DependencyObject>
    {
        public static readonly DependencyProperty TargetProperty =
            DependencyProperty.Register(
            nameof(Target), // プロパティ名を指定
            typeof(ITriggerMessageAction), // プロパティの型を指定
            typeof(MessageInvoker), // プロパティを所有する型を指定
            new PropertyMetadata(default)); // メタデータを指定。ここではデフォルト値を設定してる

        public ITriggerMessageAction Target
        {
            get { return (ITriggerMessageAction)GetValue(TargetProperty); }
            set { SetValue(TargetProperty, value); }
        }


        protected override void Invoke(object parameter)
        {
            Target?.Invoke();
        }
    }
}
