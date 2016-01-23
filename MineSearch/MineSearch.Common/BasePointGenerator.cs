using System.Collections.Generic;

namespace MineSearch.Common
{
    public abstract class BasePointGenerator : IPointGenerator
    {
        /// <summary>
        /// Generates a point.
        /// </summary>
        /// <param name="maxRow">Maximum row.</param>
        /// <param name="maxColumn">Maximum column.</param>
        /// <returns>Point.</returns>
        public abstract Point Generate(int maxRow, int maxColumn);

        /// <summary>
        /// Generates a collection of points.
        /// </summary>
        /// <param name="numPoints">Number of points to generate.</param>
        /// <param name="maxRow">Maximum row.</param>
        /// <param name="maxColumn">Maximum column.</param>
        /// <returns>Collection of points.</returns>
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
