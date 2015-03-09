using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using equalidator;
using NUnit.Framework;

namespace GeneticDrift.Domain.Tests
{
    [TestFixture]
    public class OrientedPairsFinderTest
    {
        [TestCase(1, -2)]
        [TestCase(-1, 2)]
        public void Permutation_containing_two_numbers(int nr1, int nr2)
        {
            var sut = new OrientedPairsFinder();

            var actual = sut.Find(nr1, nr2);

            var expected = new List<int[]> { new[] { nr1, nr2 } };
            Equalidator.AreEqual(actual, expected, true);
        }
    }

    public class OrientedPairsFinder
    {
        public IEnumerable<int[]> Find(params int[] numbers)
        {
            var result = new List<int[]>();
            var sum = numbers[0] + numbers[1];
            
            if (sum == -1)
                result.Add(new[] { numbers[0], numbers[1] });
            
            return result;
        }
    }
}