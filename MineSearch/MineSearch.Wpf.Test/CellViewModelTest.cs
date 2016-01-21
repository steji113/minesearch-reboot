using Microsoft.VisualStudio.TestTools.UnitTesting;
using MineSearch.Common;
using MineSearch.Game;
using MineSearch.Wpf.ViewModels;
using Moq;

namespace MineSearch.Wpf.Test
{
    [TestClass]
    public class CellViewModelTest
    {

        [TestMethod]
        public void TestFlagCommand()
        {
            // Mock a game and cell
            var mockGame = new Mock<IMineSearchGame>();
            var mockCell = new Mock<ICell>();

            // Setup the game's flag cell method
            mockGame.Setup(g => g.FlagCell(It.IsAny<Point>()));

            // Create the view model and execute the flag command
            var cellViewModel = new CellViewModel(mockGame.Object, mockCell.Object);
            cellViewModel.FlagCommand.Execute(null);

            mockGame.Verify(g => g.FlagCell(It.IsAny<Point>()), Times.Once());
        }

        [TestMethod]
        public void TestRevealCommand()
        {
            // Mock a game and cell
            var mockGame = new Mock<IMineSearchGame>();
            var mockCell = new Mock<ICell>();

            // Setup the game's reveal cell method
            mockGame.Setup(g => g.RevealCell(It.IsAny<Point>()));

            // Create the view model and execute the flag command
            var cellViewModel = new CellViewModel(mockGame.Object, mockCell.Object);
            cellViewModel.RevealCommand.Execute(null);

            mockGame.Verify(g => g.RevealCell(It.IsAny<Point>()), Times.Once());
        }

    }
}
