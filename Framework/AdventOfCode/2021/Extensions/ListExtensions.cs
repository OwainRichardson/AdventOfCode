﻿using System.Collections.Generic;

namespace AdventOfCode._2021.Extensions
{
    public static class ListExtensions
    {
        public static long Product(this IEnumerable<int> list)
        {
            long product = 1;

            foreach (var item in list)
            {
                product *= item;
            }

            return product;
        }

        public static long Product(this IEnumerable<long> list)
        {
            long product = 1;

            foreach (var item in list)
            {
                product *= item;
            }

            return product;
        }

        public static List<int> TryAdd(this List<int> list, int value)
        {
            if (!list.Contains(value))
            {
                list.Add(value);
            }

            return list;
        }
    }
}
