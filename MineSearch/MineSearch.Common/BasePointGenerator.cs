using System.Collections.Generic;

namespace MineSearch.Common
{
    public abstract class BasePointGenerator : IPointGenerator
    {
        protected BasePointGenerator(int maxRow, int maxColumn)
        {
            _maxRow = maxRow;
            _maxColumn = maxColumn;
        }

        /// <summary>
        /// Generates a point.
        /// </summary>
        /// <param name="maxRow">Maximum row.</param>
        /// <param name="maxColumn">Maximum column.</param>
        /// <returns>Point.</returns>
        public abstract Point Generate();

        /// <summary>
        /// Generates a collection of points.
        /// </summary>
        /// <param name="numPoints">Number of points to generate.</param>
        /// <param name="maxRow">Maximum row.</param>
        /// <param name="maxColumn">Maximum column.</param>
        /// <returns>Collection of points.</returns>
        public ICollection<Point> Generate(int numPoints)
        {
            var points = new List<Point>(numPoints);
            do
            {
                var point = Generate();
                if (!points.Contains(point))
                {
                    points.Add(point);
                }
            }
            while (points.Count != numPoints);
            return points;
        }

        #region Fields

        protected readonly int _maxRow;
        protected readonly int _maxColumn;

        #endregion
    }
}
