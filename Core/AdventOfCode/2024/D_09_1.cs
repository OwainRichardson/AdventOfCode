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
    public static class D_09_1
    {
        public static void Execute()
        {
            string input = File.ReadAllLines(@"2024\Data\day09.txt")[0];

            List<string> memoryBlocks = CreateMemoryBlocks(input);

            CondenseMemory(memoryBlocks);

            long total = 0;
            for (int index = 0; index < memoryBlocks.Count; index++)
            {
                if (memoryBlocks[index] == ".") break;

                total += (index * long.Parse(memoryBlocks[index]));
            }

            Console.WriteLine(total);
        }

        private static void CondenseMemory(List<string> memoryBlocks)
        {
            int indexOfFirstDot = memoryBlocks.IndexOf(".");
            string lastValue = memoryBlocks.Last(m => m != ".");
            int lastIndexOfValue = memoryBlocks.LastIndexOf(lastValue);

            while (indexOfFirstDot < lastIndexOfValue)
            {
                memoryBlocks[indexOfFirstDot] = lastValue;
                memoryBlocks[lastIndexOfValue] = ".";
                memoryBlocks.RemoveAt(lastIndexOfValue);

                indexOfFirstDot = memoryBlocks.IndexOf(".");
                lastValue = memoryBlocks.Last(m => m != ".");
                lastIndexOfValue = memoryBlocks.LastIndexOf(lastValue);
            }
        }

        private static List<string> CreateMemoryBlocks(string input)
        {
            int number = 0;
            List<string> memoryBlocks = new List<string>();

            for (int index = 0; index < input.Length; index += 2)
            {
                int numberOfBlocks = int.Parse(input[index].ToString());
                for (int n = 1; n <= numberOfBlocks; n++)
                {
                    memoryBlocks.Add(number.ToString());
                }

                if (index + 1 < input.Length)
                {
                    int gap = int.Parse(input[index + 1].ToString());
                    if (gap > 0)
                    {
                        for (int g = 1; g <= gap; g++)
                        {
                            memoryBlocks.Add(".");
                        }
                    }
                }

                number++;
            }

            return memoryBlocks;
        }
    }
}