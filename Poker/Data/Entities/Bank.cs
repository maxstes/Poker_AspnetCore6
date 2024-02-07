using Poker.Models.Game;

namespace Poker.Data.Entities
{
    public class Bank
    {
        public int Id { get; set; }
        public int Money { get; set; }

        public int IdRoom { get; set; }
        public Room? Room { get; set; }
    }
}