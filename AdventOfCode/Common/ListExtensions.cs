using AdventOfCode._2020.Models;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Common
{
    public static class ListExtensions
    {
        public static long Mult(this List<int> input)
        {
            long product = 1;

            foreach (int entry in input)
            {
                product *= entry;
            }

            return product;
        }

        public static long Mult(this List<Congruence> congruences)
        {
            long product = 1;

            foreach (Congruence entry in congruences)
            {
                product *= entry.M;
            }

            return product;
        }

        public static List<string> CustomDistinct(this List<string> perms)
        {
            List<string> stringPerms = perms.Select(x => string.Join("", x)).ToList();

            return stringPerms.Distinct().ToList();
        }
    }
}
