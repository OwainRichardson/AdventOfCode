namespace AdventOfCode._2023
{
    public static class D_01_2
    {
        private static Dictionary<string, int> Values = new Dictionary<string, int>
        {
            { "one", 1 },
            { "two", 2 },
            { "three", 3 },
            { "four", 4 },
            { "five", 5 },
            { "six", 6 },
            { "seven", 7 },
            { "eight", 8 },
            { "nine", 9 },
        };

        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2023\Data\day01.txt").ToArray();

            int total = 0;

            foreach (string input in inputs)
            {
                string updatedInput = input;

                int firstDigit = FindFirstDigit(updatedInput);
                int lastDigit = FindLastDigit(updatedInput);

                string firstAndLastDigits = $"{firstDigit}{lastDigit}";

                total += int.Parse(firstAndLastDigits);
            }

            Console.WriteLine(total);
        }

        private static int FindLastDigit(string updatedInput)
        {
            if (char.IsDigit(updatedInput[updatedInput.Length - 1]))
            {
                return int.Parse(updatedInput[updatedInput.Length - 1].ToString());
            }

            foreach (var value in Values)
            {
                if (updatedInput.EndsWith(value.Key))
                {
                    return Values[value.Key];
                }
            }

            return FindLastDigit(updatedInput.Substring(0, updatedInput.Length - 1));
        }

        private static int FindFirstDigit(string updatedInput)
        {
            if (char.IsDigit(updatedInput[0]))
            {
                return int.Parse(updatedInput[0].ToString());
            }

            foreach (var value in Values)
            {
                if (updatedInput.StartsWith(value.Key))
                {
                    return Values[value.Key];
                }
            }

            return FindFirstDigit(updatedInput.Substring(1));
        }
    }
}
