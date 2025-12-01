
namespace AdventOfCode._2025;

public static class D_01_2
{
    public static void Execute()
    {
        string[] inputs = File.ReadAllLines(@"2025\Data\day01.txt").ToArray();
        int dialPosition = 50;
        int zeroCount = 0;


        foreach (string input in inputs)
        {
            int startDialPosition = dialPosition;

            (string direction, int value) = ParseInput(input);

            if (direction.Equals("L"))
            {
                dialPosition -= value;

                while (dialPosition < 0)
                {
                    zeroCount++;

                    dialPosition += 100;
                }

                if (startDialPosition == 0)
                {
                    zeroCount--;
                }
                if (dialPosition == 0)
                {
                    zeroCount++;
                }
            }
            else
            {
                dialPosition += value;

                while (dialPosition > 99)
                {
                    zeroCount++;

                    dialPosition -= 100;
                }
            }
        }

        Console.WriteLine(zeroCount);
    }

    private static (string direction, int value) ParseInput(string input)
    {
        return (input.Substring(0, 1), int.Parse(input.Substring(1)));
    }
}
