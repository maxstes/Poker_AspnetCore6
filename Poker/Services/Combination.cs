using Poker.Models.Cards;
using Poker.Models.Cards.Enums;
using Poker.Models.Enums;

namespace Poker.Services
{
    public class Combination
    {
        private List<Card> cards;
        public Combination(List<Card> _cards)
        {
            cards = _cards;
        }

        public CombinationEnum MainFunc()
        {
            if (RoyalStraight()) { return CombinationEnum.RoyalFlush; }
            if (HasStraightFlush()) { return CombinationEnum.StraightFlush; }
            if (FourOfAKind()) { return CombinationEnum.FourOfAKind; }
            if (FullHouse()) { return CombinationEnum.FullHouse; }
            if (HasFlush()) { return CombinationEnum.Flush; }
            if (HasStraight()) { return CombinationEnum.Straight; }
            if (ThreeOfAKind()) { return CombinationEnum.ThreeOfAKind; }
            if (HasTwoPair()) { return CombinationEnum.TwoPairs; }
            if (HasPair()) { return CombinationEnum.Pair; }
            if (HighCard()) { return CombinationEnum.HighCard; }

            else
            {
                 throw new Exception("Hello Debug");
            }
        }
        private bool HighCard()
        {
            //TODO Create normal algoritm
            return true;
        }
        private bool HasPair()
        {
            var list = cards.GroupBy(x => x.Value);
            //return list
            return true;
        }
        private bool HasTwoPair()
        {
            var list = cards.GroupBy(x => x.Value);

            return list.Any(list => list.Count() == 2);
        }
        private bool ThreeOfAKind() 
        {
            var list = cards.GroupBy(x => x.Value);

            return list.Any(list => list.Count() == 3);
        }
        private bool FullHouse() 
        {
            if(HasTwoPair() && ThreeOfAKind())
            {  return true; }
                return false;
        }
        private bool FourOfAKind()
        {
            var groups = cards.GroupBy(card => card.Value);
            var x = groups.FirstOrDefault(group => group.Count() == 4);
            return x != null;
        }
        private bool HasFlush()
        {
            var groupedBySuit = cards.GroupBy(card => card.Suit);

          
            var flushSuit = groupedBySuit.FirstOrDefault(group => group.Count() >= 5);

            return flushSuit != null;
        }


        private bool HasStraight()
        {
            var cards = this.cards.OrderBy(card => card.Value).ToList();

            for (int i = 0; i < cards.Count - 4; i++)
            {
                bool isStraight = true;

                for (int j = i; j < i + 4; j++)
                {
                    if (cards[j + 1].Value - cards[j].Value != 1)
                    {
                        isStraight = false;
                        break;
                    }
                }

                if (isStraight)
                {
                    return true;
                }
            }

            return false;
        }
        private bool HasStraightFlush()
        {
            if(HasStraight() && HasFlush())
            {
                return true;
            }
            return false;
        }
        private bool RoyalStraight()
        {
            if (HasStraightFlush() && HasFiveCardsOfSuitAboveTen())
            {
                return true;
            }
            return false;
        }
        private bool HasFiveCardsOfSuitAboveTen()
        {
            
            var groupedBySuit = cards.GroupBy(card => card.Suit);

            
            var fiveOrMoreOfSameSuit = groupedBySuit.FirstOrDefault(group => group.Count() >= 5);

            if (fiveOrMoreOfSameSuit != null)
            {
                
                var cardsOfSameSuit = fiveOrMoreOfSameSuit.Where(card => card.Value >= RankEnum.Ten);

                return cardsOfSameSuit.Count() >= 5; // Возвращаем true, если есть пять или более карт одной масти, больших десятки
            }

            return false; // Если пять или более карт одной масти не найдено
        }
    }
}
