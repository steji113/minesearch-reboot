using System.Linq;
using MineSearch.Common;

namespace MineSearch.Game
{
    /// <summary>
    /// Concrete implementation of <see cref="IMineSearchCellsFactory"/>.
    /// </summary>
    public class MineSearchCellsFactory : IMineSearchCellsFactory
    {
        /// <summary>
        /// Creates a matrix of cells.
        /// </summary>
        /// <param name="gameSettings">Game settings.</param>
        /// <param name="randomGenerator">
        /// Random point generator to use when placing mine cells.
        /// </param>
        /// <returns>Matrix of cells.</returns>
        public IMatrix<ICell> CreateCells(IGameSettings gameSettings, 
            IRandomPointGenerator randomGenerator)
        {
            // Create the matrix of cells.
            var cells = new Matrix<ICell>(gameSettings.Rows, gameSettings.Columns);
            // Populate the cell matrix with cells.
            for (int i = 0; i < gameSettings.Rows * gameSettings.Columns; i++)
            {
                var point = Point.FromIndex(i, gameSettings.Columns);
                cells[point.X, point.Y] = new SafeCell(point);
            }

            // Populate a list of random points where mines will be placed.
            var mineCoordinates = randomGenerator.Generate(gameSettings.MineCount,
                gameSettings.Rows - 1, gameSettings.Columns - 1);

            // Populate the cell matrix with mines.
            foreach (var mineCoordinate in mineCoordinates)
            {
                cells[mineCoordinate.X, mineCoordinate.Y] = new MineCell(mineCoordinate);
            }

            return cells;
        }
    }
}
