using Poker.Data;
using Poker.Models.Game;

namespace Poker.Services
{
    public class BalanceCheck
    {
        public bool Check(PlayerOnline player,int bid)
        {
            int? balance = player?.Player?.Balance;
            if(balance == bid||balance >= bid )
            {
                return true;
            }
            return false;
        }
    }
}
