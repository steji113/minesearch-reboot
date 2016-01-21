using MineSearch.Common;

namespace MineSearch.Game
{
    public abstract class BaseCell : ICell
    {
        #region Properties

        /// <summary>
        /// Cell coordinates.
        /// </summary>
        public Point Coordinates { get { return _coordinates; } }

        /// <summary>
        /// Whether or not the cell has been flagged.
        /// </summary>
        public bool Flagged { get; set; }

        /// <summary>
        /// Whether or not the cell has been revealed.
        /// </summary>
        public bool Revealed { get; set; }

        #endregion

        protected BaseCell(Point coordinates)
        {
            _coordinates = coordinates;
        }

        #region Fields

        private readonly Point _coordinates;

        #endregion
    }
}
