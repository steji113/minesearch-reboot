using Microsoft.VisualStudio.TestTools.UnitTesting;
using MineSearch.Common;
using MineSearch.Game;
using MineSearch.Wpf.ViewModels;

namespace MineSearch.Wpf.Test
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

        private IGameSettings _gameSettings;
        private IMineSearchGameViewModel _gameViewModel;
        private IPointGenerator _generator;
    }
}
