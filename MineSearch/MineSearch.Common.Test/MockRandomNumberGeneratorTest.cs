using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MineSearch.Common.Test
{
    [TestClass]
    public class MockRandomNumberGeneratorTest
    {
        [TestMethod]
        public void TestGenerate()
        {
            var points = new List<Point>
            {
                new Point(0, 0),
                new Point(1, 1),
                new Point(1, 2),
                new Point(2, 2)
            };

            var generator = new MockRandomPointGenerator(points);
            var generated = new List<Point>(points.Count);
            for (int i = 0; i < points.Count; i++)
            {
                generated.Add(generator.Generate(2, 2));
            }

            Assert.IsTrue(points.SequenceEqual(generated));
        }
    }
}
