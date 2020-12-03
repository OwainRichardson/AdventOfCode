using System.Collections.Generic;

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
    }
}
