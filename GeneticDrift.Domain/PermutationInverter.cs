using System.Collections.Generic;
using System.Linq;

namespace GeneticDrift.Domain
{
    public class PermutationInverter
    {
        public string InvertPermutation(string input)
        {
            var splittedInput = input.Split(' ').Select(int.Parse).ToList();
            var length = splittedInput[0];
            var permutation = splittedInput.GetRange(1, length);
            var orientedPairs = Parse(splittedInput.Skip(1 + length).ToList());

            var actual = Invert(length, permutation, orientedPairs);
            return actual;
        }

        
        private List<OrientedPair> Parse(List<int> input)
        {
            var result = new List<OrientedPair>();
            
            for (var i = 0; i < input.Count; i += 4)
                result.Add(new OrientedPair { Xi = input[i], i = input[i + 1], Xj = input[i + 2], j = input[i + 3] });
            
            return result;
        }

        public string Invert(int length, List<int> permutation, List<OrientedPair> orientedPairs)
        {
            foreach (var oP in orientedPairs)
            {
                if (oP.Xi + oP.Xj == 1)
                {
                    var range = permutation.GetRange(oP.i, oP.j - oP.i);
                    range.Reverse();
                    range = ConvertNegativesToPositivesAndViceVersa(range);
                    permutation.RemoveRange(oP.i, oP.j - oP.i);
                    permutation.InsertRange(oP.i, range);
                    continue;
                }
                if (oP.Xi + oP.Xj == -1)
                {
                    var range = permutation.GetRange(oP.i + 1, oP.j - oP.i);
                    range.Reverse();
                    range = ConvertNegativesToPositivesAndViceVersa(range);
                    permutation.RemoveRange(oP.i + 1, oP.j - oP.i);
                    permutation.InsertRange(oP.i + 1, range);
                }
            }

            return string.Join(" ", permutation);
        }

        private List<int> ConvertNegativesToPositivesAndViceVersa(List<int> range)
        {
            var result = new List<int>();
            foreach (var val in range)
            {
                if (val > 0)
                {
                    result.Add(val - (val*2));
                }
                else if (val < 0)
                {
                    result.Add(val - (val*2));
                }
                else
                {
                    result.Add(val);
                }
            }
            return result;
        }
    }
}