using System;

namespace MineSearch.Game
{
    /// <summary>
    /// Concrete implementation of <see cref="IGameSettings"/>.
    /// </summary>
    public class GameSettings : IGameSettings
    {
        /// <summary>
        /// Number of rows.
        /// </summary>
        public int Rows { get; set; }

        /// <summary>
        /// Number of columns.
        /// </summary>
        public int Columns { get; set; }

        /// <summary>
        /// Number of mines.
        /// </summary>
        public int MineCount { get; set; }

        public GameSettings(int rows, int columns, int mineCount)
        {
            if (rows <= 0)
            {
                throw new ArgumentOutOfRangeException("rows", 
                    "row count must be greater than zero");
            }
            if (columns <= 0)
            {
                throw new ArgumentOutOfRangeException("columns",
                    "column count must be greater than zero");
            }
            if (mineCount <= 0)
            {
                throw new ArgumentOutOfRangeException("mineCount",
                    "mine count must be greater than zero");
            }
            if (mineCount > rows * columns)
            {
                throw new ArgumentOutOfRangeException("mineCount",
                    "mine count cannot be greater than the number of cells");
            }

            Rows = rows;
            Columns = columns;
            MineCount = mineCount;
        }

        /// <summary>
        /// Initializes a copy of the <see cref="IGameSettings"/> class.
        /// </summary>
        /// <param name="rhs">Settings to copy.</param>
        public GameSettings(IGameSettings rhs)
        {
            Rows = rhs.Rows;
            Columns = rhs.Columns;
            MineCount = rhs.MineCount;
        }
    }
}
