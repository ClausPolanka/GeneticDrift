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
        
        [TestCase(0, 1)]
        public void Permutation_containing_two_numbers_not_an_oriented_pair(int nr1, int nr2)
        {
            var sut = new OrientedPairsFinder();

            var actual = sut.Find(nr1, nr2);

            var expected = new List<int[]>();
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
        public void Spec_expample_unordered()
        {
            var permutation = new[] { 3, 1, 6, 5, -2, 4 };
            var sut = new OrientedPairsFinder();

            var orientedPairs = sut.Find(permutation);

            var expected = new List<int[]>
            {
                new[] { 3, -2 },
                new[] { 1, -2 }
            };
            Equalidator.AreEqual(orientedPairs, expected, true);
        }

        [Test]
        public void Spec_expample()
        {
            var permutation = new[] { 3, 1, 6, 5, -2, 4 };
            var sut = new OrientedPairsFinder();

            var orientedPairs = sut.Find(permutation);

            var expected = new List<int[]>
            {
                new[] { 1, -2 },
                new[] { 3, -2 }
            };

            Equalidator.AreEqual(orientedPairs.OrderBy(op => op[0]), expected, true);
        }

        [Test]
        public void Spec_example_oriented_pairs_as_string()
        {
            var permutation = new[] { 3, 1, 6, 5, -2, 4 };
            var finder = new OrientedPairsFinder();
            var orientedPairs = finder.Find(permutation);
            var ordered = orientedPairs.OrderBy(op => op[0]).ToList();
            var sut = new OrientedPairsToStringConverter();

            var actual = sut.Convert(ordered);

            Assert.That(actual, Is.EqualTo("2 1 -2 3 -2"), "oriented pairs");
        }

        [Test]
        public void Level_1_Input_1()
        {
            var permutation = new[] { 8, 0, 3, 1, 6, 5, -2, 4, 7 };

            var orderedOrientedPairs = new OrientedPairsFinder()
                .Find(permutation.Skip(1).ToArray())
                .OrderBy(op => op[0]).ToList();

            var actual = new OrientedPairsToStringConverter().Convert(orderedOrientedPairs);

            Assert.That(actual, Is.EqualTo("2 1 -2 3 -2"), "oriented pairs");
        }

        [Test]
        public void Level_1_Input_2()
        {
            var permutation = new[] { 9, 3, 1, 6, 5, -2, 4, -7, 8, 9 };

            var orderedOrientedPairs = new OrientedPairsFinder()
                .Find(permutation.Skip(1).ToArray())
                .OrderBy(op => op[0]).ToList();

            var actual = new OrientedPairsToStringConverter().Convert(orderedOrientedPairs);

            Assert.That(actual, Is.EqualTo("4 -7 8 1 -2 3 -2 6 -7"), "oriented pairs");
        }

        //[Test]
        public void Level_1_Input_3()
        {
            var permutation = new[] { 8, 0, -5, -6, -1, -3, -2, 4, 7 };

            var orderedOrientedPairs = new OrientedPairsFinder()
                .Find(permutation.Skip(1).ToArray())
                .OrderBy(op => op[0]).ToList();

            var actual = new OrientedPairsToStringConverter().Convert(orderedOrientedPairs);

            Assert.That(actual, Is.EqualTo("4 -7 8 1 -2 3 -2 6 -7"), "oriented pairs");
        }

    }
}