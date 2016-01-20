using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MineSearch.Common;
using MineSearch.Game;

namespace MineSearch.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Create some simple game settings
            var gameSettings = new GameSettings
            {
                Rows = 2,
                Columns = 2,
                MineCount = 1
            };
            // Use the default random number generator
            IRandomPointGenerator rndGen = new DefaultRandomPointGenerator();
            var cellFactory = new MineSearchCellsFactory();
            var cells = cellFactory.CreateCells(gameSettings, rndGen);
            var game = new MineSearchGame(cells);
        }
    }
}
