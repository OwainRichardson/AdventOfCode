
namespace AdventOfCode._2025;

public static class D_06_1
{
    public static void Execute()
    {
        string[] inputs = File.ReadAllLines(@"2025\Data\day06.txt").ToArray();

        var (values, operators) = ParseInputs(inputs);
        long totalTotal = 0;

        for (int index = 0; index < operators.Length; index++)
        {
            long problemTotal = 0;

            if (operators[index] == "+")
            {
                problemTotal = values.Sum(x => x[index]);
            }
            else if (operators[index] == "*")
            {
                problemTotal = 1;

                foreach(var value in values)
                {
                    problemTotal *= value[index];
                }
            }

            totalTotal += problemTotal;
        }

        Console.WriteLine(totalTotal);
    }

    private static (List<long[]> values, string[] operators) ParseInputs(string[] inputs)
    {
        List<long[]> values = new();
        string[] operators = [];

        foreach (string input in inputs)
        {
            if (input.StartsWith('*') || input.StartsWith('+'))
            {
                operators = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            }
            else
            {
                values.Add(input.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray());
            }
        }

        return (values, operators);
    }
}