using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode._2022
{
    public class D_06_2
    {
        public static void Execute()
        { 
            string input = File.ReadAllLines(@"2022\Data\day06.txt").Single();

            for (int i = 13; i < input.Length; i++)
            {
                List<char> inputBuffer = new List<char>
                {
                    input[i - 13],
                    input[i - 12],
                    input[i - 11],
                    input[i - 10],
                    input[i - 9],
                    input[i - 8],
                    input[i - 7],
                    input[i - 6],
                    input[i - 5],
                    input[i - 4],
                    input[i - 3],
                    input[i - 2],
                    input[i - 1],
                    input[i]
                };

                if (inputBuffer.Distinct().Count() == 14)
                {
                    Console.WriteLine(i + 1);
                    break;
                }
            }
        }
    }
}
