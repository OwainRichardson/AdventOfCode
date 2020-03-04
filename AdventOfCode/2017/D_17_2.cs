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
    public static class D_17_2
    {
        public static void Execute()
        {
            int input = 335;
            int currentValue = 1;
            LinkedList<int> values = new LinkedList<int>();
            values.AddFirst(0);

            var node = values.First;

            while (currentValue <= 50000000)
            {
                for (int i = 1; i <= input; i++)
                {
                    if (node.Next == null)
                    {
                        node = values.First;
                    }
                    else
                    {
                        node = node.Next;
                    }
                }

                values.AddAfter(node, currentValue);
                node = node.Next;
                currentValue++;

                if (currentValue % 1000000 == 0)
                {
                    Console.Write($"\r{currentValue}");
                }
            }

            node = values.First;

            Console.Write($"\r{node.Next.Value}          ");
            Console.WriteLine();
        }
    }
}
