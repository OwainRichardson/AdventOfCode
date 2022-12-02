using System;
using System.IO;
using System.Linq;

namespace AdventOfCode._2022
{
    public class D_02_2
    {
        private const string YouLose = "X";
        private const string YouDraw = "Y";
        private const string YouWin = "Z";

        private const string ElfRock = "A";
        private const string ElfPaper = "B";
        private const string ElfScissors = "C";

        private const int Win = 6;
        private const int Draw = 3;
        private const int Lose = 0;

        private const int Rock = 1;
        private const int Paper = 2;
        private const int Scissors = 3;

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
                    case YouLose:
                        roundPoints += Lose;
                        roundPoints += CalculateShape(hands[0], hands[1]);
                        break;
                    case YouDraw:
                        roundPoints += Draw;
                        roundPoints += CalculateShape(hands[0], hands[1]);
                        break;
                    case YouWin:
                        roundPoints += Win;
                        roundPoints += CalculateShape(hands[0], hands[1]);
                        break;
                    default:
                        throw new ArgumentException();
                }

                totalPoints += roundPoints;
                roundPoints = 0;
            }

            Console.WriteLine(totalPoints);
        }

        private static int CalculateShape(string elfHand, string desiredResult)
        {
            switch (elfHand)
            {
                case ElfRock:
                    switch (desiredResult)
                    {
                        case YouWin:
                            return Paper;
                        case YouDraw:
                            return Rock;
                        case YouLose:
                            return Scissors;
                    }
                    break;
                case ElfPaper:
                    switch (desiredResult)
                    {
                        case YouWin:
                            return Scissors;
                        case YouDraw:
                            return Paper;
                        case YouLose:
                            return Rock;
                    }
                    break;
                case ElfScissors:
                    switch (desiredResult)
                    {
                        case YouWin:
                            return Rock;
                        case YouDraw:
                            return Scissors;
                        case YouLose:
                            return Paper;
                    }
                    break;
            }

            return -100;
        }
    }
}
