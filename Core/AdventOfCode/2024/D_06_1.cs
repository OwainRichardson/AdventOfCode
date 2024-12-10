using AdventOfCode._2024.Models.Enums;

namespace AdventOfCode._2024
{
    public static class D_06_1
    {
        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2024\Data\day06.txt").ToArray();

            (char[,] map, int x, int y) = ParseInputs(inputs);

            Direction direction = Direction.Up;

            int distinctSquares = 1;
            map[x, y] = 'X';

            while (x >= 0 && x < inputs.Length && y >= 0 && y < inputs.Length)
            {
                if (ObjectInFront(map, x, y, direction, inputs.Length))
                {
                    direction = TurnRight(direction);
                }
                else
                {
                    (x, y) = Move(direction, x, y);

                    if (x < 0 || x >= inputs.Length || y < 0 || y >= inputs.Length)
                    {
                        continue;
                    }

                    if (map[x, y] == '.')
                    {
                        map[x, y] = 'X';
                        distinctSquares += 1;
                    }
                }
            }

            Console.WriteLine(distinctSquares);
        }

        private static (int x, int y) Move(Direction direction, int x, int y) => direction switch
        {
            Direction.Up => (x, y - 1),
            Direction.Right => (x + 1, y),
            Direction.Down => (x, y + 1),
            Direction.Left => (x - 1, y),
            _ => throw new InvalidOperationException()
        };

        private static Direction TurnRight(Direction direction) => direction switch
        {
            Direction.Up => Direction.Right,
            Direction.Right => Direction.Down,
            Direction.Down => Direction.Left,
            Direction.Left => Direction.Up,
            _ => throw new InvalidOperationException()
        };

        private static bool ObjectInFront(char[,] map, int x, int y, Direction direction, int length)
        {
            if (direction == Direction.Up && y - 1 >= 0 && map[x, y - 1] == '#')
            {
                return true;
            }

            if (direction == Direction.Right && x + 1 < length && map[x + 1, y] == '#')
            {
                return true;
            }

            if (direction == Direction.Down && y + 1 < length && map[x, y + 1] == '#')
            {
                return true;
            }

            if (direction == Direction.Left && x - 1 >= 0 && map[x - 1, y] == '#')
            {
                return true;
            }

            return false;
        }

        private static void DrawMap(char[,] map, int length)
        {
            Console.WriteLine();

            for (int y = 0; y < length; y++)
            {
                for (int x = 0; x < length; x++)
                {
                    Console.Write(map[x, y]);
                }
                Console.WriteLine();
            }
        }

        private static (char[,] map, int x, int y) ParseInputs(string[] inputs)
        {
            char[,] map = new char[inputs[0].Length, inputs.Length];

            int startX = -1;
            int startY = -1;

            int y = 0;
            foreach (string input in inputs)
            {
                int x = 0;

                foreach (char c in input)
                {
                    map[x, y] = c;

                    if (c == '^')
                    {
                        startX = x;
                        startY = y;
                    }

                    x++;
                }

                y++;
            }

            return (map, startX, startY);
        }
    }
}