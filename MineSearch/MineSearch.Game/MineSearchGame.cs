using System.Collections.Generic;
using System.Linq;
using MineSearch.Common;

namespace MineSearch.Game
{
    public class MineSearchGame : ModelBase, IMineSearchGame
    {
        #region Properties

        /// <summary>
        /// Matrix of cells.
        /// </summary>
        public IMatrix<ICell> Cells { get; private set; }

        /// <summary>
        /// Number of columns.
        /// </summary>
        public int Columns { get { return Cells.Columns; }}

        /// <summary>
        /// Number of rows.
        /// </summary>
        public int Rows { get { return Cells.Rows; } }

        /// <summary>
        /// Number of mine cels in the grid.
        /// </summary>
        public int MineCount { get { return Cells.Count(cell => cell is MineCell); } }

        /// <summary>
        /// Number of flags remaining.
        /// </summary>
        public int RemainingFlagCount
        {
            get { return _remainingFlagCount; }
            private set
            {
                if (value != _remainingFlagCount)
                {
                    _remainingFlagCount = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Collection of cells that have been flagged.
        /// </summary>
        public IEnumerable<ICell> FlaggedCells { get { return Cells.Where(cell => cell.Flagged); } }

        /// <summary>
        /// Whether or not the game is over.
        /// </summary>
        public bool GameOver
        {
            get { return _gameOver; }
            set
            {
                if (!_gameOver && value)
                {
                    _gameOver = true;
                    if (!GameWon)
                    {
                        LoseGame();
                    }
                }
            }
        }

        /// <summary>
        /// Whether or not the game has been won.
        /// </summary>
        public bool GameWon
        {
            get
            {
                var flaggedMineCells = FlaggedCells.Where(cell => cell is MineCell);
                return flaggedMineCells.Count() == MineCount;
            }
        }

        #endregion

        public MineSearchGame(IGameSettings gameSettings)
        {
            var cellsFactory = new MineSearchCellsFactory(gameSettings);
            Cells = cellsFactory.CreateCells();
            RemainingFlagCount = gameSettings.MineCount;
        }

        /// <summary>
        /// Flags a cell.
        /// </summary>
        /// <param name="point">Coordinates of cell to flag.</param>
        /// <returns>True if the cell has been flagged, false otherwise.</returns>
        public bool FlagCell(Point point)
        {
            var cell = Cells[point.X, point.Y];
            if (GameOver || cell == null)
            {
                return false;
            }
            if (!cell.Revealed && !cell.Flagged && RemainingFlagCount > 0)
            {
                cell.Flagged = true;
                if (GameWon)
                {
                    GameOver = true;
                }
                RemainingFlagCount--;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Removes a flag from a cell.
        /// </summary>
        /// <param name="point">Coordinates of cell to remove flag from.</param>
        public void RemoveFlag(Point point)
        {
            var cell = Cells[point.X, point.Y];
            if (cell != null && cell.Flagged)
            {
                RemainingFlagCount++;
                cell.Flagged = false;
            }
        }

        /// <summary>
        /// Reveals a cell.
        /// </summary>
        /// <param name="point">Coordinates of cell to reveal.</param>
        public void RevealCell(Point point)
        {
            var cell = Cells[point.X, point.Y];
            if (cell != null && !cell.Revealed && !cell.Flagged)
            {
                cell.Revealed = true;
                if (!GameOver && cell is MineCell)
                {
                    var mineCell = cell as MineCell;
                    mineCell.ExplosionSource = true;
                    GameOver = true;
                }
            }
        }

        /// <summary>
        /// Performs a cascading reveal.
        /// </summary>
        /// <param name="point">Coordinates of cell to reveal.</param>
        public void CascadeCell(Point point)
        {
            var cell = Cells[point.X, point.Y];
            if (cell != null)
            {
                var adjacentMineCount = Cells.GetAdjacentCells(cell).Count(c => c is MineCell);
                if (adjacentMineCount == 0)
                {
                    var adjacent =
                        Cells.GetAdjacentCells(cell).Where(c => !c.Revealed && !c.Flagged).Select(
                            c => c.Coordinates);
                    foreach (var adjacentPoint in adjacent)
                    {
                        RevealCell(adjacentPoint);
                        CascadeCell(adjacentPoint);
                    }
                }
            }
        }

        private void LoseGame()
        {
            foreach (var cell in Cells.Where(cell => cell is MineCell))
            {
                RevealCell(cell.Coordinates);
            }
        }

        #region Fields

        private bool _gameOver;
        private int _remainingFlagCount;

        #endregion
    }
}
