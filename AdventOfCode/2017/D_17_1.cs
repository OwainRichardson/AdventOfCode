using AdventOfCode._2017.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode._2017
{
    public static class D_17_1
    {
        public static void Execute()
        {
            int input = 335;
            int currentIndex = 0;
            int currentValue = 1;
            List<int> values = new List<int> { 0 };


            while (currentValue <= 2017)
            {
                int indexToInsert = ((currentIndex + input) % values.Count) + 1;

                values.Insert(indexToInsert, currentValue);
                currentIndex = indexToInsert;
                currentValue++;
            }

            int indexOfTopNumber = values.IndexOf(2017);
            Console.WriteLine(values[indexOfTopNumber + 1]);
        }
    }
}
