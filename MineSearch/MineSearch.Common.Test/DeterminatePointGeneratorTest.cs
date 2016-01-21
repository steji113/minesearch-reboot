using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MineSearch.Common.Test
{
    [TestClass]
    public class DeterminatePointGeneratorTest
    {
        [TestMethod]
        public void TestGenerate()
        {
            var points = new List<Point>
            {
                new Point(0, 0),
                new Point(0, 1),
                new Point(1, 0),
                new Point(1, 1)
            };

            var generator = new DeterminatePointGenerator(2, 2, points);
            var generated = new List<Point>(points.Count);
            for (int i = 0; i < points.Count; i++)
            {
                generated.Add(generator.Generate());
            }

            Assert.IsTrue(points.SequenceEqual(generated));
        }
    }
}
