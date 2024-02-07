using Poker.Models.Game;

namespace Poker.Services.Interface
{
    public interface IMovePlayer
    {
        public  Task Fold(PlayerOnline player,string move);
        public  Task Check(PlayerOnline player, string move);
        public  Task Bet(PlayerOnline player, int bid,string move);
        public  Task Call(PlayerOnline player, int bid, string move);
        public  Task Rais(PlayerOnline player, int bid, string move);
    }
}
