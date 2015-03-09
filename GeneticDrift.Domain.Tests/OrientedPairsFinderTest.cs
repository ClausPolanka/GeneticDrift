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

        [TestCase(1, -2, 3)]
        public void Permutation_containing_three_numbers_with_two_in_order_oriented_pairs(int nr1, int nr2, int nr3)
        {
            var sut = new OrientedPairsFinder();

            var actual = sut.Find(nr1, nr2, nr3);

            var expected = new List<int[]>
            {
                new[] { nr1, nr2 },
                new[] { nr2, nr3 },
            };
            Equalidator.AreEqual(actual, expected, true);
        }

        [TestCase(1, 3, -2)]
        public void Permutation_containing_three_numbers_with_two_unordered_oriented_pairs(int nr1, int nr2, int nr3)
        {
            var sut = new OrientedPairsFinder();

            var actual = sut.Find(nr1, nr2, nr3);

            var expected = new List<int[]>
            {
                new[] { nr1, nr3 },
                new[] { nr2, nr3 },
            };
            Equalidator.AreEqual(actual, expected, true);
        }

    }

    public class OrientedPairsFinder
    {
        public IEnumerable<int[]> Find(params int[] numbers)
        {
            var result = new List<int[]>();

            for (var i = 0; i < numbers.Length; i++)
            {
                if ((i + 1) == numbers.Length)
                    break;
                
                var sum = numbers[i] + numbers[i + 1];

                if (sum == -1 || sum == 1)
                    result.Add(new[] { numbers[i], numbers[i+1] });
            }

            return result;
        }
    }
}