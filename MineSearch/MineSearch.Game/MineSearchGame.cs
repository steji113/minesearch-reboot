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
        /// Number of mines remaining.
        /// </summary>
        public int RemainingMineCount
        {
            get { return _remainingMineCount; }
            private set
            {
                if (value != _remainingMineCount)
                {
                    _remainingMineCount = value;
                    OnPropertyChanged();
                }
            }
        }

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
                    // Handle game outcome
                    if (GameWon)
                    {
                        WinGame();
                    }
                    else
                    {
                        LoseGame();
                    }
                    OnPropertyChanged();
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
                return _remainingCellsToReveal == 0;
            }
        }

        #endregion

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="gameSettings">Settings used for this game.</param>
        public MineSearchGame(IGameSettings gameSettings)
        {
            var cellsFactory = new MineSearchCellsFactory(gameSettings);
            Cells = cellsFactory.CreateCells();
            RemainingMineCount = gameSettings.MineCount;
            _remainingCellsToReveal = Cells.Size - gameSettings.MineCount;
        }

        /// <summary>
        /// Flags a cell.
        /// </summary>
        /// <param name="point">Coordinates of cell to flag.</param>
        /// <returns>True if the cell has been flagged, false otherwise.</returns>
        public bool FlagCell(Point point)
        {
            var cell = Cells[point.X, point.Y];
            if (cell != null && !cell.Revealed && !cell.Flagged)
            {
                // Be sure to unmark it as questionable.
                // This cell may have been in that state and then
                // automatically flagged at the end of the game.
                cell.Questionable = false;
                cell.Flagged = true;
                RemainingMineCount--;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Removes a flag from a cell.
        /// </summary>
        /// <param name="point">Coordinates of cell to remove flag from.</param>
        /// <returns>True if the cell has been unflagged, false otherwise.</returns>
        public bool RemoveFlag(Point point)
        {
            var cell = Cells[point.X, point.Y];
            if (cell != null && cell.Flagged)
            {
                RemainingMineCount++;
                cell.Flagged = false;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Marks a cell as questionable.
        /// </summary>
        /// <param name="point">Coordinates of cell to mark as questionable.</param>
        /// <returns>True if the cell has been marked questionable, false otherwise.</returns>
        public bool MarkCellQuestionable(Point point)
        {
            var cell = Cells[point.X, point.Y];
            if (cell != null && !cell.Revealed && !cell.Questionable)
            {
                cell.Questionable = true;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Removes the questionable state from a cell.
        /// </summary>
        /// <param name="point">Coordinates of cell to remove questionable state from.</param>
        /// <returns>True if the cell has been unmarked questionable, false otherwise.</returns>
        public bool RemoveQuestionable(Point point)
        {
            var cell = Cells[point.X, point.Y];
            if (cell != null && cell.Questionable)
            {
                cell.Questionable = false;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Reveals a cell.
        /// </summary>
        /// <param name="point">Coordinates of cell to reveal.</param>
        /// <returns>True if the cell has been revealed, false otherwise.</returns>
        public bool RevealCell(Point point)
        {
            var cell = Cells[point.X, point.Y];
            if (cell != null && !cell.Revealed && !cell.Flagged)
            {
                cell.Revealed = true;
                // Is it a Mine cell?
                if (cell is MineCell)
                {
                    if (!GameOver)
                    {
                        var mineCell = cell as MineCell;
                        mineCell.ExplosionSource = true;
                        GameOver = true;
                    }
                }
                // Otherwise must be a Safe cell
                else
                {
                    _remainingCellsToReveal--;
                    if (GameWon)
                    {
                        GameOver = true;
                    }
                }
                return true;
            }
            return false;
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
                var adjacentMineCount = Cells.GetAdjacentCells(point).Count(c => c is MineCell);
                if (adjacentMineCount == 0)
                {
                    var adjacent =
                        Cells.GetAdjacentCells(point).Where(c => !c.Revealed && !c.Flagged).Select(
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
            // When game is lost, we reveal all mine cells.
            foreach (var cell in Cells.Where(cell => cell is MineCell))
            {
                RevealCell(cell.Coordinates);
            }
        }

        private void WinGame()
        {
            // When game is won, we flag all mine cells.
            foreach (var cell in Cells.Where(cell => cell is MineCell))
            {
                FlagCell(cell.Coordinates);
            }
        }

        #region Fields

        private bool _gameOver;
        private int _remainingMineCount;
        private int _remainingCellsToReveal;

        #endregion
    }
}
