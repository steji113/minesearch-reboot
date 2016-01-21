using MineSearch.Common;

namespace MineSearch.Game
{
    /// <summary>
    /// Concrete implementation of <see cref="IMineSearchCellsFactory"/>.
    /// </summary>
    public class MineSearchCellsFactory : IMineSearchCellsFactory
    {

        public MineSearchCellsFactory(IGameSettings gameSettings,
            IPointGenerator pointGenerator)
        {
            _gameSettings = gameSettings;
            _pointGenerator = pointGenerator;
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

            // Populate a list of random points where mines will be placed.
            var mineCoordinates = _pointGenerator.Generate(_gameSettings.MineCount);

            // Populate the cell matrix with mines.
            foreach (var mineCoordinate in mineCoordinates)
            {
                cells[mineCoordinate.X, mineCoordinate.Y] = new MineCell(mineCoordinate);
            }

            return cells;
        }

        #region Fields

        private readonly IGameSettings _gameSettings;
        private readonly IPointGenerator _pointGenerator;

        #endregion
    }
}
