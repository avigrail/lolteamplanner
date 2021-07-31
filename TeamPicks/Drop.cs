using Microsoft.Xaml.Behaviors;
using System.Collections;
using System.Windows;
using System.Windows.Controls;

namespace TeamPicks
{
    public class Drop : Behavior<ItemsControl>
    {
        public IList BoundItems
        {
            get { return (IList)this.GetValue(BoundItemsProperty); }
            set { this.SetValue(BoundItemsProperty, value); }
        }

        public static readonly DependencyProperty BoundItemsProperty = DependencyProperty.Register(
          "BoundItems", typeof(IList), typeof(Drop), new PropertyMetadata(default(IList)));

        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.AllowDrop = true;
            this.AssociatedObject.Drop += (s, e) =>
            {
                var data = e.Data.GetData("data");
                BoundItems.Add(data);
            };
            this.AssociatedObject.MouseRightButtonDown += (s, e) => BoundItems.Remove(((Border)e.OriginalSource).DataContext);
        }
    }
}