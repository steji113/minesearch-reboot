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
        public void TestFromIndex()
        {
            int columns = 3;

            int index = 0;
            var point = Point.FromIndex(index, columns);
            Assert.AreEqual(point.X, 0);
            Assert.AreEqual(point.Y, 0);

            index = 4;
            point = Point.FromIndex(index, columns);
            Assert.AreEqual(point.X, 1);
            Assert.AreEqual(point.Y, 1);

            index = 8;
            point = Point.FromIndex(index, columns);
            Assert.AreEqual(point.X, 2);
            Assert.AreEqual(point.Y, 2);
        }
    }
}
