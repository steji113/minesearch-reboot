using System.Collections.Generic;

namespace MineSearch.Common
{
    public abstract class BaseRandomPointGenerator : IRandomPointGenerator
    {
        /// <summary>
        /// Generates a random point.
        /// </summary>
        /// <param name="maxRow">Maximum row.</param>
        /// <param name="maxColumn">Maximum column.</param>
        /// <returns>Random point.</returns>
        public abstract Point Generate(int maxRow, int maxColumn);

        /// <summary>
        /// Generates a collection of random points.
        /// </summary>
        /// <param name="numPoints">Number of points to generate.</param>
        /// <param name="maxRow">Maximum row.</param>
        /// <param name="maxColumn">Maximum column.</param>
        /// <returns></returns>
        public ICollection<Point> Generate(int numPoints, int maxRow, int maxColumn)
        {
            var points = new List<Point>(numPoints);
            do
            {
                var point = Generate(maxRow, maxColumn);
                if (!points.Contains(point))
                {
                    points.Add(point);
                }
            }
            while (points.Count != numPoints);
            return points;
        }
    }
}
