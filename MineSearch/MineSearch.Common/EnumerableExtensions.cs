using System.Collections.Generic;
using System.Linq;

namespace MineSearch.Common
{
    /// <summary>
    /// Static extension methods relating to <see cref="IEnumerable{T}"/>.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Determines if this <see cref="IEnumerable{T}"/> contains all of the items in 
        /// a second <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <typeparam name="T">Item type.</typeparam>
        /// <param name="a">First sequence.</param>
        /// <param name="b">Second sequence.</param>
        /// <returns></returns>
        public static bool ContainsAll<T>(this IEnumerable<T> a, IEnumerable<T> b)
        {
            return !b.Except(a).Any();
        }
    }
}
