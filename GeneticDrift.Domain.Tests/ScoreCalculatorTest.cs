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
        public void Spec_example(string input, int expected)
        {
            
        }
    }
}
