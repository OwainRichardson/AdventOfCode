using AdventOfCode._2021.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode._2021
{
    public static class D_04_1
    {
        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2021\Data\day04.txt").ToArray();

            List<int> calledNumbers = inputs.First().Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(input => int.Parse(input)).ToList();

            List<Board> boards = ParseBoards(inputs);

            CallNumbers(calledNumbers, boards);
        }

        private static void CallNumbers(List<int> calledNumbers, List<Board> boards)
        {
            foreach (int calledNumber in calledNumbers)
            {
                MarkBoards(boards, calledNumber);

                bool hResult = CheckHorizontalCompletion(boards, calledNumber);
                bool result = hResult ^ CheckVerticalCompletion(boards, calledNumber);

                if (result) break;
            }
        }

        private static bool CheckVerticalCompletion(List<Board> boards, int calledNumber)
        {
            foreach (Board board in boards)
            {
                for (int index = 0; index < 5; index++)
                {
                    if (board.BoardCoords.Where(bc => bc.X == index).All(bc => bc.Called))
                    {
                        int unmarkedNumbers = board.BoardCoords.Where(bc => !bc.Called).Sum(bc => bc.Value);

                        Console.WriteLine(unmarkedNumbers * calledNumber);

                        return true;
                    }
                }
            }

            return false;
        }

        private static bool CheckHorizontalCompletion(List<Board> boards, int calledNumber)
        {
            foreach (Board board in boards)
            {
                for (int index = 0; index < 5; index++)
                {
                    if (board.BoardCoords.Where(bc => bc.Y == index).All(bc => bc.Called))
                    {
                        int unmarkedNumbers = board.BoardCoords.Where(bc => !bc.Called).Sum(bc => bc.Value);

                        Console.WriteLine(unmarkedNumbers * calledNumber);

                        return true;
                    }
                }
            }

            return false;
        }

        private static void MarkBoards(List<Board> boards, int calledNumber)
        {
            foreach (Board board in boards)
            {
                if (board.BoardCoords.Any(x => x.Value == calledNumber))
                {
                    List<BoardCoord> matchingNumbers = board.BoardCoords.Where(x => x.Value == calledNumber).ToList();

                    foreach (BoardCoord matching in matchingNumbers)
                    {
                        matching.Called = true;
                    }
                }
            }
        }

        private static List<Board> ParseBoards(string[] inputs)
        {
            List<Board> boards = new List<Board>();

            Board board = new Board();
            int boardIndex = 0;

            for (int index = 2; index < inputs.Length; index++)
            {
                if (string.IsNullOrWhiteSpace(inputs[index]))
                {
                    boards.Add(board);
                    board = new Board();
                    boardIndex = 0;

                    continue;
                }

                int[] numbers = inputs[index].Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries).Select(input => int.Parse(input)).ToArray();

                for (int x = 0; x < numbers.Length; x++)
                {
                    BoardCoord boardCoord = new BoardCoord
                    {
                        Value = numbers[x],
                        X = x,
                        Y = boardIndex
                    };

                    board.BoardCoords.Add(boardCoord);
                }

                boardIndex++;
            }

            boards.Add(board);

            return boards;
        }
    }
}
