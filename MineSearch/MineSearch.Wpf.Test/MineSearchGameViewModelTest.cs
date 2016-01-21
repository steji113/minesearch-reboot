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
            _gameSettings = new GameSettings(4, 4, 4);
            _randomGenerator = new DefaultRandomPointGenerator();
            _gameViewModel = new MineSearchGameViewModel(_gameSettings, _randomGenerator);
        }

        [TestMethod]
        public void TestCellViewModelCreation()
        {
            var cells = _gameViewModel.Game.Cells;
            var viewModels = _gameViewModel.CellViewModels;

            Assert.AreEqual(cells.Rows, viewModels.Rows);
            Assert.AreEqual(cells.Columns, viewModels.Columns);

            foreach (var cell in _gameViewModel.Game.Cells)
            {
                var viewModel = viewModels[cell.Coordinates.X, cell.Coordinates.Y];
                Assert.AreEqual(cell, viewModel.Cell);
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
        private IRandomPointGenerator _randomGenerator;
    }
}
