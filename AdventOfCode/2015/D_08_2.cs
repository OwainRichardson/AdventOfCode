using AdventOfCode.Common;
using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.IO;
using System.Text;

namespace AdventOfCode._2015
{
    public static class D_08_2
    {
        private static int _charsInEncoded = 0;
        private static int _charsInString = 0;

        public static void Execute()
        {
            var input = File.ReadAllLines(@"C:\Work\Misc Projects\AdventOfCode\AdventOfCode\AdventOfCode\2015\Data\day08_full.txt");

            Encoding ascii = Encoding.ASCII;

            for (int i = 0; i < input.Length; i++)
            {
                using (var writer = new StringWriter())
                {
                    using (var provider = CodeDomProvider.CreateProvider("CSharp"))
                    {
                        provider.GenerateCodeFromExpression(new CodePrimitiveExpression(input[i]), writer, null);
                        _charsInEncoded += writer.ToString().Length;
                    }
                }
                _charsInString += input[i].Length;
            }

            CustomConsoleColour.SetAnswerColour();
            Console.WriteLine(_charsInEncoded - _charsInString);
            Console.ResetColor();
        }
    }
}
