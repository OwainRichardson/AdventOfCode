using AdventOfCode._2020.Models;
using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode._2020
{
    public static class D_07_1
    {
        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2020\Data\day07.txt");
            List<Bag> bagRules = ParseInputs(inputs);

            string startBag = "shiny gold";

            List<string> containers = new List<string>();
            List<Bag> directContainers = bagRules.Where(x => x.Contains.Any(y => y.Colour == startBag)).ToList();

            containers.AddRange(directContainers.Select(x => x.Colour));

            List<string> parentBags = new List<string>();
            foreach (string container in containers)
            {
                parentBags.AddRange(GetParentBags(bagRules, container));
            }

            containers.AddRange(parentBags);

            containers = containers.Distinct().ToList();

            Console.WriteLine(containers.Count);
        }

        private static List<string> GetParentBags(List<Bag> bagRules, string container)
        {
            List<Bag> parentBags = bagRules.Where(x => x.Contains.Any(y => y.Colour == container)).ToList();
            List<string> colours = parentBags.Select(x => x.Colour).ToList();

            foreach (Bag parent in parentBags)
            {
                colours.AddRange(GetParentBags(bagRules, parent.Colour));
            }

            return colours;
        }

        private static List<Bag> ParseInputs(string[] inputs)
        {
            List<Bag> bagRules = new List<Bag>();

            foreach (string input in inputs)
            {
                Bag bagRule = new Bag();

                var parentBagSplit = input.Split(new string[] { " contain " }, StringSplitOptions.RemoveEmptyEntries);
                bagRule.Colour = parentBagSplit[0].Replace(" bags", "").Replace(" bag", "");

                string pattern = @"^(\d+)\s(.+)\s[bags|bag]";
                Regex regex = new Regex(pattern);
                var contentsSplit = parentBagSplit[1].Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var contents in contentsSplit)
                {
                    Match match = regex.Match(contents);

                    if (match.Success)
                    {
                        Bag bag = new Bag
                        {
                            Colour = match.Groups[2].Value,
                            Number = int.Parse(match.Groups[1].Value)
                        };

                        bagRule.Contains.Add(bag);
                    }
                }

                bagRules.Add(bagRule);
            }

            return bagRules;
        }
    }
}
