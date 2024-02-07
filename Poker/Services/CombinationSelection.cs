using Amazon.SecurityToken.Model;
using Poker.Models.Cards;
using Poker.Models.Cards.Enums;
using Poker.Models.Enums;
using Poker.Models.Game;
using Poker.Services.Interface;
using System.Xml.Linq;

namespace Poker.Services
{
    public class CombinationSelection//: ICombinationSelection
    {
        Combination combination = new();
        RankEnum rank;
        List<RankEnum> rankEnums = new();

        //public CombinationSelection()
        //{
        //    if(playerCards.Count != 7) 
        //    {
        //        throw new ArgumentException("Need be 2 cards from the player " +
        //            "and 5 on table");
        //    }
        //    cards = new List<Card>();
        //    cards.AddRange(playerCards);
        //    this.combination = new Combination(cards);
        //    this.UserId = userId;
        //}
        public Task Combination(List<Card> cards)
        {
            throw new NotImplementedException();
        }
        public void GetWinner(CombinationResultUser[] CRU)
        {
            var maxCombination = CRU.Max(r => r.Combination);

            // Знайти переможців з максимальною комбінацією
            var winners = CRU.Where(r => r.Combination == maxCombination);
            if (winners.Count() == 0)
            {
                throw new Exception("Method GetWinner Have 0 max Combination");
            }
            if (winners.Count() == 1)
            {
                CombinationResultUser? user = winners.SingleOrDefault();
                string text = $"Користувач: {user?.Id} Combinaton: {user?.Combination.ToString()}";
                Console.WriteLine(text);
            }
            else
            {
                Console.WriteLine("Цього разу маємо два переможці:");
                //var maxValue = winners.Max(w => w.rankEnums);
                //var absolutlWinner = winners.Where(w => w.rankEnums ==  maxValue);
                var sortedCollection = winners.OrderByDescending(item => item.rankEnums?.Max() ?? int.MinValue);

                int maxRank = winners.Max(item => item.rankEnums?.Max() ?? int.MinValue);

                var maxElements = winners.Where(item => item.rankEnums?.Max() == maxRank).ToList();
                if (maxElements.Count() == 1)
                {
                    var winner = maxElements.FirstOrDefault();

                    if (winner.rankEnums.Count() == 1)
                    {
                        Console.WriteLine($"виграв {winner.Id} Combination{winner.Combination} cards{winner.rankEnums[0].ToString()}");
                    }
                    else
                    {
                            Console.WriteLine($"переможець Id: {winner.Id} Combination: {winner.Combination} card: {winner.rankEnums[0].ToString()}");
                    }
                }
                if (maxElements.Count() == 2)
                {
                    foreach (var element in maxElements)
                    {
                        if (maxElements[0].rankEnums.Count == 2)
                            Console.WriteLine($"ну це повна нічия Id: {element.Id} Combination: {element.Combination} card: {element.rankEnums[0].ToString()} and {element.rankEnums[0]}");
                        else
                            Console.WriteLine($"ну це повна нічия Id: {element.Id} Combination: {element.Combination} card: {element.rankEnums[0].ToString()}");
                    }
                }
                else throw new Exception("Huynya");
            }
            // Возвращаем первый элемент (самый большой)

        }

        public CombinationResultUser[] GetBestCombination(CombinationUser[] combinationUsers)
        {
            CombinationResultUser[] combinationResultUsers = new CombinationResultUser[combinationUsers.Length];
            List<int> values = new();

            int x = -1;
            foreach (var user in combinationUsers)
            {
                var resultCombination = this.combination.WinCombination(user.Cards, out values);
                x++;
                CombinationResultUser resultUser = new() { Combination = resultCombination, Id = user.Id, rankEnums = values };
                combinationResultUsers[x] = resultUser;
            }

            return combinationResultUsers;
        }
        //public CombinationResultUser GetBestCombination(List<Card> playerCards, int userId)
        //{
        //    var NameCombination = combination.WinCombination(playerCards);
        //    CombinationResultUser combinationUser = new CombinationResultUser() { Id = userId, Combination = NameCombination,rankEnums = values };
        //    return combinationUser;
        //}
        public Task Selection()
        {
            throw new NotImplementedException();
        }
        private void WriteCardsPlayers(List<Card> cards)
        {
            foreach (var card in cards)
            {
                //TODO забахать "user"
                Console.WriteLine($"Player: {"User"} Card: {card.ToString()}");
            }
        }
        //private bool GetCardCollection (List<Card> cards)
        //{

        //}
        private bool GetCardCollection(List<int> list)
        {
            int card;
            if (IsOneElement(list, out card))
            {

                this.rank = (RankEnum)card;
                return false;
            }
            if (IsTwoElement(list))
            {
                foreach (var elemant in list)
                {
                    this.rankEnums.Add((RankEnum)elemant);
                }
                return true;
            }
            else
            {
                throw new Exception($"List values card have {list.Count} elemennta");
            }
        }
        private bool IsOneElement(List<int> list, out int cardValue)
        {
            cardValue = new();
            if (list.Count == 1)
            {
                cardValue = list[0];
                return true;
            }
            return false;
        }
        private bool IsTwoElement(List<int> list)
        {
            if (list.Count == 2)
            {
                return true;
            }
            return false;
        }

    }
}
