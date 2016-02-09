using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Input;
using System.Windows.Threading;
using Microsoft.Practices.Prism.Commands;
using MineSearch.Common.ViewModels;
using MineSearch.Game;
using MineSearch.Wpf.Models;

namespace MineSearch.Wpf.ViewModels
{
    /// <summary>
    /// Mine search game view model.
    /// </summary>
    public class MineSearchGameViewModel : ViewModelBase, IMineSearchGameViewModel
    {
        #region Constants

        private const int GameTimerIntervalSeconds = 1;

        #endregion

        #region Commands

        /// <summary>
        /// New game command.
        /// </summary>
        public ICommand NewGameCommand { get; private set; }

        /// <summary>
        /// Command to start the current game.
        /// </summary>
        public ICommand StartGameCommand { get; private set; }

        /// <summary>
        /// Command to end the current game.
        /// </summary>
        public ICommand EndGameCommand { get; private set; }

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
        /// Whether or not the game is active.
        /// </summary>
        public bool GameActive
        {
            get { return _gameActive; }
            set
            {
                if (value != _gameActive)
                {
                    _gameActive = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Current game duration in seconds.
        /// </summary>
        public int GameDurationSeconds
        {
            get { return _gameDurationSeconds; }
            set
            {
                if (value != _gameDurationSeconds)
                {
                    _gameDurationSeconds = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Game status.
        /// </summary>
        public GameStatus GameStatus
        {
            get { return _gameStatus; }
            set
            {
                if (value != _gameStatus)
                {
                    _gameStatus = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Current game settings.
        /// </summary>
        public IGameSettings GameSettings { get; private set; }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="MineSearchGameViewModel"/> class.
        /// </summary>
        /// <param name="gameSettings">Game settings to use.</param>
        public MineSearchGameViewModel(IGameSettings gameSettings)
        {
            NewGameCommand = new DelegateCommand<IGameSettings>(NewGame);
            StartGameCommand = new DelegateCommand(StartGame);
            EndGameCommand = new DelegateCommand(EndGame);

            _gameTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(GameTimerIntervalSeconds)
            };
            _gameTimer.Tick += GameTimerOnTick;

            NewGame(gameSettings);
        }

        private void NewGame(IGameSettings gameSettings)
        {
            _gameTimer.Stop();

            if (gameSettings != null)
            {
                GameSettings = gameSettings;
            }

            Game = new MineSearchGame(GameSettings);
            CreateCellViewModels();
            GameDurationSeconds = 0;
            GameStatus = GameStatus.Neutral;

            //PrintBoard();
        }

        private void StartGame()
        {
            _gameTimer.Start();
            GameActive = true;
        }

        private void EndGame()
        {
            _gameTimer.Stop();
            GameActive = false;
            GameStatus = Game.GameWon ? GameStatus.Won : GameStatus.Lost;
        }

        private void CreateCellViewModels()
        {
            var cellViewModels = new List<List<ICellViewModel>>(GameSettings.Rows);
            for (int row = 0; row < GameSettings.Rows; row++)
            {
                cellViewModels.Add(new List<ICellViewModel>(GameSettings.Columns));
                for (int col = 0; col < GameSettings.Columns; col++)
                {
                    var cell = Game.Cells[col, row];
                    if (cell is SafeCell)
                    {
                        cellViewModels[row].Add(new SafeCellViewModel(this, cell));
                    }
                    else
                    {
                        cellViewModels[row].Add(new MineCellViewModel(this, cell));
                    }
                }
            }
            CellViewModels = cellViewModels;
        }

        private void GameTimerOnTick(object sender, EventArgs e)
        {
            GameDurationSeconds++;
        }

        private void PrintBoard()
        {
            for (int row = 0; row < GameSettings.Rows; row++)
            {
                for (int col = 0; col < GameSettings.Columns; col++)
                {
                    var cell = Game.Cells[col, row];
                    Debug.Write("[");
                    string content;
                    if (cell is MineCell)
                    {
                        content = "X";
                    }
                    else
                    {
                        content = cell.AdjacentMineCount + "";
                    }
                    Debug.Write(content);
                    Debug.Write("]");
                }
                Debug.WriteLine("");
            }
        }

        #region Fields

        private IMineSearchGame _game;
        private List<List<ICellViewModel>> _cellViewModels;
        private readonly DispatcherTimer _gameTimer;
        private int _gameDurationSeconds;
        private bool _gameActive;
        private GameStatus _gameStatus;

        #endregion
    }
}
