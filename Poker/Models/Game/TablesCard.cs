using Poker.Data.Entities;
using Poker.Models.Cards;

namespace Poker.Models.Game
{
    public class TablesCard
    {
        public List<Card> Cards { get; set; }
        public int RoomId { get; set; }
    }
}
