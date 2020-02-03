using AdventOfCode._2015.Models;
using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode._2015
{
    public class D_24_1
    {
        public static void Execute()
        {
            var inputs = File.ReadAllLines(@"C:\Work\Misc Projects\AdventOfCode\AdventOfCode\AdventOfCode\2015\Data\day24_full.txt");
            List<int> weights = ParseInputs(inputs);

            var total = weights.Sum();
            var packweight = total / 3;


            bool any = false;
            int i = 1;
            while (!any)
            {
                var permutes = CombinationsRosettaWoRecursion(weights.ToArray(), i);

                any = permutes.Any(x => x.Sum() == packweight);

                if (!any)
                {
                    i++;
                }
            }

            var permutations = CombinationsRosettaWoRecursion(weights.ToArray(), i).Where(x => x.Sum() == packweight);

            List<Pack> packs = new List<Pack>();
            foreach (var permutation in permutations)
            {
                Pack pack = new Pack
                {
                    Group1 = permutation.ToList()
                };

                packs.Add(pack);
            }

            Console.Write($"Best QE = ");
            CustomConsoleColour.SetAnswerColour();
            Console.Write(packs.Min(x => x.Group1QE));
            Console.ResetColor();
            Console.WriteLine();
        }

        private static List<int> ParseInputs(string[] inputs)
        {
            List<int> weights = new List<int>();

            foreach (var input in inputs)
            {
                weights.Add(int.Parse(input));
            }

            return weights;
        }

        private static IEnumerable<int[]> CombinationsRosettaWoRecursion(int m, int n)
        {
            int[] result = new int[m];
            Stack<int> stack = new Stack<int>(m);
            stack.Push(0);
            while (stack.Count > 0)
            {
                int index = stack.Count - 1;
                int value = stack.Pop();
                while (value < n)
                {
                    result[index++] = value++;
                    stack.Push(value);
                    if (index != m) continue;
                    yield return (int[])result.Clone(); // thanks to @xanatos
                                                        //yield return result;
                    break;
                }
            }
        }

        public static IEnumerable<T[]> CombinationsRosettaWoRecursion<T>(T[] array, int m)
        {
            if (array.Length < m)
                throw new ArgumentException("Array length can't be less than number of selected elements");
            if (m < 1)
                throw new ArgumentException("Number of selected elements can't be less than 1");
            T[] result = new T[m];
            foreach (int[] j in CombinationsRosettaWoRecursion(m, array.Length))
            {
                for (int i = 0; i < m; i++)
                {
                    result[i] = array[j[i]];
                }
                yield return result;
            }
        }
    }
}