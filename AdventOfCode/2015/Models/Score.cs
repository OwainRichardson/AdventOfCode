using System.Collections.Generic;

namespace AdventOfCode._2015.Models
{
    public class Score
    {
        public List<IngredientScore> Ingredients { get; set; } = new List<IngredientScore>();
        public int TotalScore { get; set; }
    }
}
