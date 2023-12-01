using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode._2022
{
    public class D_06_1
    {
        public static void Execute()
        { 
            string input = File.ReadAllLines(@"2022\Data\day06.txt").Single();

            for (int i = 3; i < input.Length; i++)
            {
                List<char> inputBuffer = new List<char>
                {
                    input[i - 3],
                    input[i - 2],
                    input[i - 1],
                    input[i]
                };

                if (inputBuffer.Distinct().Count() == 4)
                {
                    Console.WriteLine(i + 1);
                    break;
                }
            }
        }
    }
}
