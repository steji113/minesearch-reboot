using System.Windows.Input;
using MineSearch.Common;
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
        /// Game instance.
        /// </summary>
        IMineSearchGame Game { get; }

        /// <summary>
        /// Matrix of cell view models.
        /// </summary>
        IMatrix<ICellViewModel> CellViewModels { get; }
    }
}
