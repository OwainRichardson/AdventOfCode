using System;
using System.IO;

namespace AdventOfCode._2017
{
    public static class D_09_2
    {
        public static void Execute()
        {
            string input = File.ReadAllLines(@"C:\Work\Misc Projects\AdventOfCode\AdventOfCode\AdventOfCode\2017\Data\day09_full.txt")[0];

            input = ResolveIgnores(input);
            int amountOfGarbageRemoved = RemoveGarbage(input);

            Console.WriteLine(amountOfGarbageRemoved);
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

        private static int RemoveGarbage(string input)
        {
            int total = 0;

            while (input.Contains("<") || input.Contains(">"))
            {
                int startIndex = input.IndexOf("<");
                int endIndex = input.IndexOf(">");

                input = input.Remove(startIndex, (endIndex - startIndex) + 1);

                total += (endIndex - startIndex) + 1 - 2;
            }

            return total;
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
