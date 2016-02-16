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

        public MineSearchGame(IGameSettings gameSettings)
        {
            var cellsFactory = new MineSearchCellsFactory(gameSettings);
            Cells = cellsFactory.CreateCells();
            RemainingFlagCount = gameSettings.MineCount;
            _remainingCellsToReveal = Cells.Count(cell => cell is SafeCell);
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
        /// Marks a cell as questionable.
        /// </summary>
        /// <param name="point">Coordinates of cell to mark as questionable.</param>
        public void MarkCellQuestionable(Point point)
        {
            var cell = Cells[point.X, point.Y];
            if (cell != null)
            {
                cell.Questionable = true;
            }
        }

        /// <summary>
        /// Removes the questionable state from a cell.
        /// </summary>
        /// <param name="point">Coordinates of cell to remove questionable state from.</param>
        public void RemoveQuestionable(Point point)
        {
            var cell = Cells[point.X, point.Y];
            if (cell != null)
            {
                cell.Questionable = false;
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
        private int _remainingCellsToReveal;

        #endregion
    }
}
