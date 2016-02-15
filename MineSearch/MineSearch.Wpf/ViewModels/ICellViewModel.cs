using System.Windows.Input;
using MineSearch.Game;

namespace MineSearch.Wpf.ViewModels
{
    /// <summary>
    /// Defines an interface for a cell view model.
    /// </summary>
    public interface ICellViewModel
    {
        /// <summary>
        /// Command to flag the cell.
        /// </summary>
        ICommand FlagCommand { get; }

        /// <summary>
        /// Command to reveal the cell.
        /// </summary>
        ICommand RevealCommand { get; }

        /// <summary>
        /// Game view model this cell belongs to.
        /// </summary>
        IMineSearchGameViewModel GameViewModel { get; }

        /// <summary>
        /// Game instance the cell belongs to.
        /// </summary>
        IMineSearchGame Game { get; }

        /// <summary>
        /// Cell.
        /// </summary>
        ICell Cell { get; }

        /// <summary>
        /// Whether or not to use the questionable state.
        /// </summary>
        bool UseQuestionableState { get; set; }
    }
}
