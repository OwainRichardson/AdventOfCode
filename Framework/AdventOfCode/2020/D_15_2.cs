using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode._2020
{
    public static class D_15_2
    {
        public static string Mask { get; set; }

        public static void Execute()
        {
            string inputs = File.ReadAllLines(@"2020\Data\day15.txt")[0];
            int[] firstNumbers = inputs.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();

            Dictionary<int, int> numbersSaid = new Dictionary<int, int>();

            int lastNumber = -1;
            int turn = 1;

            foreach (int number in firstNumbers)
            {
                numbersSaid.Add(number, turn);
                turn += 1;

                lastNumber = number;
            }

            numbersSaid.Remove(numbersSaid.Last().Key);
            turn -= 1;

            int endTurn = 30_000_000;

            while (turn <= endTurn)
            {
                if (numbersSaid.TryGetValue(lastNumber, out int lastTurn))
                {
                    var currentNumber = turn - lastTurn;
                    numbersSaid[lastNumber] = turn;
                    lastNumber = currentNumber;
                }
                else
                {
                    numbersSaid[lastNumber] = turn;
                    lastNumber = 0;
                }

                turn += 1;
            }

            Console.WriteLine(numbersSaid.First(x => x.Value == endTurn).Key);
        }

        private static bool DoesNotExist(KeyValuePair<int, int> exists)
        {
            return exists.Key == 0 && exists.Value == 0;
        }
    }
}