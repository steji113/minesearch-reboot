using System;

namespace MineSearch.Common
{
    /// <summary>
    /// Concrete implementation of <see cref="IRandomPointGenerator"/>.
    /// Uses <see cref="System.Random"/> as the random number generator.
    /// </summary>
    public class DefaultRandomPointGenerator : BaseRandomPointGenerator
    {
        /// <summary>
        /// Initializes a new instance of <see cref="DefaultRandomPointGenerator"/>.
        /// </summary>
        public DefaultRandomPointGenerator()
        {
            _generator = new Random();
        }

        /// <summary>
        /// Generates a random point.
        /// </summary>
        /// <param name="maxRow">Maximum row.</param>
        /// <param name="maxColumn">Maximum column.</param>
        /// <returns>Random point.</returns>
        public override Point Generate(int maxRow, int maxColumn)
        {
            int x = _generator.Next(maxColumn);
            int y = _generator.Next(maxRow);
            return new Point(x, y);
        }

        #region Fields

        private readonly Random _generator;

        #endregion;
    }
}
