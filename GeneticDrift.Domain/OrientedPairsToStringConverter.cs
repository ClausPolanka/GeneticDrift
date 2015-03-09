using System.Collections.Generic;
using System.Linq;

namespace GeneticDrift.Domain
{
    public class OrientedPairsToStringConverter
    {
        public string Convert(List<int[]> orientedPairs)
        {
            var result = orientedPairs.Count().ToString();
            orientedPairs.ForEach(op => result += string.Format(" {0} {1}", op[0], op[1]));
            return result;
        }
    }
}