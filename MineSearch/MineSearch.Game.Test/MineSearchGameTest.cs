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
            // Create some simple game settings.
            _gameSettings = new GameSettings(4, 4, 4);
            // Use the default random point generator.
           _pointGenerator = new RandomPointGenerator(4, 4);
            // Use the default cell factory.
            var cellFactory = new MineSearchCellsFactory(_gameSettings, _pointGenerator);
            // Create a new instance of a game.
            _game = new MineSearchGame(cellFactory);
        }

        [TestMethod]
        public void TestMineCount()
        {
            Assert.AreEqual(_gameSettings.MineCount, _game.MineCount);
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
        public void TestCascade()
        {
            IGameSettings gameSettings = new GameSettings(4, 4, 1);
            var mine = new List<Point>
            {
                new Point(0, 0)
            };
            var pointGenerator = new DeterminatePointGenerator(4, 4, mine);
            var cellFactory = new MineSearchCellsFactory(gameSettings, pointGenerator);
            IMineSearchGame game = new MineSearchGame(cellFactory);

            game.RevealCell(new Point(3, 3));

            var safeCells = game.Cells.Where(cell => cell is SafeCell);
            var revealedSafeCells = game.Cells.Where(cell => cell is SafeCell && cell.Revealed);

            Assert.AreEqual(safeCells.Count(), revealedSafeCells.Count());
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
            IGameSettings tinyGameSettings = new GameSettings(1, 1, 1);
            var pointGenerator = new RandomPointGenerator(1, 1);
            var cellFactory = new MineSearchCellsFactory(tinyGameSettings, pointGenerator);
            IMineSearchGame tinyGame = new MineSearchGame(cellFactory);

            tinyGame.FlagCell(new Point(0, 0));

            Assert.IsTrue(tinyGame.GameOver);
            Assert.IsTrue(tinyGame.GameWon);
        }

        [TestMethod]
        public void TestWinGameSmall()
        {
            IGameSettings smallGameSettings = new GameSettings(3, 3, 3);
            var pointGenerator = new RandomPointGenerator(3, 3);
            var cellFactory = new MineSearchCellsFactory(smallGameSettings, pointGenerator);
            IMineSearchGame smallGame = new MineSearchGame(cellFactory);

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

        private IGameSettings _gameSettings;
        private IPointGenerator _pointGenerator;
        private IMineSearchGame _game;
    }
}
