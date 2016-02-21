using System.Collections.Generic;
using MineSearch.Common;

namespace MineSearch.Game
{
    /// <summary>
    /// Handy extension methods.
    /// </summary>
    public static class ExtensionMethods
    {
        public static IEnumerable<ICell> GetAdjacentCells(this IMatrix<ICell> matrix, Point point)
        {
            // Validate the point
            if (point.X < 0 || point.X > matrix.Columns ||
                point.Y < 0 || point.Y > matrix.Rows)
            {
                yield break;
            }

            int x = point.X;
            int y = point.Y;

            // Up left
            if (x > 0 && y > 0)
            {
                yield return matrix[x - 1, y - 1];
            }
            // Up
            if (y > 0)
            {
                yield return matrix[x, y - 1];
            }
            // Up right
            if (x < matrix.Columns - 1 && y > 0)
            {
                yield return matrix[x + 1, y - 1];
            }
            // Left
            if (x > 0)
            {
                yield return matrix[x - 1, y];
            }
            // Right
            if (x < matrix.Columns - 1)
            {
                yield return matrix[x + 1, y];
            }
            // Down left
            if (x > 0 && y < matrix.Rows - 1)
            {
                yield return matrix[x - 1, y + 1];
            }
            // Down
            if (y < matrix.Rows - 1)
            {
                yield return matrix[x, y + 1];
            }
            // Down right
            if (x < matrix.Columns - 1 && y < matrix.Rows - 1)
            {
                yield return matrix[x + 1, y + 1];
            }
        }
    }
}
