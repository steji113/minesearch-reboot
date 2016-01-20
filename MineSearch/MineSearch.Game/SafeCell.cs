using MineSearch.Common;

namespace MineSearch.Game
{
    /// <summary>
    /// Class representing a "safe" cell in the grid.
    /// A cell is considered safe if it does not contain a mine.
    /// </summary>
    public class SafeCell : BaseCell
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SafeCell"/> class.
        /// </summary>
        /// <param name="coordinates">Cell coordinates.</param>
        public SafeCell(Point coordinates) : base(coordinates)
        {
        }
    }
}
