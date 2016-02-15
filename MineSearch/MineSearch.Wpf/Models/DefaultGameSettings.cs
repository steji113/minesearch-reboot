using MineSearch.Common;
using MineSearch.Game;

namespace MineSearch.Wpf.Models
{
    public class DefaultGameSettings : GameSettings
    {
        #region Constants

        private const int DefaultRows = 9;
        private const int DefaultColumns = 9;
        private const int DefaultMineCount = 15;

        #endregion

        public DefaultGameSettings(IPointGenerator generator)
            : base(DefaultRows, DefaultColumns, DefaultMineCount, generator)
        {
            UseQuestionableState = true;
        }
    }
}
