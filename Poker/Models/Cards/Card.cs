using Poker.Models.Cards.Enums;

namespace Poker.Models.Cards;
[Serializable]
public class Card
{
    public Card()
    {

    }
    public SuitEnum Suit { get; set; }
    public RankEnum Value { get; set; }
    public Card(SuitEnum suit, RankEnum value)
    {
        Suit = suit;
        Value = value;
    }
    public override string ToString()
    {
        return $"{Value} of {Suit}";
    }
}
