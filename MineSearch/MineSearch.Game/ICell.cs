using MineSearch.Common;

namespace MineSearch.Game
{
    public interface ICell
    {
        /// <summary>
        /// The <see cref="IMineSearchGame"/> this cell belongs to.
        /// </summary>
        IMineSearchGame Game { get; set; }

        /// <summary>
        /// Cell coordinates.
        /// </summary>
        Point Coordinates { get; }

        /// <summary>
        /// Whether or not the cell has been flagged.
        /// </summary>
        bool Flagged { get; set; }

        /// <summary>
        /// Whether or not the cell has been revealed.
        /// </summary>
        bool Revealed { get; set; }
    }
}
