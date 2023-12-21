using Poker.Models.Cards;

namespace Poker.Services.Interface
{
    public interface IDeck
    {
        List<Card> GenerateDeck();
        public void Shuffle();
    }
}
