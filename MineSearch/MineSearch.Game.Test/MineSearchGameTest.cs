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
           _randomPointGenerator = new DefaultRandomPointGenerator();
            // Use the default cell factory.
            _cellFactory = new MineSearchCellsFactory();
            // Create the cells via factory method.
            _cells = _cellFactory.CreateCells(_gameSettings, _randomPointGenerator);
            // Create a new instance of a game.
            _game = new MineSearchGame(_cells);
        }

        [TestMethod]
        public void TestMineCount()
        {
            Assert.AreEqual(_gameSettings.MineCount, _game.MineCount);
        }

        [TestMethod]
        public void TestRemoveFlag()
        {
            var cellToFlag = _cells.First(cell => cell is SafeCell);
            _game.FlagCell(cellToFlag.Coordinates);
            _game.RemoveFlag(cellToFlag.Coordinates);
            
            Assert.IsFalse(cellToFlag.Flagged);
        }

        [TestMethod]
        public void TestFlagSafeCell()
        {
            var cellToFlag = _cells.First(cell => cell is SafeCell);
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
            var cellToFlag = _cells.First(cell => cell is MineCell);
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
            var cellToReveal = _cells.First(cell => cell is SafeCell);
            _game.RevealCell(cellToReveal.Coordinates);

            Assert.IsTrue(cellToReveal.Revealed);
            Assert.IsFalse(_game.GameOver);
            Assert.IsFalse(_game.GameWon);
        }

        [TestMethod]
        public void TestRevealMineCell()
        {
            var cellToReveal = _cells.First(cell => cell is MineCell);
            _game.RevealCell(cellToReveal.Coordinates);

            Assert.IsTrue(cellToReveal.Revealed);
            Assert.IsTrue(_game.GameOver);
            Assert.IsFalse(_game.GameWon);
        }

        [TestMethod]
        public void TestWinGameTiny()
        {
            IGameSettings tinyGameSettings = new GameSettings(1, 1, 1);
            IMatrix<ICell> cells = _cellFactory.CreateCells(tinyGameSettings, _randomPointGenerator);
            IMineSearchGame tinyGame = new MineSearchGame(cells);

            tinyGame.FlagCell(new Point(0, 0));

            Assert.IsTrue(tinyGame.GameOver);
            Assert.IsTrue(tinyGame.GameWon);
        }

        [TestMethod]
        public void TestWinGameSmall()
        {
            IGameSettings smallGameSettings = new GameSettings(3, 3, 3);
            IMatrix<ICell> cells = _cellFactory.CreateCells(smallGameSettings, _randomPointGenerator);
            IMineSearchGame smallGame = new MineSearchGame(cells);

            var mineCoordinates =
                cells.Where(cell => cell is MineCell).Select(cell => cell.Coordinates);
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
                _cells.Where(cell => cell is SafeCell).Select(cell => cell.Coordinates);
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
            var unflaggedCell = _cells.First(cell => !cell.Flagged);
            Assert.IsFalse(_game.FlagCell(unflaggedCell.Coordinates));
        }

        private IGameSettings _gameSettings;
        private IRandomPointGenerator _randomPointGenerator;
        private IMatrix<ICell> _cells;
        private IMineSearchGame _game;
        private IMineSearchCellsFactory _cellFactory;
    }
}
