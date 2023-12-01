using AdventOfCode.Common;
using System;

namespace AdventOfCode._2015
{
    public static class D_10_1
    {
        public static void Execute()
        {
            string input = "1321131112";

            for (int i = 1; i <= 40; i++)
            {
                input = IncrementInput(input);
            }

            CustomConsoleColour.SetAnswerColour();
            Console.WriteLine(input.Length);
            Console.ResetColor();
        }

        private static string IncrementInput(string input)
        {
            string result = string.Empty;
            Char charToExamine = Char.MinValue;

            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == charToExamine)
                {
                    continue;
                }

                charToExamine = input[i];

                int count = 1;

                if (i + 1 < input.Length)
                {
                    if (input[i + 1] == input[i])
                    {
                        count++;

                        while (i + count < input.Length && input[i + count] == input[i])
                        {
                            count++;
                        }
                    }
                }

                result = $"{result}{count}{charToExamine}";
            }

            return result;
        }
    }
}