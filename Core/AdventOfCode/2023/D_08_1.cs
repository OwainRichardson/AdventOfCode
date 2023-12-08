using AdventOfCode._2023.Models;
using AdventOfCode._2023.Models.Enums;
using System.Data.Common;
using System.Text.RegularExpressions;

namespace AdventOfCode._2023
{
    public static class D_08_1
    {
        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2023\Data\day08.txt").ToArray();

            DesertMap map = ParseInputsToMap(inputs);
            string currentLocation = "AAA";
            string endLocation = "ZZZ";
            int numberOfSteps = 0;

            while (currentLocation != endLocation)
            {
                string stepToTake = map.Directions[numberOfSteps % map.Directions.Length].ToString();
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
            }

            Console.WriteLine(numberOfSteps);
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
                directionsMap.Right= match.Groups[2].Value;

                map.Maps.Add(directionsMap);
            }

            return map;
        }
    }
}