using MineSearch.Common;

namespace MineSearch.Game
{
    /// <summary>
    /// Class representing a mine cell in the grid.
    /// </summary>
    public class MineCell : BaseCell
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MineCell"/> class.
        /// </summary>
        /// <param name="coordinates">Cell coordinates.</param>
        public MineCell(Point coordinates) : base(coordinates)
        {

        }
    }
}
