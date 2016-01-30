using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using MineSearch.Wpf.Models;

namespace MineSearch.Wpf.Converters
{
    /// <summary>
    /// Represents the converter that converts <see cref="GameStatus"/> enumeration values 
    /// to the matching <see cref="ControlTemplate"/>.
    /// </summary>
    public class GameStatusToIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var status = (GameStatus) value;
            ControlTemplate template = null;
            switch (status)
            {
                case GameStatus.Neutral:
                    template = Application.Current.Resources["SmileyFaceIcon"] as ControlTemplate;
                    break;
                case GameStatus.Lost:
                    template = Application.Current.Resources["SadFaceIcon"] as ControlTemplate;
                    break;
                case GameStatus.Won:
                    template = Application.Current.Resources["SunglassesFaceIcon"] as ControlTemplate;
                    break;
            }
            return template ?? Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
