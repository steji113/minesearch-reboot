using System.Collections.Generic;
using System.Windows.Input;
using MineSearch.Game;

namespace MineSearch.Wpf.ViewModels
{
    public interface IMineSearchGameViewModel
    {
        /// <summary>
        /// Current game settings.
        /// </summary>
        IGameSettings GameSettings { get; }

        /// <summary>
        /// Command to start a new game.
        /// </summary>
        ICommand NewGameCommand { get; }

        /// <summary>
        /// Command to start the current game.
        /// </summary>
        ICommand StartGameCommand { get; }

        /// <summary>
        /// Command to end the current game.
        /// </summary>
        ICommand EndGameCommand { get; }

        /// <summary>
        /// Game instance.
        /// </summary>
        IMineSearchGame Game { get; }

        /// <summary>
        /// Matrix of cell view models.
        /// </summary>
        List<List<ICellViewModel>> CellViewModels { get; }

        /// <summary>
        /// Whether or not the game is active.
        /// </summary>
        bool GameActive { get; }

        /// <summary>
        /// Current game duration in seconds.
        /// </summary>
        int GameDurationSeconds { get; }
    }
}
