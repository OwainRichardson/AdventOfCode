using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace AdventOfCode._2016
{
    public static class D_07_2
    {
        public static void Execute()
        {
            var inputs = File.ReadAllLines(@"C:\Work\Misc Projects\AdventOfCode\AdventOfCode\AdventOfCode\2016\Data\day07_full.txt");

            int total = 0;
            foreach (var input in inputs)
            {
                if (SupportsSSL(input))
                {
                    total++;
                }
                //for (int i = 0; i < input.Length - 2; i++)
                //{
                //    bool matchFound = false;
                //    if (char.IsLetter(input[i]) && char.IsLetter(input[i + 1]) && char.IsLetter(input[i + 2]) 
                //                && input[i] == input[i + 2] && input[i] != input[i + 1])
                //    {
                //        string searchFor = $"(?<={input[i + 1]}){input[i]}{input[i + 1]}";

                //        var matches = Regex.Matches(input, searchFor);
                //        foreach (Match match in matches)
                //        {
                //            if (match.Index > i + 2)
                //            {
                //                matchFound = true;
                //                total++;
                //                break;
                //            }
                //        }

                //        if (matchFound)
                //        {
                //            break;
                //        }
                //    }
                //}
            }

            CustomConsoleColour.SetAnswerColour();
            Console.WriteLine(total);
            Console.ResetColor();
        }

        private static bool SupportsSSL(string input)
        {
            string[] ipv7 = Regex.Split(input, @"\[[^\]]*\]");
            foreach (string ip in ipv7)
            {
                List<string> aba = checkABA(ip);
                foreach (var val in aba)
                {
                    string bab = val[1].ToString() + val[0].ToString() + val[1].ToString();
                    foreach (Match m in Regex.Matches(input, @"\[(\w*)\]"))
                    {
                        if (m.Value.Contains(bab))
                            return true;
                    }

                }
            }
            return false;
        }

        static List<string> checkABA(string input)
        {
            List<string> lst = new List<string>();
            for (int i = 0; i < input.Length - 2; i++)
            {
                if (input[i] == input[i + 2] && input[i] != input[i + 1])
                    lst.Add(input[i].ToString() + input[i + 1].ToString() + input[i + 2].ToString());
            }

            return lst;
        }
    }
}
