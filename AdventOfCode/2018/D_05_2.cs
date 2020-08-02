using AdventOfCode._2018.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode._2018
{
    public static class D_05_2
    {
        public static void Execute()
        {
            var input = File.ReadAllLines(@"../../../AdventOfCode/2018/Data/day05_full.txt")[0];

            List<string> distinctLetters = input.ToLower().Select(x => x.ToString()).Distinct().ToList();
            Dictionary<string, int> possibles = new Dictionary<string, int>();

            foreach (string letter in distinctLetters)
            {
                string temp = input.Replace(letter.ToUpper(), "");
                temp = temp.Replace(letter.ToLower(), "");

                bool finished = false;

                while (!finished)
                {
                    if (temp.Length == 0)
                    {
                        possibles.Add(letter, temp.Length);

                        break;
                    }

                    for (int index = 0; index < temp.Length; index++)
                    {
                        if (index == temp.Length - 1)
                        {
                            possibles.Add(letter, temp.Length);

                            finished = true;
                            break;
                        }

                        if (char.IsUpper(temp[index]))
                        {
                            if (temp[index].ToString().ToLower() == temp[index + 1].ToString())
                            {
                                // Remove and break loop

                                temp = temp.Remove(index, 2);
                                break;
                            }
                        }
                        else
                        {
                            if (temp[index].ToString().ToUpper() == temp[index + 1].ToString())
                            {
                                // Remove and break loop

                                temp = temp.Remove(index, 2);
                                break;
                            }
                        }
                    }
                }
            }

            Console.WriteLine(possibles.Values.Min());
        }
    }
}
