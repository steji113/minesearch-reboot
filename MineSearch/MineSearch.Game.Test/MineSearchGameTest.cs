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
            
            // Flagging should have succeeded.
            Assert.IsTrue(_game.FlagCell(cellToFlag.Coordinates));

            // Double check.
            Assert.IsTrue(cellToFlag.Flagged);

            // Unflagging should have succeeded.
            Assert.IsTrue(_game.RemoveFlag(cellToFlag.Coordinates));

            // Test redundant operation.
            Assert.IsFalse(_game.RemoveFlag(cellToFlag.Coordinates));
            
            // Double check.
            Assert.IsFalse(cellToFlag.Flagged);

            // Ensure overall game state is good.
            Assert.IsFalse(_game.GameOver);
            Assert.IsFalse(_game.GameWon);
        }

        [TestMethod]
        public void TestFlagSafeCell()
        {
            var cellToFlag = _game.Cells.First(cell => cell is SafeCell);
            // Flagging should have succeeded.
            Assert.IsTrue(_game.FlagCell(cellToFlag.Coordinates));

            // Test redundant operation.
            Assert.IsFalse(_game.FlagCell(cellToFlag.Coordinates));

            // Double check.
            Assert.IsTrue(cellToFlag.Flagged);

            // Ensure overall game state is good.
            Assert.IsFalse(_game.GameOver);
            Assert.IsFalse(_game.GameWon);
        }

        [TestMethod]
        public void TestFlagMineCell()
        {
            var cellToFlag = _game.Cells.First(cell => cell is MineCell);
            // Flagging should have succeeded.
            Assert.IsTrue(_game.FlagCell(cellToFlag.Coordinates));

            // Test redundant operation.
            Assert.IsFalse(_game.FlagCell(cellToFlag.Coordinates));

            // Double check.
            Assert.IsTrue(cellToFlag.Flagged);

            // Ensure overall game state is good.
            Assert.IsFalse(_game.GameOver);
            Assert.IsFalse(_game.GameWon);
        }

        [TestMethod]
        public void TestMarkCellQuestionable()
        {
            var cellToMark = _game.Cells.First(cell => cell is MineCell);
            // Marking should have succeeded.
            Assert.IsTrue(_game.MarkCellQuestionable(cellToMark.Coordinates));

            // Test redundant operation.
            Assert.IsFalse(_game.MarkCellQuestionable(cellToMark.Coordinates));

            // Double check.
            Assert.IsTrue(cellToMark.Questionable);

            // Ensure overall game state is good.
            Assert.IsFalse(_game.GameOver);
            Assert.IsFalse(_game.GameWon);
        }

        [TestMethod]
        public void TesRemoveQuestionable()
        {
            var cellToMark = _game.Cells.First(cell => cell is MineCell);
            // Marking should have succeeded.
            Assert.IsTrue(_game.MarkCellQuestionable(cellToMark.Coordinates));

            // Double check.
            Assert.IsTrue(cellToMark.Questionable);

            // Unmarking should have succeeded.
            Assert.IsTrue(_game.RemoveQuestionable(cellToMark.Coordinates));

            // Test redundant operation.
            Assert.IsFalse(_game.RemoveQuestionable(cellToMark.Coordinates));

            // Double check.
            Assert.IsFalse(cellToMark.Questionable);

            // Ensure overall game state is good.
            Assert.IsFalse(_game.GameOver);
            Assert.IsFalse(_game.GameWon);
        }

        [TestMethod]
        public void TestRevealSafeCell()
        {
            var cellToReveal = _game.Cells.First(cell => cell is SafeCell);
            // Revealing should have succeeded.
            Assert.IsTrue(_game.RevealCell(cellToReveal.Coordinates));

            // Test redundant operation.
            Assert.IsFalse(_game.RevealCell(cellToReveal.Coordinates));

            // Double check.
            Assert.IsTrue(cellToReveal.Revealed);

            // Ensure overall game state is good.
            Assert.IsFalse(_game.GameOver);
            Assert.IsFalse(_game.GameWon);
        }

        [TestMethod]
        public void TestRevealMineCell()
        {
            var cellToReveal = _game.Cells.First(cell => cell is MineCell);
            // Revealing should have succeeded.
            Assert.IsTrue(_game.RevealCell(cellToReveal.Coordinates));

            // Test redundant operation.
            Assert.IsFalse(_game.RevealCell(cellToReveal.Coordinates));

            // Double check.
            Assert.IsTrue(cellToReveal.Revealed);

            // Ensure overall game state is good.
            Assert.IsTrue(_game.GameOver);
            Assert.IsFalse(_game.GameWon);
        }

        [TestMethod]
        public void TestWinGame()
        {
            // Reveal all safe coordinates.
            var safeCoordinates =
                _game.Cells.Where(cell => cell is SafeCell).Select(cell => cell.Coordinates);
            foreach (var coordinate in safeCoordinates)
            {
                Assert.IsTrue(_game.RevealCell(coordinate));
            }

            // Ensure all mine cells are flagged.
            foreach (var mineCell in _game.Cells.Where(cell => cell is MineCell))
            {
                Assert.IsTrue(mineCell.Flagged);
            }

            // Double check remaining mine count is good.
            Assert.AreEqual(0, _game.RemainingMineCount);

            // Ensure overall game state is good.
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
                Assert.IsTrue(_game.FlagCell(coordinate));
                expectedMineCount--;
                Assert.AreEqual(expectedMineCount, _game.RemainingMineCount);
            }

            // Remove flags
            foreach (var coordinate in safeCoordinates)
            {
                Assert.IsTrue(_game.RemoveFlag(coordinate));
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
            Assert.IsTrue(_game.RevealCell(cell.Coordinates));

            // Should have failed.
            Assert.IsFalse(_game.FlagCell(cell.Coordinates));

            // Double check.
            Assert.IsFalse(cell.Flagged);
        }

        [TestMethod]
        public void TestRevealFlaggedSafeCell()
        {
            var cell = _game.Cells.First(c => c is SafeCell);
            Assert.IsTrue(_game.FlagCell(cell.Coordinates));

            // Should have failed.
            Assert.IsFalse(_game.RevealCell(cell.Coordinates));

            // Double check.
            Assert.IsFalse(cell.Revealed);
        }

        [TestMethod]
        public void TestRevealFlaggedMineCell()
        {
            var cell = _game.Cells.First(c => c is MineCell);
            Assert.IsTrue(_game.FlagCell(cell.Coordinates));

            // Should have failed.
            Assert.IsFalse(_game.RevealCell(cell.Coordinates));

            // Double check.
            Assert.IsFalse(cell.Revealed);
        }

        [TestMethod]
        public void TestExplosionSource()
        {
            var mineCells = _game.Cells.Where(c => c is MineCell).ToList();
            var explosionSourceCell = mineCells.First() as MineCell;
            Assert.IsNotNull(explosionSourceCell);

            Assert.IsTrue(_game.RevealCell(explosionSourceCell.Coordinates));

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
