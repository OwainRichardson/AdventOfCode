using AdventOfCode.Common;
using System;

namespace AdventOfCode._2015
{
    public class D_20_2
    {
        public static void Execute()
        {
            int input = 36000000;
            int house = 2;
            int firstHouseLargerThanTarget = 0;
            int presents = 0;

            while (presents < input)
            {
                presents = 0;

                for (int elf = 1; elf <= Math.Ceiling((double)house / 2); elf++)
                {
                    if (house % elf == 0 && (elf * 50 >= house))
                    {
                        presents += elf * 11;
                    }
                }

                if (house > 1)
                {
                    presents += house * 11;
                }

                if (house % 50000 == 0)
                {
                    Console.Write(presents);
                }

                firstHouseLargerThanTarget = house;

                house += 2;
            }

            Console.Write($"Lowest possible house is: ");
            CustomConsoleColour.SetAnswerColour();
            Console.Write(firstHouseLargerThanTarget);
            Console.ResetColor();
            Console.WriteLine();
        }
    }
}