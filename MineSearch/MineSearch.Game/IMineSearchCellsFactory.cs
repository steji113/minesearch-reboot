using MineSearch.Common;

namespace MineSearch.Game
{
    /// <summary>
    /// Defines an interface for a factory that creates a <see cref="IMatrix{ICell}"/>
    /// to be used in a <see cref="IMineSearchGame"/>.
    /// </summary>
    public interface IMineSearchCellsFactory
    {
        /// <summary>
        /// Creates a matrix of cells.
        /// </summary>
        /// <param name="gameSettings">Game settings.</param>
        /// <param name="randomGenerator">
        /// Random point generator to use when placing mine cells.
        /// </param>
        /// <returns>Matrix of cells.</returns>
        IMatrix<ICell> CreateCells(IGameSettings gameSettings,
            IRandomPointGenerator randomGenerator);
    }
}
