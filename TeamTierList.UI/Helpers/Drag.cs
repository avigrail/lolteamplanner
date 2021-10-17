using Microsoft.Xaml.Behaviors;
using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TeamTierList.UI
{
    public class Drag : Behavior<ContentPresenter>
    {
        public bool RemoveOnDrop
        {
            get => (bool)GetValue(RemoveOnDropProperty);
            set => SetValue(RemoveOnDropProperty, value);
        }

        public static readonly DependencyProperty RemoveOnDropProperty = DependencyProperty.Register(nameof(RemoveOnDrop), typeof(bool), typeof(Drag), new PropertyMetadata(default(bool)));

        public IList BoundItems
        {
            get => (IList)GetValue(BoundItemsProperty);
            set => SetValue(BoundItemsProperty, value);
        }

        public static readonly DependencyProperty BoundItemsProperty = DependencyProperty.Register(
          nameof(BoundItems), typeof(IList), typeof(Drag), new PropertyMetadata(default(IList)));

        private bool isMouseClicked = false;

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.PreviewMouseLeftButtonDown += AssociatedObject_MouseLeftButtonDown;
            AssociatedObject.PreviewMouseLeftButtonUp += AssociatedObject_MouseLeftButtonUp;
            AssociatedObject.MouseLeave += AssociatedObject_MouseLeave;
        }

        void AssociatedObject_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isMouseClicked = true;
            e.Handled = true;
        }

        void AssociatedObject_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            isMouseClicked = false;
        }

        void AssociatedObject_MouseLeave(object sender, MouseEventArgs e)
        {
            if (isMouseClicked)
            {
                DataObject data = new DataObject();
                data.SetData("data", AssociatedObject.DataContext);
                data.SetData("removeOnDrop", RemoveOnDrop);
                data.SetData("removeAction", new Action<object>(_ => BoundItems?.Remove(_)));                

                DragDrop.DoDragDrop(AssociatedObject, data, DragDropEffects.Move);
            }

            isMouseClicked = false;
        }
    }
}