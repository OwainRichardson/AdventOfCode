using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> years = new List<int> { 2021 };
            List<int> days = new List<int> { 1, 16, 17 };

            foreach (int year in years)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{year}");
                Console.ForegroundColor = ConsoleColor.White;

                foreach (int day in days)
                {
                    Console.Write($"Day ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"{day}");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine();

                    for (int i = 1; i <= 2; i++)
                    {
                        Type executingClass = Type.GetType($"AdventOfCode._{year}.D_{day.ToTwoFigures()}_{i}");
                        MethodInfo method = executingClass.GetMethod("Execute", BindingFlags.Static | BindingFlags.Public);

                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write($"{i}: ");
                        Console.ForegroundColor = ConsoleColor.White;

                        method.Invoke(null, null);
                    }

                    Console.WriteLine();
                }

                Console.WriteLine();
            }

            Console.WriteLine();
        }
    }
}