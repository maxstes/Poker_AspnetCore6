using Poker.Models.Game;
using Poker.Services.Interface;

namespace Poker.Services
{
    public class PokerGame //: IPokerGame
    {
        private MovePlayer movePlayer = new MovePlayer();
        public async Task Action(PlayerOnline player,string move,int bid)
        {

            switch (move)
            {
                case "Fold":
                    //player leave
                    await movePlayer.Fold(player);
                    break;
                case "Bet":
                    await movePlayer.Bet(player, bid);
                    break;
                case "Check":
                    //Player.ContinueGame
                    await movePlayer.Check(player);
                    break;
                case "Rais":
                    //if(player.money == bid || player.money >= bid)
                    await movePlayer.Rais(player,bid);
                    break;
                case "Call":
                    //if(player.money == bid || player.money >= bid)
                    await movePlayer.Call(player, bid);
                    break;
                default:
                    // Действие по умолчанию, если move не соответствует ни одному из вариантов
                    throw new Exception("Move == null");
            }
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
