using AdventOfCode._2021.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode._2021
{
    public static class D_16_2
    {
        private static string _binary = "";
        private static int _versionSum = 0;

        public static void Execute()
        {
            string input = File.ReadAllLines(@"2021\Data\day16.txt").Single();

            _binary = ConvertToBinary(input);

            long finalBinary = ParseBinary();

            Console.WriteLine(finalBinary);
        }

        private static long ParseBinary()
        {
            int packetVersion = Convert.ToInt32(_binary.Substring(0, 3), 2);
            _binary = _binary.Substring(3);
            _versionSum += packetVersion;

            int typeId = Convert.ToInt32(_binary.Substring(0, 3), 2);
            _binary = _binary.Substring(3);
            StringBuilder finalBinary = new StringBuilder();

            switch (typeId)
            {
                case 4:
                    finalBinary.Append(HandleLiteralBinary());
                    break;
                default:
                    finalBinary.Append(HandleOperatorBinary(typeId));
                    break;
            }

            return long.Parse(finalBinary.ToString());
        }

        private static long HandleOperatorBinary(int typeId)
        {
            List<long> numbers = new List<long>();

            switch (_binary.Substring(0, 1))
            {
                case "1":
                    int numberOfPackets = Convert.ToInt32(_binary.Substring(1, 11), 2);
                    _binary = _binary.Substring(12);
                    numbers.AddRange(HandleSubPacketsByCount(numberOfPackets));
                    break;
                case "0":
                    int length = Convert.ToInt32(_binary.Substring(1, 15), 2);
                    _binary = _binary.Substring(16);
                    numbers.AddRange(HandleSubPacketsByLength(length));
                    break;
                default:
                    throw new ArgumentException();
            }

            if (typeId == 0)
            {
                return numbers.Sum();
            }
            else if (typeId == 1)
            {
                return numbers.Product();
            }
            else if (typeId == 2)
            {
                return numbers.Min();
            }
            else if (typeId == 3)
            {
                return numbers.Max();
            }
            else if (typeId == 5)
            {
                return numbers[0] > numbers[1] ? 1 : 0;
            }
            else if (typeId == 6)
            {
                return numbers[0] < numbers[1] ? 1 : 0;
            }
            else if (typeId == 7)
            {
                return numbers[0] == numbers[1] ? 1 : 0;
            }

            return -1;
        }

        private static List<long> HandleSubPacketsByCount(int numberOfPackets)
        {
            List<long> numbers = new List<long>();

            for (int packets = 1; packets <= numberOfPackets; packets++)
            {
                numbers.Add(ParseBinary());
            }

            return numbers;
        }

        private static List<long> HandleSubPacketsByLength(int length)
        {
            List<long> numbers = new List<long>();

            while (length > 0)
            {
                int oldLength = _binary.Length;

                numbers.Add(ParseBinary());

                length = length - (oldLength - _binary.Length);
            }

            return numbers;
        }

        private static long HandleLiteralBinary()
        {
            StringBuilder sb = new StringBuilder();

            bool keepCycling = true;
            while (keepCycling)
            {
                sb.Append(_binary.Substring(1, 4));

                string firstDigit = _binary.Substring(0, 1);
                if (firstDigit == "0") keepCycling = false;

                _binary = _binary.Substring(5);
            }

            return Convert.ToInt64(sb.ToString(), 2);
        }

        private static string ConvertToBinary(string input)
        {
            StringBuilder sb = new StringBuilder();

            foreach (char c in input)
            {
                string binary = Convert.ToString(Convert.ToInt64(c.ToString(), 16), 2);

                while (binary.Length < 4)
                {
                    binary = $"0{binary}";
                }
                   
                sb.Append(binary);
            }

            return sb.ToString();
        }
    }
}