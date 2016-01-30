using MineSearch.Common;

namespace MineSearch.Game
{
    /// <summary>
    /// Class representing a mine cell in the grid.
    /// </summary>
    public class MineCell : BaseCell
    {
        /// <summary>
        /// Whether or not this cell was the source of explosion.
        /// </summary>
        public bool ExplosionSource
        {
            get { return _explosionSource; }
            set
            {
                if (value != _explosionSource)
                {
                    _explosionSource = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MineCell"/> class.
        /// </summary>
        /// <param name="coordinates">Cell coordinates.</param>
        public MineCell(Point coordinates) : base(coordinates)
        {

        }

        #region Fields

        private bool _explosionSource;

        #endregion
    }
}
