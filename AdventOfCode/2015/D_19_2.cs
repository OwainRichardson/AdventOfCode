using AdventOfCode._2015.Models;
using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode._2015
{
    public class D_19_2
    {
        public static List<Replacement> _replacements = new List<Replacement>();
        public static List<string> _molecules = new List<string>();

        public static void Execute()
        {
            var inputs = File.ReadAllLines(@"C:\Work\Misc Projects\AdventOfCode\AdventOfCode\AdventOfCode\2015\Data\day19_full.txt");
            string inputText = "CRnSiRnCaPTiMgYCaPTiRnFArSiThFArCaSiThSiThPBCaCaSiRnSiRnTiTiMgArPBCaPMgYPTiRnFArFArCaSiRnBPMgArPRnCaPTiRnFArCaSiThCaCaFArPBCaCaPTiTiRnFArCaSiRnSiAlYSiThRnFArArCaSiRnBFArCaCaSiRnSiThCaCaCaFYCaPTiBCaSiThCaSiThPMgArSiRnCaPBFYCaCaFArCaCaCaCaSiThCaSiRnPRnFArPBSiThPRnFArSiRnMgArCaFYFArCaSiRnSiAlArTiTiTiTiTiTiTiRnPMgArPTiTiTiBSiRnSiAlArTiTiRnPMgArCaFYBPBPTiRnSiRnMgArSiThCaFArCaSiThFArPRnFArCaSiRnTiBSiThSiRnSiAlYCaFArPRnFArSiThCaFArCaCaSiThCaCaCaSiRnPRnCaFArFYPMgArCaPBCaPBSiRnFYPBCaFArCaSiAl";

            ParseInput(inputs);

            ReduceInputToElements(inputText);
        }

        private static void ReduceInputToElements(string inputText)
        {
            string tempInput = inputText;
            int steps = 0;

            while (_replacements.Where(x => x.ToReplace != "e").Any(x => Regex.IsMatch(tempInput, x.ReplaceWith)))
            {
                var replacement = _replacements.Where(x => x.ToReplace != "e").First(x => Regex.IsMatch(tempInput, x.ReplaceWith));

                var regex = new Regex(Regex.Escape(replacement.ReplaceWith));
                tempInput = regex.Replace(tempInput, replacement.ToReplace, 1);

                steps++;
            }

            if (_replacements.Where(x => x.ToReplace == "e").Any(x => Regex.IsMatch(tempInput, x.ReplaceWith)))
            {
                var replacement = _replacements.Where(x => x.ToReplace == "e").First(x => Regex.IsMatch(tempInput, x.ReplaceWith));

                var regex = new Regex(Regex.Escape(replacement.ReplaceWith));
                tempInput = regex.Replace(tempInput, replacement.ToReplace, 1);

                steps++;

                Console.Write("Created in ");
                CustomConsoleColour.SetAnswerColour();
                Console.Write(steps);
                Console.ResetColor();
                Console.Write(" steps");
                Console.WriteLine();
            }
            else
            {
                throw new ArithmeticException();
            }
        }

        private static void ParseInput(string[] inputs)
        {
            foreach (var input in inputs)
            {
                var split = input.Split(new string[] { " => " }, StringSplitOptions.RemoveEmptyEntries);

                _replacements.Add(new Replacement { ToReplace = split[0], ReplaceWith = split[1] });
            }
        }
    }
}