using MineSearch.Common;
using MineSearch.Game;

namespace MineSearch.Wpf.Models
{
    public class DefaultGameSettings : GameSettings
    {
        #region Constants

        private const int DefaultRows = 5;
        private const int DefaultColumns = 5;
        private const int DefaultMineCount = 12;

        #endregion

        public DefaultGameSettings(IPointGenerator generator)
            : base(DefaultRows, DefaultColumns, DefaultMineCount, generator)
        {
        }
    }
}
