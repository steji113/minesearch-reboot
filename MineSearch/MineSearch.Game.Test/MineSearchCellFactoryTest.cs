using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MineSearch.Common;

namespace MineSearch.Game.Test
{
    [TestClass]
    public class MineSearchCellFactoryTest
    {
        public MineSearchCellFactoryTest()
        {
            _cellsFactory = new MineSearchCellsFactory();
        }

        [TestMethod]
        public void TestCreateCellsMinePlacement()
        {
            // Create some simple game settings.
            IGameSettings gameSettings = new GameSettings(3, 3, 4);

            // Create the list of mine cells that will be fed to the generator.
            IList<Point> desiredCoords = new List<Point>
            {
                new Point(0, 2),
                new Point(1, 2),
                new Point(2, 1),
                new Point(2, 2)
            };
            IRandomPointGenerator generator = new MockRandomPointGenerator(desiredCoords);

            // Create the cells via factory method.
            IMatrix<ICell> cells = _cellsFactory.CreateCells(gameSettings, generator);

            // Ensure that the mine cells were placed where we wanted.
            var actualCoords =
                cells.Where(cell => cell is MineCell).Select(cell => cell.Coordinates);
            Assert.IsTrue(desiredCoords.ContainsAll(actualCoords));
        }

        private readonly IMineSearchCellsFactory _cellsFactory;

    }
}
