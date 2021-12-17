using System;

namespace AdventOfCode._2017
{
    public static class D_23_2
    {
        public static void Execute()
        {
            long a = 1;
            long b = 0;
            long c = 0;
            long d = 0;
            long e = 0;
            long f = 0;
            long g = 0;
            long h = 0;
            bool f0 = false;
            b = 65;
            c = b;

            if (a != 0)
            {
                b *= 100;
                b += 100000;
                c = b;
                c += 17000;

                while (true)
                {
                    f = 1;
                    f0 = false;
                    d = 2;
                    e = 2;
                    g = d;

                    while (g != 0)
                    {
                        for (d = 2; d <= Math.Ceiling(b / (double)2); d++)
                        {
                            if (b % d != 0)
                            {
                                continue;
                            }

                            for (e = 2; e <= Math.Ceiling(b / (double)2); e++)
                            {
                                if (d * e == b)
                                {
                                    f = 0;
                                    f0 = true;
                                    break;
                                }
                            }

                            if (f0)
                            {
                                break;
                            }
                        }

                        g = 0;
                    }

                    if (f == 0)
                    {
                        h += 1;
                    }
                    else if (f == 1)
                    {
                        Console.WriteLine($"f == 1");
                    }

                    g = b - c;

                    if (g == 0)
                    {
                        Console.WriteLine(h);
                        return;
                    }

                    b += 17;
                }
            }
        }

        private static void PrintRegisters(int a, int b, int c, int d, int e, int f, int g, int h)
        {
            Console.WriteLine($"a = {a}");
            Console.WriteLine($"b = {b}");
            Console.WriteLine($"c = {c}");
            Console.WriteLine($"d = {d}");
            Console.WriteLine($"e = {e}");
            Console.WriteLine($"f = {f}");
            Console.WriteLine($"g = {g}");
            Console.WriteLine($"h = {h}");
        }
    }
}