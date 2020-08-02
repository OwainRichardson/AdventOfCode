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
    public static class D_05_1
    {
        public static void Execute()
        {
            var input = File.ReadAllLines(@"../../../AdventOfCode/2018/Data/day05_full.txt")[0];
            bool finished = false;

            while (!finished)
            {
                if (input.Length == 0)
                {
                    break;
                }

                for (int index = 0; index < input.Length; index++)
                {
                    if (index == input.Length -1)
                    {
                        finished = true;
                        break;
                    }

                    if (char.IsUpper(input[index]))
                    {
                        if (input[index].ToString().ToLower() == input[index + 1].ToString())
                        {
                            // Remove and break loop

                            input = input.Remove(index, 2);
                            break;
                        }
                    }
                    else
                    {
                        if (input[index].ToString().ToUpper() == input[index + 1].ToString())
                        {
                            // Remove and break loop

                            input = input.Remove(index, 2);
                            break;
                        }
                    }
                }
            }

            Console.WriteLine(input.Length);
        }
    }
}
