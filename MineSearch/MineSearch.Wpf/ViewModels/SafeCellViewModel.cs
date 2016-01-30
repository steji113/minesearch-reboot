using MineSearch.Game;

namespace MineSearch.Wpf.ViewModels
{
    public class SafeCellViewModel : CellViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CellViewModel"/> class.
        /// </summary>
        /// <param name="gameViewModel">Game view model.</param>
        /// <param name="cell">Cell.</param>
        public SafeCellViewModel(IMineSearchGameViewModel gameViewModel, ICell cell)
            : base(gameViewModel, cell)
        {
        }
    }
}
