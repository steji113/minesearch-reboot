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
            // List of points the generator will use.
            var points = new List<Point>
            {
                new Point(0, 0),
                new Point(0, 1),
                new Point(1, 0),
                new Point(1, 1)
            };

            // Prime the generator.
            var generator = new DeterminatePointGenerator(points);
            var generated = new List<Point>(points.Count);
            // We want the same number of points as we passed in.
            for (int i = 0; i < points.Count; i++)
            {
                generated.Add(generator.Generate(2, 2));
            }

            Assert.IsTrue(points.SequenceEqual(generated));
        }

        [TestMethod]
        public void TestGenerateRollover()
        {
            // List of points the generator will use.
            var points = new List<Point>
            {
                new Point(0, 0),
                new Point(0, 1),
                new Point(1, 0),
                new Point(1, 1)
            };

            // Setup a list of expected points by duplicating
            // the original points.
            var expected = new List<Point>(points);
            expected.AddRange(points.Select(p => new Point(p.X, p.Y)));

            // Prime the generator.
            var generator = new DeterminatePointGenerator(points);
            var generated = new List<Point>(points.Count);
            // We want the generator to rollover and reuse the points.
            for (int i = 0; i < points.Count * 2; i++)
            {
                generated.Add(generator.Generate(2, 2));
            }

            Assert.IsTrue(expected.SequenceEqual(generated));
        }
    }
}
