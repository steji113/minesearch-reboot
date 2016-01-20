using System.Collections.Generic;

namespace MineSearch.Common
{
    /// <summary>
    /// Defines an interface for defining a random <see cref="Point"/>.
    /// </summary>
    public interface IRandomPointGenerator
    {
        /// <summary>
        /// Generates a random point.
        /// </summary>
        /// <param name="maxRow">Maximum row.</param>
        /// <param name="maxColumn">Maximum column.</param>
        /// <returns>Random point.</returns>
        Point Generate(int maxRow, int maxColumn);

        /// <summary>
        /// Generates a collection of random points.
        /// </summary>
        /// <param name="numPoints">Number of points to generate.</param>
        /// <param name="maxRow">Maximum row.</param>
        /// <param name="maxColumn">Maximum column.</param>
        /// <returns></returns>
        ICollection<Point> Generate(int numPoints, int maxRow, int maxColumn);
    }

}