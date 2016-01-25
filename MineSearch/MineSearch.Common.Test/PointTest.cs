using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MineSearch.Common.Test
{
    [TestClass]
    public class PointTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestFromIndexInvalidArgument()
        {
            Point.FromIndex(0, 0);
        }

        [TestMethod]
        public void TestFromIndexSquare()
        {
            int rows = 10;
            int columns = 10;
            int index = 0;
            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < columns; x++)
                {
                    var expectedPoint = new Point(x, y);
                    var actualPoint = Point.FromIndex(index++, columns);
                    Assert.AreEqual(expectedPoint, actualPoint);
                }
            }
        }

        [TestMethod]
        public void TestFromIndexRectangle()
        {
            int rows = 5;
            int columns = 10;
            int index = 0;
            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < columns; x++)
                {
                    var expectedPoint = new Point(x, y);
                    var actualPoint = Point.FromIndex(index++, columns);
                    Assert.AreEqual(expectedPoint, actualPoint);
                }
            }
        }
    }
}
