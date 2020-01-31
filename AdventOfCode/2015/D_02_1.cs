using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2015
{
    public class D_02_1
    {
        private static int _totalSquareFeet = 0;

        public static void Execute()
        {
            var presents = File.ReadAllLines(@"C:\Work\Misc Projects\AdventOfCode\AdventOfCode\AdventOfCode\2015\Data\day2_full.txt");

            foreach (var present in presents)
            {
                _totalSquareFeet += CalculateSquareFeet(present);
            }

            Console.WriteLine(_totalSquareFeet);
        }

        private static int CalculateSquareFeet(string input)
        {
            int indexOfX = input.IndexOf("x");
            int indexOfSecondX = input.IndexOf("x", input.IndexOf("x") + 1);

            int l = int.Parse(input.Substring(0, indexOfX));
            int w = int.Parse(input.Substring(indexOfX + 1, indexOfSecondX - (indexOfX + 1)));
            int h = int.Parse(input.Substring(indexOfSecondX + 1));

            return (2 * l * w) + (2 * w * h) + (2 * l * h) + Min(l, w, h);
        }

        private static int Min(int l, int w, int h)
        {
            int smallest = l * w;

            if (w * h < smallest)
            {
                smallest = w * h;
            }

            if (h * l < smallest)
            {
                smallest = h * l;
            }

            return smallest;
        }
    }
}
