using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Xaml.Behaviors;

namespace PokeBrowser.Controls
{
    /// <summary>
    /// テキストボックスにインテリセンスから指定させる機能を追加するビヘイビア
    /// </summary>

    public class TextBoxSelectAllBehavior : Behavior<TextBox>
    {        
        protected override void OnAttached()
        {
            if (this.AssociatedObject != null)
            {
                base.OnAttached();
                this.AssociatedObject.GotFocus += AssociatedObject_Focus;
            }
        }

        protected override void OnDetaching()
        {
            if (this.AssociatedObject != null)
            {
                this.AssociatedObject.GotFocus -= AssociatedObject_Focus;
                base.OnDetaching();
            }
        }

        private async void AssociatedObject_Focus(object sender, RoutedEventArgs e)
        {
            await Task.Delay(10);
            AssociatedObject.SelectAll();
        }
    }
}