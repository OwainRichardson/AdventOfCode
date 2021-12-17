using System;

namespace AdventOfCode._2017
{
    public static class D_15_2
    {
        public static void Execute()
        {
            long generatorA = 722;
            long generatorB = 354;
            int total = 0;
            int loops = 5000000;

            for (int loop = 1; loop <= loops; loop++)
            {
                Console.Write($"\r{Math.Round(((double)loop / loops) * 100, 0)}%");

                if (StepGenerators(ref generatorA, ref generatorB))
                {
                    total++;
                }
            }

            Console.Write($"\r{total}        ");
            Console.WriteLine();
        }

        private static bool StepGenerators(ref long generatorA, ref long generatorB)
        {
            int generatorAFactor = 16807;
            int generatorBFactor = 48271;

            generatorA = StepGenerator(generatorA, generatorAFactor);

            while (generatorA % 4 != 0)
            {
                generatorA = StepGenerator(generatorA, generatorAFactor);
            }
            generatorB = StepGenerator(generatorB, generatorBFactor);

            while (generatorB % 8 != 0)
            {
                generatorB = StepGenerator(generatorB, generatorBFactor);
            }

            string genABinary = Convert.ToString(generatorA, 2);
            string genBBinary = Convert.ToString(generatorB, 2);

            while (genABinary.Length < 16)
            {
                genABinary = $"0{genABinary}";
            }
            while (genBBinary.Length < 16)
            {
                genBBinary = $"0{genBBinary}";
            }

            string last16A = genABinary.Substring(genABinary.Length - 16);
            string last16B = genBBinary.Substring(genBBinary.Length - 16);

            if (last16A == last16B)
            {
                return true;
            }

            return false;
        }

        private static long StepGenerator(long generator, int generatorFactor)
        {
            return (generator * generatorFactor) % 2147483647;
        }
    }
}
