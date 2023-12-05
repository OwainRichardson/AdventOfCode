namespace AdventOfCode._2023.Models
{
    public class Almanac
    {
        public List<long> Seeds { get; set; }
        public List<SeedMap> SeedMaps { get; set; }
        public List<AlmanacMap> SeedToSoilMap { get; set; }
        public List<AlmanacMap> SoilToFertilizerMap { get; set; }
        public List<AlmanacMap> FertilizerToWaterMap { get; set; }
        public List<AlmanacMap> WaterToLightMap { get; set; }
        public List<AlmanacMap> LightToTemperatureMap { get; set; }
        public List<AlmanacMap> TemperatureToHumidityMap { get; set; }
        public List<AlmanacMap> HumidityToLocationMap { get; set; }
    }
}
