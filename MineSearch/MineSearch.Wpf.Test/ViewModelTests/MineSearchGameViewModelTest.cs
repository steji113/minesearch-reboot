using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MineSearch.Common;
using MineSearch.Game;
using MineSearch.Wpf.Models;
using MineSearch.Wpf.ViewModels;

namespace MineSearch.Wpf.Test.ViewModelTests
{
    [TestClass]
    public class MineSearchGameViewModelTest
    {
        [TestInitialize]
        public void TestInitialize()
        {
            _generator = new RandomPointGenerator();
            _gameSettings = new GameSettings(4, 4, 4, _generator);
            _gameViewModel = new MineSearchGameViewModel(_gameSettings);
        }

        [TestMethod]
        public void TestCellViewModelCreation()
        {
            var cells = _gameViewModel.Game.Cells;
            var viewModels = _gameViewModel.CellViewModels;

            foreach (var row in viewModels)
            {
                foreach (var viewModel in row)
                {
                    var coords = viewModel.Cell.Coordinates;
                    var cell = cells[coords.X, coords.Y];
                    Assert.AreEqual(cell, viewModel.Cell);
                }
            }
        }

        [TestMethod]
        public void TestNewGame()
        {
            var oldGame = _gameViewModel.Game;
            _gameViewModel.NewGameCommand.Execute(null);
            var newGame = _gameViewModel.Game;
            Assert.AreNotEqual(oldGame, newGame);
        }

        [TestMethod]
        public void TestGameStatusNeutral()
        {
            // Game should start neutral.
            Assert.AreEqual(GameStatus.Neutral, _gameViewModel.GameStatus);
        }

        [TestMethod]
        public void TestGameStatusWon()
        {
            // Flag all mine cells.
            foreach (var row in _gameViewModel.CellViewModels)
            {
                foreach (var cellViewModel in row.Where(vm => vm.Cell is MineCell))
                {
                    cellViewModel.FlagCommand.Execute(null);
                }
            }
            // Reveal all safe cells.
            foreach (var row in _gameViewModel.CellViewModels)
            {
                foreach (var cellViewModel in row.Where(vm => vm.Cell is SafeCell))
                {
                    cellViewModel.RevealCommand.Execute(null);
                }
            }
            // Game should be won.
            Assert.AreEqual(GameStatus.Won, _gameViewModel.GameStatus);
        }

        [TestMethod]
        public void TestGameStatusLost()
        {
            // Reveal a mine cell to lose the game.
            foreach (var row in _gameViewModel.CellViewModels)
            {
                var mineCell = row.FirstOrDefault(vm => vm.Cell is MineCell);
                if (mineCell != null)
                {
                    mineCell.RevealCommand.Execute(null);
                    break;
                }
            }
            // Game should be lost.
            Assert.AreEqual(GameStatus.Lost, _gameViewModel.GameStatus);
        }

        private IGameSettings _gameSettings;
        private IMineSearchGameViewModel _gameViewModel;
        private IPointGenerator _generator;
    }
}
