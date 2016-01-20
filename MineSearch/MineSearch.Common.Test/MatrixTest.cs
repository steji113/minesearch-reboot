using System;
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
            var test = "hello world";
            IMatrix<string> matrix = new Matrix<string>(2, 2);
            matrix[0, 1] = test;

            Assert.AreEqual(2, matrix.IndexOf(test));
        }

        [TestMethod]
        public void TestAdjacent()
        {
            IMatrix<string> matrix = new Matrix<string>(3, 3);
            List<string> items = new List<string>(9);
            for (int i = 0; i < items.Capacity; i++)
            {
                items.Add(string.Format("{0}", i));
            }

            var index = 0;
            for (int y = 0; y < matrix.Rows; y++)
            {
                int x;
                for (x = 0; x < matrix.Columns; x++)
                {
                    matrix[x, y] = items[index++];
                }
            }

            var middleCell = matrix[1, 1];
            int middleIndex = matrix.IndexOf(middleCell);
            items.RemoveAt(middleIndex);
            var adjacentCells = matrix.GetAdjacentCells(middleCell);

            Assert.IsTrue(items.SequenceEqual(adjacentCells));
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
        public void TestAdd()
        {
            IMatrix<string> matrix = new Matrix<string>(1, 1);
            string item = "";
            matrix.AddItem(item);

            Assert.AreEqual(item, matrix[0, 0]);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestAddFullCapacity()
        {
            IMatrix<string> matrix = new Matrix<string>(1, 1);
            string item = "";
            matrix.AddItem(item);
            matrix.AddItem(item);
        }
    }
}
