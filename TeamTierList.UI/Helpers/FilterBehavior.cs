using Microsoft.Xaml.Behaviors;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using TeamTierList.Core;

namespace TeamTierList.UI
{
    public class FilterBehavior : Behavior<TextBox>
    {
        public static readonly DependencyProperty IsEnabledProperty =
            DependencyProperty.Register(nameof(IsEnabled), typeof(bool), typeof(FilterBehavior), new PropertyMetadata(default(bool)));

        public bool IsEnabled
        {
            get => (bool)GetValue(IsEnabledProperty);
            set => SetValue(IsEnabledProperty, value);
        }

        public static readonly DependencyProperty KeyEventSourceProperty = DependencyProperty.Register(
       nameof(KeyEventSource), typeof(MainWindow), typeof(FilterBehavior), new PropertyMetadata(null, OnKeyEventSourceChanged));

        private static void OnKeyEventSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var behavior = (FilterBehavior)d;
            behavior.SetupInputBindings();
        }

        public MainWindow KeyEventSource
        {
            get => (MainWindow)GetValue(KeyEventSourceProperty);
            set => SetValue(KeyEventSourceProperty, value);
        }

        public static readonly DependencyProperty ColorScaleFormatProperty = DependencyProperty.Register(
        nameof(Items), typeof(ItemsControl), typeof(FilterBehavior), new PropertyMetadata(null, OnItemsSourceChanged));

        public ItemsControl Items
        {
            get => (ItemsControl)GetValue(ColorScaleFormatProperty);
            set => SetValue(ColorScaleFormatProperty, value);
        }

        private static void OnItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var behavior = (FilterBehavior)d;
            var itemsControl = (ItemsControl)e.NewValue;
            behavior?.SetupFilter(itemsControl);
        }

        private void SetupFilter(ItemsControl itemsControl)
        {
            if (Items == null) return;

            _filteredCollection = CollectionViewSource.GetDefaultView(Items.ItemsSource);
        }

        private ICollectionView _filteredCollection;

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Loaded += OnLoaded;
            AssociatedObject.LostFocus += OnLostFocus;
        }

        private void OnLostFocus(object sender, RoutedEventArgs e)
        {
            AssociatedObject.SetValue(TextBox.VisibilityProperty, Visibility.Collapsed);
            AssociatedObject.SetValue(TextBox.TextProperty, string.Empty);
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            AssociatedObject.TextChanged += UpdateFilter;

            _filteredCollection.Filter = CustomFilter;
        }

        public void SetupInputBindings()
        {
            KeyEventSource.InputBindings.Add(new KeyBinding(new RelayCommand(_ =>
            {
                if (!IsEnabled) return;
                AssociatedObject.SetValue(TextBox.VisibilityProperty, Visibility.Visible);
                AssociatedObject.Focus();
            }), Key.F, ModifierKeys.Control));

            KeyEventSource.InputBindings.Add(new KeyBinding(new RelayCommand(_ =>
            {
                if (!IsEnabled) return;
                AssociatedObject.MoveFocus(new TraversalRequest(FocusNavigationDirection.Previous));
            }), Key.Escape, ModifierKeys.None));
        }

        private void UpdateFilter(object sender, TextChangedEventArgs e)
        {
            _filteredCollection?.Refresh();
        }

        private bool CustomFilter(object obj)
        {
            var champion = (Champion)obj;
            return champion.Name.ToLower().Contains(AssociatedObject.Text.ToLower());
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            if (AssociatedObject == null) return;

            AssociatedObject.Loaded -= OnLoaded;
            AssociatedObject.LostFocus -= OnLostFocus;
        }
    }
}