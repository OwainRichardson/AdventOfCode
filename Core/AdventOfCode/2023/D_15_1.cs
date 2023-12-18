using AdventOfCode._2023.Models;
using AdventOfCode._2023.Models.Enums;
using System.Data.Common;
using System.Runtime;
using System.Runtime.ExceptionServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Schema;

namespace AdventOfCode._2023
{
    public static class D_15_1
    {
        public static void Execute()
        {
            string inputs = File.ReadAllLines(@"2023\Data\day15.txt")[0];

            List<string> splitInputs = inputs.Split(',').ToList();

            long total = 0;

            foreach (string split in splitInputs)
            {
                int subtotal = 0;
                byte[] asciiBytes = Encoding.ASCII.GetBytes(split);

                foreach (var b in asciiBytes)
                {
                    subtotal += b;
                    subtotal *= 17;
                    subtotal = subtotal % 256;
                }

                total += subtotal;
            }

            Console.WriteLine(total);
        }
    }
}