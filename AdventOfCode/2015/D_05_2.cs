using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode._2015
{
    class D_05_2
    {
        public static int _nice = 0;
        public static int _naughty = 0;

        public static void Execute()
        {
            var words = File.ReadAllLines(@"C:\Work\Misc Projects\AdventOfCode\AdventOfCode\AdventOfCode\2015\Data\day5_full.txt");

            foreach (var word in words)
            {
                IsWordNaughtyOrNice(word);
            }

            Console.WriteLine(_nice);
        }

        private static void IsWordNaughtyOrNice(string word)
        {
            var vowels = new List<string> { "a", "e", "i", "o", "u" };
            bool containsSplit = false;
            bool containsRepeatedDouble = false;

            for (int i = 0; i < word.Length; i++)
            {
                if (i + 2 < word.Length)
                {
                    if (word[i] == word[i + 2])
                    {
                        containsSplit = true;
                    }
                }

                if (i + 1 < word.Length)
                {
                    if (Regex.Matches(word, $"{word[i]}{word[i + 1]}").Count >= 2)
                    {
                        containsRepeatedDouble = true;
                    }
                }
            }

            if (containsRepeatedDouble && containsSplit)
            {
                _nice += 1;
            }
            else
            {
                _naughty += 1;
            }
        }
    }
}