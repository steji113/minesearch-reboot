﻿using MineSearch.Common;

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
        int Rows { get; set; }

        /// <summary>
        /// Number of columns.
        /// </summary>
        int Columns { get; set; }

        /// <summary>
        /// Number of mines.
        /// </summary>
        int MineCount { get; set; }

        /// <summary>
        /// Point generator.
        /// </summary>
        IPointGenerator PointGenerator { get; }

        /// <summary>
        /// Whether or not cells can be marked questionable.
        /// </summary>
        bool UseQuestionableState { get; set; }
    }
}
