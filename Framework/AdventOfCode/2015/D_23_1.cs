using AdventOfCode._2015.Models;
using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2015
{
    public class D_23_1
    {

        public static void Execute()
        {
            var computer = new D_23_Computer();
            List<Register> registers = new List<Register>();
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