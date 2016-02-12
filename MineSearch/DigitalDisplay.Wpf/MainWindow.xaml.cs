using System.Windows;
using DigitalDisplay.Wpf.ViewModels;

namespace DigitalDisplay.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as MainWindowViewModel;
            vm.GameDurationSeconds++;
        }
    }
}
