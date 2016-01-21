using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MineSearch.Game.Test
{
    [TestClass]
    public class GameSettingsTest
    {

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestInvalidMineCount()
        {
            // More mines than there are cells
            IGameSettings gameSettings = new GameSettings(1, 1, 2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestNoMines()
        {
            IGameSettings gameSettings = new GameSettings(1, 1, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestNegativeMines()
        {
            IGameSettings gameSettings = new GameSettings(1, 1,-1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestNoRows()
        {
            IGameSettings gameSettings = new GameSettings(0, 1, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestNegativeRows()
        {
            IGameSettings gameSettings = new GameSettings(-1, 1, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestNoColumns()
        {
            IGameSettings gameSettings = new GameSettings(1, 0, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestNegativeColumns()
        {
            IGameSettings gameSettings = new GameSettings(1, -1, 1);
        }

    }
}
