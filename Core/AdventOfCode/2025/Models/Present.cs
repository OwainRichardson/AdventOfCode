
namespace AdventOfCode._2025.Models;

public class Present
{
    public Present(string[] map)
    {
        PositionOne = TranslateToCoords(map);
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
}
