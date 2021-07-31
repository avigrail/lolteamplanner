using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TeamPicks
{
    public class Drag : Behavior<Button>
    {
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
                data.SetData("data", this.AssociatedObject.DataContext);

                DragDrop.DoDragDrop(this.AssociatedObject, data, DragDropEffects.Move);
            }

            isMouseClicked = false;
        }
    }
}