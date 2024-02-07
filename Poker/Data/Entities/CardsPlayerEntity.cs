using Poker.Models.Game;

namespace Poker.Data.Entities
{
    public class CardsPlayerEntity
    {
        public int Id { get; set; }
        public List<CardEntity>? Cards { get; set; }
        public PlayerOnline? Player { get; set; }
    }
}
