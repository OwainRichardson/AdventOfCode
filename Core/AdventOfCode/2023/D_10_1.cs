using AdventOfCode._2023.Models;
using AdventOfCode._2023.Models.Enums;
using System.Data.Common;
using System.Runtime.ExceptionServices;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace AdventOfCode._2023
{
    public static class D_10_1
    {
        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2023\Data\day10.txt").ToArray();

            List<PipePiece> pipePieces = ParseInputsToPipePieces(inputs);

            PipePiece firstPiece = pipePieces.Single(pp => pp.Type == 'S');
            firstPiece.Distance = 0;

            (Directions firstDirection, Directions secondDirection) = CalculateDirection(pipePieces, firstPiece);

            DistanceMap(pipePieces, firstPiece, firstDirection, 1);
            PipePiece middlePiece = DistanceMap(pipePieces, firstPiece, secondDirection, 1);

            Console.WriteLine(middlePiece.Distance);
        }

        private static PipePiece DistanceMap(List<PipePiece> pipePieces, PipePiece piece, Directions direction, int distance)
        {
            while (piece.Type != 'S' || distance == 1)
            {
                PipePiece nextPiece;

                if (direction == Directions.Up)
                {
                    nextPiece = pipePieces.Single(pp => pp.X == piece.X && pp.Y == piece.Y - 1);
                }
                else if (direction == Directions.Down)
                {
                    nextPiece = pipePieces.Single(pp => pp.X == piece.X && pp.Y == piece.Y + 1);
                }
                else if (direction == Directions.Left)
                {
                    nextPiece = pipePieces.Single(pp => pp.X == piece.X - 1 && pp.Y == piece.Y);
                }
                else if (direction == Directions.Right)
                {
                    nextPiece = pipePieces.Single(pp => pp.X == piece.X + 1 && pp.Y == piece.Y);
                }
                else
                {
                    throw new InvalidOperationException();
                }

                if (nextPiece.Distance == distance)
                {
                    return nextPiece;
                }

                nextPiece.Distance = distance;

                direction = CalculateNextDirection(direction, nextPiece);
                distance += 1;
                piece = nextPiece;
            }

            return piece;
        }

        private static Directions CalculateNextDirection(Directions direction, PipePiece nextPiece)
        {
            if (nextPiece.Type == '|') return direction;
            if (nextPiece.Type == '-') return direction;
            if (nextPiece.Type == 'F' && direction == Directions.Up) return Directions.Right;
            if (nextPiece.Type == 'F' && direction == Directions.Left) return Directions.Down;
            if (nextPiece.Type == 'L' && direction == Directions.Down) return Directions.Right;
            if (nextPiece.Type == 'L' && direction == Directions.Left) return Directions.Up;
            if (nextPiece.Type == 'J' && direction == Directions.Down) return Directions.Left;
            if (nextPiece.Type == 'J' && direction == Directions.Right) return Directions.Up;
            if (nextPiece.Type == '7' && direction == Directions.Right) return Directions.Down;
            if (nextPiece.Type == '7' && direction == Directions.Up) return Directions.Left;

            if (nextPiece.Type == 'S') return Directions.None;

            throw new InvalidOperationException();
        }

        private static (Directions firstDirection, Directions secondDirection) CalculateDirection(List<PipePiece> pipePieces, PipePiece firstPiece)
        {
            Directions? firstDirection = null;
            Directions? secondDirection = null;

            while (firstDirection == null && secondDirection == null)
            {
                // Up
                if (pipePieces.SingleOrDefault(pp => pp.X == firstPiece.X && pp.Y == firstPiece.Y - 1) != null)
                {
                    var upPiece = pipePieces.SingleOrDefault(pp => pp.X == firstPiece.X && pp.Y == firstPiece.Y - 1);
                    if (upPiece.Type == '|' || upPiece.Type == '7' || upPiece.Type == 'F')
                    {
                        firstDirection = Directions.Up;
                    }
                }

                // Down
                if (pipePieces.SingleOrDefault(pp => pp.X == firstPiece.X && pp.Y == firstPiece.Y + 1) != null)
                {
                    var downPiece = pipePieces.SingleOrDefault(pp => pp.X == firstPiece.X && pp.Y == firstPiece.Y + 1);
                    if (downPiece.Type == '|' || downPiece.Type == 'L' || downPiece.Type == 'J')
                    {
                        if (firstDirection == null)
                        {
                            firstDirection = Directions.Down;
                        }
                        else
                        {
                            secondDirection = Directions.Down;
                        }
                    }
                }

                // Right
                if (pipePieces.SingleOrDefault(pp => pp.X == firstPiece.X + 1 && pp.Y == firstPiece.Y) != null)
                {
                    var rightPiece = pipePieces.SingleOrDefault(pp => pp.X == firstPiece.X + 1 && pp.Y == firstPiece.Y);
                    if (rightPiece.Type == '-' || rightPiece.Type == '7' || rightPiece.Type == 'J')
                    {
                        if (firstDirection == null)
                        {
                            firstDirection = Directions.Right;
                        }
                        else
                        {
                            secondDirection = Directions.Right;
                        }
                    }
                }

                // Left
                if (pipePieces.SingleOrDefault(pp => pp.X == firstPiece.X - 1 && pp.Y == firstPiece.Y) != null)
                {
                    var leftPiece = pipePieces.SingleOrDefault(pp => pp.X == firstPiece.X - 1 && pp.Y == firstPiece.Y);
                    if (leftPiece.Type == '-' || leftPiece.Type == 'L' || leftPiece.Type == 'F')
                    {
                        if (firstDirection == null)
                        {
                            firstDirection = Directions.Left;
                        }
                        else
                        {
                            secondDirection = Directions.Left;
                        }
                    }
                }
            }

            return ((Directions)Enum.Parse(typeof(Directions), firstDirection.ToString()), (Directions)Enum.Parse(typeof(Directions), secondDirection.ToString()));
        }

        private static List<PipePiece> ParseInputsToPipePieces(string[] inputs)
        {
            List<PipePiece> pipePieces = new List<PipePiece>();

            int index = 1;
            for (int y = 0; y < inputs.Length; y++)
            {
                for (int x = 0; x < inputs[y].Length; x++)
                {
                    PipePiece pipePiece = new PipePiece
                    {
                        Id = index,
                        X = x,
                        Y = y,
                        Type = inputs[y][x]
                    };

                    pipePieces.Add(pipePiece);

                    index++;
                }
            }

            return pipePieces;
        }
    }
}