using AdventOfCode._2023.Models;
using System.Text.RegularExpressions;

namespace AdventOfCode._2023
{
    public static class D_08_2
    {
        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2023\Data\day08.txt").ToArray();

            DesertMap map = ParseInputsToMap(inputs);
            List<DesertMapDirections> startLocations = map.Maps.Where(m => m.Location.EndsWith('A')).ToList();
            long numberOfSteps = 0;
            CalculatePatterns(map, startLocations, numberOfSteps);

            while (startLocations.Select(x => x.CurrentSteps).Distinct().Count() != 1)
            {
                long maxCurrentSteps = startLocations.Max(l => l.CurrentSteps);
                List<DesertMapDirections> locationsUnderMaxSteps = startLocations.Where(l => l.CurrentSteps < maxCurrentSteps).ToList();
                foreach (var locationUnderMaxSteps in locationsUnderMaxSteps)
                {
                    locationUnderMaxSteps.CurrentSteps += locationUnderMaxSteps.Pattern;
                }
            }

            Console.WriteLine(startLocations.First().CurrentSteps);
        }

        private static void CalculatePatterns(DesertMap map, List<DesertMapDirections> startLocations, long numberOfSteps)
        {
            foreach (string location in startLocations.Select(l => l.Location))
            {
                string currentLocation = location;
                List<long> numbersWhenLocationEndsInZ = new List<long>();

                while (numbersWhenLocationEndsInZ.Count < 20)
                {
                    string stepToTake = map.Directions[(int)(numberOfSteps % map.Directions.Length)].ToString();
                    var currentMapSpot = map.Maps.Single(m => m.Location == currentLocation);

                    if (stepToTake == "R")
                    {
                        currentLocation = currentMapSpot.Right;
                    }
                    else if (stepToTake == "L")
                    {
                        currentLocation = currentMapSpot.Left;
                    }
                    else
                    {
                        throw new InvalidOperationException();
                    }

                    numberOfSteps++;

                    if (currentLocation.EndsWith('Z'))
                    {
                        numbersWhenLocationEndsInZ.Add(numberOfSteps);
                    }
                }

                var startLocation = startLocations.Single(l => l.Location == location);
                startLocation.StartOfPattern = numbersWhenLocationEndsInZ[1] - numbersWhenLocationEndsInZ[0];
                startLocation.Pattern = numbersWhenLocationEndsInZ[1] - numbersWhenLocationEndsInZ[0];
            }

            startLocations.ForEach(sl =>
            {
                sl.CurrentSteps = sl.StartOfPattern;
            });
        }

        private static DesertMap ParseInputsToMap(string[] inputs)
        {
            DesertMap map = new DesertMap
            {
                Directions = inputs[0]
            };

            for (int index = 2; index < inputs.Length; index++)
            {
                DesertMapDirections directionsMap = new DesertMapDirections();

                string pattern = @"\((\w+),\W(\w+)\)";
                Regex regex = new Regex(pattern);
                Match match = regex.Match(inputs[index]);

                directionsMap.Location = inputs[index].Substring(0, 3);
                directionsMap.Left = match.Groups[1].Value;
                directionsMap.Right = match.Groups[2].Value;

                map.Maps.Add(directionsMap);
            }

            return map;
        }
    }
}