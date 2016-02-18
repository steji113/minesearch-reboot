using MineSearch.Common;

namespace MineSearch.Game
{
    /// <summary>
    /// Defines an interface for a MineSearch game.
    /// </summary>
    public interface IMineSearchGame
    {
        /// <summary>
        /// Matrix of cells.
        /// </summary>
        IMatrix<ICell> Cells { get; }

        /// <summary>
        /// Number of columns.
        /// </summary>
        int Columns { get; }

        /// <summary>
        /// Number of rows.
        /// </summary>
        int Rows { get; }

        /// <summary>
        /// Number of mine cells in the grid.
        /// </summary>
        int MineCount { get; }

        /// <summary>
        /// Number of mines remaining.
        /// </summary>
        int RemainingMineCount { get; }

        /// <summary>
        /// Whether or not the game is over.
        /// </summary>
        bool GameOver { get; }

        /// <summary>
        /// Whether or not the game has been won.
        /// </summary>
        bool GameWon { get; }

        /// <summary>
        /// Flags a cell.
        /// </summary>
        /// <param name="point">Coordinates of cell to flag.</param>
        /// <returns>True if the cell has been flagged, false otherwise.</returns>
        bool FlagCell(Point point);

        /// <summary>
        /// Removes a flag from a cell.
        /// </summary>
        /// <param name="point">Coordinates of cell to remove flag from.</param>
        /// <returns>True if the cell has been unflagged, false otherwise.</returns>
        bool RemoveFlag(Point point);

        /// <summary>
        /// Marks a cell as questionable.
        /// </summary>
        /// <param name="point">Coordinates of cell to mark as questionable.</param>
        /// <returns>True if the cell has been marked questionable, false otherwise.</returns>
        bool MarkCellQuestionable(Point point);

        /// <summary>
        /// Removes the questionable state from a cell.
        /// </summary>
        /// <param name="point">Coordinates of cell to remove questionable state from.</param>
        /// <returns>True if the cell has been unmarked questionable, false otherwise.</returns>
        bool RemoveQuestionable(Point point);

        /// <summary>
        /// Reveals a cell.
        /// </summary>
        /// <param name="point">Coordinates of cell to reveal.</param>
        /// <returns>True if the cell has been revealed, false otherwise.</returns>
        bool RevealCell(Point point);

        /// <summary>
        /// Performs a cascading reveal.
        /// </summary>
        /// <param name="point">Coordinates of cell to reveal.</param>
        void CascadeCell(Point point);
    }
}
