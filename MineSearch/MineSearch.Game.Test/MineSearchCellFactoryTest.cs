using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MineSearch.Common;

namespace MineSearch.Game.Test
{
    [TestClass]
    public class MineSearchCellFactoryTest
    {

        [TestMethod]
        public void TestCreateCellsMinePlacement()
        {
            // Create the list of mine cells that will be fed to the generator.
            IList<Point> desiredCoords = new List<Point>
            {
                new Point(0, 2),
                new Point(1, 2),
                new Point(2, 1),
                new Point(2, 2)
            };
            IPointGenerator generator = new DeterminatePointGenerator(2, 2, desiredCoords);

            // Create some simple game settings.
            IGameSettings gameSettings = new GameSettings(3, 3, 4, generator);
            var cellFactory = new MineSearchCellsFactory(gameSettings);
            // Create the cells via factory method.
            IMatrix<ICell> cells = cellFactory.CreateCells();

            // Ensure that the mine cells were placed where we wanted.
            var actualCoords =
                cells.Where(cell => cell is MineCell).Select(cell => cell.Coordinates);
            Assert.IsTrue(desiredCoords.ContainsAll(actualCoords));
        }

    }
}
