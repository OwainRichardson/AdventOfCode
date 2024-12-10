using AdventOfCode._2023.Models;

namespace AdventOfCode._2023
{
    public static class D_05_1
    {
        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2023\Data\day05.txt").ToArray();

            Almanac almanac = ParseInputsToAlmanac(inputs);

            long lowestLocation = long.MaxValue;

            foreach (long seed in almanac.Seeds)
            {
                // Calculate soil
                long soil = CalculateMap(seed, almanac.SeedToSoilMap);
                long fertilizer = CalculateMap(soil, almanac.SoilToFertilizerMap);
                long water = CalculateMap(fertilizer, almanac.FertilizerToWaterMap);
                long light = CalculateMap(water, almanac.WaterToLightMap);
                long temparature = CalculateMap(light, almanac.LightToTemperatureMap);
                long humidity = CalculateMap(temparature, almanac.TemperatureToHumidityMap);
                long location = CalculateMap(humidity, almanac.HumidityToLocationMap);

                if (location < lowestLocation)
                {
                    lowestLocation = location;
                }
            }

            Console.WriteLine(lowestLocation);
        }

        private static long CalculateMap(long initialValue, List<AlmanacMap> almanacMap)
        {
            if (almanacMap.Any(m => m.SourceRangeStart <= initialValue && (m.SourceRangeStart + m.RangeLength) >= initialValue))
            {
                AlmanacMap map = almanacMap.First(m => m.SourceRangeStart <= initialValue && (m.SourceRangeStart + m.RangeLength) >= initialValue);
                return initialValue + (map.DestinationRangeStart - map.SourceRangeStart);
            }
            else
            {
                return initialValue;
            }
        }

        private static Almanac ParseInputsToAlmanac(string[] inputs)
        {
            Almanac almanac = new Almanac();

            string seedsLine = inputs[0].Replace("seeds: ", string.Empty);
            almanac.Seeds = seedsLine.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(sl => long.Parse(sl)).ToList();
            almanac.SeedToSoilMap = new List<AlmanacMap>();
            almanac.SoilToFertilizerMap = new List<AlmanacMap>();
            almanac.FertilizerToWaterMap = new List<AlmanacMap>();
            almanac.WaterToLightMap = new List<AlmanacMap>();
            almanac.LightToTemperatureMap = new List<AlmanacMap>();
            almanac.TemperatureToHumidityMap = new List<AlmanacMap>();
            almanac.HumidityToLocationMap = new List<AlmanacMap>();

            int currentIndex = 3;

            for (int index = currentIndex; index < inputs.Count(); index++)
            {
                if (string.IsNullOrWhiteSpace(inputs[index]))
                {
                    currentIndex = index;
                    break;
                }

                long[] rangeDetails = inputs[index].Split(' ').Select(x => long.Parse(x)).ToArray();
                almanac.SeedToSoilMap.Add(new AlmanacMap
                {
                    DestinationRangeStart = rangeDetails[0],
                    SourceRangeStart = rangeDetails[1],
                    RangeLength = rangeDetails[2]
                });
            }

            for (int index = currentIndex + 2; index < inputs.Count(); index++)
            {
                if (string.IsNullOrWhiteSpace(inputs[index]))
                {
                    currentIndex = index;
                    break;
                }

                long[] rangeDetails = inputs[index].Split(' ').Select(x => long.Parse(x)).ToArray();
                almanac.SoilToFertilizerMap.Add(new AlmanacMap
                {
                    DestinationRangeStart = rangeDetails[0],
                    SourceRangeStart = rangeDetails[1],
                    RangeLength = rangeDetails[2]
                });
            }

            for (int index = currentIndex + 2; index < inputs.Count(); index++)
            {
                if (string.IsNullOrWhiteSpace(inputs[index]))
                {
                    currentIndex = index;
                    break;
                }

                long[] rangeDetails = inputs[index].Split(' ').Select(x => long.Parse(x)).ToArray();
                almanac.FertilizerToWaterMap.Add(new AlmanacMap
                {
                    DestinationRangeStart = rangeDetails[0],
                    SourceRangeStart = rangeDetails[1],
                    RangeLength = rangeDetails[2]
                });
            }

            for (int index = currentIndex + 2; index < inputs.Count(); index++)
            {
                if (string.IsNullOrWhiteSpace(inputs[index]))
                {
                    currentIndex = index;
                    break;
                }

                long[] rangeDetails = inputs[index].Split(' ').Select(x => long.Parse(x)).ToArray();
                almanac.WaterToLightMap.Add(new AlmanacMap
                {
                    DestinationRangeStart = rangeDetails[0],
                    SourceRangeStart = rangeDetails[1],
                    RangeLength = rangeDetails[2]
                });
            }

            for (int index = currentIndex + 2; index < inputs.Count(); index++)
            {
                if (string.IsNullOrWhiteSpace(inputs[index]))
                {
                    currentIndex = index;
                    break;
                }

                long[] rangeDetails = inputs[index].Split(' ').Select(x => long.Parse(x)).ToArray();
                almanac.LightToTemperatureMap.Add(new AlmanacMap
                {
                    DestinationRangeStart = rangeDetails[0],
                    SourceRangeStart = rangeDetails[1],
                    RangeLength = rangeDetails[2]
                });
            }

            for (int index = currentIndex + 2; index < inputs.Count(); index++)
            {
                if (string.IsNullOrWhiteSpace(inputs[index]))
                {
                    currentIndex = index;
                    break;
                }

                long[] rangeDetails = inputs[index].Split(' ').Select(x => long.Parse(x)).ToArray();
                almanac.TemperatureToHumidityMap.Add(new AlmanacMap
                {
                    DestinationRangeStart = rangeDetails[0],
                    SourceRangeStart = rangeDetails[1],
                    RangeLength = rangeDetails[2]
                });
            }

            for (int index = currentIndex + 2; index < inputs.Count(); index++)
            {
                if (string.IsNullOrWhiteSpace(inputs[index]))
                {
                    currentIndex = index;
                    break;
                }

                long[] rangeDetails = inputs[index].Split(' ').Select(x => long.Parse(x)).ToArray();
                almanac.HumidityToLocationMap.Add(new AlmanacMap
                {
                    DestinationRangeStart = rangeDetails[0],
                    SourceRangeStart = rangeDetails[1],
                    RangeLength = rangeDetails[2]
                });
            }

            return almanac;
        }
    }
}