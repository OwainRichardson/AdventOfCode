using AdventOfCode._2020.Models;
using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode._2020
{
    public static class D_07_2
    {
        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2020\Data\day07.txt");
            List<Bag> bagRules = ParseInputs(inputs);

            Bag startBag = bagRules.First(x => x.Colour == "shiny gold");

            int numberOfBagsInside = 0;

            foreach (Bag insideBag in startBag.Contains)
            {
                numberOfBagsInside += CalculateInsideBags(bagRules, insideBag);
            }

            Console.WriteLine(numberOfBagsInside);
        }

        private static int CalculateInsideBags(List<Bag> bagRules, Bag insideBag)
        {
            int numberOfBags = insideBag.Number;

            Bag containedBag = bagRules.First(x => x.Colour == insideBag.Colour);

            foreach (Bag bag in containedBag.Contains)
            {
                numberOfBags += (insideBag.Number * CalculateInsideBags(bagRules, bag));
            }

            return numberOfBags;
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
