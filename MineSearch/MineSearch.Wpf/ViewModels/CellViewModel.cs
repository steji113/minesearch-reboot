using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using MineSearch.Common.ViewModels;
using MineSearch.Game;

namespace MineSearch.Wpf.ViewModels
{
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
        /// Cell.
        /// </summary>
        public ICell Cell { get; private set; }

        #endregion

        public CellViewModel(ICell cell)
        {
            Cell = cell;
            FlagCommand = new DelegateCommand(FlagCell);
            RevealCommand = new DelegateCommand(RevealCell);
        }

        private void FlagCell()
        {
            Cell.Game.FlagCell(Cell.Coordinates);
        }

        private void RevealCell()
        {
            Cell.Game.RevealCell(Cell.Coordinates);
        }

        #region Fields



        #endregion
    }
}
