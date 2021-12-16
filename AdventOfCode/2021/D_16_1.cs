using AdventOfCode._2021.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode._2021
{
    public static class D_16_1
    {
        private static string _binary = "";
        private static int _versionSum = 0;

        public static void Execute()
        {
            string input = File.ReadAllLines(@"2021\Data\day16.txt").Single();

            _binary = ConvertToBinary(input);

            string finalBinary = ParseBinary();

            Console.WriteLine(_versionSum);
        }

        private static string ParseBinary()
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
                    finalBinary.Append(HandleOperatorBinary());
                    break;
            }

            return finalBinary.ToString();
        }

        private static string HandleOperatorBinary()
        {
            StringBuilder finalBinary = new StringBuilder();

            switch (_binary.Substring(0, 1))
            {
                case "1":
                    int numberOfPackets = Convert.ToInt32(_binary.Substring(1, 11), 2);
                    _binary = _binary.Substring(12);
                    finalBinary.Append(HandleSubPacketsByCount(numberOfPackets));
                    break;
                case "0":
                    int length = Convert.ToInt32(_binary.Substring(1, 15), 2);
                    _binary = _binary.Substring(16);
                    finalBinary.Append(HandleSubPacketsByLength(length));
                    break;
                default:
                    throw new ArgumentException();
            }

            return finalBinary.ToString();
        }

        private static string HandleSubPacketsByCount(int numberOfPackets)
        {
            StringBuilder finalBinary = new StringBuilder();

            for (int packets = 1; packets <= numberOfPackets; packets++)
            {
                finalBinary.Append(ParseBinary());
            }

            return finalBinary.ToString();
        }

        private static string HandleSubPacketsByLength(int length)
        {
            StringBuilder finalBinary = new StringBuilder();

            while (length > 0)
            {
                int oldLength = _binary.Length;

                finalBinary.Append(ParseBinary());

                length = length - (oldLength - _binary.Length);
            }

            return finalBinary.ToString();
        }

        private static string HandleLiteralBinary()
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

            return sb.ToString();
        }

        private static string ConvertToBinary(string input)
        {
            StringBuilder sb = new StringBuilder();

            foreach (char c in input)
            {
                string binary = Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2);

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