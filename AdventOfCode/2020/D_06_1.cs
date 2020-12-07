using AdventOfCode._2020.Models;
using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode._2020
{
    public static class D_06_1
    {
        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2020\Data\day06.txt");

            List<char[]> groupsAnswers = new List<char[]>();
            string answers = string.Empty;

            for (int index = 0; index <= inputs.Length; index++)
            {
                if (index < inputs.Length && !string.IsNullOrWhiteSpace(inputs[index]))
                {
                    answers = $"{answers}{inputs[index]}";
                }
                else
                {
                    groupsAnswers.Add(answers.Distinct().ToArray());
                    answers = string.Empty;
                }
            }

            Console.WriteLine(groupsAnswers.Sum(x => x.Length));
        }
    }
}
