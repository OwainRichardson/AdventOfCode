using AdventOfCode._2015.Models;
using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode._2015
{
    public class D_14_2
    {
        public static readonly List<Reindeer> _reindeer = new List<Reindeer>();

        public static void Execute()
        {
            var input = File.ReadAllLines(@"C:\Work\Misc Projects\AdventOfCode\AdventOfCode\AdventOfCode\2015\Data\day14_full.txt");

            ParseReindeer(input);

            for (int i = 1; i <= 2503; i++)
            {
                foreach (var deer in _reindeer)
                {
                    MoveDeer(deer, i);
                }

                AwardPoints();
            }

            foreach (var deer in _reindeer)
            {
                if (deer.Points == _reindeer.Max(x => x.Points))
                {
                    Console.Write($"{deer.Name} has won ");
                    CustomConsoleColour.SetAnswerColour();
                    Console.Write(deer.Points);
                    Console.ResetColor();
                    Console.Write(" points");
                    Console.WriteLine();
                }
            }
        }

        private static void AwardPoints()
        {
            List<Reindeer> winningDeer = _reindeer.Where(x => x.DistanceTravelled == _reindeer.Max(y => y.DistanceTravelled)).ToList();

            foreach (var deer in winningDeer)
            {
                deer.Points += 1;
            }
        }

        private static void MoveDeer(Reindeer deer, int second)
        {
            var movementPhase = second % (deer.RestTime + deer.TravelTime);
            if (movementPhase != 0 && movementPhase <= deer.TravelTime)
            {
                deer.DistanceTravelled += deer.Speed;
            }
        }

        private static void ParseReindeer(string[] input)
        {
            foreach (var description in input)
            {
                Reindeer deer = new Reindeer();

                var nameSplit = description.Split(new string[] { " can fly " }, StringSplitOptions.RemoveEmptyEntries);
                deer.Name = nameSplit[0];

                var speedSplit = nameSplit[1].Split(new string[] { " km/s for " }, StringSplitOptions.RemoveEmptyEntries);
                deer.Speed = int.Parse(speedSplit[0]);

                var travelTimeSplit = speedSplit[1].Split(new string[] { " seconds" }, StringSplitOptions.RemoveEmptyEntries);
                deer.TravelTime = int.Parse(travelTimeSplit[0]);

                deer.RestTime = int.Parse(travelTimeSplit[1].Replace(", but then must rest for ", "").Replace(" seconds.", ""));

                _reindeer.Add(deer);
            }
        }
    }
}