using MineSearch.Common.ViewModels;

namespace DigitalDisplay.Wpf.ViewModels
{
    public class DigitViewModel : ViewModelBase
    {
        #region Constants

        private const int MaxValue = 9;

        #endregion

        #region Properties

        /// <summary>
        /// Digit value.
        /// </summary>
        public uint Value
        {
            get { return _value; }
            set
            {
                if (value > MaxValue)
                {
                    value = 0;
                }
                if (_value != value)
                {
                    _value = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="DigitViewModel"/>.
        /// </summary>
        public DigitViewModel()
        {
            Value = 0;
        }

        #region Fields

        private uint _value;

        #endregion
    }
}
