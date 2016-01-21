using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            var mockCell = new Mock<ICell>();
            mockCell.Setup(c => c.Flag());
            var cellViewModel = new CellViewModel(mockCell.Object);
            cellViewModel.FlagCommand.Execute(null);
            mockCell.Verify(c => c.Flag(), Times.Once());
        }

        [TestMethod]
        public void TestRevealCommand()
        {
            var mockCell = new Mock<ICell>();
            mockCell.Setup(c => c.Reveal());
            var cellViewModel = new CellViewModel(mockCell.Object);
            cellViewModel.RevealCommand.Execute(null);
            mockCell.Verify(c => c.Reveal(), Times.Once());
        }

    }
}
