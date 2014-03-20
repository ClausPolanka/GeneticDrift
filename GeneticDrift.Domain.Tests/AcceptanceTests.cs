using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace GeneticDrift.Domain.Tests
{
    [TestFixture]
    public class AcceptanceTests
    {
        [TestCase("3 1 6 5 -2 4", "2 1 -2 3 -2")]
        public void Level_1(string input, string expected)
        {
            var permutations = new List<int>();
            Array.ForEach(input.Split(' '), s => permutations.Add(int.Parse(s)));
            var positives = permutations.FindAll(p => p >= 0);
            var negatives = permutations.FindAll(p => p < 0);
            var orientedPairs = new List<OrientedPair>();
            foreach (var pos in positives)
            {
                orientedPairs.AddRange(from neg in negatives
                                       where Math.Abs(pos - Math.Abs(neg)) == 1
                                       select new OrientedPair { positive = pos, negative = neg });
            }
            orientedPairs = orientedPairs.OrderBy(p => p.positive).ToList();
            Assert.That(
                string.Format("{0} {1}", orientedPairs.Count, string.Join(" ", orientedPairs)),
                Is.EqualTo(expected), "oriented pairs");
        }
    }

    public struct OrientedPair
    {
        public int positive;
        public int negative;
        public override string ToString()
        {
            return string.Format("{0} {1}", positive, negative);
        }
    }
}