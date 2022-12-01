using System;
using System.IO;
using System.Linq;

namespace AdventOfCode._2022
{
    public class D_01_1
    {
        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2022\Data\day01.txt").ToArray();

            int elf = 1;
            int mostCalories = 0;
            int elfWithMostCalories = 0;

            int currentCalories = 0;

            for (int index = 0; index < inputs.Length; index++)
            {
                if (inputs[index] == "")
                {
                    if (currentCalories > mostCalories)
                    {
                        mostCalories = currentCalories;
                        elfWithMostCalories = elf;
                    }

                    currentCalories = 0;
                    elf++;
                }
                else
                {
                    currentCalories += int.Parse(inputs[index]);
                }
            }

            Console.WriteLine(mostCalories);
        }
    }
}
