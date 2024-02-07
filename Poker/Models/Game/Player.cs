using Poker.Data.Entities;

namespace Poker.Models.Game
{
    public class Player
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Level { get; set; }
        public int Experience { get; set; }
        public string? Rank { get; set; }
        public int? Balance { get; set; }
    }
}
