using AdventOfCode._2017.Models;
using System;
using System.IO;

namespace AdventOfCode._2017
{
    public static class D_19_2
    {
        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"C:\Work\Misc Projects\AdventOfCode\AdventOfCode\AdventOfCode\2017\Data\day19_full.txt");

            int row = 0;
            int column = 0;

            column = FindStartIndex(inputs[row]);
            bool finished = false;
            string lettersFound = string.Empty;
            int direction = ScannerDirections.Down;
            int steps = 0;

            while (!finished)
            {
                if(string.IsNullOrWhiteSpace(inputs[row][column].ToString()))
                {
                    break;
                }
                else if (inputs[row][column].ToString() == "+")
                {
                    direction = ChangeDirection(inputs, row, column, direction);
                }
                else if (char.IsLetter(inputs[row][column]))
                {
                    lettersFound = $"{lettersFound}{inputs[row][column]}";
                }

                if (direction == -1)
                {
                    finished = true;
                    break;
                }

                steps++;

                Move(direction, ref row, ref column);
            }

            Console.WriteLine(steps);

        }

        private static int ChangeDirection(string[] inputs, int row, int column, int direction)
        {
            switch (direction)
            {
                case ScannerDirections.Down:
                    if (CheckLeft(inputs[row], column))
                    {
                        return ScannerDirections.Left;
                    }
                    if (CheckRight(inputs[row], column))
                    {
                        return ScannerDirections.Right;
                    }
                    break;
                case ScannerDirections.Up:
                    if (CheckLeft(inputs[row], column))
                    {
                        return ScannerDirections.Left;
                    }
                    if (CheckRight(inputs[row], column))
                    {
                        return ScannerDirections.Right;
                    }
                    break;
                case ScannerDirections.Left:
                    if (CheckUp(inputs, row, column))
                    {
                        return ScannerDirections.Up;
                    }
                    if (CheckDown(inputs, row, column))
                    {
                        return ScannerDirections.Down;
                    }
                    break;
                case ScannerDirections.Right:
                    if (CheckUp(inputs, row, column))
                    {
                        return ScannerDirections.Up;
                    }
                    if (CheckDown(inputs, row, column))
                    {
                        return ScannerDirections.Down;
                    }
                    break;
                default:
                    return -1;
            }

            return -1;
        }

        private static bool CheckUp(string[] inputs, int row, int column)
        {
            if (!string.IsNullOrWhiteSpace(inputs[row - 1][column].ToString()))
            {
                return true;
            }

            return false;
        }

        private static bool CheckDown(string[] inputs, int row, int column)
        {
            if (!string.IsNullOrWhiteSpace(inputs[row + 1][column].ToString()))
            {
                return true;
            }

            return false;
        }

        private static bool CheckRight(string input, int column)
        {
            if (!string.IsNullOrWhiteSpace(input[column + 1].ToString()))
            {
                return true;
            }

            return false;
        }

        private static bool CheckLeft(string input, int column)
        {
            if (!string.IsNullOrWhiteSpace(input[column - 1].ToString()))
            {
                return true;
            }

            return false;
        }

        private static void Move(int direction, ref int row, ref int column)
        {
            switch (direction)
            {
                case ScannerDirections.Down:
                    row++;
                    break;
                case ScannerDirections.Up:
                    row--;
                    break;
                case ScannerDirections.Right:
                    column++;
                    break;
                case ScannerDirections.Left:
                    column--;
                    break;
                default:
                    throw new ArgumentException();
            }
        }

        private static int FindStartIndex(string input)
        {
            int index = 0;
            foreach (Char c in input)
            {
                if (c.ToString() == "|")
                {
                    return index;
                }

                index++;
            }

            return -1;
        }
    }
}
