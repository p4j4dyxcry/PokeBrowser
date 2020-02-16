using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace PokeBrowser.Controls
{
    public class ItemsControlFilterBehavior : Behavior<ItemsControl> , ITriggerMessageAction
    {
        public static readonly DependencyProperty FilterActionPathProperty =
            DependencyProperty.Register(
                nameof(FilterActionPath), // プロパティ名を指定
                typeof(string), // プロパティの型を指定
                typeof(ItemsControlFilterBehavior), // プロパティを所有する型を指定
                new PropertyMetadata(default)); // メタデータを指定。ここではデフォルト値を設定してる

        public string FilterActionPath
        {
            get { return (string)GetValue(FilterActionPathProperty); }
            set { SetValue(FilterActionPathProperty, value); }
        }

        private ICollectionView _collectionView; 

        protected override void OnAttached()
        {
            base.OnAttached();
            _collectionView = CollectionViewSource.GetDefaultView(AssociatedObject.ItemsSource);

            var dataContext = AssociatedObject.DataContext;

            var function = dataContext
                .GetType()
                .GetMethod(FilterActionPath)
                .CreateDelegate(typeof(Func<object, bool>), dataContext) as Func<object, bool>;

            _collectionView.Filter += (e) =>
            {
                return function(e);
            };
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
        }

        public void Invoke()
        {
            _collectionView.Refresh();
        }
    }
}
