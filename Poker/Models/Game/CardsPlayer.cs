using Poker.Data.Entities;
using Poker.Models.Cards;

namespace Poker.Models.Game
{
    public class CardsPlayer
    {
        
        public List<Card>?  Cards{ get; set; }
        public PlayerOnline? Player { get; set; }

    }
}
