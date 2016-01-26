using System;
using System.Globalization;
using System.Windows.Data;

namespace MineSearch.Wpf.Converters
{
    public class AdjacentMineCountToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int val = (int)value;
            if (val == 0)
            {
                return "";
            }
            return string.Format("{0}", val);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
