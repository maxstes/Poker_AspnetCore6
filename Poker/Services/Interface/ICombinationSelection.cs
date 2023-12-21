using Poker.Models.Cards;

namespace Poker.Services.Interface
{
    public interface ICombinationSelection
    {
        public Task Selection();
        public Task GetWinnerCombination();
        public Task Combination(List<Card> cards);
    }
}
