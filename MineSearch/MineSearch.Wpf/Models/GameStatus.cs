namespace MineSearch.Wpf.Models
{
    /// <summary>
    /// Game status.
    /// </summary>
    public enum GameStatus
    {
        /// <summary>
        /// Game is in a neutral state; neither won nor lost.
        /// </summary>
        Neutral,
        /// <summary>
        /// Game has been won.
        /// </summary>
        Won,
        /// <summary>
        /// Game has been lost.
        /// </summary>
        Lost
    }
}
