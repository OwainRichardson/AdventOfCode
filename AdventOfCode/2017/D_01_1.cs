using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2017
{
    public static class D_01_1
    {
        public static void Execute()
        {
            var input = File.ReadAllLines(@"C:\Work\Misc Projects\AdventOfCode\AdventOfCode\AdventOfCode\2017\Data\day01_full.txt")[0];

            ReadInput(input);
        }

        private static void ReadInput(string input)
        {
            int total = 0;

            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == input[(i + 1) % input.Length])
                {
                    total += int.Parse(input[i].ToString());
                }
            }

            Console.WriteLine(total);
        }
    }
}
