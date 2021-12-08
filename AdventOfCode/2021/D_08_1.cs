using AdventOfCode._2021.Extensions;
using AdventOfCode._2021.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode._2021
{
    public static class D_08_1
    {
        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2021\Data\day08.txt");
            int chosenNumbers = 0;

            foreach (string input in inputs)
            {
                string codedNumbers = input.Split('|')[1];

                string[] numbers = codedNumbers.Trim().Split(' ');

                foreach (string number in numbers)
                {
                    switch (number.Length)
                    {
                        case 2:
                        case 3:
                        case 4:
                        case 7:
                            chosenNumbers += 1;
                            break;
                        default:
                            break;
                    }
                }
            }

            Console.WriteLine(chosenNumbers);
        }
    }
}
