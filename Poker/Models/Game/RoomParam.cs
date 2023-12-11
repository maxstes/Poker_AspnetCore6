namespace Poker.Models.Game
{
    public class RoomParam
    {
        public string Name { get; set; }
        public int MinBet { get; set; }
        public int MaxBet { get; set; }
        public int PlayersCount { get; set; }
        public int FreeMisc { get; set; }
    }
}
