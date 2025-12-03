namespace AdventOfCode._2025;

public static class D_03_1
{
    public static void Execute()
    {
        string[] inputs = File.ReadAllLines(@"2025\Data\day03.txt").ToArray();

        List<List<int>> batteryBanks = ParseInputs(inputs);
        int totalJoltage = 0;

        foreach (List<int> batteryBank in batteryBanks)
        {
            int highestJoltage = 0;

            for (int x = 0; x < batteryBank.Count - 1; x++)
            {
                for (int y = x + 1; y < batteryBank.Count; y++)
                {
                    int joltage = int.Parse($"{batteryBank[x]}{batteryBank[y]}");
                    if (joltage >  highestJoltage)
                    {
                        highestJoltage = joltage;
                    }
                }
            }

            totalJoltage += highestJoltage;
        }

        Console.WriteLine(totalJoltage);
    }

    private static List<List<int>> ParseInputs(string[] inputs)
    {
        List<List<int>> batteryBanks = new();

        foreach (string input in inputs)
        {
            List<int> batteryBank = new();

            foreach (char battery in input)
            {
                batteryBank.Add(int.Parse(battery.ToString()));
            }

            batteryBanks.Add(batteryBank);
        }

        return batteryBanks;
    }
}