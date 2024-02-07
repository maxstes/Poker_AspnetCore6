using Poker.Models.Cards;

namespace Poker.Models.Game
{
    public class CombinationUser
    {
        public int Id { get; set; }
        public List<Card> Cards { get; set; }
        public CombinationUser(int id,List<Card> cards,List<Card> tableCards) 
        {
            Id = id; 
            Cards = cards;
            Cards.AddRange(tableCards);
        }
    }
}
