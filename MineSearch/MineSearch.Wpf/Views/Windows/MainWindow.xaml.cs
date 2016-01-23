using MineSearch.Wpf.ViewModels;

namespace MineSearch.Wpf.Views.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        #region Properties

        public MainWindowViewModel ViewModel { get; private set; }

        #endregion

        public MainWindow(MainWindowViewModel viewModel)
        {
            InitializeComponent();

            ViewModel = viewModel;
        }
    }
}
