using System.Collections.Generic;

namespace GeneticDrift.Domain
{
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

                    if ((sum == -1 || sum == 1) && (permutation[i] < 0 || permutation[j] < 0))
                        result.Add(new[] { permutation[i], permutation[j] });
                }
            }

            return result;
        }
    }
}