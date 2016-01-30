using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MineSearch.Game;

namespace MineSearch.Wpf.ViewModels
{
    public class MineCellViewModel : CellViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CellViewModel"/> class.
        /// </summary>
        /// <param name="gameViewModel">Game view model.</param>
        /// <param name="cell">Cell.</param>
        public MineCellViewModel(IMineSearchGameViewModel gameViewModel, ICell cell) : base(gameViewModel, cell)
        {
        }
    }
}
