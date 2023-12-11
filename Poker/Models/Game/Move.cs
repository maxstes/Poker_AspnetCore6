namespace Poker.Models.Game
{
    public class Move
    {
        public string Fold { get; set; }
        public int Check { get; set; }
        public int Bet { get; set; }
        public int Call { get; set; }
        public int Rais { get; set; }
    }
}
