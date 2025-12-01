namespace AdventOfCode._2025;

public static class D_01_1
{
    public static void Execute()
    {
        string[] inputs = File.ReadAllLines(@"2025\Data\day01.txt").ToArray();
        int dialPosition = 50;
        int zeroCount = 0;

        foreach (string input in inputs)
        {
            (string direction, int value) = ParseInput(input);

            if (direction.Equals("L"))
            {
                dialPosition = (dialPosition - value) % 100;
            }
            else
            {
                dialPosition = (dialPosition + value) % 100;
            }

            if (dialPosition == 0)
            {
                zeroCount++;
            }
        }

        Console.WriteLine(zeroCount);
    }

    private static (string direction, int value) ParseInput(string input)
    {
        return (input.Substring(0, 1), int.Parse(input.Substring(1)));
    }
}
