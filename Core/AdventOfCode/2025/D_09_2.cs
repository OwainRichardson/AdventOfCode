

using AdventOfCode._2025.Models;
using NetTopologySuite.Geometries;

namespace AdventOfCode._2025;

public static class D_09_2
{
    public static void Execute()
    {
        GeometryFactory geometryFactory = new();

        long biggestRectangle = 0;

        string[] inputs = File.ReadAllLines(@"2025\Data\day09.txt").ToArray();

        List<Tile> map = ParseInputs(inputs);
        map = map.Append(map[0]).ToList();

        Coordinate[] mapPolygon = map.Select(m => new Coordinate(m.X, m.Y)).ToArray();

        for (int corner = map.Min(m => m.Id); corner <= map.Max(m => m.Id); corner++)
        {
            for (int otherCorner = corner + 1; otherCorner <= map.Max(m => m.Id); otherCorner++)
            {
                if (corner == otherCorner) continue;

                Tile cornerCoord = map.First(c => c.Id == corner);
                Tile otherCornerCoord = map.First(c => c.Id == otherCorner);

                Coordinate[] rectanglePolygon = [new() { X = cornerCoord.X, Y = cornerCoord.Y }, new() { X = otherCornerCoord.X, Y = cornerCoord.Y }, new() { X = otherCornerCoord.X, Y = otherCornerCoord.Y }, new() { X = cornerCoord.X, Y = otherCornerCoord.Y }, new() { X = cornerCoord.X, Y = cornerCoord.Y }];

                if (MapPolygonContainsRectanglePolygon(mapPolygon, rectanglePolygon))
                {
                    long xDifference = Math.Abs(cornerCoord.X - otherCornerCoord.X) + 1;
                    long yDifference = Math.Abs(cornerCoord.Y - otherCornerCoord.Y) + 1;

                    long rectangleSize = xDifference * yDifference;

                    if (rectangleSize > biggestRectangle)
                    {
                        biggestRectangle = rectangleSize;
                    }
                }
            }
        }

        Console.WriteLine(biggestRectangle);
    }

    private static List<Tile> ParseInputs(string[] inputs)
    {
        List<Tile> map = new();

        int currentId = 1;
        foreach (string input in inputs)
        {
            string[] split = input.Split(',', StringSplitOptions.RemoveEmptyEntries);

            map.Add(new()
            {
                Id = currentId,
                X = int.Parse(split[0]),
                Y = int.Parse(split[1])
            });

            currentId++;
        }

        return map;
    }

    private static bool MapPolygonContainsRectanglePolygon(Coordinate[] mapPoly, Coordinate[] rectanglePoly)
    {
        var geomFactory = new GeometryFactory();

        var mapPolygon = geomFactory.CreatePolygon(mapPoly);
        var rectanglePolygon = geomFactory.CreatePolygon(rectanglePoly);

        return mapPolygon.Contains(rectanglePolygon);
    }
}