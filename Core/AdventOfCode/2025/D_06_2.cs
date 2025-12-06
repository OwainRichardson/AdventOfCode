

namespace AdventOfCode._2025;

public static class D_06_2
{
    public static void Execute()
    {
        string[] inputs = File.ReadAllLines(@"2025\Data\day06.txt").ToArray();

        var (values, operators) = ParseInputs(inputs);
        long totalTotal = 0;

        for (int index = 0; index < operators.Count; index++)
        {
            long rowTotal = 0;
            string op = operators[index].Value;
            if (op == "*")
            {
                rowTotal = 1;
            }

            int lengthOfInput = values[0][index].Length;

            for (int currentIndex = lengthOfInput - 1; currentIndex >= 0; currentIndex--)
            {
                List<string> columnCharacters = values.Select(v => v[index][currentIndex].ToString()).ToList();

                int joined = int.Parse(string.Join("", columnCharacters));

                if (op == "+")
                {
                    rowTotal += joined;
                }
                else
                {
                    rowTotal *= joined;
                }
            }

            totalTotal += rowTotal;
        }

        Console.WriteLine(totalTotal);
    }

    private static (List<string[]> values, List<(string Value, int Index)> operators) ParseInputs(string[] inputs)
    {
        List<string[]> values = new();
        List<(string Value, int Index)> operators = new();

        for (int index = 0; index < inputs.Last().Length; index++)
        {
            if (inputs.Last()[index] == '+' || inputs.Last()[index] == '*')
            {
                operators.Add(new(inputs.Last()[index].ToString(), index));
            }
        }

        foreach (string input in inputs)
        {
            if (!input.StartsWith('*') && !input.StartsWith('+'))
            {
                string[] rowValues = new string[operators.Count];

                for (int opIndex = 0; opIndex < operators.Count; opIndex++)
                {
                    int startIndex = operators[opIndex].Index;
                    int length = opIndex + 1 < operators.Count ? operators[opIndex + 1].Index - startIndex - 1 : input.Length - startIndex;

                    rowValues[opIndex] = input.Substring(startIndex, length);
                }

                values.Add(rowValues);
            }
        }

        return (values, operators);
    }
}