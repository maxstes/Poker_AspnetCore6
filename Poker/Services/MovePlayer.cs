using Microsoft.EntityFrameworkCore;
using Poker.Data;
using Poker.Models.Game;
using Poker.Services.Interface;
using Poker.SignalR;

namespace Poker.Services
{
    public class MovePlayer : IMovePlayer
    {
        ApplicationDbContext context = new ();
        ChatHub Chat = new ChatHub ();
        public async Task Fold(PlayerOnline player)
        {
            await Chat.SendMessage($"{player.Player.Name} chose the move Fold","Admin",player.RoomId.ToString());
            context.PlayerOnline.Remove(player);
        }
        public async Task Check(PlayerOnline player)
        {
            await Chat.SendMessage($"{player.Player.Name} chose the move Check", "Admin", player.RoomId.ToString());
        }
        public async Task Bet(PlayerOnline player, int bid)
        {
            await Chat.SendMessage($"{player.Player.Name} chose the move Bet", "Admin", player.RoomId.ToString());
        }
        public async Task Call(PlayerOnline player, int bid)
        {
            await Chat.SendMessage($"{player.Player.Name} chose the move Call", "Admin", player.RoomId.ToString());
        }
        public async Task Rais(PlayerOnline player,int bid)
        {
            await Chat.SendMessage($"{player.Player.Name} chose the move Rails", "Admin", player.RoomId.ToString());
        }
    }
}
