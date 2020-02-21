using AdventOfCode._2017.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode._2017
{
    public static class D_07_1
    {
        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"C:\Work\Misc Projects\AdventOfCode\AdventOfCode\AdventOfCode\2017\Data\day07_full.txt");

            List<ProgramStack> programStacks = ParseInputs(inputs);

            List<ProgramStack> progsWithDependencies = programStacks.Where(x => x.DependentPrograms.Any()).ToList();

            foreach (var prog in progsWithDependencies)
            {
                if (!progsWithDependencies.Any(x => x.DependentPrograms.Contains(prog.ProgramName)))
                {
                    Console.WriteLine(prog.ProgramName);
                }
            }
        }

        private static List<ProgramStack> ParseInputs(string[] inputs)
        {
            List<ProgramStack> programStacks = new List<ProgramStack>();
            string pattern = @"(\w+) \((\d+)\)( -> )?(.*)?";
            Regex regex = new Regex(pattern);

            foreach (string input in inputs)
            {
                Match match = regex.Match(input);

                ProgramStack prog = new ProgramStack
                {
                    ProgramName = match.Groups[1].Value,
                    Weight = int.Parse(match.Groups[2].Value)
                };

                if (!string.IsNullOrWhiteSpace(match.Groups[4].Value))
                {
                    string[] dependents = match.Groups[4].Value.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string dependent in dependents)
                    {
                        prog.DependentPrograms.Add(dependent);
                    }
                }

                programStacks.Add(prog);
            }

            return programStacks;
        }
    }
}
