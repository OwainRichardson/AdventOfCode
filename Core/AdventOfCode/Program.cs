// See https://aka.ms/new-console-template for more information

using AdventOfCode.Common;
using System.Diagnostics;
using System.Reflection;

List<int> years = new List<int> { 2024 };
List<int> days = new List<int> { 11 };

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

            if (executingClass == null) continue;

            try
            {
                MethodInfo method = executingClass.GetMethod("Execute", BindingFlags.Static | BindingFlags.Public);

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"{i}: ");
                Console.ForegroundColor = ConsoleColor.White;

                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                object value = method.Invoke(null, null);
                Console.Write((string)value);
                stopwatch.Stop();

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write($" in {stopwatch.Elapsed.TotalMilliseconds}ms");
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.White;
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