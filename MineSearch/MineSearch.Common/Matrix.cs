using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MineSearch.Common
{
    /// <summary>
    /// Represents a strongly typed matrix of objects that can be accessed by coordinates.
    /// </summary>
    /// <typeparam name="T">The type of elements in the matrix.</typeparam>
    public class Matrix<T> : IMatrix<T> where T : class
    {
        #region Properties

        /// <summary>
        /// Number of rows in the matrix.
        /// </summary>
        public int Rows { get; private set; }

        /// <summary>
        /// Number of columns in the matrix.
        /// </summary>
        public int Columns { get; private set; }

        /// <summary>
        /// Size of matrix.
        /// </summary>
        public int Size { get { return Rows * Columns; } }

        #endregion

        #region Operator Overloads

        /// <summary>
        /// Gets or sets the element at the specified coordinates.
        /// </summary>
        /// <param name="x">The zero-based X coordinate of the element to get.</param>
        /// <param name="y">The zero-based Y coordinate of the element to get.</param>
        /// <returns>The element at the specified coordinates.</returns>
        public T this[int x, int y]
        {
            // Due to the way cells are stored in the backing 2-dimensional list,
            // we must swap the X and Y coordinates.
            get
            {
                ValidateCoordinates(x, y);
                return _cells[y][x];
            }
            set
            {
                ValidateCoordinates(x, y);
                _cells[y][x] = value;
            }
        }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix{T}"/> class.
        /// </summary>
        /// <param name="rows">Number of rows.</param>
        /// <param name="columns">Number of columns.</param>
        public Matrix(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            _cells = new List<List<T>>(Rows);
            _points = new List<Point>(Rows * Columns);
            for (int y = 0; y < Rows; y++)
            {
                _cells.Add(new List<T>(Columns));
                for (int x = 0; x < Columns; x++)
                {
                    _cells[y].Add(null);
                    _points.Add(new Point(x, y));
                }
            }
        }

        /// <summary>
        /// Determines the index of a specific item in the <see cref="IMatrix{T}"/>.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="IMatrix{T}"/>.</param>
        /// <returns></returns>
        public int IndexOf(T item)
        {
            int index = 0;
            foreach (var element in this)
            {
                if (element == item)
                {
                    return index;
                }
                index++;
            }
            return 0;
        }

        /// <summary>
        /// Adds an item to the next empty cell.
        /// </summary>
        /// <param name="item"></param>
        public void AddItem(T item)
        {
            var firstEmptyCell = this.First(cell => cell == null);
            int index = IndexOf(firstEmptyCell);
            if (index >= Columns * Rows)
            {
                throw new InvalidOperationException();
            }
            var p = Point.FromIndex(index, Columns);
            this[p.X, p.Y] = item;
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to
        ///  iterate through the collection.
        /// </returns>
        public IEnumerator<T> GetEnumerator()
        {
            for (int y = 0; y < Rows; y++)
            {
                for (int x = 0; x < Columns; x++)
                {
                    yield return this[x, y];
                }
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to 
        /// iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Computes the cells adjacent to the specified item.
        /// </summary>
        /// <param name="item">The item to find.</param>
        /// <returns><see cref="IEnumerable{T}"/> containing adjacent cells.</returns>
        public IEnumerable<T> GetAdjacentCells(T item)
        {
            int index = IndexOf(item);
            var point = Point.FromIndex(index, Columns);
            int x = point.X;
            int y = point.Y;

            // Up left
            if (x > 0 && y > 0)
            {
                yield return this[x - 1, y - 1];
            }
            // Up
            if (y > 0)
            {
                yield return this[x, y - 1];
            }
            // Up right
            if (x < Columns - 1 && y > 0)
            {
                yield return this[x + 1, y - 1];
            }
            // Left
            if (x > 0)
            {
                yield return this[x - 1, y];
            }
            // Right
            if (x < Columns - 1)
            {
                yield return this[x + 1, y];
            }
            // Down left
            if (x > 0 && y < Rows - 1)
            {
                yield return this[x - 1, y + 1];
            }
            // Down
            if (y < Rows - 1)
            {
                yield return this[x, y + 1];
            }
            // Down right
            if (x < Columns - 1 && y < Rows - 1)
            {
                yield return this[x + 1, y + 1];
            }
        }

        private void ValidateCoordinates(int x, int y)
        {
            if (y < 0 || y >= Rows)
            {
                throw new ArgumentOutOfRangeException("y", y,
                    "Y coordinate is not a valid index in the matrix");
            }
            if (x < 0 || x >= Columns)
            {
                throw new ArgumentOutOfRangeException("x", x,
                    "X coordinate is not a valid index in the matrix");
            }
        }

        #region Fields

        private readonly List<List<T>> _cells;
        private readonly List<Point> _points;

        #endregion
    }
}
