using AdventOfCode._2019.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2019
{
    public static class D_04_2
    {
        private static int TotalPossible = 0;

        public static void Execute()
        {
            int startValue = 138307;
            int endValue = 654504;

            for (int i = startValue; i <= endValue; i++)
            {
                if (PasswordIsValid(i.ToString()))
                {
                    TotalPossible += 1;
                }
            }

            Console.WriteLine($"Total possible passwords: {TotalPossible}");
        }

        private static bool PasswordIsValid(string password)
        {
            if (PasswordHasADouble(password))
            {
                if (PasswordNeverDecreases(password))
                {
                    return true;
                }
            }

            return false;
        }

        private static bool PasswordHasADouble(string password)
        {
            List<Digit> digits = new List<Digit>();

            for (int j = 0; j < password.Length; j++)
            {
                var currentDigit = password[j].ToString();

                if (digits.Any())
                {
                    if (digits.Last().Value == currentDigit)
                    {
                        digits.Last().NumberOfOccurences += 1;
                    }
                    else
                    {
                        digits.Add(new Digit { Value = currentDigit, NumberOfOccurences = 1 });
                    }
                }
                else
                {
                    digits.Add(new Digit { Value = currentDigit, NumberOfOccurences = 1 });
                }
            }

            if (digits.Any(x => x.NumberOfOccurences == 2))
            {
                return true;
            }

            return false;
        }

        private static bool PasswordNeverDecreases(string password)
        {
            for (int j = 1; j < password.Length; j++)
            {
                if ((int)password[j] < (int)password[j - 1])
                {
                    return false;
                }
            }

            return true;
        }
    }

    public class Digit
    {
        public int NumberOfOccurences { get; set; }
        public string Value { get; set; }
    }
}
