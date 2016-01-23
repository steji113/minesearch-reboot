using MineSearch.Common;
using MineSearch.Common.ViewModels;
using MineSearch.Wpf.Models;

namespace MineSearch.Wpf.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region Properties

        /// <summary>
        /// Game view model.
        /// </summary>
        public IMineSearchGameViewModel GameViewModel { get; private set; }

        #endregion

        public MainWindowViewModel(IPointGenerator pointGenerator)
        {
            var defaultSettings = new DefaultGameSettings(pointGenerator);
            GameViewModel = new MineSearchGameViewModel(defaultSettings);
        }
    }
}
