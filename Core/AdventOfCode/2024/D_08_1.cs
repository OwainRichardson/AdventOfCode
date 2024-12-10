using AdventOfCode._2024.Models;

namespace AdventOfCode._2024
{
    public static class D_08_1
    {
        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2024\Data\day08.txt").ToArray();

            List<AntinodeCoord> map = ParseInputs(inputs);

            List<string> uniqueCharacters = map.Where(c => c.Value != ".").Select(c => c.Value).Distinct().ToList();

            foreach (string character in uniqueCharacters)
            {
                List<AntinodeCoord> characterCoords = map.Where(c => c.Value == character).ToList();

                foreach (AntinodeCoord characterCoord in characterCoords)
                {
                    List<AntinodeCoord> otherCoords = characterCoords.Where(c => c.Id != characterCoord.Id).ToList();

                    foreach (Coord otherCoord in otherCoords)
                    {
                        int xDistance = characterCoord.X - otherCoord.X;
                        int yDistance = characterCoord.Y - otherCoord.Y;

                        AntinodeCoord antinode1 = map.FirstOrDefault(c => c.X == otherCoord.X - xDistance && c.Y == otherCoord.Y - yDistance);
                        if (antinode1 != null)
                        {
                            antinode1.IsAntinode = true;
                        }

                        AntinodeCoord antinode2 = map.FirstOrDefault(c => c.X == characterCoord.X + xDistance && c.Y == characterCoord.Y + yDistance);
                        if (antinode2 != null)
                        {
                            antinode2.IsAntinode = true;
                        }
                    }
                }
            }

            Console.WriteLine(map.Count(c => c.IsAntinode));
        }

        private static List<AntinodeCoord> ParseInputs(string[] inputs)
        {
            List<AntinodeCoord> map = new List<AntinodeCoord>(); 

            int y = 0;
            int id = 0;

            foreach (string input in inputs)
            {
                int x = 0;

                foreach (char c in input)
                {
                    map.Add(new AntinodeCoord { X = x, Y = y, Value = c.ToString(), IsAntinode = false, Id = id });

                    id++;
                    x++;
                }

                y++;
            }

            return map;
        }
    }
}