using AdventOfCode._2017.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode._2017
{
    public static class D_21_1
    {
        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"C:\Work\Misc Projects\AdventOfCode\AdventOfCode\AdventOfCode\2017\Data\day21_full.txt");

            List<Rule> rules = ParseInputs(inputs);

            string input = "#..#/..../..../#..#";

            string[,] picture = PopulatePicture(input);

            PrintInput(picture);

            for (int iteration = 1; iteration <= 5; iteration++)
            {
                List<string[,]> pictures = new List<string[,]>();

                if (SizeIsDivisible(picture, 2))
                {
                    pictures = SplitInToTwos(picture);
                }
                else if (SizeIsDivisible(picture, 3))
                {
                    pictures = SplitInToThrees(picture);
                }
                else
                {
                    throw new ArithmeticException();
                }

                List<string[,]> translatedPictures = new List<string[,]>();
                foreach (var picturesToTranslate in pictures)
                {
                    translatedPictures.Add(TranslatePictures(rules, picturesToTranslate));
                }

                picture = PutPictureBackTogether(translatedPictures);

                PrintInput(picture);
            }
        }

        private static string[,] TranslatePictures(List<Rule> rules, string[,] picture, int numberOfRotations = 0)
        {
            if (numberOfRotations == 4)
            {
                throw new ArgumentNullException();
            }

            string joinedPicture = JoinPicture(picture);

            if (rules.Any(x => x.Match == joinedPicture))
            {
                var rule = rules.First(x => x.Match == joinedPicture).Replace;

                return TranslateRuleToArray(rule);
            }

            // mirror
            picture = ReversePicture(picture);
            if (rules.Any(x => x.Match == joinedPicture))
            {
                var rule = rules.First(x => x.Match == joinedPicture).Replace;

                return TranslateRuleToArray(rule);
            }

            picture = ReversePicture(picture);

            // rotate
            picture = RotatePicture(picture);

            return TranslatePictures(rules, picture, numberOfRotations + 1);
        }

        private static string[,] RotatePicture(string[,] picture)
        {
            if (Math.Sqrt(picture.Length) % 2 == 0)
            {
                string[,] temp = new string[2, 2];
                temp[0, 1] = picture[0, 0];
                temp[1, 1] = picture[0, 1];
                temp[0, 0] = picture[1, 0];
                temp[1, 0] = picture[1, 1];

                return temp;
            }
            else if (Math.Sqrt(picture.Length) % 3 == 0)
            {
                string[,] temp = new string[3, 3];
                temp[0, 2] = picture[0, 0];
                temp[1, 2] = picture[0, 1];
                temp[2, 2] = picture[0, 2];
                temp[0, 1] = picture[1, 0];
                temp[2, 1] = picture[1, 2];
                temp[0, 0] = picture[2, 0];
                temp[1, 0] = picture[2, 1];
                temp[2, 0] = picture[2, 2];
                temp[1, 1] = picture[1, 1];

                PrintInput(temp);

                return temp;
            }

            throw new ArithmeticException();
        }

        private static string[,] ReversePicture(string[,] picture)
        {
            int length = (int)Math.Sqrt(picture.Length);
            string[,] result = new string[length, length];

            for (int y = 0; y < length; y++)
            {
                int originalX = 0;
                for (int x = length - 1; x >= 0; x--)
                {
                    result[y, x] = picture[y, originalX];

                    originalX++;
                }
            }

            return result;
        }

        private static string JoinPicture(string[,] picture)
        {
            string joined = string.Empty;

            for (int y = 0; y < Math.Sqrt(picture.Length); y++)
            {
                for (int x = 0; x < Math.Sqrt(picture.Length); x++)
                {
                    joined = $"{joined}{picture[y, x]}";
                }

                joined = $"{joined}/";
            }

            return joined.Substring(0, joined.Length - 1);
        }

        private static string[,] TranslateRuleToArray(string rule)
        {
            int length = rule.IndexOf("/");

            string[,] picture = new string[length, length];
            int index = 0;
            rule = rule.Replace("/", "");

            for (int y = 0; y < Math.Sqrt(picture.Length); y++)
            {
                for (int x = 0; x < Math.Sqrt(picture.Length); x++)
                {
                    picture[y, x] = rule[index].ToString();
                    index++;
                }
            }

            return picture;
        }

        private static string[,] PutPictureBackTogether(List<string[,]> pictures)
        {
            int numberOfPicturesRoot = (int)Math.Sqrt(pictures.Count);
            int pictureWidth = (int)Math.Sqrt(pictures.First().Length);

            string[,] temp = new string[numberOfPicturesRoot * pictureWidth, numberOfPicturesRoot * pictureWidth];

            for (int iteration = 0; iteration < (numberOfPicturesRoot * pictureWidth); iteration++)
            {
                int take = numberOfPicturesRoot;
                int skip = (int)Math.Floor(iteration / (double)pictureWidth) * numberOfPicturesRoot;

                List<string[,]> picturesToAdd = pictures.Skip(skip).Take(take).ToList();

                int colIndex = 0;
                foreach (var pic in picturesToAdd)
                {
                    for (int i = 0; i < pictureWidth; i++)
                    {
                        temp[iteration, colIndex] = pic[iteration % pictureWidth, i];
                        colIndex++;
                    }
                }
            }

            return temp;
        }

        private static List<string[,]> SplitInToTwos(string[,] picture)
        {
            List<string[,]> pictures = new List<string[,]>();

            for (int colSkip = 0; colSkip < (int)Math.Sqrt(picture.Length) / 2; colSkip++)
            {
                for (int rowSkip = 0; rowSkip < (int)Math.Sqrt(picture.Length) / 2; rowSkip++)
                {
                    string[,] splitPicture = new string[2, 2];

                    for (int y = 0 + (colSkip * 2); y < 2 + (colSkip * 2); y++)
                    {
                        for (int x = 0 + (rowSkip * 2); x < 2 + (rowSkip * 2); x++)
                        {
                            splitPicture[y - (colSkip * 2), x - (rowSkip * 2)] = picture[y, x];
                        }
                    }

                    pictures.Add(splitPicture);
                }
            }

            return pictures;
        }

        private static List<string[,]> SplitInToThrees(string[,] picture)
        {
            List<string[,]> pictures = new List<string[,]>();

            for (int colSkip = 0; colSkip < (int)Math.Sqrt(picture.Length) / 3; colSkip++)
            {
                for (int rowSkip = 0; rowSkip < (int)Math.Sqrt(picture.Length) / 3; rowSkip++)
                {
                    string[,] splitPicture = new string[3, 3];

                    for (int y = 0 + (colSkip * 3); y < 3 + (colSkip * 3); y++)
                    {
                        for (int x = 0 + (rowSkip * 3); x < 3 + (rowSkip * 3); x++)
                        {
                            splitPicture[y - (colSkip * 3), x - (rowSkip * 3)] = picture[y, x];
                        }
                    }

                    pictures.Add(splitPicture);
                }
            }

            return pictures;
        }

        private static bool SizeIsDivisible(string[,] picture, int divisor)
        {
            int length = (int)Math.Sqrt(picture.Length);

            return length % divisor == 0;
        }

        private static string[,] PopulatePicture(string input)
        {
            input = input.Replace("/", "");
            int length = (int)Math.Sqrt(input.Replace("/", "").Length);
            string[,] picture = new string[length, length];
            int index = 0;

            for (int y = 0; y < length; y++)
            {
                for (int x = 0; x < length; x++)
                {
                    picture[y, x] = input[index].ToString();

                    index++;
                }
            }

            return picture;
        }

        private static void PrintInput(string[,] picture)
        {
            int length = (int)Math.Sqrt(picture.Length);
            for (int y = 0; y < length; y++)
            {
                for (int x = 0; x < length; x++)
                {
                    Console.Write(picture[y, x]);
                }

                Console.WriteLine();
            }

            Console.WriteLine();
            Console.WriteLine();
        }

        private static List<Rule> ParseInputs(string[] inputs)
        {
            List<Rule> rules = new List<Rule>();

            foreach (string input in inputs)
            {
                var split = input.Split(new string[] { " => " }, StringSplitOptions.RemoveEmptyEntries);

                Rule rule = new Rule
                {
                    Match = split[0],
                    Replace = split[1]
                };

                rules.Add(rule);
            }

            return rules;
        }
    }
}
