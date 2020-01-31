using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2015
{
    public static class D_11_2
    {
        private static char[] alpha = "abcdefghijklmnopqrstuvwxyz".ToCharArray();

        public static void Execute()
        {
            char[] password = "hepxcrrq".ToCharArray();

            while (!PasswordIsValid(ref password))
            {
                IncrementPassword(ref password);
            }

            IncrementPassword(ref password);

            while (!PasswordIsValid(ref password))
            {
                IncrementPassword(ref password);
            }

            CustomConsoleColour.SetAnswerColour();
            Console.WriteLine(password);
            Console.ResetColor();
        }

        private static void IncrementPassword(ref char[] password, int index = -1)
        {
            if (index == -1)
            {
                index = password.Length - 1;
            }

            password[index]++;

            if (password[index] > 'z')
            {
                password[index] = 'a';
                IncrementPassword(ref password, index - 1);
            }
        }

        private static bool PasswordIsValid(ref char[] password)
        {
            // Check Letters
            foreach (Char c in password)
            {
                if (c == 'i' || c == 'o' || c == 'l')
                {
                    return false;
                }
            }

            string passwordAsIntegers = string.Empty;
            foreach (Char c in password)
            {
                passwordAsIntegers += Array.IndexOf(alpha, c).ToString();
            }

            // Check Increasing
            bool containsIncreasing = false;
            for (int i = 2; i < password.Length; i++)
            {
                if (password[i - 2] + 1 == password[i - 1])
                {
                    if (password[i - 1] + 1 == password[i - 0])
                    {
                        containsIncreasing = true;
                        break;
                    }
                }
            }

            if (!containsIncreasing)
            {
                return false;
            }

            List<string> doubleLetters = new List<string>();
            for (int i = 1; i < password.Length; i++)
            {
                if (password[i - 1] == password[i])
                {
                    doubleLetters.Add(password[i].ToString());
                }
            }

            if (doubleLetters.Distinct().Count() < 2)
            {
                return false;
            }

            return true;
        }
    }
}