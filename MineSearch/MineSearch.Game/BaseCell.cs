using MineSearch.Common;

namespace MineSearch.Game
{
    public abstract class BaseCell : ICell
    {
        #region Properties

        /// <summary>
        /// The <see cref="IMineSearchGame"/> this cell belongs to.
        /// </summary>
        public IMineSearchGame Game { get; set; }

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

        /// <summary>
        /// Flags the cell.
        /// </summary>
        public void Flag()
        {
            Game.FlagCell(Coordinates);
        }

        /// <summary>
        /// Reveals the cell.
        /// </summary>
        public void Reveal()
        {
            Game.RevealCell(Coordinates);
        }

        #region Fields

        private readonly Point _coordinates;

        #endregion
    }
}
