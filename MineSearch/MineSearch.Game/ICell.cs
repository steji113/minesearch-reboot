using MineSearch.Common;

namespace MineSearch.Game
{
    public interface ICell
    {
        /// <summary>
        /// Cell coordinates.
        /// </summary>
        Point Coordinates { get; }

        /// <summary>
        /// Whether or not the cell has been flagged.
        /// </summary>
        bool Flagged { get; set; }

        /// <summary>
        /// Whether or not the cell has been marked questionable.
        /// </summary>
        bool Questionable { get; set; }

        /// <summary>
        /// Whether or not the cell has been revealed.
        /// </summary>
        bool Revealed { get; set; }

        /// <summary>
        /// Number of mines adjacent to this cell.
        /// </summary>
        int AdjacentMineCount { get; set; }
    }
}
