using AdventOfCode._2023.Models;
using System.Text.RegularExpressions;

namespace AdventOfCode._2023
{
    public static class D_02_1
    {
        private const int RedLimit = 12;
        private const int GreenLimit = 13;
        private const int BlueLimit = 14;

        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2023\Data\day02.txt").ToArray();

            List<Game> games = ParseInputsToGames(inputs);

            int validGameTotal = 0;

            foreach (Game game in games)
            {
                if  (game.Sets.Any(s => s.Red > RedLimit) || game.Sets.Any(s => s.Green > GreenLimit) || game.Sets.Any(s => s.Blue > BlueLimit))
                {
                    continue;
                }

                validGameTotal += game.GameId;
            }

            Console.WriteLine(validGameTotal);
        }

        private static List<Game> ParseInputsToGames(string[] inputs)
        {
            List<Game> games = new List<Game>();

            foreach (string input in inputs)
            {
                string pattern = @"^Game\W(\d+)";
                Regex regex = new Regex(pattern);

                int gameId = int.Parse(regex.Match(input).Groups[1].Value);

                Game game = new Game { GameId = gameId };

                string sets = input.Substring(input.IndexOf(':') + 2);
                string[] setsSplit = sets.Split(';').ToArray();

                string setPattern = @"(\d+)\Wblue|(\d+)\Wred|(\d+)\Wgreen";
                Regex setRegex = new Regex(setPattern);

                foreach (string set in setsSplit)
                {
                    MatchCollection matches = setRegex.Matches(set);
                    GameSet gameSet = new GameSet(); 

                    foreach (Match match in matches)
                    {
                        if (match.Groups[0].Value.Contains("red"))
                        {
                            int value = int.Parse(match.Groups[2].Value);
                            gameSet.Red = value;
                        }
                        else if (match.Groups[0].Value.Contains("blue"))
                        {
                            int value = int.Parse(match.Groups[1].Value);
                            gameSet.Blue = value;
                        }
                        else if (match.Groups[0].Value.Contains("green"))
                        {
                            int value = int.Parse(match.Groups[3].Value);
                            gameSet.Green = value;
                        }
                    }

                    game.Sets.Add(gameSet);
                }


                games.Add(game);
            }

            return games;
        }
    }
}
