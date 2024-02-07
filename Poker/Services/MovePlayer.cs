using Microsoft.EntityFrameworkCore;
using Poker.Data;
using Poker.Models.Game;
using Poker.Services.Interface;
using Poker.SignalR;

namespace Poker.Services
{
    public class MovePlayer : IMovePlayer
    {
        readonly ApplicationDbContext context = new();
        readonly ChatHub Chat = new ();
        private async Task SendChatMessageAsync(PlayerOnline player, int bid, string move)
        {
            await Chat.SendMessageAsync($"{player.Player?.Name} chose the move {move} bid: {bid} ", "Admin", player.RoomId.ToString());
        }
        private async Task SendChatMessageAsync(PlayerOnline player, string move)
        {
            await Chat.SendMessageAsync($"{player.Player?.Name} chose the move {move}", "Admin", player.RoomId.ToString());
        }
        private bool IsCheckOrFold(PlayerOnline player, string move)
        {
            if (move == "Check")
            {
                return true;
            }
            else
                return false;
        }
        private bool IsEnoughMoney(PlayerOnline player, int bid)
        {
            if (player.Player?.Balance >= bid)
            {
                return true;
            }
            return false;
        }
        private async Task<bool> BetToBank(PlayerOnline? player, int bid)
        {
            if (IsEnoughMoney(player!, bid))
            {
                //TODO Ставка поввина підти в банк
                player!.Player!.Balance = player.Player?.Balance - bid;
                await context.PlayerOnline
                    .Update(player)
                    .Context.SaveChangesAsync();
                return true;
            }
            else
                return false;
        }
        public async Task DefaultMove(PlayerOnline player, int bid, string move)
        {
            if (BetToBank(player, bid).IsCompleted)
            {
                await SendChatMessageAsync(player, bid, move);
            }
            else
            {
                throw new Exception("BetToBank not Copmleted,maybe you don't have enough");
            }
            //TODO придумати реалізацію щоб воно підсказувало користовачу на насі ходу ,що в нього не вистачає
        }


        public async Task DefaultMove(PlayerOnline player, string Move)
        {

            await SendChatMessageAsync(player, Move);
        }


        public async Task Fold(PlayerOnline? player, string move)
        {
            await SendChatMessageAsync(player!, move);
            context.PlayerOnline.Remove(player!);
        }
        public async Task Check(PlayerOnline player, string move)
        {
            await SendChatMessageAsync(player, move);
        }
        public async Task Bet(PlayerOnline player, int bid, string move)
        {
            await SendChatMessageAsync(player, bid, move);
        }
        public async Task Call(PlayerOnline player, int bid, string move)
        {
            await Chat.SendMessageAsync($"{player.Player?.Name} chose the move {move}", "Admin", player.RoomId.ToString());
        }
        public async Task Rais(PlayerOnline player, int bid, string move)
        {
            await Chat.SendMessageAsync($"{player.Player?.Name} chose the move Rails", "Admin", player.RoomId.ToString());
        }
    }
}
