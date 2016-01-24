using System.Collections.Generic;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
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
        public List<List<ICellViewModel>> CellViewModels
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

        public MineSearchGameViewModel(IGameSettings gameSettings)
        {
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
            Game = new MineSearchGame(GameSettings);
            var cellViewModels = new List<List<ICellViewModel>>(GameSettings.Rows);
            for (int row = 0; row < GameSettings.Rows; row++)
            {
                cellViewModels.Add(new List<ICellViewModel>(GameSettings.Columns));
                for (int col = 0; col < GameSettings.Columns; col++)
                {
                    var cell = Game.Cells[row, col];
                    cellViewModels[row].Add(new CellViewModel(Game, cell));
                }
            }
            CellViewModels = cellViewModels;
        }

        #region Fields

        private IMineSearchGame _game;
        private List<List<ICellViewModel>> _cellViewModels;

        #endregion
    }
}
