using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using MineSearch.Common;
using MineSearch.Common.ViewModels;
using MineSearch.Game;

namespace MineSearch.Wpf.ViewModels
{
    public class MineSearchGameViewModel : ViewModelBase, IMineSearchGameViewModel
    {
        #region Commands

        /// <summary>
        /// New game command.
        /// </summary>
        public ICommand NewGameCommand { get; private set; }

        #endregion

        #region Properties

        /// <summary>
        /// MineSearch game.
        /// </summary>
        public IMineSearchGame Game
        {
            get { return _game; }
            private set
            {
                if (_game != value)
                {
                    _game = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Matrix of cell view models.
        /// </summary>
        public IMatrix<ICellViewModel> CellViewModels
        {
            get { return _cellViewModels; }
            private set
            {
                if (_cellViewModels != value)
                {
                    _cellViewModels = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Current game settings.
        /// </summary>
        public IGameSettings GameSettings { get; private set; }

        #endregion

        public MineSearchGameViewModel(IGameSettings gameSettings,
            IRandomPointGenerator randomGenerator)
        {
            _randomGenerator = randomGenerator;
            NewGameCommand = new DelegateCommand(NewGame);
            NewGame(gameSettings);
        }

        private void NewGame()
        {
            NewGame(GameSettings);
        }

        private void NewGame(IGameSettings gameSettings)
        {
            GameSettings = gameSettings;
            var cellFactory = new MineSearchCellsFactory();
            var cells = cellFactory.CreateCells(gameSettings, _randomGenerator);
            Game = new MineSearchGame(cells);
            var cellViewModels = new Matrix<ICellViewModel>(Game.Rows, Game.Columns);
            foreach (var cell in Game.Cells)
            {
                var viewModel = new CellViewModel(cell);
                cellViewModels[cell.Coordinates.X, cell.Coordinates.Y] = viewModel;
            }
            CellViewModels = cellViewModels;
        }

        #region Fields

        private IMineSearchGame _game;
        private IMatrix<ICellViewModel> _cellViewModels;
        private readonly IRandomPointGenerator _randomGenerator;

        #endregion
    }
}
