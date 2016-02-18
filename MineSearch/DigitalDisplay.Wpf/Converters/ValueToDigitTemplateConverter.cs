using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using DigitalDisplay.Wpf.Models;

namespace DigitalDisplay.Wpf.Converters
{
    /// <summary>
    /// Represents the converter that converts <see cref="DigitValue"/> values to 
    /// <see cref="ControlTemplate"/> values.
    /// </summary>
    public class ValueToDigitTemplateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var val = (int) value;
            string template = string.Format("Digit{0}", val);
            if ((DigitValue) val == DigitValue.Dash)
            {
                template = "DigitDash";
            }
            return Application.Current.Resources[template];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
