

namespace AdventOfCode._2025.Models;

public class Present
{
    public Present(string[] map)
    {
        PositionOne = TranslateToCoords(map);
        PositionTwo = Rotate(PositionOne);
        PositionThree = Rotate(PositionTwo);
        PositionFour = Rotate(PositionThree);
    }

    private List<Coord> Rotate(List<Coord> position)
    {
        List<Coord> newPosition = new();

        foreach (Coord coord in position)
        {
            int x, y;

            switch (coord.Y, coord.X)
            {
                case (0, 0):
                    x = 2;
                    y = 0;
                    break;
                case (0, 1):
                    x = 2;
                    y = 1;
                    break;
                case (0, 2):
                    x = 2;
                    y = 2;
                    break;

                case (1, 0):
                    x = 1;
                    y = 0;
                    break;
                case (1, 1):
                    x = 1;
                    y = 1;
                    break;
                case (1, 2):
                    x = 1;
                    y = 2;
                    break;

                case (2, 0):
                    x = 0;
                    y = 0;
                    break;
                case (2, 1):
                    x = 0;
                    y = 1;
                    break;
                case (2, 2):
                    x = 0;
                    y = 2;
                    break;
                default:
                    throw new ArgumentException();
            }

            newPosition.Add(new() { X = x, Y = y, Value = coord.Value });
        }

        return newPosition;
    }

    private List<Coord> TranslateToCoords(string[] map)
    {
        List<Coord> coords = new();
        for (int y = 0; y < map.Length; y++)
        {
            for (int x = 0; x < map[0].Length; x++)
            {
                if (map[y][x] == '#')
                {
                    coords.Add(new()
                    {
                        X = x,
                        Y = y,
                        Value = '#'
                    });
                }
            }
        }

        return coords;
    }

    public int Id { get; set; }
    public List<Coord> PositionOne { get; set; }
    public List<Coord> PositionTwo { get; set; }
    public List<Coord> PositionThree { get; set; }
    public List<Coord> PositionFour { get; set; }
    public List<List<Coord>> Positions => [PositionOne, PositionTwo, PositionThree, PositionFour];
}
