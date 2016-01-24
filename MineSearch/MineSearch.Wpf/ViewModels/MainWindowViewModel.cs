using MineSearch.Common;
using MineSearch.Common.ViewModels;
using MineSearch.Wpf.Models;

namespace MineSearch.Wpf.ViewModels
{
    /// <summary>
    /// Main window view model.
    /// </summary>
    public class MainWindowViewModel : ViewModelBase
    {
        #region Properties

        /// <summary>
        /// Game view model.
        /// </summary>
        public IMineSearchGameViewModel GameViewModel { get; private set; }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindowViewModel"/> class.
        /// </summary>
        /// <param name="pointGenerator">Point generator to use when creating mine cells.</param>
        public MainWindowViewModel(IPointGenerator pointGenerator)
        {
            var defaultSettings = new DefaultGameSettings(pointGenerator);
            GameViewModel = new MineSearchGameViewModel(defaultSettings);
        }
    }
}
