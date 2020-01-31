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
    public static class D_09_1
    {
        private static List<JourneyDistance> _journeys = new List<JourneyDistance>();
        private static int _totalPlaces = 0;
        private static List<Route> _routes = new List<Route>();

        public static void Execute()
        {
            var input = File.ReadAllLines(@"C:\Work\Misc Projects\AdventOfCode\AdventOfCode\AdventOfCode\2015\Data\day9_full.txt");

            ParseInput(input);
            List<string> places = CalculateTotalPlaces();
            List<string[]> permutations = GetPermutations(places);

            CalculatePermutationDistances(permutations);

            Console.Write($"The shortest distance to travel is: ");
            CustomConsoleColour.SetAnswerColour();
            Console.Write(_routes.Min(x => x.TotalDistance));
            Console.ResetColor();
            Console.WriteLine();
        }

        private static void CalculatePermutationDistances(List<string[]> permutations)
        {
            foreach (var permutation in permutations)
            {
                Route route = new Route();
                int i = 0;

                foreach (var perm in permutation)
                {
                    route.Locations.Add(perm);

                    if (i > 0)
                    {
                        var journey = _journeys.First(x => (x.From == permutation[i] && x.To == permutation[i - 1]) || (x.From == permutation[i - 1] && x.To == permutation[i]));

                        route.TotalDistance += journey.Distance;
                    }

                    i++;
                }

                _routes.Add(route);
            }
        }

        private static List<string> CalculateTotalPlaces()
        {
            var places = _journeys.Select(x => x.From).Union(_journeys.Select(x => x.To).Distinct());
            _totalPlaces = places.Count();

            return places.ToList();
        }

        private static void ParseInput(string[] input)
        {
            foreach (var line in input)
            {
                var journey = new JourneyDistance();

                var distanceSplit = line.Split(new string[] { " = " }, StringSplitOptions.RemoveEmptyEntries);
                journey.Distance = int.Parse(distanceSplit[1]);

                var placesSplit = distanceSplit[0].Split(new string[] { " to " }, StringSplitOptions.RemoveEmptyEntries);

                journey.From = placesSplit[0];
                journey.To = placesSplit[1];

                _journeys.Add(journey);
            }
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
    }
}
