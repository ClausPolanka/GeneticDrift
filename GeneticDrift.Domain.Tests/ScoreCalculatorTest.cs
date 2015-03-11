using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace GeneticDrift.Domain.Tests
{
    [TestFixture]
    public class ScoreCalculatorTest
    {
        [TestCase("8 0 3 1 6 5 -2 4 7 1 2 -2 5", 2)]
        [TestCase("8 0 3 1 6 5 -2 4 7 3 1 -2 5", 4)]
        public void Spec_example(string input, int expected)
        {
            var inverted = new PermutationInverter().InvertPermutation(input);
            var orientedPairs = new OrientedPairsFinder().Find(inverted.Split(' ').Select(int.Parse).ToArray());
            var actual = orientedPairs.Count();
            Assert.That(actual, Is.EqualTo(expected), "oriented pairs after inverting permutation");
        }

        [TestCase("8 0 3 1 6 5 -2 4 7 1 2 -2 5", -1)]
        public void Level_3_Input_1(string input, int expected)
        {
            var inverted = new PermutationInverter().InvertPermutation(input);
            var orientedPairs = new OrientedPairsFinder().Find(inverted.Split(' ').Select(int.Parse).ToArray());
            var actual = orientedPairs.Count();
            Assert.That(actual, Is.EqualTo(expected), "oriented pairs after inverting permutation");
        }
    }
}
