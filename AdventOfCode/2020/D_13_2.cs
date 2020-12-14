﻿using AdventOfCode._2020.Models;
using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode._2020
{
    public static class D_13_2
    {
        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2020\Data\day13.txt");

            List<string> busServicesBeforeParse = inputs[1].Split(',').ToList();

            Dictionary<int, int> timesAfterFirstBus = new Dictionary<int, int>();

            for (int index = 0; index < busServicesBeforeParse.Count; index++)
            {
                if (busServicesBeforeParse[index] == "x") continue;

                timesAfterFirstBus.Add(int.Parse(busServicesBeforeParse[index]), index);
            }

            List<Congruence> congruences = new List<Congruence>();
            foreach (var input in timesAfterFirstBus)
            {
                congruences.Add(new Congruence { M = input.Key, A = input.Value == 0 ? 0 : input.Key - input.Value });
            }

            long totalM = congruences.Mult();

            long a = 0;
            foreach (var congruence in congruences)
            {
                long ai = congruence.A;
                long ni = CalculateN(congruence.M, totalM);
                long ui = CalculateU(ni, congruence.M);

                a += (ai * ni * ui);
            }

            Congruence totalCongruence = new Congruence { A = a % totalM, M = congruences.Mult() };

            Console.WriteLine(totalCongruence.A);
        }

        private static long CalculateU(long ni, long m)
        {
            long u = 0;
            long congruence = 0;

            while (congruence != 1)
            {
                u += 1;

                congruence = (ni * u) % m;
            }

            return u;
        }

        private static long CalculateN(long m, long totalM)
        {
            return totalM / m;
        }

        public static long CalculateLcmForArray(long[] element_array)
        {
            long lcm_of_array_elements = 1;
            int divisor = 2;

            while (true)
            {

                int counter = 0;
                bool divisible = false;
                for (int i = 0; i < element_array.Length; i++)
                {
                    // lcm_of_array_elements (n1, n2, ... 0) = 0. 
                    // For negative number we convert into 
                    // positive and calculate lcm_of_array_elements. 
                    if (element_array[i] == 0)
                    {
                        return 0;
                    }
                    else if (element_array[i] < 0)
                    {
                        element_array[i] = element_array[i] * (-1);
                    }
                    if (element_array[i] == 1)
                    {
                        counter++;
                    }

                    // Divide element_array by devisor if complete 
                    // division i.e. without remainder then replace 
                    // number with quotient; used for find next factor 
                    if (element_array[i] % divisor == 0)
                    {
                        divisible = true;
                        element_array[i] = element_array[i] / divisor;
                    }
                }

                // If divisor able to completely divide any number 
                // from array multiply with lcm_of_array_elements 
                // and store into lcm_of_array_elements and continue 
                // to same divisor for next factor finding. 
                // else increment divisor 
                if (divisible)
                {
                    lcm_of_array_elements = lcm_of_array_elements * divisor;
                }
                else
                {
                    divisor++;
                }

                // Check if all element_array is 1 indicate  
                // we found all factors and terminate while loop. 
                if (counter == element_array.Length)
                {
                    return lcm_of_array_elements;
                }
            }
        }
    }
}
