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
            FlagCommand = new DelegateCommand(FlagCell);
            RevealCommand = new DelegateCommand(RevealCell);
        }

        private void FlagCell()
        {
            if (!GameViewModel.GameActive)
            {
                GameViewModel.StartGameCommand.Execute(null);
            }

            if (!Game.GameOver)
            {
                if (Cell.Flagged)
                {
                    Game.RemoveFlag(Cell.Coordinates);
                }
                else
                {
                    Game.FlagCell(Cell.Coordinates);
                } 
            }
        }

        private void RevealCell()
        {
            if (!GameViewModel.GameActive)
            {
                GameViewModel.StartGameCommand.Execute(null);
            }

            if (!Game.GameOver)
            {
                Game.RevealCell(Cell.Coordinates);
                if (Cell is SafeCell)
                {
                    Game.CascadeCell(Cell.Coordinates);
                }
                if (Game.GameOver)
                {
                    GameViewModel.EndGameCommand.Execute(null);
                }
            }
        }

        #region Fields



        #endregion
    }
}
