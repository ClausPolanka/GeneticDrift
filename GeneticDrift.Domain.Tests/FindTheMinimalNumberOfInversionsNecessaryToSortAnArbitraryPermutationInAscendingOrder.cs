using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace GeneticDrift.Domain.Tests
{
    [TestFixture]
    public class FindTheMinimalNumberOfInversionsNecessaryToSortAnArbitraryPermutationInAscendingOrder
    {
        [TestCase("8 0 3 1 6 5 -2 4 7")]
        public void TestName(string input)
        {
            var perm = ToIntArray(input);

            var finder = new OrientedPairsFinder();
            var ops = finder.Find(perm).ToList();

            var op1 = CreateOrientedPair(ops[0], perm);
            var input1 = input + " " + op1.Xi + " " + op1.i + " " + op1.Xj + " " + op1.j;
            
            var op2 = CreateOrientedPair(ops[1], perm);
            var input2 = input + " " + op2.Xi + " " + op2.i + " " + op2.Xj + " " + op2.j;

            var inverter = new PermutationInverter();

            var invertPermutation1 = inverter.InvertPermutation(input1);
            var ops1 = finder.Find(ToIntArray(invertPermutation1));

            var invertPermutation2 = inverter.InvertPermutation(input2);
            var ops2 = finder.Find(ToIntArray(invertPermutation2));


        }

        private static int[] ToIntArray(string invertPermutation1)
        {
            return invertPermutation1.Split(' ').Skip(1).Select(int.Parse).ToArray();
        }

        private static OrientedPair CreateOrientedPair(int[] oldOp, int[] perm)
        {
            var X1i = oldOp[0];
            var i1 = perm.ToList().IndexOf(X1i);
            var X1j = oldOp[1];
            var j1 = perm.ToList().IndexOf(X1j);
            var op = new OrientedPair { i = i1, j = j1, Xi = X1i, Xj = X1j };
            return op;
        }
    }
}
