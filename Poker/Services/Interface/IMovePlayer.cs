using Poker.Models.Game;

namespace Poker.Services.Interface
{
    public interface IMovePlayer
    {
        public  Task Fold(PlayerOnline player);
        public  Task Check(PlayerOnline player);
        public  Task Bet(PlayerOnline player, int bid);
        public  Task Call(PlayerOnline player, int bid);
        public  Task Rais(PlayerOnline player, int bid);
    }
}
