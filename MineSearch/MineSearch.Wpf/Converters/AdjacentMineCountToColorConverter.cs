using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace MineSearch.Wpf.Converters
{
    public class AdjacentMineCountToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int mineCount = (int) value;
            Color color;
            switch (mineCount)
            {
                case 1:
                    color = Colors.Blue;
                    break;
                case 2:
                    color = Colors.Green;
                    break;
                case 3:
                    color = Colors.Red;
                    break;
                case 4:
                    color = Colors.Purple;
                    break;
                case 5:
                    color = Colors.Maroon;
                    break;
                case 6:
                    color = Colors.Teal;
                    break;
                case 7:
                    color = Colors.Black;
                    break;
                case 8:
                    color = Colors.DimGray;
                    break;
                default:
                    color = Colors.Black;
                    break;
            }
            return new SolidColorBrush(color);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
