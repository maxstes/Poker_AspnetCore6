using Poker.Models.Cards.Enums;
using Poker.Models.Enums;

namespace Poker.Models.Game
{
    public class CombinationResultUser
    {
        public int Id { get; set; }
        public CombinationEnum Combination { get; set; }
      //  public RankEnum? RankCard { get; set; } 
        public List<int>? rankEnums { get; set; }
    }
}
