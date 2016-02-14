using Microsoft.VisualStudio.TestTools.UnitTesting;
using MineSearch.Common;
using MineSearch.Game;
using MineSearch.Wpf.ViewModels;

namespace MineSearch.Wpf.Test.ViewModelTests
{
    [TestClass]
    public class SettingsViewModelTest
    {
        [TestMethod]
        public void TestSave()
        {
            var gameSettings = new GameSettings(2, 2, 1, new RandomPointGenerator());
            var viewModel = new SettingsViewModel
            {
                GameSettings = gameSettings
            };
            viewModel.SaveCommand.Execute(null);
            Assert.IsTrue(viewModel.Saved);
        }

        [TestMethod]
        public void TestCancel()
        {
            var gameSettings = new GameSettings(2, 2, 1, new RandomPointGenerator());
            var viewModel = new SettingsViewModel
            {
                GameSettings = gameSettings
            };
            viewModel.CancelCommand.Execute(null);
            Assert.IsFalse(viewModel.Saved);
        }
    }
}
