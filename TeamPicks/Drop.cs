using Microsoft.Xaml.Behaviors;
using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;

namespace TeamPicks
{
    public class Drop : Behavior<ItemsControl>
    {
        public IList BoundItems
        {
            get => (IList)GetValue(BoundItemsProperty);
            set => SetValue(BoundItemsProperty, value);
        }

        public static readonly DependencyProperty BoundItemsProperty = DependencyProperty.Register(
          nameof(BoundItems), typeof(IList), typeof(Drop), new PropertyMetadata(default(IList)));

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.AllowDrop = true;
            AssociatedObject.Drop += (s, e) =>
            {
                var data = e.Data.GetData("data");
                BoundItems.Add(data);

                var shouldBeRemovedOnDrop = (bool)e.Data.GetData("removeOnDrop");

                if (!shouldBeRemovedOnDrop) return;

                var removeAction = (Action<object>)e.Data.GetData("removeAction");
                removeAction?.Invoke(data);

            };

            AssociatedObject.MouseDown += (s, e) => 
            {
                if (e.MiddleButton == System.Windows.Input.MouseButtonState.Pressed)
                {
                    BoundItems.Remove(((Border)e.OriginalSource).DataContext);
                }
            };
        }
    }
}