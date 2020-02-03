using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2015
{
    public class D_05_1
    {
        public static int _nice = 0;
        public static int _naughty = 0;

        public static void Execute()
        {
            var words = File.ReadAllLines(@"C:\Work\Misc Projects\AdventOfCode\AdventOfCode\AdventOfCode\2015\Data\day05_full.txt");

            foreach (var word in words)
            {
                IsWordNaughtyOrNice(word);
            }

            CustomConsoleColour.SetAnswerColour();
            Console.WriteLine(_nice);
            Console.ResetColor();
        }

        private static void IsWordNaughtyOrNice(string word)
        {
            var vowels = new List<string> { "a", "e", "i", "o", "u" };
            bool containsDouble = false;
            int vowelsCount = 0;

            if (word.Contains("ab") || word.Contains("cd") || word.Contains("pq") || word.Contains("xy"))
            {
                _naughty += 1;
                return;
            }

            for (int i = 0; i < word.Length; i++)
            {
                if (i + 1 < word.Length)
                {
                    if (word[i] == word[i + 1])
                    {
                        containsDouble = true;
                    }
                }

                if (vowels.Contains(word[i].ToString()))
                {
                    vowelsCount += 1;
                }
            }

            if (containsDouble && vowelsCount >= 3)
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
