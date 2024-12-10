namespace AdventOfCode._2023
{
    public static class D_06_2
    {
        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2023\Data\day06.txt").ToArray();

            KeyValuePair<long, long> race = ParseInputsToRaces(inputs);

            int numberOfWaysToWin = 0;
            for (long time = 1; time <= race.Key; time++)
            {
                long distanceTravelled = time * (race.Key - time);

                if (distanceTravelled > race.Value || distanceTravelled < 0)
                {
                    numberOfWaysToWin += 1;
                }
            }

            Console.WriteLine(numberOfWaysToWin);
        }

        private static KeyValuePair<long, long> ParseInputsToRaces(string[] inputs)
        {
            long raceDetails = long.Parse(inputs[0].Replace("Time:", string.Empty).Replace(" ", string.Empty));
            long distanceDetails = long.Parse(inputs[1].Replace("Distance:", string.Empty).Replace(" ", string.Empty));

            return new KeyValuePair<long, long>(raceDetails, distanceDetails);
        }
    }
}