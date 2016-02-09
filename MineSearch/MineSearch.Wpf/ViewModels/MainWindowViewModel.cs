using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using MineSearch.Common;
using MineSearch.Common.ViewModels;
using MineSearch.Wpf.Models;
using Prism.Interactivity.InteractionRequest;

namespace MineSearch.Wpf.ViewModels
{
    /// <summary>
    /// Main window view model.
    /// </summary>
    public class MainWindowViewModel : ViewModelBase
    {
        #region Commands

        /// <summary>
        /// Settings command.
        /// </summary>
        public ICommand SettingsCommand { get; private set; }

        #endregion

        #region Properties

        /// <summary>
        /// Game view model.
        /// </summary>
        public IMineSearchGameViewModel GameViewModel { get; private set; }

        /// <summary>
        /// Settings dialog request.
        /// </summary>
        public InteractionRequest<SettingsViewModel> SettingsRequest { get; private set; } 

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindowViewModel"/> class.
        /// </summary>
        /// <param name="pointGenerator">Point generator to use when creating mine cells.</param>
        public MainWindowViewModel(IPointGenerator pointGenerator)
        {
            var defaultSettings = new DefaultGameSettings(pointGenerator);
            GameViewModel = new MineSearchGameViewModel(defaultSettings);
            SettingsRequest = new InteractionRequest<SettingsViewModel>();
            SettingsCommand = new DelegateCommand(RaiseSettingsRequest);
        }

        private void RaiseSettingsRequest()
        {
            var settingsViewModel = new SettingsViewModel
            {
                GameSettings = GameViewModel.GameSettings
            };
            SettingsRequest.Raise(settingsViewModel, result =>
            {
                if (result.Saved)
                {
                    // Start a new game with the new settings
                    GameViewModel.NewGameCommand.Execute(result.GameSettings);
                }
            });
        }
    }
}
