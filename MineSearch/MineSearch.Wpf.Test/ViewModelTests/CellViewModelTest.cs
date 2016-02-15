using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MineSearch.Common;
using MineSearch.Game;
using MineSearch.Wpf.ViewModels;
using Moq;

namespace MineSearch.Wpf.Test.ViewModelTests
{
    [TestClass]
    public class CellViewModelTest
    {
        [TestInitialize]
        public void TestInitialize()
        {
            _generator = new RandomPointGenerator();
            _gameSettings = new GameSettings(4, 4, 4, _generator);
            _gameViewModel = new MineSearchGameViewModel(_gameSettings);
        }

        [TestMethod]
        public void TestFlagCommand()
        {
            // Grab the first view model
            var cellViewModel = _gameViewModel.CellViewModels[0].First();
            var cell = cellViewModel.Cell;

            // Execute the flag command to mark the cell as flagged
            cellViewModel.FlagCommand.Execute(null);
            Assert.IsTrue(cell.Flagged);
            Assert.IsFalse(cell.Questionable);

            // Execute the flag command a second time to clear the cell
            cellViewModel.FlagCommand.Execute(null);
            Assert.IsFalse(cell.Flagged);
            Assert.IsFalse(cell.Questionable);
        }

        [TestMethod]
        public void TestFlagCommandQuestionableEnabled()
        {
            _gameSettings.UseQuestionableState = true;
            _gameViewModel = new MineSearchGameViewModel(_gameSettings);

            // Grab the first view model
            var cellViewModel = _gameViewModel.CellViewModels[0].First();
            var cell = cellViewModel.Cell;

            // Execute the flag command to mark the cell as flagged
            cellViewModel.FlagCommand.Execute(null);
            Assert.IsTrue(cell.Flagged);
            Assert.IsFalse(cell.Questionable);

            // Execute the flag command a second time to mark the cell as questionable
            cellViewModel.FlagCommand.Execute(null);
            Assert.IsTrue(cell.Questionable);
            Assert.IsFalse(cell.Flagged);

            // Execute the flag command a third time to clear the cell
            cellViewModel.FlagCommand.Execute(null);
            Assert.IsFalse(cell.Flagged);
            Assert.IsFalse(cell.Questionable);
        }

        [TestMethod]
        public void TestRevealCommand()
        {
            var mockGameViewModel = new Mock<IMineSearchGameViewModel>();
            var mockGame = new Mock<IMineSearchGame>();
            var mockCell = new Mock<ICell>();

            mockGame.Setup(g => g.RevealCell(It.IsAny<Point>()));
            mockGameViewModel.SetupGet(v => v.Game).Returns(mockGame.Object);
            mockGameViewModel.SetupGet(vm => vm.GameActive).Returns(true);

            // Create the view model and execute the flag command
            var cellViewModel = new CellViewModel(mockGameViewModel.Object, mockCell.Object);
            cellViewModel.RevealCommand.Execute(null);

            mockGame.Verify(g => g.RevealCell(It.IsAny<Point>()), Times.Once());
        }

        private IGameSettings _gameSettings;
        private IMineSearchGameViewModel _gameViewModel;
        private IPointGenerator _generator;

    }
}
