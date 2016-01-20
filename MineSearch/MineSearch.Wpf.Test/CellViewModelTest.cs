using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MineSearch.Common;
using MineSearch.Game;

namespace MineSearch.Wpf.Test
{

    [TestClass]
    public class CellViewModelTest
    {
        [TestInitialize]
        public void TestInitialize()
        {
            // Create some simple game settings.
            _gameSettings = new GameSettings
            {
                Rows = 4,
                Columns = 4,
                MineCount = 4
            };
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
        public void TestFlagCell()
        {

        }

        private IGameSettings _gameSettings;
        private IRandomPointGenerator _randomPointGenerator;
        private IMatrix<ICell> _cells;
        private IMineSearchGame _game;
        private IMineSearchCellsFactory _cellFactory;
    }
}
