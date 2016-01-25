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
        /// <param name="game">Game instance.</param>
        /// <param name="cell">Cell.</param>
        public CellViewModel(IMineSearchGame game, ICell cell)
        {
            Game = game;
            Cell = cell;
            FlagCommand = new DelegateCommand(FlagCell);
            RevealCommand = new DelegateCommand(RevealCell);
        }

        private void FlagCell()
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

        private void RevealCell()
        {
            Game.RevealCell(Cell.Coordinates);
            Game.CascadeCell(Cell.Coordinates);
        }

        #region Fields



        #endregion
    }
}
