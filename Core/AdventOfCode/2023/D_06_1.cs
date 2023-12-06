using AdventOfCode._2023.Models;
using System.Data.Common;
using System.Text.RegularExpressions;

namespace AdventOfCode._2023
{
    public static class D_06_1
    {
        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2023\Data\day06.txt").ToArray();

            Dictionary<int, int> races = ParseInputsToRaces(inputs);

            long total = 1;

            foreach (var race in races)
            {
                int numberOfWaysToWin = 0;

                for (int time = 1; time <= race.Key; time++)
                {
                    int distanceTravelled = time * (race.Key - time);

                    if (distanceTravelled > race.Value)
                    {
                        numberOfWaysToWin += 1;
                    }
                }

                total *= numberOfWaysToWin;
            }

            Console.WriteLine(total);
        }

        private static Dictionary<int, int> ParseInputsToRaces(string[] inputs)
        {
            List<int> raceDetails = inputs[0].Replace("Time:", string.Empty).Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(i => int.Parse(i)).ToList();
            List<int> distanceDetails = inputs[1].Replace("Distance:", string.Empty).Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(i => int.Parse(i)).ToList();

            Dictionary<int, int> races = new Dictionary<int, int>();

            for (int index = 0; index < raceDetails.Count; index++)
            {
                races.Add(raceDetails[index], distanceDetails[index]);
            }

            return races;
        }
    }
}