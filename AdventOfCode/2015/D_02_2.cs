using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2015
{
    public class D_02_2
    {
        public static int _totalSquareFeet = 0;

        public static void Execute()
        {
            var presents = File.ReadAllLines(@"C:\Work\Misc Projects\AdventOfCode\AdventOfCode\AdventOfCode\2015\Data\day02_full.txt");

            foreach (var present in presents)
            {
                _totalSquareFeet += CalculateRibbon(present);
            }

            CustomConsoleColour.SetAnswerColour();
            Console.WriteLine(_totalSquareFeet);
            Console.ResetColor();
        }

        private static int CalculateRibbon(string input)
        {
            int indexOfX = input.IndexOf("x");
            int indexOfSecondX = input.IndexOf("x", input.IndexOf("x") + 1);

            int l = int.Parse(input.Substring(0, indexOfX));
            int w = int.Parse(input.Substring(indexOfX + 1, indexOfSecondX - (indexOfX + 1)));
            int h = int.Parse(input.Substring(indexOfSecondX + 1));

            var wh = (2 * w) + (2 * h);
            var lh = (2 * l) + (2 * h);
            var lw = (2 * l) + (2 * w);

            return Min(wh, lh, lw) + Bow(l, w, h);
        }

        private static int Min(int wh, int lh, int lw)
        {
            var min = wh;

            if (lh < min)
            {
                min = lh;
            }

            if (lw < min)
            {
                min = lw;
            }

            return min;
        }

        private static int Bow(int l, int w, int h)
        {
            return l * w * h;
        }
    }
}
