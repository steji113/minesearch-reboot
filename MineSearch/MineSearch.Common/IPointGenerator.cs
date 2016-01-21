using System.Collections.Generic;

namespace MineSearch.Common
{
    /// <summary>
    /// Defines an interface for generating a <see cref="Point"/>.
    /// </summary>
    public interface IPointGenerator
    {
        /// <summary>
        /// Generates a point.
        /// </summary>
        /// <param name="maxRow">Maximum row.</param>
        /// <param name="maxColumn">Maximum column.</param>
        /// <returns>Point.</returns>
        Point Generate();

        /// <summary>
        /// Generates a collection of points.
        /// </summary>
        /// <param name="numPoints">Number of points to generate.</param>
        /// <param name="maxRow">Maximum row.</param>
        /// <param name="maxColumn">Maximum column.</param>
        /// <returns>Collection of points.</returns>
        ICollection<Point> Generate(int numPoints);
    }

}