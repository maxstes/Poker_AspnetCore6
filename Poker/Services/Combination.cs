using Poker.Models.Cards;
using Poker.Models.Cards.Enums;
using Poker.Models.Enums;
using SharpCompress.Common;
using System.Collections.Generic;
using static MongoDB.Driver.WriteConcern;

namespace Poker.Services
{
    public class Combination
    {
        public CombinationEnum WinCombination(List<Card> cards, out List<int> values)
        {
            values = new();
            //TODO UP Case
            if (RoyalStraight(cards)) { return CombinationEnum.RoyalFlush; }
            if (HasStraightFlush(cards, out values)) { return CombinationEnum.StraightFlush; }
            if (FourOfAKind(cards, out values)) { return CombinationEnum.FourOfAKind; }
            if (FullHouse(cards, out values)) { return CombinationEnum.FullHouse; }
            if (HasFlush(cards,out values)) { return CombinationEnum.Flush; }
            if (HasStraight(cards, out values)) { return CombinationEnum.Straight; }
            if (ThreeOfAKind(cards, out values)) { return CombinationEnum.ThreeOfAKind; }
            if (HasTwoPair(cards, out values)) { return CombinationEnum.TwoPairs; }
            if (HasPair(cards, out values)) { return CombinationEnum.Pair; }
            if (HighCard(cards,out values)) { return CombinationEnum.HighCard; }

            else
            {
                throw new Exception("Hello Debug");
            }
        }
        //private int GetValueCombination(IEnumerable<IGrouping<RankEnum, Card>> pairs)
        //{
        //    if (pairs.Any())
        //    {

        //        // Є пара, отже повертаємо true та значення пари
        //        var dpairValue = (pairs.First().Key);
        //        int value = (int)dpairValue;
        //        return value;
        //    }
        //}
        private bool HighCard(List<Card> cards,out List<int> value)
        {
            value = new();
            int maxCard = (int)cards.Max(x => x.Value);
            value.Add(maxCard);
            return true;
        }
        private bool HasPair(List<Card> cards, out List<int> value)
        {// ____________________-
         // original realization   var list = cards.GroupBy(x => x.Value);
         // ___________________________
            value = new();

            var groupedByRank = cards.GroupBy(card => card.Value);
            var pairs = groupedByRank.Where(group => group.Count() == 2);

            if (pairs.Any())
            {

                // Є пара, отже повертаємо true та значення пари
                var dpairValue = (pairs.First().Key);
                value.Add((int)dpairValue);
                return true;
            }
            return false;
        }
        private bool HasTwoPair(List<Card> cards, out List<int> values)
        {
            values = new List<int>();

            var groupedByRank = cards.GroupBy(card => card.Value);
            var pairs = groupedByRank.Where(group => group.Count() == 2);

            if (pairs.Count() >= 2)
            {
                foreach (var pair in pairs)
                {
                    var pairValue = (int)pair.Key;
                    values.Add(pairValue);
                }

                return true;
            }

            return false;
        }

        private bool ThreeOfAKind(List<Card> cards, out List<int> value)
        {
            value = new();

            var groupedByRank = cards.GroupBy(card => card.Value);
            var triplet = groupedByRank.FirstOrDefault(group => group.Count() == 3);

            if (triplet != null)
            {
                value.Add((int)triplet.Key);
                return true;
            }

            return false;
        }
        private bool FullHouse(List<Card> cards, out List<int> values)
        {
            values = new();
            if (HasTwoPair(cards, out values) && ThreeOfAKind(cards, out values))
            {
                ChooseBigCard(values);
                return true;
            }
            return false;
        }
        private bool FourOfAKind(List<Card> cards, out List<int> value)
        {
            value = new();
            var groups = cards.GroupBy(card => card.Value);
            var fourKind = groups.FirstOrDefault(group => group.Count() == 4);

            if (fourKind != null)
            {
                value.Add((int)fourKind!.FirstOrDefault()!.Value);
                return true;
            }
            // debug
            return false;
        }
        private bool HasFlush(List<Card> cards, out List<int> values)
        {
            values = new();
            var groupedBySuit = cards.GroupBy(card => card.Suit);

            var flushSuit = groupedBySuit.FirstOrDefault(group => group.Count() >= 5);
            if (flushSuit == null) { return false; }
            var max = flushSuit.Max(x=> x.Value);
            int value = (int)flushSuit.Where(x => x.Value == max).FirstOrDefault().Value;
            values.Add(value);
            return flushSuit != null;
        }


        private bool HasStraight(List<Card> cards, out List<int> value)
        {
            value = new();
            var cardsOrderBy = cards.OrderBy(card => card.Value).ToList();

            for (int i = 0; i < cardsOrderBy.Count - 4; i++)
            {
                bool isStraight = true;

                for (int j = i; j < i + 4; j++)
                {
                    if (cardsOrderBy[j + 1].Value - cardsOrderBy[j].Value != 1)
                    {
                        isStraight = false;
                        break;
                    }
                }

                if (isStraight)
                {
                    value.Add((int)cardsOrderBy[i + 4].Value);

                    return true;
                }
            }

            return false;
        }
        private bool HasStraightFlush(List<Card> cards, out List<int> values)
        {
            values = new();
            if (HasStraight(cards, out values) && HasFlush(cards, out values))
            {
                ChooseBigCard(values);
                return true;
            }
            return false;
        }
        private bool RoyalStraight(List<Card> cards)
        {
           
            List<int> value = new();
            if (HasStraightFlush(cards, out value) && HasFiveCardsOfSuitAboveTen(cards))
            {
                return true;
            }
            return false;
        }
        private bool HasFiveCardsOfSuitAboveTen(List<Card> cards)
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
        private void ChooseBigCard(List<int> values)
        {
            int x = values.Max();
            values.Clear();
            values.Add(x);
        }
    }
}
