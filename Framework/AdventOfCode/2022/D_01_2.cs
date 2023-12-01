using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode._2022
{
    public class D_01_2
    {
        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2022\Data\day01.txt").ToArray();

            List<int> calories = new List<int>();
            int currentCalories = 0;

            for (int index = 0; index < inputs.Length; index++)
            {
                if (inputs[index] == "")
                {
                    calories.Add(currentCalories);
                    currentCalories = 0;
                }
                else
                {
                    currentCalories += int.Parse(inputs[index]);
                }
            }

            calories.Add(currentCalories);

            Console.WriteLine(calories.OrderByDescending(x => x).Take(3).Sum());
        }
    }
}
