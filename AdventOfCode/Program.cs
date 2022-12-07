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
            List<int> years = new List<int> { 2022 };
            List<int> days = new List<int> { 7 };

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

                        try
                        {
                            MethodInfo method = executingClass.GetMethod("Execute", BindingFlags.Static | BindingFlags.Public);

                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write($"{i}: ");
                            Console.ForegroundColor = ConsoleColor.White;

                            method.Invoke(null, null);
                        }
                        catch
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write($"{i}: ");
                            Console.ForegroundColor = ConsoleColor.White;

                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write($"NOT CREATED");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                    }

                    Console.WriteLine();
                }

                Console.WriteLine();
            }

            Console.WriteLine();
        }
    }
}