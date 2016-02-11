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
            DigitalDisplay.DataContext = new DigitalDisplayViewModel(3);
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var vm = DigitalDisplay.DataContext as DigitalDisplayViewModel;
            vm.Value++;
        }
    }
}
