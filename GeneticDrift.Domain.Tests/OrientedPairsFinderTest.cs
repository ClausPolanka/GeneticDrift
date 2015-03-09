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
        public void Permutation_containing_two_numbers_beeing_an_oriented_pair(int nr1, int nr2)
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

        [Test]
        public void Spec_expample()
        {
            var permutation = new[] { 3, 1, 6, 5, -2, 4 };
            var sut = new OrientedPairsFinder();

            var orientedPairs = sut.Find(permutation);

            var expected = new List<int[]>
            {
                new[] { 3, -2 },
                new[] { 1, -2 },
            };
            Equalidator.AreEqual(orientedPairs, expected, true);
        }
    }

    public class OrientedPairsFinder
    {
        public IEnumerable<int[]> Find(params int[] permutation)
        {
            var result = new List<int[]>();

            for (var i = 0; i < permutation.Length; i++)
            {
                if ((i + 1) == permutation.Length)
                    break;

                for (var j = i + 1; j < permutation.Length; j++)
                {
                    if (j == permutation.Length)
                        break;

                    var sum = permutation[i] + permutation[j];

                    if (sum == -1 || sum == 1)
                        result.Add(new[] { permutation[i], permutation[j] });
                }
            }

            return result;
        }
    }
}