using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MineSearch.Common;
// ReSharper disable UnusedVariable

namespace MineSearch.Game.Test
{
    [TestClass]
    public class GameSettingsTest
    {

        [TestInitialize]
        public void TestInitialize()
        {
            _generator = new RandomPointGenerator();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestInvalidMineCount()
        {
            // More mines than there are cells
            IGameSettings gameSettings = new GameSettings(1, 1, 2, _generator);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestNoMines()
        {
            IGameSettings gameSettings = new GameSettings(1, 1, 0, _generator);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestNegativeMines()
        {
            IGameSettings gameSettings = new GameSettings(1, 1, -1, _generator);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestNoRows()
        {
            IGameSettings gameSettings = new GameSettings(0, 1, 1, _generator);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestNegativeRows()
        {
            IGameSettings gameSettings = new GameSettings(-1, 1, 1, _generator);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestNoColumns()
        {
            IGameSettings gameSettings = new GameSettings(1, 0, 1, _generator);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestNegativeColumns()
        {
            IGameSettings gameSettings = new GameSettings(1, -1, 1, _generator);
        }

        [TestMethod]
        public void TestCopyConstructor()
        {
            IGameSettings gameSettings1 = new GameSettings(10, 10, 10, _generator);
            IGameSettings gameSettings2 = new GameSettings(gameSettings1);

            Assert.AreEqual(gameSettings1.Rows, gameSettings2.Rows);
            Assert.AreEqual(gameSettings1.Columns, gameSettings2.Columns);
            Assert.AreEqual(gameSettings1.MineCount, gameSettings2.MineCount);
            Assert.AreEqual(gameSettings1.PointGenerator, gameSettings2.PointGenerator);
        }

        private IPointGenerator _generator;

    }
}
