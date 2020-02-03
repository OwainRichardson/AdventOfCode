using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2015.External
{
    public class D_17_2
    {
        public static int _toStore = 150;
        public static List<int> _containers = new List<int>();

        public static void Execute()
        {
            var inputs = File.ReadAllLines(@"C:\Work\Misc Projects\AdventOfCode\AdventOfCode\AdventOfCode\2015\Data\day17_full.txt");

            ParseContainers(inputs);

            var query = Enumerable
                        .Range(1, (1 << _containers.Count()) - 1)
                        .Select(index => _containers.Where((item, idx) => ((1 << idx) & index) != 0))
                        .Where(x => x.Sum() == _toStore);

            var part2 = query.GroupBy(x => x.Count())
            .OrderBy(x => x.Key)
            .First()
            .Count();

            CustomConsoleColour.SetAnswerColour();
            Console.WriteLine($"{part2}");
            Console.ResetColor();
        }

        private static void ParseContainers(string[] inputs)
        {
            foreach (var input in inputs)
            {
                _containers.Add(int.Parse(input));
            }
        }
    }
}
