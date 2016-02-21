using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MineSearch.Common.Test
{
    [TestClass]
    public class MatrixTest
    {
        [TestMethod]
        public void TestIndexOverload()
        {
            var test = "hello world";
            IMatrix<string> matrix = new Matrix<string>(1, 1);
            matrix[0, 0] = test;

            Assert.AreEqual(test, matrix[0, 0]);
        }

        [TestMethod]
        public void TestIndexOf()
        {
            var added = "hello world";
            var notAdded = "goodbye world";
            IMatrix<string> matrix = new Matrix<string>(2, 2);
            matrix[0, 1] = added;

            // Try finding index of an object that was added.
            Assert.AreEqual(2, matrix.IndexOf(added));

            // Try finding index of an object that was not added.
            Assert.AreEqual(-1, matrix.IndexOf(notAdded));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestXCoordOutOfBounds()
        {
            IMatrix<string> matrix = new Matrix<string>(2, 2);
            matrix[2, 0] = "";
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestNegativeXCoord()
        {
            IMatrix<string> matrix = new Matrix<string>(2, 2);
            matrix[-1, 0] = "";
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestYCoordOutOfBounds()
        {
            IMatrix<string> matrix = new Matrix<string>(2, 2);
            matrix[0, 2] = "";
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestNegativeYCoord()
        {
            IMatrix<string> matrix = new Matrix<string>(2, 2);
            matrix[0, -1] = "";
        }

        [TestMethod]
        public void TestAddItem()
        {
            IMatrix<string> matrix = new Matrix<string>(1, 1);
            string item = "";
            matrix.AddItem(item);

            Assert.AreEqual(item, matrix[0, 0]);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestAddItemFullCapacity()
        {
            IMatrix<string> matrix = new Matrix<string>(1, 1);
            string item = "";
            matrix.AddItem(item);
            matrix.AddItem(item);
        }

        [TestMethod]
        public void TestEnumerator()
        {
            IMatrix<string> matrix = new Matrix<string>(1, 2);
            string item1 = "1";
            string item2 = "2";
            matrix.AddItem(item1);
            matrix.AddItem(item2);

            // Test the strongly typed enumerator.
            int itemNum = 1;
            foreach (var element in matrix)
            {
                Assert.AreEqual(element, itemNum.ToString());
                itemNum++;
            }

            // Test the weakly typed enumerator.
            itemNum = 1;
            foreach (var element in matrix as IEnumerable)
            {
                Assert.AreEqual((string) element, itemNum.ToString());
                itemNum++;
            }
        }
    }
}
