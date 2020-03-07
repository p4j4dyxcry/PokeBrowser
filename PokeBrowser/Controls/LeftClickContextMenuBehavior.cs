using System.Windows;
using System.Windows.Controls;
using Microsoft.Xaml.Behaviors;

namespace PokeBrowser.Controls
{
    public class LeftClickContextMenuBehavior : Behavior<Button>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Click += (s, e) =>
            {
                if (AssociatedObject.ContextMenu != null)
                {
                    AssociatedObject.ContextMenu.IsOpen = true;
                    AssociatedObject.ContextMenu.PlacementTarget = AssociatedObject;
                    e.Handled = true;
                }
            };
        }
    }
}