using AdventOfCode._2020.Models;
using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode._2020
{
    public static class D_06_2
    {
        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2020\Data\day06.txt");

            List<string> groupsAnswers = new List<string>();
            List<string> answers = new List<string>();

            for (int index = 0; index <= inputs.Length; index++)
            {
                if (index < inputs.Length && !string.IsNullOrWhiteSpace(inputs[index]))
                {
                    answers.Add(inputs[index]);
                }
                else
                {
                    if (answers.Count == 1)
                    {
                        groupsAnswers.Add(string.Join("", answers));
                    }
                    else
                    {
                        List<string> answersToAdd = new List<string>();
                        foreach (var a in answers)
                        {
                            foreach (Char c in a)
                            {
                                if (answers.All(x => x.Contains(c.ToString())))
                                {
                                    answersToAdd.Add(c.ToString());
                                }
                            }
                        }

                        groupsAnswers.Add(string.Join("", answersToAdd.Distinct()));
                    }

                    answers = new List<string>();
                }
            }

            Console.WriteLine(groupsAnswers.Sum(x => x.Length));
        }
    }
}
