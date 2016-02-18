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
            Assert.IsFalse(_game.GameOver);
            Assert.IsFalse(_game.GameWon);
        }

        [TestMethod]
        public void TestFlagMineCell()
        {
            var cellToFlag = _game.Cells.First(cell => cell is MineCell);
            _game.FlagCell(cellToFlag.Coordinates);

            Assert.IsTrue(cellToFlag.Flagged);
            Assert.IsFalse(_game.GameOver);
            Assert.IsFalse(_game.GameWon);
        }

        [TestMethod]
        public void TestMarkCellQuestionable()
        {
            var cellToMark = _game.Cells.First(cell => cell is MineCell);
            _game.MarkCellQuestionable(cellToMark.Coordinates);

            Assert.IsTrue(cellToMark.Questionable);
        }

        [TestMethod]
        public void TesRemoveQuestionable()
        {
            var cellToMark = _game.Cells.First(cell => cell is MineCell);
            _game.MarkCellQuestionable(cellToMark.Coordinates);
            _game.RemoveQuestionable(cellToMark.Coordinates);

            Assert.IsFalse(cellToMark.Questionable);
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
        public void TestWinGame()
        {
            var safeCoordinates =
                _game.Cells.Where(cell => cell is SafeCell).Select(cell => cell.Coordinates);
            foreach (var coordinate in safeCoordinates)
            {
                _game.RevealCell(coordinate);
            }

            Assert.IsTrue(_game.GameOver);
            Assert.IsTrue(_game.GameWon);
        }

        [TestMethod]
        public void TestRemainingMineCount()
        {
            int expectedMineCount = _game.MineCount;
            Assert.AreEqual(expectedMineCount, _game.RemainingMineCount);

            var safeCoordinates = _game.Cells.
                    Where(cell => cell is SafeCell).
                    Select(cell => cell.Coordinates).
                    ToList();

            // Flag safe cells
            foreach (var coordinate in safeCoordinates)
            {
                _game.FlagCell(coordinate);
                expectedMineCount--;
                Assert.AreEqual(expectedMineCount, _game.RemainingMineCount);
            }

            // Remove flags
            foreach (var coordinate in safeCoordinates)
            {
                _game.RemoveFlag(coordinate);
                expectedMineCount++;
                Assert.AreEqual(expectedMineCount, _game.RemainingMineCount);
            }

            Assert.AreEqual(_game.MineCount, expectedMineCount);
            Assert.AreEqual(_game.MineCount, _game.RemainingMineCount);
        }

        [TestMethod]
        public void TestFlagRevealedCell()
        {
            var cell = _game.Cells.First(c => c is SafeCell);
            _game.RevealCell(cell.Coordinates);

            _game.FlagCell(cell.Coordinates);

            Assert.IsFalse(cell.Flagged);
        }

        [TestMethod]
        public void TestRevealFlaggedSafeCell()
        {
            var cell = _game.Cells.First(c => c is SafeCell);
            _game.FlagCell(cell.Coordinates);

            _game.RevealCell(cell.Coordinates);

            Assert.IsFalse(cell.Revealed);
        }

        [TestMethod]
        public void TestRevealFlaggedMineCell()
        {
            var cell = _game.Cells.First(c => c is MineCell);
            _game.FlagCell(cell.Coordinates);

            _game.RevealCell(cell.Coordinates);

            Assert.IsFalse(cell.Revealed);
        }

        [TestMethod]
        public void TestExplosionSource()
        {
            var mineCells = _game.Cells.Where(c => c is MineCell).ToList();
            var explosionSourceCell = mineCells.First() as MineCell;
            Assert.IsNotNull(explosionSourceCell);

            _game.RevealCell(explosionSourceCell.Coordinates);

            Assert.IsTrue(explosionSourceCell.ExplosionSource);

            foreach (var cell in mineCells.Skip(1))
            {
                var mineCell = (MineCell) cell;
                Assert.IsFalse(mineCell.ExplosionSource);
            }
        }

        private IMineSearchGame _game;
    }
}
