using System.Text.RegularExpressions;

namespace AdventOfCode._2023
{
    public static class D_01_1
    {
        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2023\Data\day01.txt").ToArray();

            int total = 0;

            foreach (string input in inputs)
            {
                string digits = Regex.Replace(input, @"\D", string.Empty);

                total += int.Parse($"{digits.First()}{digits.Last()}");
            }

            Console.WriteLine(total);
        }
    }
}
