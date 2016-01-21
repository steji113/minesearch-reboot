using System.Collections.Generic;
using System.Linq;
using MineSearch.Common;

namespace MineSearch.Game
{
    public class MineSearchGame : IMineSearchGame
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
        /// Collection of cells that have been flagged.
        /// </summary>
        public IEnumerable<ICell> FlaggedCells { get { return Cells.Where(cell => cell.Flagged); } }

        /// <summary>
        /// Whether or not the game is over.
        /// </summary>
        public bool GameOver { get; private set; }

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

        public MineSearchGame(IMatrix<ICell> cells)
        {
            Cells = cells;
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
            if (!cell.Flagged && FlaggedCells.Count() < MineCount)
            {
                cell.Flagged = true;
                if (GameWon)
                {
                    GameOver = true;
                }
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
            if (!GameOver && cell != null)
            {
                cell.Flagged = false;
            }
        }

        /// <summary>
        /// Flags a cell.
        /// </summary>
        /// <param name="cell">Cell to flag.</param>
        public void FlagCell(ICell cell)
        {
            if (!GameOver)
            {
                cell.Flagged = !cell.Flagged;
                if (GameWon)
                {
                    GameOver = true;
                }
            }
        }

        /// <summary>
        /// Reveals a cell.
        /// </summary>
        /// <param name="point">Coordinates of cell to reveal.</param>
        public void RevealCell(Point point)
        {
            var cell = Cells[point.X, point.Y];
            if (!GameOver && cell != null)
            {
                cell.Revealed = true;
                if (cell is MineCell)
                {
                    GameOver = true;
                }
            }
        }
    }
}
