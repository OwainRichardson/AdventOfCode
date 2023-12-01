using AdventOfCode._2017.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode._2017
{
    public static class D_07_2
    {
        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"C:\Work\Misc Projects\AdventOfCode\AdventOfCode\AdventOfCode\2017\Data\day07_full.txt");

            List<ProgramStack> programStacks = ParseInputs(inputs);

            List<ProgramStack> progsWithDependencies = programStacks.Where(x => x.DependentPrograms.Any()).ToList();

            ProgramStack baseProg = new ProgramStack();

            foreach (var prog in progsWithDependencies)
            {
                if (!progsWithDependencies.Any(x => x.DependentPrograms.Contains(prog.ProgramName)))
                {
                    baseProg = prog;
                }
            }

            int baseProgWeight = CalculateWeights(baseProg, ref programStacks);

            FindMisbalancedNode(programStacks, baseProg);
        }

        private static void FindMisbalancedNode(List<ProgramStack> programStacks, ProgramStack baseProg)
        {
            var selectedProg = programStacks.First(x => x.ProgramName == baseProg.ProgramName);

            if (selectedProg.DependentWeights.Any() && selectedProg.DependentWeights.Distinct().Count() != 1)
            {
                Console.WriteLine($"dependent nodes of {selectedProg.ProgramName} don't balance");

                var differentProgValue = selectedProg.DependentWeights.GroupBy(x => x).First(x => x.Count() == 1).Key;
                var differentProg = programStacks.First(x => x.TotalWeight == differentProgValue && selectedProg.DependentPrograms.Contains(x.ProgramName));

                Console.WriteLine($"{differentProg.ProgramName} is the node that doesn't balance");

                var distinctWeights = selectedProg.DependentWeights.GroupBy(x => x);
                int differenceInWeight = distinctWeights.First().Key;
                foreach (var weight in distinctWeights.Skip(1))
                {
                    differenceInWeight -= weight.Key;
                }

                Console.WriteLine($"It's weight should be {differentProg.Weight - differenceInWeight}");

                FindMisbalancedNode(programStacks, differentProg);
            }
            else
            {
                Console.WriteLine($"Dependent programs of {selectedProg.ProgramName} are balanced.");
            }
        }

        private static int CalculateWeights(ProgramStack prog, ref List<ProgramStack> progs)
        {
            var selectedProg = progs.First(x => x.ProgramName == prog.ProgramName);

            if (selectedProg.DependentPrograms.Any())
            {
                int weight = selectedProg.Weight;
                foreach (var dp in selectedProg.DependentPrograms)
                {
                    var p = progs.First(x => x.ProgramName == dp);
                    int weightToAdd = CalculateWeights(p, ref progs);
                    selectedProg.DependentWeights.Add(weightToAdd);
                    weight += weightToAdd;
                }
                selectedProg.TotalWeight = weight;
            }
            else
            {
                return selectedProg.Weight;
            }

            return selectedProg.TotalWeight;
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
