using Poker.Models.Game;
using Poker.Services.Interface;

namespace Poker.Services
{
    public class PokerGame //: IPokerGame
    {
        private MovePlayer movePlayer = new MovePlayer();
        public async Task Action(PlayerOnline player, int bid, string move)
        {

            await movePlayer.DefaultMove(player, bid, move);

        }


        public Task Blind()
        {
            throw new NotImplementedException();
        }

        public PlayerOnline GetWinner()
        {
            throw new NotImplementedException();
        }
    }
}

