using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using MineSearch.Common.ViewModels;
using MineSearch.Game;

namespace MineSearch.Wpf.ViewModels
{
    /// <summary>
    /// Cell view model.
    /// </summary>
    public class CellViewModel : ViewModelBase, ICellViewModel
    {
        #region Commands

        /// <summary>
        /// Command to flag the cell.
        /// </summary>
        public ICommand FlagCommand { get; private set; }

        /// <summary>
        /// Command to reveal the cell.
        /// </summary>
        public ICommand RevealCommand { get; private set; }

        #endregion

        #region Propertes

        /// <summary>
        /// Game view model this cell belongs to.
        /// </summary>
        public IMineSearchGameViewModel GameViewModel { get; private set; }

        /// <summary>
        /// Game instance the cell belongs to.
        /// </summary>
        public IMineSearchGame Game { get; private set; }

        /// <summary>
        /// Cell.
        /// </summary>
        public ICell Cell { get; private set; }

        /// <summary>
        /// Whether or not to use the questionable state.
        /// </summary>
        public bool UseQuestionableState { get; set; }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="CellViewModel"/> class.
        /// </summary>
        /// <param name="gameViewModel">Game view model.</param>
        /// <param name="cell">Cell.</param>
        public CellViewModel(IMineSearchGameViewModel gameViewModel, ICell cell)
        {
            GameViewModel = gameViewModel;
            Game = gameViewModel.Game;
            Cell = cell;
            UseQuestionableState = gameViewModel.GameSettings.UseQuestionableState;
            FlagCommand = new DelegateCommand(FlagCell);
            RevealCommand = new DelegateCommand(RevealCell);
        }

        private void FlagCell()
        {
            // Do nothing if the game has already ended.
            if (Game.GameOver)
            {
                return;
            }
            // Start the game if it hasn't yet been started.
            if (!GameViewModel.GameActive)
            {
                GameViewModel.StartGameCommand.Execute(null);
            }
            if (Cell.Flagged)
            {
                // Remove the flag.
                Game.RemoveFlag(Cell.Coordinates);
                // If the questionable state is being used, mark the cell as questionable.
                if (UseQuestionableState)
                {
                    Game.MarkCellQuestionable(Cell.Coordinates);
                }
            }
            else if (Cell.Questionable)
            {
                // Remove the questionable state.
                Game.RemoveQuestionable(Cell.Coordinates);
            }
            else
            {
                // Flag the cell.
                Game.FlagCell(Cell.Coordinates);
                // End the game if this flag resulted in game over.
                if (Game.GameOver)
                {
                    GameViewModel.EndGameCommand.Execute(null);
                }
            }
        }

        private void RevealCell()
        {
            // Do nothing if the game has already ended.
            if (Game.GameOver)
            {
                return;
            }
            // Start the game if it hasn't yet been started.
            if (!GameViewModel.GameActive)
            {
                GameViewModel.StartGameCommand.Execute(null);
            }
            // Reveal the cell.
            Game.RevealCell(Cell.Coordinates);
            // Cascde if a safe cell was revealed.
            if (Cell is SafeCell)
            {
                Game.CascadeCell(Cell.Coordinates);
            }
            // End the game if this reveal resulted in game over.
            if (Game.GameOver)
            {
                GameViewModel.EndGameCommand.Execute(null);
            }
        }

        #region Fields



        #endregion
    }
}
