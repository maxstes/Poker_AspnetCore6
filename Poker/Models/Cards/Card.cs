using Poker.Models.Cards.Enums;

namespace Poker.Models.Cards;

public class Card
{
    public SuitEnum Suit{ get;}
    public RankEnum Value { get;}
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
