using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MineSearch.Common;

namespace MineSearch.Game.Test
{
    [TestClass]
    public class MineSearchGameCascadeTest
    {
        [TestMethod]
        public void TestSimple()
        {
            int rows = 5;
            int cols = 5;

            var mines = new List<Point>
            {
                new Point(0, 0)
            };
            IPointGenerator generator = new DeterminatePointGenerator(mines);
            IGameSettings settings = new GameSettings(rows, cols, mines.Count, generator);
            IMineSearchGame game = new MineSearchGame(settings);

            var point = new Point(cols - 1, rows - 1);
            game.RevealCell(point);
            game.CascadeCell(point);

            var revealedCells = game.Cells.Count(c => c.Revealed);
            Assert.AreEqual(game.Cells.Size - 1, revealedCells);
        }

        [TestMethod]
        public void TestCascade()
        {
            var mine = new List<Point>
            {
                new Point(0, 2),
                new Point(3, 2),
                new Point(4, 2),
                new Point(0, 3),
                new Point(4, 4)
            };
            var pointGenerator = new DeterminatePointGenerator(mine);
            IGameSettings gameSettings = new GameSettings(5, 5, 5, pointGenerator);
            IMineSearchGame game = new MineSearchGame(gameSettings);

            var point = new Point(0, 0);
            game.RevealCell(point);
            game.CascadeCell(point);

            var revealedSafeCells = game.Cells.Where(cell => cell is SafeCell && cell.Revealed);

            Assert.AreEqual(10, revealedSafeCells.Count());
        }
    }
}
