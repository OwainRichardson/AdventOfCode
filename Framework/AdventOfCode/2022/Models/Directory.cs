using System.Collections.Generic;

namespace AdventOfCode._2022.Models
{
    public class Directory
    {
        public List<DirectoryFile> Files { get; set; } = new List<DirectoryFile>();
        public string Name { get; set; }
        public int ParentId { get; set; }
        public long Size { get; set; }
        public int Id { get; set; }
    }
}
