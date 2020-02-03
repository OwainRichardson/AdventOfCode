using AdventOfCode._2015.Models;
using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode._2015
{
    public class D_16_2
    {
        private static List<Aunt> _aunts = new List<Aunt>();
        private static Aunt _idealAunt = new Aunt
        {
            Children = 3,
            Cats = 7,
            Samoyeds = 2,
            Pomeranians = 3,
            Akitas = 0,
            Vizslas = 0,
            Goldfish = 5,
            Trees = 3,
            Cars = 2,
            Perfumes = 1
        };

        public static void Execute()
        {
            var inputs = File.ReadAllLines(@"C:\Work\Misc Projects\AdventOfCode\AdventOfCode\AdventOfCode\2015\Data\day16_full.txt");

            ParseAunts(inputs);

            FindTheAunt();
        }

        private static void FindTheAunt()
        {
            List<Aunt> possibleAunts = new List<Aunt>();

            foreach (Aunt aunt in _aunts)
            {
                if (aunt.Children != -1 && aunt.Children != _idealAunt.Children)
                {
                    continue;
                }
                if (aunt.Cats != -1 && aunt.Cats <= _idealAunt.Cats)
                {
                    continue;
                }
                if (aunt.Samoyeds != -1 && aunt.Samoyeds != _idealAunt.Samoyeds)
                {
                    continue;
                }
                if (aunt.Pomeranians != -1 && aunt.Pomeranians >= _idealAunt.Pomeranians)
                {
                    continue;
                }
                if (aunt.Akitas != -1 && aunt.Akitas != _idealAunt.Akitas)
                {
                    continue;
                }
                if (aunt.Vizslas != -1 && aunt.Vizslas != _idealAunt.Vizslas)
                {
                    continue;
                }
                if (aunt.Goldfish != -1 && aunt.Goldfish >= _idealAunt.Goldfish)
                {
                    continue;
                }
                if (aunt.Trees != -1 && aunt.Trees <= _idealAunt.Trees)
                {
                    continue;
                }
                if (aunt.Cars != -1 && aunt.Cars != _idealAunt.Cars)
                {
                    continue;
                }
                if (aunt.Perfumes != -1 && aunt.Perfumes != _idealAunt.Perfumes)
                {
                    continue;
                }

                possibleAunts.Add(aunt);
            }

            Console.Write("Aunt Sue #");
            CustomConsoleColour.SetAnswerColour();
            Console.Write(possibleAunts.Single().Number);
            Console.ResetColor();
            Console.Write(" sent that parcel");
            Console.WriteLine();
        }

        private static void ParseAunts(string[] inputs)
        {
            foreach (var input in inputs)
            {
                Aunt aunt = new Aunt();

                aunt.Number = int.Parse(Regex.Match(input, @"^Sue (\d+)").Groups[1].Value);

                var children = Regex.Match(input, @"children: (\d+)").Groups[1].Value;
                aunt.Children = !string.IsNullOrWhiteSpace(children) ? int.Parse(children) : -1;

                var cats = Regex.Match(input, @"cats: (\d+)").Groups[1].Value;
                aunt.Cats = !string.IsNullOrWhiteSpace(cats) ? int.Parse(cats) : -1;

                var samoyeds = Regex.Match(input, @"samoyeds: (\d+)").Groups[1].Value;
                aunt.Samoyeds = !string.IsNullOrWhiteSpace(samoyeds) ? int.Parse(samoyeds) : -1;

                var pomeranians = Regex.Match(input, @"pomeranians: (\d+)").Groups[1].Value;
                aunt.Pomeranians = !string.IsNullOrWhiteSpace(pomeranians) ? int.Parse(pomeranians) : -1;

                var akitas = Regex.Match(input, @"akitas: (\d+)").Groups[1].Value;
                aunt.Akitas = !string.IsNullOrWhiteSpace(akitas) ? int.Parse(akitas) : -1;

                var vizslas = Regex.Match(input, @"vizslas: (\d+)").Groups[1].Value;
                aunt.Vizslas = !string.IsNullOrWhiteSpace(vizslas) ? int.Parse(vizslas) : -1;

                var goldfish = Regex.Match(input, @"goldfish: (\d+)").Groups[1].Value;
                aunt.Goldfish = !string.IsNullOrWhiteSpace(goldfish) ? int.Parse(goldfish) : -1;

                var trees = Regex.Match(input, @"trees: (\d+)").Groups[1].Value;
                aunt.Trees = !string.IsNullOrWhiteSpace(trees) ? int.Parse(trees) : -1;

                var cars = Regex.Match(input, @"cars: (\d+)").Groups[1].Value;
                aunt.Cars = !string.IsNullOrWhiteSpace(cars) ? int.Parse(cars) : -1;

                var perfumes = Regex.Match(input, @"perfumes: (\d+)").Groups[1].Value;
                aunt.Perfumes = !string.IsNullOrWhiteSpace(perfumes) ? int.Parse(perfumes) : -1;

                _aunts.Add(aunt);
            }
        }
    }
}