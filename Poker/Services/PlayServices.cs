using Microsoft.EntityFrameworkCore;
using Poker.Data;
using Poker.Models.Game;
using System.Numerics;
using System.Xml.Linq;

namespace Poker.Services
{
    public class PlayServices
    {
        private readonly ApplicationDbContext _context = new ();
        private readonly Deck deck = new Deck();
        List<CardsPlayer> playersList = new List<CardsPlayer>();
        List<TablesCard> TablesCards = new List<TablesCard>();
  
        public async Task DealingCardsAsync(int roomId)
        {
            deck.Shuffle();

            var players = await _context.PlayerOnline
                .Where(x => x.RoomId == roomId)
                .ToListAsync();

            foreach (var player in players)
            {
                playersList.Add(new CardsPlayer { Cards = deck.GetPlayersCard(), Player = player });
            }

            TablesCards.Add(new TablesCard {Cards = deck.GetTableCard(), RoomId = roomId });
            await Console.Out.WriteLineAsync("Роздача карт завершена");
        }
        public async Task JoinRoom(int playerId, int roomId)
        { 
            var PlayerOnline = GetPlayersOnline(playerId, roomId);

            _context.PlayerOnline.Add(PlayerOnline);
            await _context.SaveChangesAsync();
            //TODO "забацать кнопку готовые для игры"
        }
        public async Task<PlayerOnline> GetPlayerOnline(string? userName)
        {
            var userId = GetUserIdAsync(userName).Result;

            var player = await _context.PlayerOnline
                .Where(x => x.PlayerId == userId)
                .FirstOrDefaultAsync();

            return player;
        }
        public async Task LeaveRoomAsync(string? userName)
        {
            var playerOnline = GetPlayerOnline(userName).Result;

            _context.PlayerOnline.Remove(playerOnline);
            await _context.SaveChangesAsync();
        }
         public PlayerOnline GetPlayersOnline(PlayerOnline playersOnline)
        {
            var Player = GetPlayers(playersOnline.Id);
            var Room = GetRoom(playersOnline.RoomId);

            return new PlayerOnline();
        }
        public PlayerOnline GetPlayersOnline (int playerId, int roomId)
        {
            var Player = GetPlayers(playerId);
            var Room = GetRoom(roomId);
            //TODO доробити
            var PlayerOnline =  new PlayerOnline {Player = Player,Room = Room,PlayerId=Player.Id,RoomId = Room.Id };

            return  PlayerOnline;
        }
        public async Task<PlayerOnline> GetPlayersInRoom(string? userName, int roomId)
        {
            var Player = GetPlayers(GetUserIdAsync(userName).Result);
            var Room = GetRoom(roomId);
            //TODO доробити

            var PlayerOnline = await _context.PlayerOnline
                                .Where(x => x.Player == Player)
                                .FirstOrDefaultAsync();

            return PlayerOnline;
        }
        private async Task<int> GetUserIdAsync (string userName)
        {
            var Id = await _context.Player
                .Where(x => x.Name == userName)
                .Select(x => x.Id)
                .FirstOrDefaultAsync();

            return Id;
        }
        private Player GetPlayers(int id)
        {
            var Player = _context.Player
                .Where(x => x.Id == id)
                .FirstOrDefault();
            return Player;
        }
        private Room GetRoom(int roomId)
        {
            var Room = _context.Room
                .Where(x => x.Id == roomId)
                .FirstOrDefault();
            return Room;
        }
    }
}

