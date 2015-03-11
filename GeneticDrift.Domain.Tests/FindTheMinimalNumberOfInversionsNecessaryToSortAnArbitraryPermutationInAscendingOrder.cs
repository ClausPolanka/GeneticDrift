using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace GeneticDrift.Domain.Tests
{
    [TestFixture]
    public class FindTheMinimalNumberOfInversionsNecessaryToSortAnArbitraryPermutationInAscendingOrder
    {
        OrientedPairsFinder finder = new OrientedPairsFinder();
        PermutationInverter inverter = new PermutationInverter();
        
        [TestCase("8 0 3 1 6 5 -2 4 7")]
        public void TestName(string input)
        {

            var length = input[0].ToString();
            var permString = input.Substring(2);
            var permutation = input.Split(' ').Skip(1).Select(int.Parse).ToList();
            var pairs = finder.Find(permutation.ToArray());
            
            var scoresPair = new List<Tuple<int, int[]>>();

            foreach (var pair in pairs)
            {
                var newInput = string.Format("{0} {1} {2} {3} {4} {5}", length, permString, pair[0], permutation.IndexOf(pair[0]), pair[1], permutation.IndexOf(pair[1]));
                
                var score = CalcScore(newInput);

                scoresPair.Add(new Tuple<int, int[]>(score, pair));
            }


        }

        private int CalcScore(string newInput)
        {
            var invPermString = inverter.InvertPermutation(newInput);
            var invPerm = invPermString.Split(' ').Skip(1).Select(int.Parse).ToList();
            var newPair = finder.Find(invPerm.ToArray());
            var score = newPair.Count();
            return score;
        }

        public void old(string input)
        {
            var perm = ToIntArray(input);

            var finder = new OrientedPairsFinder();
            var ops = finder.Find(perm).ToList();

            var op1 = CreateOrientedPair(ops[0], perm);
            var input1 = string.Format("{0} {1} {2} {3} {4}", input, op1.Xi, op1.i, op1.Xj, op1.j);
            
            var op2 = CreateOrientedPair(ops[1], perm);
            var input2 = string.Format("{0} {1} {2} {3} {4}", input, op2.Xi, op2.i, op2.Xj, op2.j);

            var inverter = new PermutationInverter();

            var invertPermutation1 = inverter.InvertPermutation(input1);
            var ops1 = finder.Find(ToIntArray(invertPermutation1)).ToList();

            var invertPermutation2 = inverter.InvertPermutation(input2);
            var ops2 = finder.Find(ToIntArray(invertPermutation2)).ToList();

            var newInput = input[0] + " " + invertPermutation1;
            var op11 = CreateOrientedPair(ops1[0], ToIntArray(newInput));
            var input11 = string.Format("{0} {1} {2} {3} {4}", newInput, op11.Xi, op11.i, op11.Xj, op11.j);
            var op12 = CreateOrientedPair(ops1[1], ToIntArray(newInput));
            var input12 = string.Format("{0} {1} {2} {3} {4}", newInput, op12.Xi, op12.i, op12.Xj, op12.j);
            var op13 = CreateOrientedPair(ops1[2], ToIntArray(newInput));
            var input13 = string.Format("{0} {1} {2} {3} {4}", newInput, op13.Xi, op13.i, op13.Xj, op13.j);

            var invPerm11 = inverter.InvertPermutation(input11);
            var ops11 = finder.Find(ToIntArray(invPerm11)).ToList();
            var invPerm12 = inverter.InvertPermutation(input12);
            var ops12 = finder.Find(ToIntArray(invPerm12)).ToList();
            var invPerm13 = inverter.InvertPermutation(input13);
            var ops13 = finder.Find(ToIntArray(invPerm13)).ToList();
        }

        private static int[] ToIntArray(string permutationWithPrependedLength)
        {
            return permutationWithPrependedLength.Split(' ').Skip(1).Select(int.Parse).ToArray();
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
