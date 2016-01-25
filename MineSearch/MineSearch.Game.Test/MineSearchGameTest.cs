using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MineSearch.Common;

namespace MineSearch.Game.Test
{
    [TestClass]
    public class MineSearchGameTest
    {

        [TestInitialize]
        public void TestInitialize()
        {
            // Use the default random point generator.
            var generator = new RandomPointGenerator();
            // Create some simple game settings.
            var gameSettings = new GameSettings(4, 4, 4, generator);
            // Create a new instance of a game.
            _game = new MineSearchGame(gameSettings);
        }

        [TestMethod]
        public void TestRemoveFlag()
        {
            var cellToFlag = _game.Cells.First(cell => cell is SafeCell);
            _game.FlagCell(cellToFlag.Coordinates);
            _game.RemoveFlag(cellToFlag.Coordinates);
            
            Assert.IsFalse(cellToFlag.Flagged);
        }

        [TestMethod]
        public void TestFlagSafeCell()
        {
            var cellToFlag = _game.Cells.First(cell => cell is SafeCell);
            _game.FlagCell(cellToFlag.Coordinates);

            Assert.IsTrue(cellToFlag.Flagged);
            Assert.AreEqual(_game.FlaggedCells.Count(), 1);
            Assert.AreEqual(_game.FlaggedCells.First().Coordinates, cellToFlag.Coordinates);
            Assert.IsFalse(_game.GameOver);
            Assert.IsFalse(_game.GameWon);
        }

        [TestMethod]
        public void TestFlagMineCell()
        {
            var cellToFlag = _game.Cells.First(cell => cell is MineCell);
            _game.FlagCell(cellToFlag.Coordinates);

            Assert.IsTrue(cellToFlag.Flagged);
            Assert.AreEqual(_game.FlaggedCells.Count(), 1);
            Assert.AreEqual(_game.FlaggedCells.First().Coordinates, cellToFlag.Coordinates);
            Assert.IsFalse(_game.GameOver);
            Assert.IsFalse(_game.GameWon);
        }

        [TestMethod]
        public void TestRevealSafeCell()
        {
            var cellToReveal = _game.Cells.First(cell => cell is SafeCell);
            _game.RevealCell(cellToReveal.Coordinates);

            Assert.IsTrue(cellToReveal.Revealed);
            Assert.IsFalse(_game.GameOver);
            Assert.IsFalse(_game.GameWon);
        }

        [TestMethod]
        public void TestRevealMineCell()
        {
            var cellToReveal = _game.Cells.First(cell => cell is MineCell);
            _game.RevealCell(cellToReveal.Coordinates);

            Assert.IsTrue(cellToReveal.Revealed);
            Assert.IsTrue(_game.GameOver);
            Assert.IsFalse(_game.GameWon);
        }

        [TestMethod]
        public void TestWinGameTiny()
        {
            var generator = new RandomPointGenerator();
            IGameSettings tinyGameSettings = new GameSettings(1, 1, 1, generator);
            IMineSearchGame tinyGame = new MineSearchGame(tinyGameSettings);

            tinyGame.FlagCell(new Point(0, 0));

            Assert.IsTrue(tinyGame.GameOver);
            Assert.IsTrue(tinyGame.GameWon);
        }

        [TestMethod]
        public void TestWinGameSmall()
        {
            var generator = new RandomPointGenerator();
            IGameSettings smallGameSettings = new GameSettings(3, 3, 3, generator);
            IMineSearchGame smallGame = new MineSearchGame(smallGameSettings);

            var mineCoordinates =
                smallGame.Cells.Where(cell => cell is MineCell).Select(cell => cell.Coordinates);
            foreach (var coordinate in mineCoordinates)
            {
                smallGame.FlagCell(coordinate);
            }

            Assert.IsTrue(smallGame.GameOver);
            Assert.IsTrue(smallGame.GameWon);
        }

        [TestMethod]
        public void TestNonWin()
        {
            var safeCoordinates =
                _game.Cells.Where(cell => cell is SafeCell).Select(cell => cell.Coordinates);
            foreach (var coordinate in safeCoordinates)
            {
                _game.FlagCell(coordinate);
            }

            Assert.IsFalse(_game.GameOver);
            Assert.IsFalse(_game.GameWon);
        }

        [TestMethod]
        public void TestFlagLimit()
        {
            int flagLimit = _game.MineCount;
            // Exhaust flags
            for (int i = 0; i < flagLimit; i++)
            {
                var point = Point.FromIndex(i, _game.Columns);
                bool flagged = _game.FlagCell(point);
                Assert.IsTrue(flagged);
            }
            // Ensure we cannot flag any more cells
            var unflaggedCell = _game.Cells.First(cell => !cell.Flagged);
            Assert.IsFalse(_game.FlagCell(unflaggedCell.Coordinates));
        }

        private IMineSearchGame _game;
    }
}
