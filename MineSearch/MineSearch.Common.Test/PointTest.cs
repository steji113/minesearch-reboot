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

        [TestMethod]
        public void TestEquality()
        {
            // Setup.
            var p1 = new Point(1, 0);
            var p2 = new Point(1, 0);
            var p3 = new Point(0, 1);

            // Dissimilar objects.
            var s = "Test";
            Assert.IsFalse(p1.Equals(s));

            // Null object.
            Assert.IsFalse(p1.Equals((object) null));

            // Null point.
            Assert.IsFalse(p1.Equals(null));

            // Point as object equals.
            Assert.IsTrue(p1.Equals((object) p2));
            Assert.IsTrue(p2.Equals((object) p1));
            Assert.IsFalse(p3.Equals((object) p1));

            // Point as point equals.
            Assert.IsTrue(p1.Equals(p2));
            Assert.IsTrue(p2.Equals(p1));
            Assert.IsFalse(p3.Equals(p1));

            // Reference equals.
            Assert.IsTrue(p1 == p1);

            // Null checks.
            Assert.IsFalse(null == p1);
            Assert.IsFalse(p1 == null);

            // Two different objects.
            Assert.IsTrue(p1 == p2);

            // Inequality operator.
            Assert.IsTrue(p1 != p3);

        }

        [TestMethod]
        public void TestHashCode()
        {
            int x = 2;
            int y = 4;
            var p = new Point(x, y);

            Assert.AreEqual(x ^ y, p.GetHashCode());
        }
    }
}
