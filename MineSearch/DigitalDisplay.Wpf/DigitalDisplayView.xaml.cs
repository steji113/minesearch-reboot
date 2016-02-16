using System;
using System.Collections.ObjectModel;
using System.Windows;
using DigitalDisplay.Wpf.Models;
using DigitalDisplay.Wpf.ViewModels;

namespace DigitalDisplay.Wpf
{
    /// <summary>
    /// Interaction logic for DigitalDisplayView.xaml
    /// </summary>
    public partial class DigitalDisplayView
    {
        #region Constants

        /// <summary>
        /// Defafult number of digits.
        /// </summary>
        private const uint DefaultDigits = 1;

        #endregion

        #region Dependency Properties

        /// <summary>
        /// Value dependency property.
        /// </summary>
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof (int), typeof (DigitalDisplayView),
                new PropertyMetadata(OnValueChanged));

        #endregion

        #region Properties

        /// <summary>
        /// Value to display.
        /// </summary>
        public int Value
        {
            get { return (int) GetValue(ValueProperty); }
            set
            {
                if (value < _minValue)
                {
                    value = _minValue;
                }
                if (value > _maxValue)
                {
                    value = _maxValue;
                }

                SetValue(ValueProperty, value);

                var values = GetDigitValues(value, _digitsFormat);
                for (int i = 0; i < DigitViewModels.Count; i++)
                {
                    DigitViewModels[i].Value = values[i];
                }
            }
        }

        /// <summary>
        /// Number of digits.
        /// </summary>
        public uint Digits
        {
            get { return _digits; }
            set
            {
                if (_digits != value)
                {
                    if (value == 0)
                    {
                        throw new ArgumentOutOfRangeException("value", value,
                            @"Display must have at least one digit.");
                    }

                    _digits = value;
                    // Create the format string to achieve the correct number of leading zeros.
                    _digitsFormat = string.Format("D{0}", _digits);
                    // Calculate the min and max value we can have.
                    _minValue = -((int) Math.Pow(10, _digits - 1)) + 1;
                    _maxValue = ((int)Math.Pow(10, _digits)) - 1;
                    // Create a view model for each digit.
                    DigitViewModels.Clear();
                    for (int i = 0; i < _digits; i++)
                    {
                        DigitViewModels.Add(new DigitViewModel());
                    }
                }
            }
        }

        /// <summary>
        /// Digit view models.
        /// </summary>
        public ObservableCollection<DigitViewModel> DigitViewModels { get; private set; }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="DigitalDisplayView"/> class.
        /// </summary>
        public DigitalDisplayView()
        {
            DigitViewModels = new ObservableCollection<DigitViewModel>();
            Digits = DefaultDigits;
            InitializeComponent();
        }

        private static void OnValueChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            var control = source as DigitalDisplayView;
            if (control == null)
            {
                return;
            }
            control.Value = (int) e.NewValue;
        }

        private static DigitValue[] GetDigitValues(int value, string format)
        {
            var valueStr = value.ToString(format);
            if (value < 0)
            {
                // Lop off negative sign from string
                valueStr = valueStr.Substring(1);
            }
            var digits = new DigitValue[valueStr.Length];
            int i = 0;
            if (value < 0)
            {
                // Pad the array with a dash if the value is negative
                digits[i++] = DigitValue.Dash;
            }
            for (; i < valueStr.Length; i++)
            {
                digits[i] = (DigitValue) Convert.ToInt32(valueStr.Substring(i, 1));
            }
            return digits;
        }

        #region Fields

        private uint _digits;
        private string _digitsFormat;
        private int _minValue;
        private int _maxValue;

        #endregion
    }
}
