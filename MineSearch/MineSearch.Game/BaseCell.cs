using MineSearch.Common;

namespace MineSearch.Game
{
    public abstract class BaseCell : ModelBase, ICell
    {
        #region Properties

        /// <summary>
        /// Cell coordinates.
        /// </summary>
        public Point Coordinates { get { return _coordinates; } }

        /// <summary>
        /// Whether or not the cell has been flagged.
        /// </summary>
        public bool Flagged
        {
            get { return _flagged; }
            set
            {
                if (value != _flagged)
                {
                    _flagged = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Whether or not the cell has been revealed.
        /// </summary>
        public bool Revealed
        {
            get { return _revealed; }
            set
            {
                if (value != _revealed)
                {
                    _revealed = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Number of mines adjacent to this cell.
        /// </summary>
        public int AdjacentMineCount { get; set; }

        #endregion

        protected BaseCell(Point coordinates)
        {
            _coordinates = coordinates;
        }

        #region Fields

        private readonly Point _coordinates;
        private bool _flagged;
        private bool _revealed;

        #endregion
    }
}
