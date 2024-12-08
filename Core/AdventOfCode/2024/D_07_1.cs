using AdventOfCode._2024.Models;
using AdventOfCode._2024.Models.Enums;
using System.IO.MemoryMappedFiles;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using System.Net.Http.Headers;

namespace AdventOfCode._2024
{
    public static class D_07_1
    {
        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2024\Data\day07.txt").ToArray();
            long answer = 0;

            foreach (string input in inputs)
            {
                string[] split = input.Split(':', StringSplitOptions.RemoveEmptyEntries);
                long target = long.Parse(split[0]);

                List<int> values = split[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(s => int.Parse(s)).ToList();

                int numberOfOptions = (int)Math.Pow(2, values.Count - 1);
                int lengthOfMaxString = Convert.ToString(numberOfOptions - 1, 2).Length;

                for (int i = 0; i < numberOfOptions; i++)
                {
                    string binary =  Convert.ToString(i, 2).PadLeft(lengthOfMaxString, '0');

                    int binaryIndex = 0;
                    long total = values[0];
                    for (int index = 1; index <= values.Count - 1; index++)
                    {
                        if (binary[binaryIndex] == '0')
                        {
                            total += values[index];
                        }
                        else if (binary[binaryIndex] == '1')
                        {
                            total *= values[index];
                        }

                        binaryIndex++;
                    }

                    if (total == target)
                    {
                        answer += target;
                        break;
                    }

                }
            }

            Console.WriteLine(answer);
        }
    }
}