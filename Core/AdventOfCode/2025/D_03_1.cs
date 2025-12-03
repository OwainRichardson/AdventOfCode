namespace AdventOfCode._2025;

public static class D_03_1
{
    public static void Execute()
    {
        string[] inputs = File.ReadAllLines(@"2025\Data\day03.txt").ToArray();

        List<List<int>> batteryBanks = ParseInputs(inputs);
        long totalJoltage = 0;

        foreach (List<int> batteryBank in batteryBanks)
        {
            long highestJoltage = 0;

            highestJoltage = FindHighestJoltage(batteryBank);

            totalJoltage += highestJoltage;
        }

        Console.WriteLine(totalJoltage);
    }

    private static long FindHighestJoltage(List<int> batteryBank)
    {
        string highestJoltage = string.Empty;
        int numberOfRequiredBatteries = 2;

        while (highestJoltage.Length < numberOfRequiredBatteries)
        {
            int j = FindNextHighestJoltage(batteryBank, null, numberOfRequiredBatteries - highestJoltage.Length);

            highestJoltage = $"{highestJoltage}{j}";
        }

        return long.Parse(highestJoltage);
    }

    private static int FindNextHighestJoltage(List<int> batteryBank, int? valueToFind, int numberOfRequiredBatteries)
    {
        int numberOfBatteries = batteryBank.Count;

        if (!valueToFind.HasValue)
        {
            valueToFind = batteryBank.Max();
        }

        if (batteryBank.IndexOf(valueToFind.Value) == -1)
        {
            return FindNextHighestJoltage(batteryBank, valueToFind - 1, numberOfRequiredBatteries);
        }

        if (batteryBank.IndexOf(valueToFind.Value) <= (numberOfBatteries - numberOfRequiredBatteries))
        {
            batteryBank.RemoveRange(0, batteryBank.IndexOf(valueToFind.Value) + 1);

            return valueToFind.Value;
        }
        else
        {
            return FindNextHighestJoltage(batteryBank, valueToFind - 1, numberOfRequiredBatteries);
        }
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