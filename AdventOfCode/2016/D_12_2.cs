using AdventOfCode._2016.Models;
using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode._2016
{
    public static class D_12_2
    {
        public static void Execute()
        {
            var inputs = File.ReadAllLines(@"C:\Work\Misc Projects\AdventOfCode\AdventOfCode\AdventOfCode\2016\Data\day12_full.txt");

            Dictionary<string, int> registers = new Dictionary<string, int>
            {
                { "a", 0 },
                { "b", 0 },
                { "c", 1 },
                { "d", 0 }
            };

            D_12_Computer.Execute(inputs, ref registers);

            Console.WriteLine(registers["a"]);
        }
    }
}
