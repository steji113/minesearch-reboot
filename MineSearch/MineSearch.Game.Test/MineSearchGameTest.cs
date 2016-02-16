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

            var safeCoordinates =
                smallGame.Cells.Where(cell => cell is SafeCell).Select(cell => cell.Coordinates);
            foreach (var coordinate in safeCoordinates)
            {
                smallGame.RevealCell(coordinate);
            }

            Assert.IsTrue(smallGame.GameOver);
            Assert.IsTrue(smallGame.GameWon);
        }

        [TestMethod]
        public void TestNonWinIncorrectFlags()
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
        public void TestNonWinUnrevealedSafeCells()
        {
            // Flag each mine cell
            var mineCoordinates =
                _game.Cells.Where(cell => cell is MineCell).Select(cell => cell.Coordinates);
            foreach (var coordinate in mineCoordinates)
            {
                _game.FlagCell(coordinate);
            }
            // Ensure the game has not been won yet
            Assert.IsFalse(_game.GameOver);
            Assert.IsFalse(_game.GameWon);

            // Now reveal the safe cells to win the game
            var safeCoordinates =
                _game.Cells.Where(cell => cell is SafeCell).Select(cell => cell.Coordinates);
            foreach (var coordinate in safeCoordinates)
            {
                _game.RevealCell(coordinate);
            }
            // Ensure the game has now been won
            Assert.IsTrue(_game.GameOver);
            Assert.IsTrue(_game.GameWon);
        }

        [TestMethod]
        public void TestRemainingFlagCount()
        {
            int expectedFlagCount = _game.MineCount;
            Assert.AreEqual(expectedFlagCount, _game.RemainingFlagCount);

            var safeCoordinates = _game.Cells.
                    Where(cell => cell is SafeCell).
                    Select(cell => cell.Coordinates).
                    Take(_game.MineCount).
                    ToList();
            // Flag safe cells
            foreach (var coordinate in safeCoordinates)
            {
                _game.FlagCell(coordinate);
                expectedFlagCount--;
                Assert.AreEqual(expectedFlagCount, _game.RemainingFlagCount);
            }
            Assert.AreEqual(0, expectedFlagCount);
            Assert.AreEqual(0, _game.RemainingFlagCount);
            // Remove flags
            foreach (var coordinate in safeCoordinates)
            {
                _game.RemoveFlag(coordinate);
                expectedFlagCount++;
                Assert.AreEqual(expectedFlagCount, _game.RemainingFlagCount);
            }
            Assert.AreEqual(_game.MineCount, expectedFlagCount);
            Assert.AreEqual(_game.MineCount, _game.RemainingFlagCount);
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
