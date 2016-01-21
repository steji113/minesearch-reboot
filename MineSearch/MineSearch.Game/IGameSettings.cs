namespace MineSearch.Game
{
    /// <summary>
    /// Defines the game settings.
    /// </summary>
    public interface IGameSettings
    {
        /// <summary>
        /// Number of rows.
        /// </summary>
        int Rows { get; }

        /// <summary>
        /// Number of columns.
        /// </summary>
        int Columns { get; }

        /// <summary>
        /// Number of mines.
        /// </summary>
        int MineCount { get; }
    }
}
