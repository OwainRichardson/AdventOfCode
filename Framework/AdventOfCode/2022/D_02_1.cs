using System;
using System.IO;
using System.Linq;

namespace AdventOfCode._2022
{
    public class D_02_1
    {
        private const string YouRock = "X";
        private const string YouPaper = "Y";
        private const string YouScissors = "Z";

        private const string ElfRock = "A";
        private const string ElfPaper = "B";
        private const string ElfScissors = "C";

        private const int Win = 6;
        private const int Draw = 3;
        private const int Lose = 0;

        public static void Execute()
        { 
            string[] rounds = File.ReadAllLines(@"2022\Data\day02.txt").ToArray();
            int totalPoints = 0;

            foreach (string round in rounds)
            {
                int roundPoints = 0;
                string[] hands = round.Split(' ');

                switch (hands[1])
                {
                    case YouRock:
                        roundPoints += 1;
                        roundPoints += CalculateWinLoseDraw(hands[0], hands[1]);
                        break;
                    case YouPaper:
                        roundPoints += 2;
                        roundPoints += CalculateWinLoseDraw(hands[0], hands[1]);
                        break;
                    case YouScissors:
                        roundPoints += 3;
                        roundPoints += CalculateWinLoseDraw(hands[0], hands[1]);
                        break;
                    default:
                        throw new ArgumentException();
                }

                totalPoints += roundPoints;
                roundPoints = 0;
            }

            Console.WriteLine(totalPoints);
        }

        private static int CalculateWinLoseDraw(string elfHand, string yourHand)
        {
            switch (yourHand)
            {
                case YouRock:
                    switch (elfHand)
                    {
                        case ElfRock:
                            return Draw;
                        case ElfPaper:
                            return Lose;
                        case ElfScissors:
                            return Win;
                    }
                    break;
                case YouPaper:
                    switch (elfHand)
                    {
                        case ElfRock:
                            return Win;
                        case ElfPaper:
                            return Draw;
                        case ElfScissors:
                            return Lose;
                    }
                    break;
                case YouScissors:
                    switch (elfHand)
                    {
                        case ElfRock:
                            return Lose;
                        case ElfPaper:
                            return Win;
                        case ElfScissors:
                            return Draw;
                    }
                    break;
            }

            return -100;
        }
    }
}
