using AdventOfCode._2020.Models;
using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode._2020
{
    public static class D_11_1
    {
        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2020\Data\day11.txt");
            string[,] currentState = new string[inputs.Length, inputs[0].Length];
            string[,] newState = new string[inputs.Length, inputs[0].Length];

            for (int y = 0; y < inputs.Length; y++)
            {
                for (int x = 0; x < inputs[0].Length; x++)
                {
                    currentState[y, x] = inputs[y][x].ToString();
                }
            }

            while(true)
            {
                newState = CalculateNewState(currentState);

                if (ArraysAreTheSame(currentState, newState)) break;

                currentState = newState;
            }

            Console.WriteLine($"Number of seats occupied: {CountOccupiedSeats(currentState)}");
        }

        private static int CountOccupiedSeats(string[,] currentState)
        {
            int count = 0;

            for (int y = 0; y < currentState.GetLength(0); y++)
            {
                for (int x = 0; x < currentState.GetLength(1); x++)
                {
                    if (currentState[y, x] == "#") count += 1;
                }
            }

            return count;
        }

        private static string[,] CalculateNewState(string[,] currentState)
        {
            string[,] newState = new string[currentState.GetLength(0), currentState.GetLength(1)];

            for (int y = 0; y < currentState.GetLength(0); y++)
            {
                for (int x = 0; x < currentState.GetLength(1); x++)
                {
                    if (currentState[y, x] == ".") newState[y, x] = ".";

                    if (currentState[y, x] == "L")
                    {
                        if (NoOccupiedAdjacentSeats(currentState, y, x))
                        {
                            newState[y, x] = "#";
                        }
                        else
                        {
                            newState[y, x] = "L";
                        }
                    }

                    if (currentState[y, x] == "#")
                    {
                        if (FourOrMoreSeatsAreOccupied(currentState, y, x))
                        {
                            newState[y, x] = "L";
                        }
                        else
                        {
                            newState[y, x] = "#";
                        }
                    }
                }
            }

            return newState;
        }

        private static bool FourOrMoreSeatsAreOccupied(string[,] currentState, int y, int x)
        {
            int occupiedSeats = 0;

            if (y > 0)
            {
                if (x > 0)
                {
                    if (currentState[y - 1, x - 1] == "#") occupiedSeats += 1;
                }

                if (currentState[y - 1, x] == "#") occupiedSeats += 1;

                if (x + 1 < currentState.GetLength(1))
                {
                    if (currentState[y - 1, x + 1] == "#") occupiedSeats += 1;
                }
            }

            if (x > 0)
            {
                if (currentState[y, x - 1] == "#") occupiedSeats += 1;
            }

            if (x + 1 < currentState.GetLength(1))
            {
                if (currentState[y, x + 1] == "#") occupiedSeats += 1;
            }

            if (y + 1 < currentState.GetLength(0))
            {
                if (x > 0)
                {
                    if (currentState[y + 1, x - 1] == "#") occupiedSeats += 1;
                }

                if (currentState[y + 1, x] == "#") occupiedSeats += 1;

                if (x + 1 < currentState.GetLength(1))
                {
                    if (currentState[y + 1, x + 1] == "#") occupiedSeats += 1;
                }
            }

            if (occupiedSeats >= 4)
            {
                return true;
            }

            return false;
        }

        private static bool NoOccupiedAdjacentSeats(string[,] currentState, int y, int x)
        {
            if (y > 0)
            {
                if (x > 0)
                {
                    if (currentState[y - 1, x - 1] == "#") return false;
                }

                if (currentState[y - 1, x] == "#") return false;

                if (x + 1 < currentState.GetLength(1))
                {
                    if (currentState[y - 1, x + 1] == "#") return false;
                }
            }

            if (x > 0)
            {
                if (currentState[y, x - 1] == "#") return false;
            }

            if (x + 1 < currentState.GetLength(1))
            {
                if (currentState[y, x + 1] == "#") return false;
            }

            if (y + 1 < currentState.GetLength(0))
            {
                if (x > 0)
                {
                    if (currentState[y + 1, x - 1] == "#") return false;
                }

                if (currentState[y + 1, x] == "#") return false;

                if (x + 1 < currentState.GetLength(1))
                {
                    if (currentState[y + 1, x + 1] == "#") return false;
                }
            }

            return true;
        }

        private static bool ArraysAreTheSame(string[,] currentState, string[,] newState)
        {
            if (currentState.GetLength(0) != newState.GetLength(0)) return false;
            if (currentState.GetLength(1) != newState.GetLength(1)) return false;

            for (int y = 0; y < currentState.GetLength(0); y++)
            {
                for (int x = 0; x < currentState.GetLength(1); x++)
                {
                    if (currentState[y, x] != newState[y, x]) return false;
                }
            }

            return true;
        }

        private static void PrintCurrentState(string[,] currentState)
        {
            for (int y = 0; y < currentState.GetLength(0); y++)
            {
                for (int x = 0; x < currentState.GetLength(1); x++)
                {
                    Console.Write(currentState[y, x]);
                }

                Console.WriteLine();
            }
        }
    }
}
