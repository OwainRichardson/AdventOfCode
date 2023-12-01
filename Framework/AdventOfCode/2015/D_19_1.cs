using AdventOfCode._2015.Models;
using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode._2015
{
    public class D_19_1
    {
        public static List<Replacement> _replacements = new List<Replacement>();
        public static List<string> _molecules = new List<string>();

        public static void Execute()
        {
            var inputs = File.ReadAllLines(@"C:\Work\Misc Projects\AdventOfCode\AdventOfCode\AdventOfCode\2015\Data\day19_full.txt");
            string inputText = "CRnSiRnCaPTiMgYCaPTiRnFArSiThFArCaSiThSiThPBCaCaSiRnSiRnTiTiMgArPBCaPMgYPTiRnFArFArCaSiRnBPMgArPRnCaPTiRnFArCaSiThCaCaFArPBCaCaPTiTiRnFArCaSiRnSiAlYSiThRnFArArCaSiRnBFArCaCaSiRnSiThCaCaCaFYCaPTiBCaSiThCaSiThPMgArSiRnCaPBFYCaCaFArCaCaCaCaSiThCaSiRnPRnFArPBSiThPRnFArSiRnMgArCaFYFArCaSiRnSiAlArTiTiTiTiTiTiTiRnPMgArPTiTiTiBSiRnSiAlArTiTiRnPMgArCaFYBPBPTiRnSiRnMgArSiThCaFArCaSiThFArPRnFArCaSiRnTiBSiThSiRnSiAlYCaFArPRnFArSiThCaFArCaCaSiThCaCaCaSiRnPRnCaFArFYPMgArCaPBCaPBSiRnFYPBCaFArCaSiAl";

            ParseInput(inputs);

            CalculateMolecules(inputText);

            //foreach (var molecule in _molecules.Distinct())
            //{
            //    Console.WriteLine(molecule);
            //}

            CustomConsoleColour.SetAnswerColour();
            Console.WriteLine(_molecules.Distinct().Count());
            Console.ResetColor();
        }

        private static void CalculateMolecules(string inputText)
        {
            foreach (var replacement in _replacements)
            {
                if (Regex.IsMatch(inputText, replacement.ToReplace))
                {
                    foreach (Match match in Regex.Matches(inputText, replacement.ToReplace))
                    {
                        var stringToReplace = inputText;
                        var regex = new Regex(Regex.Escape(replacement.ToReplace));
                        var result = regex.Replace(stringToReplace, replacement.ReplaceWith, 1, match.Index);
                        _molecules.Add(result);
                    }
                }
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