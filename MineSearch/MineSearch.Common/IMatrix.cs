using System.Collections.Generic;

namespace MineSearch.Common
{
    /// <summary>
    /// Represents a collection of objects that can be individually accessed by coordinates.
    /// </summary>
    /// <typeparam name="T">The type of elements in the matrix.</typeparam>
    public interface IMatrix<T> : IEnumerable<T> where T : class
    {
        /// <summary>
        /// Number of rows in the matrix.
        /// </summary>
        int Rows { get; }

        /// <summary>
        /// Number of columns in the matrix.
        /// </summary>
        int Columns { get; }

        /// <summary>
        /// Size of matrix.
        /// </summary>
        int Size { get; }

        /// <summary>
        /// Gets or sets the element at the specified coordinates.
        /// </summary>
        /// <param name="x">The zero-based X coordinate of the element to get.</param>
        /// <param name="y">The zero-based Y coordinate of the element to get.</param>
        /// <returns>The element at the specified coordinates.</returns>
        T this[int x, int y]
        {
            get;
            set;
        }

        /// <summary>
        /// Determines the index of a specific item in the <see cref="IMatrix{T}"/>.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="IMatrix{T}"/>.</param>
        /// <returns></returns>
        int IndexOf(T item);

        /// <summary>
        /// Adds an item to the next empty cell.
        /// </summary>
        /// <param name="item"></param>
        void AddItem(T item);
    }
}
