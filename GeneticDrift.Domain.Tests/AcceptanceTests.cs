﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace GeneticDrift.Domain.Tests
{
    [TestFixture]
    public class AcceptanceTests
    {
        private const char WHITE_SPACE = ' ';

        [TestCase("6 3 1 6 5 -2 4", "2 1 -2 3 -2")]
        [TestCase("8 0 3 1 6 5 -2 4 7", "2 1 -2 3 -2")]
        [TestCase("9 3 1 6 5 -2 4 -7 8 9", "4 -7 8 1 -2 3 -2 6 -7")]
        [TestCase("8 0 -5 -6 -1 -3 -2 4 7", "4 -6 7 -5 4 -3 4 0 -1")]
        [TestCase(
            "193 125 133 134 135 136 -52 -51 -50 -49 -48 -47 -46 -45 66 67 68 69 70 71 -38 -37 -36 -35 -34 -33 -32 -31 -30 -29 -132 -131 -130 -193 -192 -191 -190 -189 -188 -187 -186 -185 -184 -183 -182 -181 -180 -179 -178 -177 -176 -175 -174 -173 -172 -171 -170 -169 -77 -76 -75 -74 -73 -72 18 19 20 21 22 23 24 25 26 27 28 -164 -163 -65 -64 -63 -62 -61 -60 -59 -58 -57 -56 -55 -54 -53 39 40 41 42 43 44 159 160 161 162 -17 -16 -15 -14 -13 -12 -11 -10 -9 -8 -7 -6 -5 -4 -3 -2 -1 -168 -167 -166 -165 126 127 128 129 86 87 88 89 90 91 92 93 94 95 96 -124 -123 -122 -121 -120 -119 -118 -117 -116 -115 -114 -113 -112 -111 -110 -109 -108 -107 -106 -105 -104 -103 -102 -101 -100 -99 -98 -97 153 154 155 156 157 158 -148 -147 -146 -145 -144 -143 -142 -141 -140 -139 -138 -137 -85 -84 -83 -82 -81 -80 -79 -78 -152 -151 -150 -149",
            "14 -163 162 -130 129 -45 44 -38 39 -29 28 18 -17 66 -65 71 -72 86 -85 96 -97 125 -124 133 -132 136 -137 153 -152"
            )]
        public void Level_1(string input, string expected)
        {
            var permutations = new List<int>();
            Array.ForEach(input.Split(WHITE_SPACE).Skip(1).ToArray(), s => permutations.Add(int.Parse(s)));
            var orientedPairs = new List<OrientedPair>();
            var neg = permutations.FindAll(p => p < 0);
            var pos = permutations.FindAll(p => p >= 0);

            foreach (var p1 in pos)
            {
                orientedPairs.AddRange(from p2 in neg
                                       where Neighbours(p1, p2)
                                       let i1 = permutations.IndexOf(p1)
                                       let i2 = permutations.IndexOf(p2)
                                       select i1 < i2 ? new OrientedPair(p1, p2) : new OrientedPair(p2, p1));
            }

            orientedPairs = orientedPairs.OrderBy(p => p.x).ToList();

            Assert.That(
                string.Format("{0} {1}", orientedPairs.Count, string.Join(WHITE_SPACE.ToString(), orientedPairs)),
                Is.EqualTo(expected), "oriented pairs");
        }

        private static bool Neighbours(int p1, int p2)
        {
            return Math.Abs(Math.Abs(p1) - Math.Abs(p2)) == 1;
        }
    }

    public struct OrientedPair
    {
        public readonly int x;
        private readonly int y;

        public OrientedPair(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", x, y);
        }
    }
}