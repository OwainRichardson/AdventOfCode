using AdventOfCode._2015.Models;
using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode._2015
{
    public class D_13_1
    {
        private static List<DinnerGuest> _guests = new List<DinnerGuest>();
        private static int _totalGuests = 0;

        public static void Execute()
        {
            var input = File.ReadAllLines(@"C:\Work\Misc Projects\AdventOfCode\AdventOfCode\AdventOfCode\2015\Data\day13_full.txt");

            ParseInputToPeople(input);

            List<string> guests = CalculateTotalGuests();
            List<string[]> permutations = GetPermutations(guests);

            CalculateArrangements(permutations);
        }

        private static void CalculateArrangements(List<string[]> permutations)
        {
            var arrangements = new List<Arrangement>();

            foreach (var permutation in permutations)
            {
                var arrangement = new Arrangement();

                foreach (var guest in permutation)
                {
                    arrangement.Guests.Add(guest);
                }

                for (int i = 0; i < _totalGuests; i++)
                {
                    var guest = permutation[i];
                    var toLeft = permutation[i - 1 < 0 ? _totalGuests - 1 : i - 1];
                    var toLeftHappiness = _guests.First(x => x.Name == guest && x.NextTo == toLeft).Happiness;
                    arrangement.TotalHappiness += toLeftHappiness;

                    var toRight = permutation[i + 1 > _totalGuests - 1 ? 0 : i + 1];
                    var toRightHappiness = _guests.First(x => x.Name == guest && x.NextTo == toRight).Happiness;
                    arrangement.TotalHappiness += toRightHappiness;
                }

                arrangements.Add(arrangement);
            }

            CustomConsoleColour.SetAnswerColour();
            Console.WriteLine($"{arrangements.Max(x => x.TotalHappiness)}");
            Console.ResetColor();
        }

        private static List<string[]> GetPermutations(List<string> things, List<string> current = null)
        {
            List<string[]> res = new List<string[]>();
            if (current == null)
            {
                current = new List<string>();
            }
            if (things.Count > 0)
            {
                foreach (string t in things)
                {
                    List<string> newP = new List<string>(current);
                    newP.Add(t);

                    List<string> newThings = new List<string>(things);
                    newThings.Remove(t);
                    res.AddRange(GetPermutations(newThings, newP));
                }
            }
            else
            {
                res.Add(current.ToArray());
            }

            return res;
        }

        private static List<string> CalculateTotalGuests()
        {
            var guests = _guests.Select(x => x.Name).Distinct();
            _totalGuests = guests.Count();

            return guests.ToList();
        }

        private static void ParseInputToPeople(string[] input)
        {
            foreach (var line in input)
            {
                DinnerGuest guest = new DinnerGuest();

                var personSplit = line.Split(new string[] { " would " }, StringSplitOptions.RemoveEmptyEntries);
                guest.Name = personSplit[0];

                var nextToSplit = personSplit[1].Split(new string[] { " happiness units by sitting next to " }, StringSplitOptions.RemoveEmptyEntries);
                guest.NextTo = nextToSplit[1].Replace(".", "");

                var happiness = int.Parse(Regex.Match(nextToSplit[0], "([0-9]+)").Value);

                if (nextToSplit[0].StartsWith("gain"))
                {
                    guest.Happiness += happiness;
                }
                else if (nextToSplit[0].StartsWith("lose"))
                {
                    guest.Happiness -= happiness;
                }

                _guests.Add(guest);
            }
        }
    }
}