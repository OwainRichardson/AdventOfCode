using AdventOfCode._2023.Models;
using System.Text.RegularExpressions;

namespace AdventOfCode._2023
{
    public static class D_02_2
    {
        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2023\Data\day02.txt").ToArray();

            List<Game> games = ParseInputsToGames(inputs);

            foreach (Game game in games)
            {
                int redMinimum = game.Sets.Select(s => s.Red).Max();
                int blueMinimum = game.Sets.Select(s => s.Blue).Max();
                int greenMinimum = game.Sets.Select(s => s.Green).Max();

                game.GamePower += (redMinimum * blueMinimum * greenMinimum);
            }

            Console.WriteLine(games.Sum(g => g.GamePower));
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
