using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode._2021
{
    public static class D_08_2
    {
        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2021\Data\day08.txt");
            int runningTotal = 0;

            foreach (string input in inputs)
            {
                string[] inputSplit = input.Split('|');
                string allNumbers = inputSplit[0];
                string codedNumbers = inputSplit[1];

                string[] splitOfAllNumbers = allNumbers.Trim().Split(' ');
                List<string> numbersList = new List<string>();

                foreach (string number in splitOfAllNumbers)
                {
                    numbersList.Add(string.Concat(number.OrderBy(c => c)).ToString());
                }

                string[] allNumbersSplit = numbersList.ToArray();

                string topCharacter = CalculateTopNumber(allNumbersSplit);
                (string topLeftCharacter, string middleCharacter) = CalculateTopLeftAndMiddleCharacters(allNumbersSplit);

                string zero = GetZero(allNumbersSplit, middleCharacter);
                string one = allNumbersSplit.Single(x => x.Length == 2);
                string four = allNumbersSplit.Single(x => x.Length == 4);
                string five = GetFive(allNumbersSplit, topCharacter, topLeftCharacter, middleCharacter);
                string seven = allNumbersSplit.Single(x => x.Length == 3);
                string eight = allNumbersSplit.Single(x => x.Length == 7);
                (string two, string three) = GetTwoAndThree(allNumbersSplit, five, one);
                (string six, string nine) = GetSixAndNine(allNumbersSplit, zero, two, three);

                string codedNumber = string.Empty;
                string[] numbers = codedNumbers.Trim().Split(' ').Select(x => string.Concat(x.OrderBy(c => c))).ToArray();

                foreach (string number in numbers)
                {
                    string match = string.Empty;
                    if (number == zero)
                    {
                        codedNumber += "0";
                    }
                    else if (number == one)
                    {
                        codedNumber += "1";
                    }
                    else if (number == two)
                    {
                        codedNumber += "2";
                    }
                    else if (number == three)
                    {
                        codedNumber += "3";
                    }
                    else if (number == four)
                    {
                        codedNumber += "4";
                    }
                    else if (number == five)
                    {
                        codedNumber += "5";
                    }
                    else if (number == six)
                    {
                        codedNumber += "6";
                    }
                    else if (number == seven)
                    {
                        codedNumber += "7";
                    }
                    else if (number == eight)
                    {
                        codedNumber += "8";
                    }
                    else if (number == nine)
                    {
                        codedNumber += "9";
                    }
                }

                runningTotal += int.Parse(codedNumber);
            }

            Console.WriteLine(runningTotal);
        }

        private static (string six, string nine) GetSixAndNine(string[] allNumbersSplit, string zero, string two, string three)
        {
            List<string> sixDigits = allNumbersSplit.Where(x => x.Length == 6).Select(x => string.Concat(x.OrderBy(c => c))).ToList();
            List<string> sixAndNine = sixDigits.Where(x => x != string.Concat(zero.OrderBy(c => c))).ToList();

            string bottomLeftDigit = two.Except(three).Single().ToString();

            string six = sixAndNine.Single(x => x.Contains(bottomLeftDigit));
            string nine = sixAndNine.Single(x => x != six);

            return (six, nine);
        }

        private static (string, string) GetTwoAndThree(string[] allNumbersSplit, string five, string one)
        {
            List<string> fiveDigits = allNumbersSplit.Where(x => x.Length == 5).Select(x => string.Concat(x.OrderBy(c => c))).ToList();

            fiveDigits = fiveDigits.Where(x => x != string.Concat(five.OrderBy(c => c))).ToList();

            string three = fiveDigits.Where(x => x.Contains(one[0].ToString()) && x.Contains(one[1].ToString())).Single().ToString();
            string two = fiveDigits.Single(x => x != three);

            return (two, three);
        }

        private static string GetZero(string[] allNumbersSplit, string middleCharacter)
        {
            return allNumbersSplit.Where(x => x.Length == 6 && !x.Contains(middleCharacter)).Single();
        }

        private static string GetFive(string[] allNumbersSplit, string topCharacter, string topLeftCharacter, string middleCharacter)
        {
            return allNumbersSplit.Where(x => x.Length == 5 && x.Contains(topCharacter) && x.Contains(topLeftCharacter) && x.Contains(middleCharacter)).Single();
        }

        private static (string, string) CalculateTopLeftAndMiddleCharacters(string[] allNumbersSplit)
        {
            string fourDigits = allNumbersSplit.Single(x => x.Length == 4);
            List<string> fiveDigits = allNumbersSplit.Where(x => x.Length == 5).ToList();

            string middleDigit = fourDigits;

            foreach (string fiveDigit in fiveDigits)
            {
                List<char> intersect = middleDigit.Intersect(fiveDigit).ToList();

                middleDigit = String.Join("", intersect);
            }

            string twoDigit = allNumbersSplit.Single(x => x.Length == 2);

            string topLeftDigit = fourDigits.Where(x => !twoDigit.Contains(x) && x.ToString() != middleDigit).Single().ToString();

            return (topLeftDigit, middleDigit);
        }

        private static string CalculateTopNumber(string[] allNumbersSplit)
        {
            string threeDigit = allNumbersSplit.Single(x => x.Length == 3);
            string twoDigit = allNumbersSplit.Single(x => x.Length == 2);

            return threeDigit.Where(x => !twoDigit.Contains(x)).Single().ToString();
        }
    }
}
