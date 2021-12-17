using AdventOfCode.Common;
using System;

namespace AdventOfCode._2015
{
    public class D_20_1
    {
        public static void Execute()
        {
            int input = 36000000;
            int house = 2;
            int tooBigHouse = 0;
            int presents = 0;

            while (presents < input)
            {
                presents = 0;

                for (int elf = 1; elf <= house / 2; elf++)
                {
                    if (house % elf == 0)
                    {
                        presents += elf * 10;
                    }
                }

                presents += house * 10;

                tooBigHouse = house;

                house += 2;
            }

            Console.Write($"Lowest possible house is: ");
            CustomConsoleColour.SetAnswerColour();
            Console.Write(tooBigHouse);
            Console.ResetColor();
            Console.WriteLine();
        }
    }
}