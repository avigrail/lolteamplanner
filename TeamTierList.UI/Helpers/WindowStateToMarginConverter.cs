using System.Windows;
using System.Windows.Data;
using System;
using System.Globalization;

namespace TeamTierList.UI
{
    public class WindowStateToMarginConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is WindowState state && state == WindowState.Maximized)
            {
                return new Thickness(6);
            }

            return new Thickness(0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
