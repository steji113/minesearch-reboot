using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace MineSearch.Wpf.Converters
{
    class ExplosionSourceToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool explosionSource = (bool) value;
            Color color = explosionSource ? Colors.Red : Colors.LightGray;
            return new SolidColorBrush(color);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
