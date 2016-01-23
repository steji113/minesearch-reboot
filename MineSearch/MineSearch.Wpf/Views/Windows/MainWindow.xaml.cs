using Microsoft.Practices.Unity;
using MineSearch.Wpf.ViewModels;

namespace MineSearch.Wpf.Views.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        #region Properties

        public MainWindowViewModel ViewModel { get; set; }

        #endregion

        public MainWindow(IUnityContainer container)
        {
            InitializeComponent();

            ViewModel = container.Resolve<MainWindowViewModel>();
        }
    }
}
