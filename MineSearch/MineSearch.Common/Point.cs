using System;

namespace MineSearch.Common
{
    /// <summary>
    /// Represents a point in a grid.
    /// </summary>
    public class Point
    {
        #region Properties

        /// <summary>
        /// X coordinate.
        /// </summary>
        public int X { get; private set; }

        /// <summary>
        /// Y coordinate.
        /// </summary>
        public int Y { get; private set; }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="Point"/> class.
        /// </summary>
        /// <param name="x">X coordinate.</param>
        /// <param name="y">Y coordinate.</param>
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Point"/> class with the
        /// specified index.
        /// </summary>
        /// <param name="index">Index into the grid.</param>
        /// <param name="columns">Number of columns in the grid.</param>
        /// <returns>(x, y) coordinates of the cell</returns>
        public static Point FromIndex(int index, int columns)
        {
            if (columns <= 0)
            {
                throw new ArgumentOutOfRangeException("columns", "columns must be greater than 0");
            }
            int x = index / columns;
            int y = index % columns;
            return new Point(x, y);
        }

        #region Equality Implementation

        public override bool Equals(Object obj)
        {
            var point = obj as Point;
            if ((object) point == null)
            {
                return false;
            }

            return (X == point.X) && (Y == point.Y);
        }

        public bool Equals(Point point)
        {
            if ((object) point == null)
            {
                return false;
            }

            return (X == point.X) && (Y == point.Y);
        }

        public override int GetHashCode()
        {
            return X ^ Y;
        }

        public static bool operator ==(Point a, Point b)
        {
            if (ReferenceEquals(a, b))
            {
                return true;
            }

            if (((object) a == null) || ((object) b == null))
            {
                return false;
            }

            return a.X == b.X && a.Y == b.Y;
        }


        public static bool operator !=(Point a, Point b)
        {
            return !(a == b);
        }

        #endregion
    }
}
