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
            var mockGameViewModel = new Mock<IMineSearchGameViewModel>();
            var mockGame = new Mock<IMineSearchGame>();
            var mockCell = new Mock<ICell>();

            mockGame.Setup(g => g.FlagCell(It.IsAny<Point>()));
            mockGameViewModel.SetupGet(vm => vm.Game).Returns(mockGame.Object);
            mockGameViewModel.SetupGet(vm => vm.GameActive).Returns(true);

            // Create the view model and execute the flag command
            var cellViewModel = new CellViewModel(mockGameViewModel.Object, mockCell.Object);
            cellViewModel.FlagCommand.Execute(null);

            mockGame.Verify(g => g.FlagCell(It.IsAny<Point>()), Times.Once());
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

    }
}
