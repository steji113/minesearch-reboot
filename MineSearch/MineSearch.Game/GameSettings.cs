using System;
using MineSearch.Common;

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

        /// <summary>
        /// Point generator.
        /// </summary>
        public IPointGenerator PointGenerator { get; private set; }

        public GameSettings(int rows, int columns, int mineCount, IPointGenerator generator)
        {
            if (rows <= 0)
            {
                throw new ArgumentOutOfRangeException("rows", rows,
                    "row count must be greater than zero");
            }
            if (columns <= 0)
            {
                throw new ArgumentOutOfRangeException("columns", columns,
                    "column count must be greater than zero");
            }
            if (mineCount <= 0)
            {
                throw new ArgumentOutOfRangeException("mineCount", mineCount,
                    "mine count must be greater than zero");
            }
            if (mineCount > rows * columns)
            {
                throw new ArgumentOutOfRangeException("mineCount", mineCount,
                    "mine count cannot be greater than the number of cells");
            }

            Rows = rows;
            Columns = columns;
            MineCount = mineCount;
            PointGenerator = generator;
        }
    }
}
