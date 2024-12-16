using AdventOfCode._2024.Models.Enums;

namespace AdventOfCode._2024.Models
{
    public class WarehouseCoord : Coord
    {
        public bool IsWall { get; set; }
        public bool IsPackage { get; set; }
        public bool IsRobot { get; set; }
        public string Value { get; set; }
        public bool IsEmpty { get; set; }
        public PackageSide Side { get; set; }
    }
}
