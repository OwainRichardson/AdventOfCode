using AdventOfCode._2017.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode._2017
{
    public static class D_09_1
    {
        public static void Execute()
        {
            string input = File.ReadAllLines(@"C:\Work\Misc Projects\AdventOfCode\AdventOfCode\AdventOfCode\2017\Data\day09_full.txt")[0];

            input = ResolveIgnores(input);
            input = RemoveGarbage(input);

            int total = CountGroups(input);

            Console.WriteLine(total);
        }

        private static int CountGroups(string input)
        {
            int total = 0;
            int level = 1;

            foreach (Char c in input)
            {
                if (c.ToString() == "{")
                {
                    total += level;
                    level++;
                }
                else if (c.ToString() == "}")
                {
                    level--;
                }
            }

            return total;
        }

        private static string RemoveGarbage(string input)
        {
            while (input.Contains("<") || input.Contains(">"))
            {
                int startIndex = input.IndexOf("<");
                int endIndex = input.IndexOf(">");

                input = input.Remove(startIndex, (endIndex - startIndex) + 1);
            }

            return input;
        }

        private static string ResolveIgnores(string input)
        {
            while (input.Contains("!"))
            {
                int index = input.IndexOf("!");
                input = input.Remove(index, 2);
            }

            return input;
        }
    }
}
