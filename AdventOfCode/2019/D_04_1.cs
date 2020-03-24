using AdventOfCode._2019.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2019
{
    public static class D_04_1
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
            for (int j = 1; j < password.Length; j++)
            {
                if (password[j] == password[j - 1])
                {
                    return true;
                }
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
}
