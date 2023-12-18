using AdventOfCode._2023.Models;
using AdventOfCode._2023.Models.Enums;
using System.Xml.Serialization;

namespace AdventOfCode._2023
{
    public static class D_10_2
    {
        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2023\Data\day10.txt").ToArray();

            List<PipePiece> pipePieces = ParseInputsToPipePieces(inputs);

            PipePiece firstPiece = pipePieces.Single(pp => pp.Type == 'S');
            firstPiece.Distance = 0;

            (Directions firstDirection, Directions secondDirection) = CalculateDirection(pipePieces, firstPiece);

            DistanceMap(pipePieces, firstPiece, firstDirection, 1);

            SetDirectionsToInnerLoop(pipePieces, firstPiece, firstDirection, 1, null);
            if (pipePieces.Any(p => p.IsLoop && (p.FirstDirectionToInLoop == Directions.None || p.SecondDirectionToInLoop == Directions.None) && p.Type != 'S'))
            {
                SetDirectionsToInnerLoop(pipePieces, firstPiece, secondDirection, 1, null);
            }

            MarkPiecesAsInLoop(pipePieces, firstPiece, firstDirection, 1);

            Console.WriteLine(pipePieces.Count(p => p.IsContainedByLoop));
        }

        private static void MarkPiecesAsInLoop(List<PipePiece> pipePieces, PipePiece piece, Directions direction, int distance)
        {
            while (piece.Type != 'S' || distance == 1)
            {
                MarkPieces(piece, piece.FirstDirectionToInLoop, pipePieces);
                if (piece.SecondDirectionToInLoop != null)
                {
                    MarkPieces(piece, piece.SecondDirectionToInLoop, pipePieces);
                }

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

                nextPiece.Distance = distance;

                direction = CalculateNextDirection(direction, nextPiece);
                distance += 1;
                piece = nextPiece;
            }
        }

        private static void MarkPieces(PipePiece piece, Directions? directionToInLoop, List<PipePiece> pipePieces)
        {
            if (piece.Type == 'S') return;

            if (!piece.IsLoop)
            {
                piece.IsContainedByLoop = true;
            }

            PipePiece nextPiece;
            if (directionToInLoop == Directions.Down)
            {
                nextPiece = pipePieces.Single(p => p.Y == piece.Y + 1 && p.X == piece.X);
            }
            else if (directionToInLoop == Directions.Up)
            {
                nextPiece = pipePieces.Single(p => p.Y == piece.Y - 1 && p.X == piece.X);
            }
            else if (directionToInLoop == Directions.Left)
            {
                nextPiece = pipePieces.Single(p => p.Y == piece.Y && p.X == piece.X - 1);
            }
            else if (directionToInLoop == Directions.Right)
            {
                nextPiece = pipePieces.Single(p => p.Y == piece.Y && p.X == piece.X + 1);
            }
            else
            {
                throw new InvalidOperationException();
            }

            if (nextPiece.IsLoop) return;

            MarkPieces(nextPiece, directionToInLoop, pipePieces);
        }

        private static void SetDirectionsToInnerLoop(List<PipePiece> pipePieces, PipePiece piece, Directions direction, int distance, Directions? directionToCentre)
        {
            while (piece.Type != 'S' || distance == 1)
            {
                PipePiece nextPiece;
                Directions? firstDirectionToCentre;
                Directions? secondDirectionToCentre;

                if ((piece.Type == '-' || piece.Type == '|') && (piece.FirstDirectionToInLoop != null && piece.FirstDirectionToInLoop != Directions.None))
                {
                    firstDirectionToCentre = piece.FirstDirectionToInLoop;
                    secondDirectionToCentre = piece.SecondDirectionToInLoop;
                }
                else if (piece.FirstDirectionToInLoop != null && piece.FirstDirectionToInLoop != Directions.None && piece.SecondDirectionToInLoop != null && piece.SecondDirectionToInLoop != Directions.None)
                {
                    firstDirectionToCentre = piece.FirstDirectionToInLoop;
                    secondDirectionToCentre = piece.SecondDirectionToInLoop;
                }
                else
                {
                    (firstDirectionToCentre, secondDirectionToCentre) = SetDirectionToCentre(directionToCentre, piece, pipePieces);
                    piece.FirstDirectionToInLoop = firstDirectionToCentre;
                    piece.SecondDirectionToInLoop = secondDirectionToCentre;
                }

                directionToCentre = firstDirectionToCentre;

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

                direction = CalculateNextDirection(direction, nextPiece);
                distance += 1;
                piece = nextPiece;
            }
        }

        private static (Directions? firstDirection, Directions? secondDirection) SetDirectionToCentre(Directions? directionToCentre, PipePiece piece, List<PipePiece> pipePieces)
        {
            if (piece.Type == 'S') return (Directions.None, null);

            if (directionToCentre == null || directionToCentre == Directions.None)
            {
                if (piece.Type == '-')
                {
                    int piecesAbove = pipePieces.Count(p => p.Y < piece.Y && p.IsLoop);
                    int piecesBelow = pipePieces.Count(p => p.Y > piece.Y && p.IsLoop);

                    if (piecesAbove > piecesBelow)
                    {
                        return (Directions.Up, null);
                    }

                    return (Directions.Down, null);
                }
                else if (piece.Type == '|')
                {
                    int piecesLeft = pipePieces.Count(p => p.X < piece.X && p.IsLoop);
                    int piecesRight = pipePieces.Count(p => p.X > piece.X && p.IsLoop);

                    if (piecesLeft > piecesRight)
                    {
                        return (Directions.Left, null);
                    }

                    return (Directions.Right, null);
                }
                else
                {
                    return (Directions.None, null);
                }
            }

            if (piece.Type == '-' || piece.Type == '|')
            {
                return (directionToCentre, null);
            }

            if (piece.Type == 'J')
            {
                switch (directionToCentre.Value)
                {
                    case Directions.Up:
                        return (Directions.Left, Directions.Up);
                    case Directions.Down:
                        return (Directions.Right, Directions.Down);
                    case Directions.Left:
                        return (Directions.Up, Directions.Left);
                    case Directions.Right:
                        return (Directions.Down, Directions.Down);
                    default:
                        throw new InvalidOperationException();
                }
            }

            if (piece.Type == '7')
            {
                switch (directionToCentre.Value)
                {
                    case Directions.Up:
                        return (Directions.Right, Directions.Up);
                    case Directions.Down:
                        return (Directions.Left, Directions.Down);
                    case Directions.Left:
                        return (Directions.Down, Directions.Left);
                    case Directions.Right:
                        return (Directions.Up, Directions.Right);
                    default:
                        throw new InvalidOperationException();
                }
            }

            if (piece.Type == 'F')
            {
                switch (directionToCentre.Value)
                {
                    case Directions.Up:
                        return (Directions.Left, Directions.Up);
                    case Directions.Down:
                        return (Directions.Right, Directions.Down);
                    case Directions.Left:
                        return (Directions.Up, Directions.Left);
                    case Directions.Right:
                        return (Directions.Down, Directions.Right);
                    default:
                        throw new InvalidOperationException();
                }
            }

            if (piece.Type == 'L')
            {
                switch (directionToCentre.Value)
                {
                    case Directions.Up:
                        return (Directions.Up, Directions.Right);
                    case Directions.Down:
                        return (Directions.Left, Directions.Down);
                    case Directions.Left:
                        return (Directions.Down, Directions.Left);
                    case Directions.Right:
                        return (Directions.Up, Directions.Right);
                    default:
                        throw new InvalidOperationException();
                }
            }

            throw new InvalidOperationException();
        }

        private static void DrawMap(List<PipePiece> pipePieces)
        {
            Console.WriteLine();

            for (int y = pipePieces.Min(p => p.Y); y <= pipePieces.Max(p => p.Y); y++)
            {
                for (int x = pipePieces.Min(p => p.X); x <= pipePieces.Max(p => p.X); x++)
                {
                    PipePiece pipe = pipePieces.Single(p => p.X == x && p.Y == y);

                    if (!pipe.IsLoop)
                    {
                        if (pipe.IsContainedByLoop)
                        {
                            Console.Write("1");
                        }
                        else
                        {
                            Console.Write("0");
                        }
                    }
                    else
                    {
                        Console.Write(pipe.Type.ToString());
                    }
                }

                Console.WriteLine();
            }
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
                nextPiece.IsLoop = true;

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