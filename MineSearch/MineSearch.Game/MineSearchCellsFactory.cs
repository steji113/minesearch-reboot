using System.Linq;
using MineSearch.Common;

namespace MineSearch.Game
{
    /// <summary>
    /// Concrete implementation of <see cref="IMineSearchCellsFactory"/>.
    /// </summary>
    public class MineSearchCellsFactory : IMineSearchCellsFactory
    {

        public MineSearchCellsFactory(IGameSettings gameSettings)
        {
            _gameSettings = gameSettings;
        }

        /// <summary>
        /// Creates a matrix of cells.
        /// </summary>
        /// <returns>Matrix of cells.</returns>
        public IMatrix<ICell> CreateCells()
        {
            // Create the matrix of cells.
            var cells = new Matrix<ICell>(_gameSettings.Rows, _gameSettings.Columns);
            // Populate the cell matrix with cells.
            for (int i = 0; i < _gameSettings.Rows * _gameSettings.Columns; i++)
            {
                var point = Point.FromIndex(i, _gameSettings.Columns);
                cells[point.X, point.Y] = new SafeCell(point);
            }

            // Populate a list of points where mines will be placed.
            IPointGenerator generator = _gameSettings.PointGenerator;
            var mineCoordinates = generator.Generate(_gameSettings.MineCount,
                _gameSettings.Rows, _gameSettings.Columns);

            // Populate the cell matrix with mines.
            foreach (var mineCoordinate in mineCoordinates)
            {
                cells[mineCoordinate.X, mineCoordinate.Y] = new MineCell(mineCoordinate);
            }

            // Calculate each cell's adjacent mine count.
            foreach (var cell in cells)
            {
                cell.AdjacentMineCount = cells.GetAdjacentCells(cell).Count(c => c is MineCell);
            }

            return cells;
        }

        #region Fields

        private readonly IGameSettings _gameSettings;

        #endregion
    }
}
