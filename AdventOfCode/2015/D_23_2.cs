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
    public class D_23_2
    {
        public static void Execute()
        {
            var computer = new D_23_Computer();
            List<Register> registers = new List<Register>();
            registers.Add(new Register { Name = "a", Value = 1 });

            registers = computer.Execute(registers);

            var register = registers.First(x => x.Name == "b");
            Console.Write($"Register {register.Name} has value ");
            CustomConsoleColour.SetAnswerColour();
            Console.Write(register.Value);
            Console.ResetColor();
            Console.WriteLine();
        }
    }
}