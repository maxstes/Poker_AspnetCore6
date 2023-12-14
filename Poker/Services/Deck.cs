using Poker.Models.Cards;
using Poker.Models.Cards.Enums;

namespace Poker.Services
{
    public class Deck
    {
        private List<Card> cards;
        public Deck()
        {
            cards = GenerateDeck();
        }
        private List<Card> GenerateDeck()
        {
            var deck = new List<Card>();
            foreach (SuitEnum suit in Enum.GetValues(typeof(SuitEnum)))
            {
                foreach (RankEnum value in Enum.GetValues(typeof(RankEnum)))
                {
                    deck.Add(new Card(suit, value));
                }
            }
            return deck;
        }
        public void Shuffle()
        {
            Random rng = new Random();
            int n = cards.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                Card value = cards[k];
                cards[k] = cards[n];
                cards[n] = value;
            }
        }
        public Card DealCard()
        {
            if (cards.Count == 0)
            {
                throw new Exception("Deck empty");
            }

            Card dealtCard = cards[cards.Count - 1];
            cards.RemoveAt(cards.Count - 1);
            return dealtCard;
        }
        public void AddCard(Card card)
        {
            cards.Add(card);
        }
        public void DisplayDeck()
        {
            foreach (Card card in cards)
            {
                Console.WriteLine(card);
            }
        }
    }
}
