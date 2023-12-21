using Poker.Models.Cards;
using Poker.Models.Enums;
using Poker.Services.Interface;

namespace Poker.Services
{
    public class CombinationSelection//: ICombinationSelection
    {
        private Combination combination;
        private List<Card> cards;
        public CombinationSelection(List<Card> playerCards)
        {
            if(playerCards.Count != 7) 
            {
                throw new ArgumentException("Need be 2 cards from the player " +
                    "and 5 on table");
            }
            cards = new List<Card>();
            cards.AddRange(playerCards);
            combination = new Combination(cards);
        }
        public Task Combination(List<Card> cards)
        {
            throw new NotImplementedException();
        }

        public CombinationEnum GetBestCombination()
        {
            var name = combination.MainFunc();
            return name;
        }

        public Task Selection()
        {
            throw new NotImplementedException();
        }
        public void WriteCardsPlayers(List<Card> cards,string NamePlayer) 
        {
            foreach(var card in cards)
            {
                Console.WriteLine($"Player: {NamePlayer} Card: {card.ToString()}");
            }
        }
        

    }
}
