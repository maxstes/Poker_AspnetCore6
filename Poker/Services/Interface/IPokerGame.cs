using Poker.Models.Game;

namespace Poker.Services.Interface
{
    public interface IPokerGame
    {
        public Task Action(PlayerOnline players,string move,int bid);
        public void Blind();
        public PlayerOnline GetWinner();
        
    }
}
