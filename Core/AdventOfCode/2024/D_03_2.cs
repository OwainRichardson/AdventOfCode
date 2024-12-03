using System.Text.RegularExpressions;

namespace AdventOfCode._2024
{
    public static class D_03_2
    {
        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2024\Data\day03.txt").ToArray();

            string pattern = @"mul\((\d+)\,(\d+)\)|don't|do";
            Regex regex = new Regex(pattern);


            long total = 0;
            bool doMul = true;

            foreach (string input in inputs)
            {
                MatchCollection matches = regex.Matches(input);
                foreach (Match match in matches)
                {
                    if (match.Groups[0].ToString() == "do")
                    {
                        doMul = true;
                    }
                    else if (match.Groups[0].ToString() == "don't")
                    {
                        doMul = false;
                    }
                    else
                    {
                        if (doMul)
                        {
                            int number1 = int.Parse(match.Groups[1].ToString());
                            int number2 = int.Parse(match.Groups[2].ToString());

                            total += (number1 * number2);
                        }
                    }
                }
            }

            Console.WriteLine(total);
        }
    }
}