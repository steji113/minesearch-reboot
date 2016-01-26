using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace MineSearch.Wpf.Converters
{
    /// <summary>
    /// Represents the converter that converts <see cref="Boolean"/> values to and from
    /// <see cref="Visibility"/> enumeration values.
    /// </summary>
    /// <remarks>
    /// This class is the inverse of <see cref="BooleanToVisibilityConverter"/>.
    /// </remarks>
    public class InverseBooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool val = (bool) value;
            return !val ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Visibility visibility = (Visibility) value;
            return visibility != Visibility.Visible;
        }
    }
}
